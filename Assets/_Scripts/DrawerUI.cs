using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawerUI : MonoBehaviour
{
    public static DrawerUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject drawerPanel;
    [SerializeField] private Transform drawerGrid;
    [SerializeField] private TMP_Text titleText;

    [Header("Buttons")]
    [SerializeField] private Button closeButton;

    [Header("Prefab for each item inside the drawer")]
    [SerializeField] private Button drawerItemButtonPrefab;

    private InteractableDocumentStackSource currentDrawer;

    private void Awake()
    {
        // Singleton safety 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (drawerPanel != null)
            drawerPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
    }

    public void Open(InteractableDocumentStackSource drawer, string title = "Drawer")
    {
        currentDrawer = drawer;

        if (titleText != null)
            titleText.text = title;

        if (drawerPanel != null)
            drawerPanel.SetActive(true);

        Refresh();
    }

    public void Close()
    {
        if (drawerPanel != null)
            drawerPanel.SetActive(false);

        currentDrawer = null;
        ClearGrid();
    }

    public void Refresh()
{
    ClearGrid();
    if (currentDrawer == null) return;

    var docsCopy = new List<DocumentData>(currentDrawer.GetDocuments());

    foreach (var doc in docsCopy)
    {
        if (doc == null) continue;

        var btn = Instantiate(drawerItemButtonPrefab, drawerGrid);

        // Find label child (Label) or fallback to any TMP in children
        TMP_Text label = null;
        var labelT = btn.transform.Find("Label");
        if (labelT != null) label = labelT.GetComponent<TMP_Text>();
        if (label == null) label = btn.GetComponentInChildren<TMP_Text>();

        if (label != null) label.text = doc.displayName;

        // Find icon child (Icon)
        var iconT = btn.transform.Find("Icon");
        if (iconT != null)
        {
            var img = iconT.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = doc.icon;
                img.enabled = img.sprite != null;
                img.preserveAspect = true;
            }
        }

        btn.onClick.AddListener(() =>
        {
            bool removed = currentDrawer.TakeSpecific(doc);
            if (!removed) return;

            InventoryManager.Instance.Add(doc);
            Refresh();
        });
    }
}

    private void ClearGrid()
    {
        if (drawerGrid == null) return;

        for (int i = drawerGrid.childCount - 1; i >= 0; i--)
            Destroy(drawerGrid.GetChild(i).gameObject);
    }
}