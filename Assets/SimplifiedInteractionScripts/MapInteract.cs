using UnityEngine;
using score_system;

namespace SimplifiedInteractionScripts
{
    public class MapInteract : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        private bool _playerNearby = false;

        void OnCollisionEnter2D(Collision2D col)
        {
            if (ScoreScript.Instance.phase2) return;
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
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && !ScoreScript.Instance.mapDone)
                panel.SetActive(true);
        }
    }
}