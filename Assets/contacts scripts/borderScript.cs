using UnityEngine;
using UnityEngine.EventSystems;

namespace contacts_scripts
{
    public class BorderScript : MonoBehaviour, IDropHandler
    {
        private RectTransform _contentParent;
        
        public void Awake()
        {
            _contentParent = transform as RectTransform;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedObj = eventData.pointerDrag;
            if (draggedObj == null) return;
            
            draggedObj.transform.SetParent(_contentParent, false);

            RectTransform draggedRect = draggedObj.GetComponent<RectTransform>();
            draggedRect.anchorMin = new Vector2(0.5f, 0.5f);
            draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
            draggedRect.pivot     = new Vector2(0.5f, 0.5f);
            draggedRect.anchoredPosition = Vector2.zero;
            draggedRect.localRotation = Quaternion.identity;
            draggedRect.localScale = Vector3.one;
        }
    }
}
