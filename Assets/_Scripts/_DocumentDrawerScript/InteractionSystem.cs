using UnityEngine;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private GameObject interactButton; // valgfri

    private IInteractable current;

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        // PC: Trykk Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            TryInteract();
    }

    public void SetCurrent(IInteractable interactable)
    {
        current = interactable;
        RefreshUI();
    }

    public void ClearCurrent(IInteractable interactable)
    {
        if (current == interactable)
            current = null;

        RefreshUI();
    }

    public void TryInteract()
    {
        Debug.Log("TryInteract called. Current = " + (current == null ? "NULL" : current.GetType().Name));

        if (current == null) return;

        Debug.Log("Interacting with: " + current.GetType().Name);
        current.Interact();

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (promptText != null)
            promptText.text = current == null ? "" : current.PromptText;

        if (interactButton != null)
            interactButton.SetActive(current != null);
    }
}