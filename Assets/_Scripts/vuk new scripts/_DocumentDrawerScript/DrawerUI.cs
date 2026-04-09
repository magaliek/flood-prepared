using System.Collections;
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

    [Header("Drawer Item Prefab")]
    [SerializeField] private Button drawerItemButtonPrefab;

    [Header("Ziplock Bag")]
    [SerializeField] private GameObject ziplockBagButton;
    [SerializeField] private ZiplockBagUI ziplockBagUI;
    [SerializeField] private RectTransform ziplockTarget;

    [Header("Animation")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject flyingIconPrefab;
    [SerializeField] private float flyDuration = 0.4f;

    private InteractableDocumentStackSource currentDrawer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (drawerPanel != null)
            drawerPanel.SetActive(false);

        if (ziplockBagButton != null)
            ziplockBagButton.SetActive(false);

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

        if (ziplockBagButton != null)
            ziplockBagButton.SetActive(true);

        if (ziplockBagUI != null)
            ziplockBagUI.OpenBagForDrawer();

        Refresh();
    }

    public void Close()
    {
        if (drawerPanel != null)
            drawerPanel.SetActive(false);

        currentDrawer = null;
        ClearGrid();

        if (ziplockBagUI != null)
            ziplockBagUI.CloseBagAfterDrawer();
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

            if (btn.GetComponent<DrawerItemHover>() == null)
                btn.gameObject.AddComponent<DrawerItemHover>();

            TMP_Text label = btn.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = doc.displayName;

            var iconT = btn.transform.Find("Icon");
            Image iconImg = null;

            if (iconT != null)
            {
                iconImg = iconT.GetComponent<Image>();
                if (iconImg != null)
                {
                    iconImg.sprite = doc.icon;
                    iconImg.enabled = iconImg.sprite != null;
                    iconImg.preserveAspect = true;
                }
            }

            btn.onClick.AddListener(() =>
            {
                Vector3 startPos = btn.transform.position;
                Sprite docSprite = iconImg != null ? iconImg.sprite : null;

                bool removed = currentDrawer.TakeSpecific(doc);
                if (!removed) return;

                btn.gameObject.SetActive(false);

                StartCoroutine(AnimateDocumentToBag(docSprite, startPos, () =>
                {
                    InventoryManager.Instance.Add(doc);

                    if (ziplockBagUI != null)
                        ziplockBagUI.UpdateBagVisual();

                    Refresh();
                }));
            });
        }
    }

    private IEnumerator AnimateDocumentToBag(Sprite docSprite, Vector3 startWorldPos, System.Action onComplete)
    {
        if (canvas == null || ziplockTarget == null || flyingIconPrefab == null)
        {
            onComplete?.Invoke();
            yield break;
        }

        GameObject flyingObj = Instantiate(flyingIconPrefab, canvas.transform);
        RectTransform flyingRect = flyingObj.GetComponent<RectTransform>();
        Image flyingImage = flyingObj.GetComponent<Image>();

        if (flyingImage != null)
            flyingImage.sprite = docSprite;

        flyingRect.position = startWorldPos;

        Vector3 endPos = ziplockTarget.position;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.one * 0.3f;

        float elapsed = 0f;

        while (elapsed < flyDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / flyDuration;

            flyingRect.position = Vector3.Lerp(startWorldPos, endPos, t);
            flyingRect.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        Destroy(flyingObj);
        onComplete?.Invoke();
    }

    private void ClearGrid()
    {
        if (drawerGrid == null) return;

        for (int i = drawerGrid.childCount - 1; i >= 0; i--)
            Destroy(drawerGrid.GetChild(i).gameObject);
    }
}