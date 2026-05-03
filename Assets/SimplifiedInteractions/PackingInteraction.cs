using UnityEngine;
using score_system;
using UnityEngine.SocialPlatforms.Impl;

namespace SimplifiedInteractions
{
    public class PackingInteraction : MonoBehaviour
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
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && !ScoreScript.Instance.packingDone && !ScoreScript.Instance.phase2)
                panel.SetActive(true);
            
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return) && ScoreScript.Instance.phase2)
                this.gameObject.SetActive(false);
                ScoreScript.Instance.tookBackpack = true;
        }
    }
}