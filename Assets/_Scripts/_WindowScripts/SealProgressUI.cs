using UnityEngine;
using UnityEngine.UI;

public class SealProgressUI : MonoBehaviour
{
    [SerializeField] private RectTransform fillRect;   // SealBarFill sin RectTransform
    [SerializeField] private CanvasGroup group;        // valgfritt (for show/hide)
    [SerializeField] private float smooth = 12f;

    private float target01 = 0f;
    private float current01 = 0f;

    private void Awake()
    {
        if (!fillRect) fillRect = GetComponentInChildren<RectTransform>();
        if (!group) group = GetComponent<CanvasGroup>();
        HideImmediate();
    }

    public void SetProgress01(float value01)
    {
        target01 = Mathf.Clamp01(value01);
    }

    public void Show()
    {
        if (group)
        {
            group.alpha = 1f;
            group.interactable = true;
            group.blocksRaycasts = false;
        }
        else gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (group)
        {
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
        else gameObject.SetActive(false);

        target01 = 0f;
        current01 = 0f;
        ApplyScale(0f);
    }

    private void HideImmediate()
    {
        if (group) group.alpha = 0f;
        else gameObject.SetActive(false);

        ApplyScale(0f);
    }

    private void Update()
    {
        // smooth mot target
        current01 = Mathf.Lerp(current01, target01, Time.deltaTime * smooth);
        ApplyScale(current01);
    }

    private void ApplyScale(float x01)
    {
        if (!fillRect) return;
        var s = fillRect.localScale;
        s.x = Mathf.Clamp01(x01);
        s.y = 1f;
        s.z = 1f;
        fillRect.localScale = s;
    }
}