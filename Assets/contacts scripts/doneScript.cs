using packing_scripts;
using score_system;
using UnityEngine;
using UnityEngine.UI;

namespace contacts_scripts
{
    public class DoneScript : MonoBehaviour
    {
        [SerializeField] private NoteborderScript[] validZones;
        [SerializeField] private GameObject panel;
        [SerializeField] private Notebook pNoteBookScript;

        private bool _anyEmpty;
        
        private bool IsRed()
        {
            bool anyRed = false;
            _anyEmpty = false;
            foreach (var zone in validZones)
            {
                Outline outline = zone.GetComponent<Outline>();
                if (outline.effectColor == Color.red) anyRed = true;
                if (zone.isEmpty) _anyEmpty = true;
            }
            return anyRed;
        }

        public void OnDoneClicked()
        {
            if (IsRed() || _anyEmpty)
            {
                GetComponent<AudioSource>().Play();
                _anyEmpty = false;
                panel.SetActive(false);
            } else {
            ScoreScript.Instance.notebookDone = true;
            _anyEmpty = false;
            pNoteBookScript.ChangeImage();
            panel.SetActive(false);
            }
        }
    }
}