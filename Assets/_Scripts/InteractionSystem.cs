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
        // PC: Trykk E
        if (Input.GetKeyDown(KeyCode.E))
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

    // Kan kalles fra E eller fra knapp (OnClick)
    public void TryInteract()
    {
        if (current == null) return;

        Debug.Log("Interacting with: " + current.GetType().Name);
        current.Interact();

        // Objektet kan deaktivere seg selv etter interaksjon
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