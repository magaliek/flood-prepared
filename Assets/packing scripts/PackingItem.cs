using UnityEngine;
using UnityEngine.EventSystems;

namespace packing_scripts
{
    public abstract class PackingItem : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private int calories;
        public int Calories => calories;

        private RectTransform _rect;
        private CanvasGroup _cg;
        private Canvas _rootCanvas;
        private Transform _originalParent;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _cg = GetComponent<CanvasGroup>();
            _rootCanvas = GetComponentInParent<Canvas>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CursorScript.Instance.SetClick();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CursorScript.Instance.SetDefault();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;

            transform.SetParent(_rootCanvas.transform, true);

            _cg.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rect.anchoredPosition += eventData.delta / _rootCanvas.scaleFactor;
            CursorScript.Instance.SetClick();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _cg.blocksRaycasts = true;

            if (transform.parent == _rootCanvas.transform)
            {
                transform.SetParent(_originalParent, true);
            }
        }
    }
}