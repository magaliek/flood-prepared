using UnityEngine;
using score_system;

namespace SimplifiedInteractionScripts
{
    public class PackingInteract : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        private bool _playerNearby = false;
        private bool _phase2 = false;

        void OnCollisionEnter2D(Collision2D col)
        {
            if (ScoreScript.Instance.phase2)
            {
                if (col.gameObject.CompareTag("Player"))
                    _playerNearby = true;
                _phase2 = true;
            }
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
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && _phase2)
            {
                this.gameObject.SetActive(false);
                ScoreScript.Instance.tookBackpack = true;
                return;
            }
                
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && !ScoreScript.Instance.packingDone)
                panel.SetActive(true);
        }
    }
}