using UnityEngine;

namespace MenuScripts
{
    public class menuScript : MonoBehaviour
    {


        [SerializeField] private GameObject userNamePanel;
        [SerializeField] private GameObject gameInstrcutionPanel;
        [SerializeField] private GameObject bPreparedPanel;
        [SerializeField] private GameObject makeAccountPanel;

        public void Start()
        {
            userNamePanel.SetActive(false);
            gameInstrcutionPanel.SetActive(false);
            bPreparedPanel.SetActive(false);
            makeAccountPanel.SetActive(false);
        }

        public void showUsername()
        {
            userNamePanel.SetActive(true);
            userNamePanel.transform.SetAsLastSibling();
        }
        public void showGameInstrcution()
        {
            gameInstrcutionPanel.SetActive(true);
            gameInstrcutionPanel.transform.SetAsLastSibling();
        }
        public void showbPreparedPanel()
        {
            bPreparedPanel.SetActive(true);
            bPreparedPanel.transform.SetAsLastSibling();
        }
        public void showMakeAccountPanel()
        {
            makeAccountPanel.SetActive(true);
            makeAccountPanel.transform.SetAsLastSibling();
        }
    }
}
