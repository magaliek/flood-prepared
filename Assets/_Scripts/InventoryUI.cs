using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        if (InventoryManager.Instance == null) return;
        InventoryManager.Instance.OnInventoryChanged += Rebuild;
        Rebuild();
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= Rebuild;
    }

    private void Rebuild()
    {
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (var doc in InventoryManager.Instance.Items)
        {
            var go = Instantiate(itemPrefab, gridParent);
            go.GetComponent<DraggableInventoryItem>()
              .Init(doc, canvas);
        }
    }
}