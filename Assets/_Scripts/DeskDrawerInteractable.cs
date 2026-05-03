using System.Collections.Generic;
using UnityEngine;
using score_system;

public class DeskDrawerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<DocumentData> documents = new List<DocumentData>();
    [SerializeField] private string drawerTitle = "Desk Drawer";

    public string PromptText => DocumentsLeft() > 0
        ? $"Press Enter to open ({DocumentsLeft()} left)"
        : "Documents collected";

    public void Interact()
    {
        if (DocumentsLeft() <= 0)
        {
            ScoreScript.Instance.drawerDone = true;
            return;
        }

        if (ScoreScript.Instance.drawerDone)
        {
            return;
        }

        if (DrawerUI.Instance == null)
        {
            Debug.LogError("DrawerUI.Instance is null — is DrawerUI in the scene?");
            return;
        }

        DrawerUI.Instance.Open(documents, drawerTitle, OnDocumentTaken);
    }

    private void OnDocumentTaken(DocumentData doc)
    {
        documents.Remove(doc);

        if (DocumentsLeft() <= 0)
            ScoreScript.Instance.drawerDone = true;
    }

    private int DocumentsLeft()
    {
        int count = 0;
        foreach (var d in documents)
            if (d != null) count++;
        return count;
    }
}