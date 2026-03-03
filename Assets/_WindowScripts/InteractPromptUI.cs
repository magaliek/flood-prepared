using TMPro;
using UnityEngine;

public class InteractPromptUI : MonoBehaviour
{
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        if (!promptText) promptText = GetComponentInChildren<TMP_Text>();
        Hide();
    }

    public void Show(string text)
    {
        promptText.text = text;
        promptText.enabled = true;
    }

    public void Hide()
    {
        promptText.text = "";
        promptText.enabled = false;
    }
}