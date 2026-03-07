using UnityEngine;

namespace packing_scripts
{
    public class CursorScript : MonoBehaviour
    {
        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D clickCursor;
        [SerializeField] private Vector2 defaultHotspot;
        [SerializeField] private Vector2 clickHotspot;
        
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
            Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
        }

        public void SetClick()
        {
            Cursor.SetCursor(clickCursor, clickHotspot, CursorMode.Auto);
        }
        
    }
}