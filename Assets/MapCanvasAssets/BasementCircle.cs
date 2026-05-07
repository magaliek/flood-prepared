using UnityEngine;
using score_system;
using UnityEngine.UI;

namespace MapCanvasAssets
{
    public class BasementCircle : MonoBehaviour
    {
        private MapChoice choice = MapChoice.Basement;
        
        [SerializeField] public Image imageCircle;
        [SerializeField] public bool ticked = false;

        [SerializeField] private Sprite  NotTickedCircle;
        [SerializeField] private Sprite TickedCircle;

        [SerializeField] private RiverCircle _riverCircle;
        [SerializeField] private ShelterCircle _shelterCircle;
        [SerializeField] private HillCircle _hillCircle;
   
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