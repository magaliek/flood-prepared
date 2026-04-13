using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private ZiplockBagUI ziplockBagUI;

    private void Start()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged -= Rebuild;
            InventoryManager.Instance.OnInventoryChanged += Rebuild;
        }

        Rebuild();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= Rebuild;
    }

    private void Rebuild()
    {
        if (gridParent == null || itemPrefab == null || canvas == null)
            return;

        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        if (InventoryManager.Instance == null)
            return;

        foreach (var doc in InventoryManager.Instance.Items)
        {
            var go = Instantiate(itemPrefab, gridParent);
            var item = go.GetComponent<DraggableInventoryItem>();

            if (item != null)
                item.Init(doc, canvas);
        }

        if (ziplockBagUI != null)
    ziplockBagUI.RefreshVisibility();
    }
}