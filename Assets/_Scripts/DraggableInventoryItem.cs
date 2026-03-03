using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableInventoryItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public DocumentData Document { get; private set; }

    [Header("Optional refs (auto-find if empty)")]
    [SerializeField] private Image iconImage;          // child: Icon (Image)
    [SerializeField] private TMP_Text labelText;       // child: Label (TMP)

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Auto-find children if not assigned
        if (iconImage == null)
        {
            var t = transform.Find("Icon");
            if (t != null) iconImage = t.GetComponent<Image>();
        }

        if (labelText == null)
        {
            var t = transform.Find("Label");
            if (t != null) labelText = t.GetComponent<TMP_Text>();
        }
    }

    public void Init(DocumentData doc, Canvas parentCanvas)
    {
        Document = doc;
        canvas = parentCanvas;

        // Set icon
        if (iconImage != null)
        {
            iconImage.sprite = (doc != null) ? doc.icon : null;
            iconImage.enabled = iconImage.sprite != null;
            iconImage.preserveAspect = true;
        }

        // Set label
        if (labelText != null)
        {
            labelText.text = (doc != null) ? doc.displayName : "";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // If not dropped into a new parent, snap back
        if (transform.parent == canvas.transform)
            transform.SetParent(originalParent, true);
    }
}