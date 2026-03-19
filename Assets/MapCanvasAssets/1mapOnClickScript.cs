using UnityEngine;


public class mapOnClickScript : MonoBehaviour
  {
        [SerializeField] private GameObject panel;

        public void OnClick()
        {
            panel.SetActive(true);
        }
    }
