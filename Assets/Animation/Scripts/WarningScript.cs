using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Animation.Scripts
{
    public class WarningScript : MonoBehaviour
    {
        [SerializeField] public GameObject panel;
        [SerializeField] private float totalDuration = 10f;
        [SerializeField] private float flashSpeed = 1.5f;

        public static WarningScript Instance;
        
        private Image _image;
        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        
        void Start()
        {
            panel.SetActive(false);
            _image = this.gameObject.GetComponent<Image>();
        }
        
        public void playFlashAnim()
        {
            StartCoroutine(flashAnim());
        }
        
        IEnumerator flashAnim()
        {
            panel.SetActive(true);
            
            float elapsed = 0f;

            Color c = _image.color;

            while (elapsed < totalDuration)
            {
                elapsed += Time.deltaTime;

                float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f);

                alpha = Mathf.Lerp(0.2f, 1f, alpha);

                c.a = alpha;
                _image.color = c;

                yield return null;
            }

            c.a = 0f;
            _image.color = c;
            panel.SetActive(false);
        }

    }
}
