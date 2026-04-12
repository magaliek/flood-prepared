using UnityEngine;

namespace TemporaryScripts
{
    public class HideScript : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            this.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

