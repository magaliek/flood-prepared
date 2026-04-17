using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuseSwitchUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image switchImage;
    [SerializeField] private TMP_Text statusText;

    [Header("Sprites")]
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    [Header("Settings")]
    [SerializeField] private string roomName = "Kitchen";
    [SerializeField] private bool isOn = true;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
            button.onClick.AddListener(ToggleSwitch);

        RefreshVisual();
    }

    public void ToggleSwitch()
    {
        isOn = !isOn;
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        if (switchImage != null)
            switchImage.sprite = isOn ? onSprite : offSprite;

        if (statusText != null)
            statusText.text = $"{roomName}: {(isOn ? "ON" : "OFF")}";
    }

    public bool IsOn()
    {
        return isOn;
    }

    public void SetState(bool value)
    {
        isOn = value;
        RefreshVisual();
    }
}