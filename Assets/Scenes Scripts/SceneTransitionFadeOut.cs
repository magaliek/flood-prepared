using UnityEngine;
using System.Collections;

namespace Scenes_Scripts
{
    public class SceneTransitionFadeOut : MonoBehaviour
    {
        private CanvasGroup _fadeOverlay;

        void Awake()
        {
            _fadeOverlay = GetComponent<CanvasGroup>();
            _fadeOverlay.alpha = 1f;
        }
        
        void OnEnable()
        {
            _fadeOverlay = GetComponent<CanvasGroup>();
            _fadeOverlay.alpha = 1f;
        }

        void Start()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_fadeOverlay.alpha > 0f)
            {
                _fadeOverlay.alpha -= Mathf.Clamp(Time.deltaTime * 2f, 0, 1);
                yield return null;
            }
        }
    }
}