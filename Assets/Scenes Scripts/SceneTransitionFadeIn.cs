using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Scenes_Scripts
{
    public class SceneTransitionFadeIn : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeOverlay;
        [SerializeField] private string sceneName;
        
        private void Awake()
        {
            fadeOverlay.alpha = 0f;
        }
        
        private void OnEnable()
        {
            fadeOverlay.alpha = 0f;
        }
        
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                StartCoroutine(FadeAndLoad());
        }

        IEnumerator FadeAndLoad()
        {
            while (fadeOverlay.alpha < 1f)
            {
                fadeOverlay.alpha += Mathf.Clamp(Time.deltaTime * 2f, 0, 1);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneName);
        }
    }

}