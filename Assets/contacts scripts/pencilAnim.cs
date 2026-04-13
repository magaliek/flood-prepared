using System.Collections;
using UnityEngine;
using TMPro;

namespace contacts_scripts
{
    public class PencilAnim : MonoBehaviour
    {
        [SerializeField] private RectTransform pencilPrefab;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float duration;
        [SerializeField] private RectTransform notebookRect;
        
        public void PlayAnimate(TMP_Text targetText)
        {
            StartCoroutine(Animate(targetText));
        }

        private IEnumerator Animate(TMP_Text target)
        {
            RectTransform pencil = Instantiate(pencilPrefab, notebookRect);
            pencil.localScale = Vector3.one;
            pencil.gameObject.SetActive(true);

            target.ForceMeshUpdate();

            target.maxVisibleCharacters = 0;
            
            audioSource.Play();

            Vector3 worldStart = target.rectTransform.TransformPoint(
                new Vector3(target.rectTransform.rect.xMin, 0, 0));
            Vector3 worldEnd = target.rectTransform.TransformPoint(
                new Vector3(target.rectTransform.rect.xMax, 0, 0));

            Vector3 localStart = pencil.parent.InverseTransformPoint(worldStart);
            Vector3 localEnd = pencil.parent.InverseTransformPoint(worldEnd);

            float startX = localStart.x;
            float endX = localEnd.x;
            float textY = localStart.y;
            
            float elapsed = 0f;

            float offsetX = 30f;
            float offsetY = 18f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                float x = Mathf.Lerp(startX, endX, t);
                pencil.anchoredPosition = new Vector2(x + offsetX, textY + offsetY);
                
                float pencilTipX = x;
                for (int i = 0; i < target.textInfo.characterCount; i++)
                {
                    Vector3 worldChar = target.rectTransform.TransformPoint(
                        new Vector3(target.textInfo.characterInfo[i].bottomLeft.x, 0, 0));
                    float charX = pencil.parent.InverseTransformPoint(worldChar).x;

                    if (pencilTipX > charX)
                    {
                        target.maxVisibleCharacters = i + 1;
                    }
                }

                yield return null;
            }

            target.maxVisibleCharacters = target.textInfo.characterCount;
            
            pencil.gameObject.SetActive(false);
            Destroy(pencil.gameObject);
        }
    }
}