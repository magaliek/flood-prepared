using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class MakeAccountScript : MonoBehaviour
    {
        [SerializeField] private GameObject makeAccPanel;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            makeAccPanel.SetActive(false);
        }

        public void OnNoClick()
        {
            makeAccPanel.SetActive(false);
        }

        public void OnYesClick()
        {
            SceneManager.LoadScene("Hallway");
            makeAccPanel.SetActive(false);
            //TODO: make account
        }
    }
}
