using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FolderSlotUI : MonoBehaviour, IDropHandler
{
    [Header("Optional: icon image inside this slot")]
    [SerializeField] private Image icon;

    public DocumentData Stored { get; private set; }

    public void OnDrop(PointerEventData eventData)
    {
        // This is the dragged UI item (from inventory)
        var dragged = eventData.pointerDrag?.GetComponent<DraggableInventoryItem>();
        if (dragged == null) return;

        // Don’t allow dropping if slot already filled
        if (Stored != null) return;

        // Make sure dragged item actually has a document
        if (dragged.Document == null)
        {
            Debug.LogWarning("FolderSlotUI: Dropped item has no DocumentData.");
            return;
        }

        // Store document in this slot
        Stored = dragged.Document;

        // Show icon if you use icons (and if icon exists on DocumentData)
        if (icon != null)
        {
            icon.sprite = Stored.icon;            // OK even if null, but then it shows nothing
            icon.enabled = (icon.sprite != null); // only enable if we actually have a sprite
        }

        // Remove from inventory (so it can’t be used again)
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.Remove(Stored);
        }
        else
        {
            Debug.LogWarning("FolderSlotUI: InventoryManager.Instance is null (not in scene?).");
        }

        // Remove the dragged UI element from the screen
        Destroy(dragged.gameObject);

        // Tell the controller this doc is packed
        if (DocumentTaskController.Instance != null)
        {
            DocumentTaskController.Instance.OnDocumentPacked(Stored);
        }
        else
        {
            Debug.LogWarning("FolderSlotUI: DocumentTaskController.Instance is null (not in scene?).");
        }
    }
}