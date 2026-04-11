using UnityEngine;

namespace MenuScripts
{
    public class usernameScript : MonoBehaviour
    {
        [SerializeField] private GameObject makeAccountPanel;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void OnPlayClick()
        {
            //todo: check database for username, ask to make account, etc...
            makeAccountPanel.SetActive(true);
            makeAccountPanel.transform.SetAsLastSibling();
        }
        
    }
}
