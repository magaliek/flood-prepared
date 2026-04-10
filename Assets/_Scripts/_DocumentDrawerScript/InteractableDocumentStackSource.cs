using System.Collections.Generic;
using UnityEngine;

public class InteractableDocumentStackSource : MonoBehaviour, IInteractable
{
    [Header("Docs inside this drawer")]
    [SerializeField] private List<DocumentData> documents = new List<DocumentData>();

    [Header("Prompt text")]
    [SerializeField] private string prompt = "Press E to open";

    public string PromptText
    {
        get
        {
            int left = DocumentsLeft();
            if (left <= 0) return "Empty";
            return $"{prompt} ({left} left)";
        }
    }

    public void Interact()
    {
        if (DocumentsLeft() <= 0)
        {
            DisableWhenEmpty();
            return;
        }

        if (DrawerUI.Instance == null)
        {
            Debug.LogWarning("DrawerUI.Instance is null. Is DrawerUI in the scene?");
            return;
        }

        // Open drawer UI and show these documents
        DrawerUI.Instance.Open(this, gameObject.name);
    }

    // DrawerUI calls this to show what's inside
    public List<DocumentData> GetDocuments()
    {
        // return the actual list (simple + ok for prototype)
        return documents;
    }

    // DrawerUI calls this when player clicks a specific document
    public bool TakeSpecific(DocumentData doc)
    {
        if (doc == null) return false;
        if (documents == null) return false;

        bool removed = documents.Remove(doc);

        if (DocumentsLeft() <= 0)
            DisableWhenEmpty();

        return removed;
    }

    private int DocumentsLeft()
    {
        if (documents == null) return 0;

        int count = 0;
        for (int i = 0; i < documents.Count; i++)
            if (documents[i] != null) count++;

        return count;
    }

    private void DisableWhenEmpty()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }
}