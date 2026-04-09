using UnityEngine;
using UnityEngine.UI;

public class ZiplockBagUI : MonoBehaviour
{
    [SerializeField] private GameObject bagButton;
    [SerializeField] private GameObject bagTitle;
    [SerializeField] private GameObject bagContentsPanel;
    [SerializeField] private Transform bagGrid;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Canvas canvas;

    [Header("Bag Sprites")]
    [SerializeField] private Image bagButtonImage;
    [SerializeField] private Sprite openBagSprite;
    [SerializeField] private Sprite closedEmptyBagSprite;
    [SerializeField] private Sprite closedMediumBagSprite;
    [SerializeField] private Sprite closedFullBagSprite;

    private bool isOpen = false;

    private void Start()
    {
        RefreshVisibility();
        ApplyVisualState();
    }

    public void ToggleBag()
    {
        if (InventoryManager.Instance == null || InventoryManager.Instance.Items.Count == 0)
            return;

        isOpen = !isOpen;
        ApplyVisualState();

        if (isOpen)
            RebuildBag();
    }

    public void OpenBagForDrawer()
    {
        isOpen = true;
        ApplyVisualState();
        RebuildBag();
    }

    public void CloseBagAfterDrawer()
    {
        isOpen = false;
        ApplyVisualState();
    }

    public void UpdateBagVisual()
    {
        RefreshVisibility();
        ApplyVisualState();

        if (isOpen)
            RebuildBag();
    }

    public void RefreshVisibility()
    {
        bool hasItems = InventoryManager.Instance != null && InventoryManager.Instance.Items.Count > 0;

        if (bagButton != null)
            bagButton.SetActive(hasItems);

        if (!hasItems)
        {
            isOpen = false;
        }
    }

    private void ApplyVisualState()
    {
        bool hasItems = InventoryManager.Instance != null && InventoryManager.Instance.Items.Count > 0;

        if (bagTitle != null)
            bagTitle.SetActive(isOpen && hasItems);

        if (bagContentsPanel != null)
            bagContentsPanel.SetActive(isOpen && hasItems);

        if (bagButtonImage == null || !hasItems)
            return;

        int count = InventoryManager.Instance.Items.Count;

        if (isOpen)
        {
            if (openBagSprite != null)
                bagButtonImage.sprite = openBagSprite;
        }
        else
        {
            if (count <= 0)
            {
                if (closedEmptyBagSprite != null)
                    bagButtonImage.sprite = closedEmptyBagSprite;
            }
            else if (count <= 3)
            {
                if (closedMediumBagSprite != null)
                    bagButtonImage.sprite = closedMediumBagSprite;
            }
            else
            {
                if (closedFullBagSprite != null)
                    bagButtonImage.sprite = closedFullBagSprite;
            }
        }
    }

    private void RebuildBag()
    {
        if (bagGrid == null || itemPrefab == null || canvas == null || InventoryManager.Instance == null)
            return;

        foreach (Transform child in bagGrid)
            Destroy(child.gameObject);

        foreach (var doc in InventoryManager.Instance.Items)
        {
            var go = Instantiate(itemPrefab, bagGrid);
            var item = go.GetComponent<DraggableInventoryItem>();

            if (item != null)
                item.Init(doc, canvas);
        }
    }
}