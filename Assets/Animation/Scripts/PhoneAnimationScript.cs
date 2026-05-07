using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Animation.Scripts
{
    public class PhoneAnimationScript : MonoBehaviour
    {
        [SerializeField] private GameObject Panel;
        [SerializeField] private GameObject screen;
        [SerializeField] private float _delay = 3;
        [SerializeField] private float delay2 = 5;
        private Image _imageFinger;
        private Image _imagePhone;
        [SerializeField] private RectTransform banner;
        [SerializeField] private RectTransform fingerRect;
        
        [SerializeField] private float duration = 1.5f;
        [SerializeField] private float fingerMoveDuration = 0.8f;
        [SerializeField] private float moveDistanceY = 600f;
        [SerializeField] private float fingerMoveDistanceX = 1200f;

        private static bool animPlayed = false;
        
        void Start()
        {
            if (animPlayed)
            {
                Panel.SetActive(false);
                return;
            }
            
            screen.SetActive(false);
            _delay -= Time.deltaTime;
            _imageFinger = fingerRect.gameObject.GetComponent<Image>();

            _imagePhone = this.gameObject.GetComponent<Image>();
            
            Color c = _imagePhone.color;
            c.a = 0f;
            _imagePhone.color = c;
            
            if (!animPlayed) StartCoroutine(FadeIn());
        }
        
        public void PhoneAnimate()
        {
            if (!animPlayed) StartCoroutine(FingerAnimate());
        }

        private IEnumerator FingerAnimate()
        {
            banner.gameObject.SetActive(true);
            fingerRect.gameObject.SetActive(true);

            Vector2 bannerStart = banner.anchoredPosition;
            Vector2 fingerStart = fingerRect.anchoredPosition;

            Vector2 fingerLeftPosition = fingerStart + new Vector2(-fingerMoveDistanceX, 0f);

            float elapsed = 0f;

            while (elapsed < fingerMoveDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / fingerMoveDuration;

                fingerRect.anchoredPosition = Vector2.Lerp(fingerStart, fingerLeftPosition, t);

                yield return null;
            }

            fingerRect.anchoredPosition = fingerLeftPosition;

            elapsed = 0f;

            Vector2 bannerEnd = bannerStart + new Vector2(0f, moveDistanceY*1.5f);
            Vector2 fingerEnd = fingerLeftPosition + new Vector2(0f, moveDistanceY);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                banner.anchoredPosition = Vector2.Lerp(bannerStart, bannerEnd, t);
                fingerRect.anchoredPosition = Vector2.Lerp(fingerLeftPosition, fingerEnd, t);

                yield return null;
            }

            banner.anchoredPosition = bannerEnd;
            fingerRect.anchoredPosition = fingerEnd;
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(_delay);
            
            float phoneDuration = 1f;
            float elapsed = 0f;
            Color c = _imagePhone.color;
            
            while (elapsed < phoneDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsed / phoneDuration);
                
                c.a = alpha;
                _imagePhone.color = c;
                
                yield return null;
            }
            c.a = 1f;
            _imagePhone.color = c;
            screen.SetActive(true);
            PhoneAnimate();
        }

        public IEnumerator FadeOut()
        {
            float duration = 1f;
            float elapsed = 0f;
            
            Color c = _imageFinger.color;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
                
                c.a = alpha;
                _imageFinger.color = c;
                
                yield return null;
            }
            c.a = 0f;
            _imageFinger.color = c;
            yield return new WaitForSeconds(delay2);
            Panel.SetActive(false);
            animPlayed = true;
        }
    }
}