using UnityEngine;
using UnityEngine.UI;

namespace contacts_scripts
{
    public class DoneScript : MonoBehaviour
    {
        [SerializeField] private NoteborderScript[] validZones;
        [SerializeField] private GameObject panel;

        private bool _anyEmpty;
        
        private bool IsRed()
        {
            bool anyRed = false;
            _anyEmpty = false;
            foreach (var zone in validZones)
            {
                Outline outline = zone.GetComponent<Outline>();
                if (outline.enabled) anyRed = true;
                if (zone.isEmpty) _anyEmpty = true;
            }
            return anyRed;
        }

        public void OnDoneClicked()
        {
            if (IsRed() || _anyEmpty)
            {
                GetComponent<AudioSource>().Play();
                return;
            };
            _anyEmpty = false;
            panel.SetActive(false);
        }
    }
}