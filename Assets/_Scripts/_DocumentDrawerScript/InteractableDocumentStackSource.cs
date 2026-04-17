using System.Collections.Generic;
using UnityEngine;

public class InteractableDocumentStackSource : MonoBehaviour, IInteractable
{
    [Header("Docs inside this drawer")]
    [SerializeField] private List<DocumentData> documents = new List<DocumentData>();

    [Header("Prompt text")]
    [SerializeField] private string prompt = "Press Enter to open";
    [SerializeField] private string completedPrompt = "Documents picked up";

    public string PromptText
    {
        get
        {
            int left = DocumentsLeft();

            if (left <= 0)
                return completedPrompt;

            return $"{prompt} ({left} left)";
        }
    }

    public void Interact()
    {
        if (DocumentsLeft() <= 0)
        {
            return;
        }

        if (DrawerUI.Instance == null)
        {
            Debug.LogWarning("DrawerUI.Instance is null. Is DrawerUI in the scene?");
            return;
        }

        DrawerUI.Instance.Open(this, gameObject.name);
    }

    public List<DocumentData> GetDocuments()
    {
        return documents;
    }

    public bool TakeSpecific(DocumentData doc)
    {
        if (doc == null) return false;
        if (documents == null) return false;

        bool removed = documents.Remove(doc);
        return removed;
    }

    private int DocumentsLeft()
    {
        if (documents == null) return 0;

        int count = 0;
        for (int i = 0; i < documents.Count; i++)
        {
            if (documents[i] != null)
                count++;
        }

        return count;
    }
}