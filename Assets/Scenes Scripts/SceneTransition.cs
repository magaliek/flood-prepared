using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Scenes_Scripts
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeOverlay;
        [SerializeField] private string sceneName;

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                StartCoroutine(FadeAndLoad());
        }

        IEnumerator FadeAndLoad()
        {
            while (fadeOverlay.alpha < 1f)
            {
                fadeOverlay.alpha += Time.deltaTime * 0.5f;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneName);
        }
}

}