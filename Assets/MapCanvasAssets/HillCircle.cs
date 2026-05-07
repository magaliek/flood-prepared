using UnityEngine;
using score_system;
using UnityEngine.UI;

namespace MapCanvasAssets
{
    public class HillCircle : MonoBehaviour
    {
        private MapChoice choice = MapChoice.Hill;
        
        [SerializeField] public Image imageCircle;
        [SerializeField] public bool ticked = false;

        [SerializeField] private Sprite  NotTickedCircle;
        [SerializeField] private Sprite TickedCircle;

        [SerializeField] private RiverCircle _riverCircle;
        [SerializeField] private ShelterCircle _shelterCircle;
        [SerializeField] private BasementCircle _basementCircle;
   
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            imageCircle = GetComponent<Image>();
            imageCircle.sprite = NotTickedCircle;
        }

        public void tickingCircle()
        {
            ticked = !ticked;
            if (ticked)
            {
                _riverCircle.ticked = false;
                _riverCircle.imageCircle.sprite = NotTickedCircle;
                _shelterCircle.ticked = false;
                _shelterCircle.imageCircle.sprite = NotTickedCircle;
                _basementCircle.ticked = false;
                _basementCircle.imageCircle.sprite = NotTickedCircle;
                
                imageCircle.sprite = TickedCircle;
                ScoreScript.Instance.chosenShelter = choice;
            }
            else
            {
                imageCircle.sprite = NotTickedCircle;
            }
        }
    }
}