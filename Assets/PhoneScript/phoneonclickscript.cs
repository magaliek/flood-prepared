using UnityEngine;

public class phoneonclickscript : MonoBehaviour
{
   [SerializeField] private GameObject panel;

        public void OnClick()
        {
            panel.SetActive(true);
        }
}
