using UnityEngine;

namespace MenuScripts
{
    public class menuScript : MonoBehaviour
    {


        [SerializeField] private GameObject userNamePanel;
        [SerializeField] private GameObject gameInstrcutionPanel;
        [SerializeField] private GameObject bPreparedPanel;


        public void showUsername()
        {
            userNamePanel.SetActive(true);
        }
        public void showGameInstrcution()
        {
            gameInstrcutionPanel.SetActive(true);
        }
        public void showbPreparedPanel()
        {
            bPreparedPanel.SetActive(true);
        }
    
    }
}
