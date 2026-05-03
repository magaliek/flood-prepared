using UnityEngine;
using score_system;

namespace SimplifiedInteractions
{
    public class ContactInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        private bool _playerNearby = false;

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                _playerNearby = true;
        }

        void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                _playerNearby = false;
        }

        void Update()
        {
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && !ScoreScript.Instance.notebookDone && !ScoreScript.Instance.phase2)
                panel.SetActive(true);
        }
    }
}