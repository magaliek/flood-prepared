using UnityEngine;

public class InteractableDocumentSource : MonoBehaviour, IInteractable
{
    [SerializeField] private DocumentData document;
    [SerializeField] private string prompt = "Press E to collect";

    private bool collected = false;

    public string PromptText
    {
        get
        {
            if (collected) return "";
            if (document != null) return $"{prompt} {document.displayName}";
            return prompt;
        }
    }

    public void Interact()
    {
        if (collected) return;

        if (document == null)
        {
            Debug.LogWarning($"{name}: No DocumentData assigned!");
            return;
        }

        // Add to inventory
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.Add(document);
        }
        else
        {
            Debug.LogWarning("InventoryManager.Instance is null (not in scene?)");
            return;
        }

        collected = true;
        
        gameObject.SetActive(false);

    }
}