using System.Collections;
using TMPro;
using UnityEngine;

namespace packing_scripts
{
    public class MaxWeight : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float duration;
        [SerializeField] private RectTransform textRect;
        public int Max { get; } = 13;


        public void PlayAnimate()
        {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            audioSource.Play();

            float elapsed = 0f;
            

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                float x = Mathf.Lerp(5f, 0f, t);
                float randomRot = Random.Range(-x, x);
                transform.localRotation = Quaternion.Euler(0f, 0f, randomRot);

                yield return null;
            }
        }
    }
}