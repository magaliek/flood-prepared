using UnityEngine;

namespace MenuScripts
{
    public class DoneScriptMenu : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void OnClickHidePanel()
        {
            panel.SetActive(false);
        }
    }
}