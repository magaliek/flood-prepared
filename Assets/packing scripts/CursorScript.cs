using UnityEngine;

namespace packing_scripts
{
    public class CursorScript : MonoBehaviour
    {
        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D clickCursor;
        [SerializeField] private Vector2 defaultHotspot;
        [SerializeField] private Vector2 clickHotspot;
        
        private bool _isDrag;

        public static CursorScript Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            SetDefault();
        }

        public void SetDefault()
        {
            if (!_isDrag)
            {
                Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
                return;
            }

            _isDrag = false;
            Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
        }

        public void SetClick()
        {
            if (_isDrag) return;

            _isDrag = true;
            Cursor.SetCursor(clickCursor, clickHotspot, CursorMode.Auto);
        }
        
    }
}