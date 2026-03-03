using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private readonly List<DocumentData> items = new();
    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public IReadOnlyList<DocumentData> Items => items;

    public void Add(DocumentData doc)
    {
        if (doc == null) return;
        items.Add(doc);
        OnInventoryChanged?.Invoke();
    }

    public void Remove(DocumentData doc)
    {
        if (doc == null) return;
        items.Remove(doc);
        OnInventoryChanged?.Invoke();
    }
}