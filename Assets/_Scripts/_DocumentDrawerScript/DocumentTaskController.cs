using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DocumentTaskController : MonoBehaviour
{
    public static DocumentTaskController Instance { get; private set; }

    [Header("Required documents (drag DocumentData assets here)")]
    [SerializeField] private List<DocumentData> requiredDocuments = new List<DocumentData>();

    [Header("UI")]
    [SerializeField] private TMP_Text folderProgressText;
    [SerializeField] private GameObject scanButtonObject;

    // Track packed docs by id (avoids duplicates)
    private HashSet<string> packedDocumentIds = new HashSet<string>();

    private bool scannedToCloud = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Hide scan button until all required docs are packed
        if (scanButtonObject != null)
            scanButtonObject.SetActive(false);

        UpdateFolderProgressText();
    }

    /// <summary>
    /// Called by FolderSlotUI when a document is dropped into a folder slot.
    /// </summary>
    public void OnDocumentPacked(DocumentData doc)
    {
        if (doc == null)
        {
            Debug.LogWarning("OnDocumentPacked called with null doc.");
            return;
        }

        if (string.IsNullOrWhiteSpace(doc.id))
        {
            Debug.LogWarning($"Document '{doc.name}' has empty id. Set id in DocumentData.");
            return;
        }

        // Only count it if it's actually required (optional but recommended)
        if (!IsRequired(doc))
        {
            Debug.Log($"Packed '{doc.displayName}', but it is not in requiredDocuments list.");
            // You can choose to return here if you want strict behavior:
            // return;
        }

        bool added = packedDocumentIds.Add(doc.id); // false if already packed
        if (!added)
        {
            Debug.Log($"Document already packed: {doc.displayName} ({doc.id})");
            return;
        }

        UpdateFolderProgressText();

        // If all required are packed, show scan button
        if (HasAllRequiredDocuments() && scanButtonObject != null)
            scanButtonObject.SetActive(true);
    }

    /// <summary>
    /// Called after scan animation finishes (or when scan button is pressed).
    /// </summary>
    public void MarkScannedToCloud()
    {
        if (!HasAllRequiredDocuments())
        {
            Debug.LogWarning("Tried to scan, but not all required documents are packed yet.");
            return;
        }

        scannedToCloud = true;
        Debug.Log("Documents scanned and saved to cloud!");

        UpdateFolderProgressText();

        // Optional: hide scan button after scanning
        if (scanButtonObject != null)
            scanButtonObject.SetActive(false);
    }

    private void UpdateFolderProgressText()
    {
        if (folderProgressText == null) return;

        int requiredCount = requiredDocuments.Count;
        int packedCount = CountPackedRequired();

        if (!HasAllRequiredDocuments())
        {
            folderProgressText.text =
                $"Folder: {packedCount}/{requiredCount} required documents packed";
        }
        else if (!scannedToCloud)
        {
            folderProgressText.text =
                $"Folder complete: {packedCount}/{requiredCount}. Ready to scan!";
        }
        else
        {
            folderProgressText.text =
                $" Documents packed + scanned to cloud ({packedCount}/{requiredCount})";
        }
    }

    private bool HasAllRequiredDocuments()
    {
        if (requiredDocuments == null || requiredDocuments.Count == 0)
            return false;

        foreach (var doc in requiredDocuments)
        {
            if (doc == null) continue;

            if (string.IsNullOrWhiteSpace(doc.id)) continue;

            if (!packedDocumentIds.Contains(doc.id))
                return false;
        }
        return true;
    }

    private int CountPackedRequired()
    {
        int count = 0;
        foreach (var doc in requiredDocuments)
        {
            if (doc == null) continue;
            if (string.IsNullOrWhiteSpace(doc.id)) continue;

            if (packedDocumentIds.Contains(doc.id))
                count++;
        }
        return count;
    }

    private bool IsRequired(DocumentData doc)
    {
        foreach (var r in requiredDocuments)
        {
            if (r == null) continue;
            if (r.id == doc.id) return true;
        }
        return false;
    }
}