using UnityEngine;
using UnityEngine.EventSystems;

namespace packing_scripts
{
    public abstract class PackingItem : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float calories;
        public float Calories => calories;
        [SerializeField] private float carbs;
        public float Carbs => carbs;
        [SerializeField] private float protein;
        public float Protein => protein;
        [SerializeField] private float fat;
        public float Fat => fat;
        [SerializeField] private float sodium;
        public float Sodium => sodium;
        [SerializeField] private float water;
        public float Water => water;
        [SerializeField] private float weight;
        public float Weight => weight;
        

        private RectTransform _rect;
        private CanvasGroup _cg;
        private Canvas _rootCanvas;
        private Transform _originalParent;
        private Vector2 _originalPosition;
        private Vector2 _originalAnchorMin;
        private Vector2 _originalAnchorMax;
        private Vector2 _originalPivot;
        
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _cg = GetComponent<CanvasGroup>();
            _rootCanvas = FindRootCanvas();
        }
        
        private void Start()
        {
            _originalParent = transform.parent;
            _originalPosition = _rect.anchoredPosition;
            _originalAnchorMin = _rect.anchorMin;
            _originalAnchorMax = _rect.anchorMax;
            _originalPivot = _rect.pivot;
        }

        private void OnEnable()
        {
            _rect = GetComponent<RectTransform>();
            _cg = GetComponent<CanvasGroup>();
            _rootCanvas = FindRootCanvas();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CursorScript.Instance?.SetClick();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CursorScript.Instance?.SetDefault();
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
            
            RectTransform canvasRect = _rootCanvas.GetComponent<RectTransform>();
            float halfW = canvasRect.rect.width  / 2f;
            float halfH = canvasRect.rect.height / 2f;
            float itemHalfW = _rect.rect.width  / 2f;
            float itemHalfH = _rect.rect.height / 2f;

            _rect.anchoredPosition = new Vector2(
                Mathf.Clamp(_rect.anchoredPosition.x, -halfW + itemHalfW, halfW - itemHalfW),
                Mathf.Clamp(_rect.anchoredPosition.y, -halfH + itemHalfH, halfH - itemHalfH)
            );

            
            CursorScript.Instance?.SetClick();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _cg.blocksRaycasts = true;

            if (transform.parent == _rootCanvas.transform)
            {
                transform.SetParent(_originalParent, true);
            }
        }

        private Canvas FindRootCanvas()
        {
            Canvas c = GetComponentInParent<Canvas>();
            return c != null ? c.rootCanvas : null;
        }

        public void ResetItem()
        {
            transform.SetParent(_originalParent, false);
            _rect.anchorMin = _originalAnchorMin;
            _rect.anchorMax = _originalAnchorMax;
            _rect.pivot = _originalPivot;
            _rect.anchoredPosition = _originalPosition;
            _cg.alpha = 1f;
            _cg.blocksRaycasts = true;
            _cg.interactable = true;
        }
    }
}