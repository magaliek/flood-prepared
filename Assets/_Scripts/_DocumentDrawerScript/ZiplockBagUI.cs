using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZiplockBagUI : MonoBehaviour
{
    [SerializeField] private GameObject bagContentsPanel;
    [SerializeField] private Transform bagGrid;
    [SerializeField] private GameObject itemPrefab; // just an Image + TMP_Text prefab

    [Header("Bag Sprites")]
    [SerializeField] private Image bagButtonImage;
    [SerializeField] private Sprite openBagSprite;
    [SerializeField] private Sprite closedEmptyBagSprite;
    [SerializeField] private Sprite closedMediumBagSprite;
    [SerializeField] private Sprite closedFullBagSprite;

    private readonly List<DocumentData> packedDocs = new();
    private bool isOpen = false;

    private void Start()
    {
        bagContentsPanel?.SetActive(false);
        ApplySprite();
    }

    public void OpenBagForDrawer()
    {
        isOpen = true;
        bagContentsPanel?.SetActive(true);
        ApplySprite();
    }

    public void CloseBagAfterDrawer()
    {
        isOpen = false;
        bagContentsPanel?.SetActive(false);
        ApplySprite();
    }

    public void AddDocument(DocumentData doc)
    {
        if (doc == null) return;
        packedDocs.Add(doc);
        RebuildGrid();
        ApplySprite();
    }

    private void RebuildGrid()
    {
        
        if (bagGrid == null || itemPrefab == null) return;

        foreach (Transform child in bagGrid)
            Destroy(child.gameObject);

        foreach (var doc in packedDocs)
        {
            var go = Instantiate(itemPrefab, bagGrid);

            var iconT = go.transform.Find("Icon");
            if (iconT != null)
            {
                var img = iconT.GetComponent<Image>();
                if (img != null) { img.sprite = doc.icon; img.preserveAspect = true; }
            }

            var label = go.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = doc.displayName;
        }
    }

    private void ApplySprite()
    {
        if (bagButtonImage == null) return;
        int count = packedDocs.Count;

        if (isOpen && openBagSprite != null)
            { bagButtonImage.sprite = openBagSprite; return; }

        if (count == 0 && closedEmptyBagSprite != null)
            bagButtonImage.sprite = closedEmptyBagSprite;
        else if (count <= 3 && closedMediumBagSprite != null)
            bagButtonImage.sprite = closedMediumBagSprite;
        else if (closedFullBagSprite != null)
            bagButtonImage.sprite = closedFullBagSprite;
    }

    public void ToggleBag()
    {
        if (packedDocs.Count == 0) return;
        isOpen = !isOpen;
        bagContentsPanel?.SetActive(isOpen);
        ApplySprite();
        if (isOpen) RebuildGrid();
    }
}