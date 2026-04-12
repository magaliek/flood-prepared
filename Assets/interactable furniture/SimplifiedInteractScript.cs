using UnityEngine;

namespace interactable_furniture
{
    public class SimplifiedInteractScript : MonoBehaviour
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
            if (_playerNearby && Input.GetKeyDown(KeyCode.Return))
                panel.SetActive(true);
        }
    }
}