using UnityEngine;

namespace packing_scripts
{
    public class DoneScriptP : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void OnDoneClicked()
        {
            panel.SetActive(false);
        }
    }
}