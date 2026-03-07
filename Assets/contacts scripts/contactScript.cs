using System.Linq;
using packing_scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace contacts_scripts
{
    public class ContactScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rect;
        private CanvasGroup _cg;
        private Canvas _rootCanvas;
        private Transform _originalParent;
        private Transform _originParent;
        private Vector2 _originPosition;

        [SerializeField] private string[] validZoneIds;
        [SerializeField] PencilAnim pencilAnimator;

        private NoteborderScript _currentZone;
        public TMP_Text text;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _cg = GetComponent<CanvasGroup>();
            _rootCanvas = GetComponentInParent<Canvas>();
            
            _originParent = transform.parent;
            _originPosition = _rect.anchoredPosition;
            text = GetComponent<TMP_Text>();
        }
        
        public NoteborderScript GetCurrentZone() => _currentZone;

        public void SnapToOrigin()
        {
            transform.SetParent(_originParent, false);
            _rect.anchoredPosition = _originPosition;
            SetCurrentZone(null);
        }
        
        public bool IsValidZone(string zoneId)
        {
            return validZoneIds.Contains(zoneId);
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
            if (_currentZone != null)
            {
                _currentZone.isEmpty = true;
                _currentZone.GetComponent<Outline>().enabled = false;
            }
            
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
        
        public void SetCurrentZone(NoteborderScript zone)
        {
            _currentZone = zone;
        }
    }
}
