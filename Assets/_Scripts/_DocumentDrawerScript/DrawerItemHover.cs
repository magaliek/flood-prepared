using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawerItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;

    private Vector3 originalScale;
    private Color originalIconColor;
    private Color originalBackgroundColor;

    private void Awake()
    {
        originalScale = transform.localScale;

        if (iconImage == null)
        {
            var icon = transform.Find("Icon");
            if (icon != null)
                iconImage = icon.GetComponent<Image>();
        }

        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();

        if (iconImage != null)
            originalIconColor = iconImage.color;

        if (backgroundImage != null)
            originalBackgroundColor = backgroundImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.08f;

        if (iconImage != null)
            iconImage.color = new Color(1f, 1f, 0.85f, 1f);

        if (backgroundImage != null)
            backgroundImage.color = new Color(1f, 1f, 1f, 0.85f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;

        if (iconImage != null)
            iconImage.color = originalIconColor;

        if (backgroundImage != null)
            backgroundImage.color = originalBackgroundColor;
    }
}