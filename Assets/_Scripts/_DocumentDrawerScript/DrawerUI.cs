using System;
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
    [SerializeField] private Button closeButton;

    [Header("Prefab")]
    [SerializeField] private Button drawerItemButtonPrefab;

    [Header("Ziplock")]
    [SerializeField] private ZiplockBagUI ziplockBagUI;
    [SerializeField] private RectTransform ziplockTarget;

    [Header("Fly Animation")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject flyingIconPrefab;
    [SerializeField] private float flyDuration = 0.4f;

    private List<DocumentData> currentDocs;
    private Action<DocumentData> onDocTaken;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        drawerPanel?.SetActive(false);
        closeButton?.onClick.AddListener(Close);
    }

    public void Open(List<DocumentData> docs, string title, Action<DocumentData> onTaken)
    {
        currentDocs = docs;
        onDocTaken = onTaken;

        if (titleText != null) titleText.text = title;
        drawerPanel?.SetActive(true);
        ziplockBagUI?.OpenBagForDrawer();

        Refresh();
    }

    public void Close()
    {
        drawerPanel?.SetActive(false);
        currentDocs = null;
        onDocTaken = null;
        ClearGrid();
        ziplockBagUI?.CloseBagAfterDrawer();
    }

    private void Refresh()
    {
        ClearGrid();
        if (currentDocs == null) return;

        // iterate a copy so removing mid-loop is safe
        foreach (var doc in new List<DocumentData>(currentDocs))
        {
            if (doc == null) continue;

            var btn = Instantiate(drawerItemButtonPrefab, drawerGrid);

            var label = btn.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = doc.displayName;

            var iconT = btn.transform.Find("Icon");
            Image iconImg = iconT?.GetComponent<Image>();
            if (iconImg != null)
            {
                iconImg.sprite = doc.icon;
                iconImg.enabled = doc.icon != null;
                iconImg.preserveAspect = true;
            }

            btn.onClick.AddListener(() =>
            {
                Vector3 startPos = btn.transform.position;
                Sprite sprite = iconImg?.sprite;

                btn.gameObject.SetActive(false);
                onDocTaken?.Invoke(doc);       // tell the drawer the doc was taken
                ziplockBagUI?.AddDocument(doc); // tell the bag to show it

                StartCoroutine(AnimateToBag(sprite, startPos, Refresh));
            });
        }
    }

    private IEnumerator AnimateToBag(Sprite sprite, Vector3 from, Action onComplete)
    {
        if (canvas == null || ziplockTarget == null || flyingIconPrefab == null)
        {
            onComplete?.Invoke();
            yield break;
        }

        var flyObj = Instantiate(flyingIconPrefab, canvas.transform);
        var flyRect = flyObj.GetComponent<RectTransform>();
        var flyImg = flyObj.GetComponent<Image>();

        if (flyImg != null) flyImg.sprite = sprite;
        flyRect.position = from;

        float elapsed = 0f;
        Vector3 endPos = ziplockTarget.position;

        while (elapsed < flyDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / flyDuration;
            flyRect.position = Vector3.Lerp(from, endPos, t);
            flyRect.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.3f, t);
            yield return null;
        }

        Destroy(flyObj);
        onComplete?.Invoke();
    }

    private void ClearGrid()
    {
        if (drawerGrid == null) return;
        for (int i = drawerGrid.childCount - 1; i >= 0; i--)
            Destroy(drawerGrid.GetChild(i).gameObject);
    }
}