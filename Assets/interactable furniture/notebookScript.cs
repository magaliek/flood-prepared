using UnityEngine;

namespace interactable_furniture
{
    public class NotebookScript : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void OnClick()
        {
            panel.SetActive(true);
        }
    }
}