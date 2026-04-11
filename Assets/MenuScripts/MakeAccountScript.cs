using UnityEngine;

namespace MenuScripts
{
    public class MakeAccountScript : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void OnNoClick()
        {
            this.gameObject.SetActive(false);
        }

        public void OnYesClick()
        {
            this.gameObject.SetActive(false);
            //TODO: make account
        }
    }
}
