using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections.Generic;
using UnityEngine.UI;

namespace packing_scripts
{
    public class Backpack : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private List<PackingItem> _packed = new List<PackingItem>();
        public int Calories { get; private set; }

        public IReadOnlyList<PackingItem> Packed => _packed;

        private RectTransform _contentParent;
        private CanvasGroup _cg;
        
        private Outline _outline;
        
        private readonly Dictionary<CanvasGroup, Coroutine> _fadeRoutines = new();

        [SerializeField] private CalorieTextScript calorieText;

        public void Awake()
        {
            _contentParent = transform as RectTransform;
            _cg = GetComponent<CanvasGroup>();
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedObj = eventData.pointerDrag;
            if (draggedObj == null) return;
            
            draggedObj.transform.SetParent(_contentParent, false);

            RectTransform draggedRect = draggedObj.GetComponent<RectTransform>();
            draggedRect.anchorMin = new Vector2(0.5f, 0.25f);
            draggedRect.anchorMax = new Vector2(0.5f, 0.25f);
            draggedRect.pivot     = new Vector2(0.5f, 0.25f);
            draggedRect.anchoredPosition = Vector2.zero;
            draggedRect.localRotation = Quaternion.identity;
            draggedRect.localScale = Vector3.one;

            PackingItem item = draggedObj.GetComponent<PackingItem>();
            CanvasGroup itemCg = draggedObj.GetComponent<CanvasGroup>();
            AddNewItem(item);
            UpdateCalories(item);
            FadeOut(itemCg, 2f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _outline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outline.enabled = false;
        }

        private void FadeOut(CanvasGroup cg, float duration)
        {
            if (cg == null) return;
            
            if (_fadeRoutines.TryGetValue(cg, out var routine) && routine != null)
                StopCoroutine(routine);

            _fadeRoutines[cg] = StartCoroutine(FadeOutRoutine(cg, duration));
        }
        
        private IEnumerator FadeOutRoutine(CanvasGroup cg, float duration)
        {
            if (cg == null) yield break;
            cg.blocksRaycasts = false;
            cg.interactable = false;

            float startAlpha = cg.alpha;
            float t = 0f;

            while (t < duration)
            {
                if (cg == null) yield break;
                
                t += Time.deltaTime;
                float p = Mathf.Clamp01(t / duration);

                cg.alpha = Mathf.Lerp(startAlpha, 0f, p);
                yield return null;
            }

            cg.alpha = 0f;
            _fadeRoutines.Remove(cg);
        }

        private void UpdateCalories(PackingItem item)
        {
            Calories += item.Calories;
            calorieText.UpdateText();
        }

        private PackingItem AddNewItem(PackingItem newItem)
        {
            try
            {
                _packed.Add(newItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return newItem;
        }

        public void RemoveItem(PackingItem item)
        {
            try
            {
                _packed.Remove(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}