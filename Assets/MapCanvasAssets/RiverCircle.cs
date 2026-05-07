using UnityEngine;
using score_system;
using UnityEngine.UI;

namespace MapCanvasAssets
{
    public class RiverCircle : MonoBehaviour
    {
        private MapChoice choice = MapChoice.NearRiver;
        
        [SerializeField] public Image imageCircle;
        [SerializeField] public bool ticked = false;

        [SerializeField] private Sprite  NotTickedCircle;
        [SerializeField] private Sprite TickedCircle;

        [SerializeField] private BasementCircle _basementCircle;
        [SerializeField] private ShelterCircle _shelterCircle;
        [SerializeField] private HillCircle _hillCircle;
   
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
                _basementCircle.ticked = false;
                _basementCircle.imageCircle.sprite = NotTickedCircle;
                _shelterCircle.ticked = false;
                _shelterCircle.imageCircle.sprite = NotTickedCircle;
                _hillCircle.ticked = false;
                _hillCircle.imageCircle.sprite = NotTickedCircle;
                
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