using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SealCell : MonoBehaviour, IPointerEnterHandler
{
    private Image img;
    private bool filled;
    private SealMinigame manager;

    private void Awake()
    {
        img = GetComponent<Image>();
        manager = GetComponentInParent<SealMinigame>();
        ResetCell();
    }

    public void ResetCell()
    {
        filled = false;

        if (img != null)
            img.color = new Color(0.878f, 0.925f, 0.922f, 0.08f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (filled) return;
        if (manager == null || !manager.IsDrawing || !manager.PlasticSelected) return;

        filled = true;

        if (img != null)
            img.color = new Color(0.878f, 0.925f, 0.922f, 0.45f);

        manager.NotifyCellFilled();
    }
}