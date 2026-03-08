using UnityEngine;

namespace interactable_furniture
{
    public class BackpackScript : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void OnClick()
        {
            panel.SetActive(true);
        }
    }
}
