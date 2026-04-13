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
        public static Backpack Instance { get; private set; }
        
        private List<PackingItem> _packed = new List<PackingItem>();
        private float _calories;
        private float _carbs;
        private float _protein;
        private float _fat;
        private float _sodium;
        private float _water;
        private float _weight;
        private MaxWeight _maxWeightScript;

        public IReadOnlyList<PackingItem> Packed => _packed;

        private RectTransform _contentParent;
        
        private Outline _outline;
        
        private readonly Dictionary<CanvasGroup, Coroutine> _fadeRoutines = new();

        [SerializeField] private MacroText calorieText;
        [SerializeField] private MacroText carbsText;
        [SerializeField] private MacroText proteinText;
        [SerializeField] private MacroText fatText;
        [SerializeField] private MacroText sodiumText;
        [SerializeField] private MacroText waterText;
        [SerializeField] private MacroText weightText;
        [SerializeField] private GameObject maxWeight;

        public void Awake()
        {
            if (Instance != null) {Destroy(gameObject); return;}
            Instance = this;
            
            _contentParent = transform as RectTransform;
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            _maxWeightScript = maxWeight.GetComponent<MaxWeight>();
        }
        
        public void OnEnable()
        {
            _contentParent = transform as RectTransform;
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedObj = eventData.pointerDrag;
            if (draggedObj == null) return;

            if (_weight >= _maxWeightScript.Max)
            {
                _maxWeightScript.PlayAnimate();
                return;
            }
            
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
            UpdateMacros(item);
            
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

        private void UpdateMacros(PackingItem item)
        {
            _calories += item.Calories;
            _carbs += item.Carbs;
            _protein += item.Protein;
            _fat += item.Fat;
            _sodium += item.Sodium;
            _water += item.Water;
            _weight += item.Weight;
            
            calorieText.UpdateText($"Calories: {_calories}kcal / 3570kcal");
            carbsText.UpdateText($"Carbs: {_carbs}g / 390g");
            proteinText.UpdateText($"Protein: {_protein}g / 168g");
            fatText.UpdateText($"Fat: {_fat}g / 148.5g");
            sodiumText.UpdateText($"Sodium: {_sodium}mg / 1500mg");
            waterText.UpdateText($"Water: {_water}L / 7.5L");
            weightText.UpdateText($"Weight: {_weight}kg / {_maxWeightScript.Max}");
        }

        private PackingItem AddNewItem(PackingItem newItem) //maybe useless
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
                item.ResetItem();
                _weight = 0f;
                weightText.UpdateText($"Weight: {_weight}kg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}