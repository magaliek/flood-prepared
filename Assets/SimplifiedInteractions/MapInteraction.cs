using UnityEngine;
using score_system;

namespace SimplifiedInteractions
{
    public class MapInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject popupText; // NY

        private bool _playerNearby = false;

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _playerNearby = true;

                if (popupText != null) // NY
                    popupText.SetActive(true);
            }
        }

        void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _playerNearby = false;

                if (popupText != null) 
                    popupText.SetActive(false);
            }
        }

        void Update()
        {
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && !ScoreScript.Instance.mapDone && !ScoreScript.Instance.phase2)
                panel.SetActive(true);
        }
    }
}