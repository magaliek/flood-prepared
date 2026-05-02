using UnityEngine;
using UnityEngine.UI;
using score_system;
using System.Collections;

namespace persistentUI
{
    public class PersistentUIManager : MonoBehaviour
    {
        public Button notification;
        public GameObject phonePanel;

        public Button phase2button;

        [SerializeField] public AudioSource phase1Music;
        [SerializeField] public AudioSource phase2Music;

        public static PersistentUIManager Instance;

        public bool gameEnded = false;

        private Coroutine musicRoutine;

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
            notification.onClick.AddListener(OnNotifButtonClick);
            phase2button.onClick.AddListener(OnPhase2ButtonClick);

            if (phonePanel != null)
                phonePanel.SetActive(false);

            // Phase 1 startar på 0 og bygger til 0.75
            phase1Music.volume = 0f;
            phase1Music.Play();

            musicRoutine = StartCoroutine(StepVolume(phase1Music, 0.75f));
        }

        public void Update()
        {
            if (gameEnded)
                return;
            
            if (ScoreScript.Instance.phase2)
            {
                if (!phase2Music.isPlaying)
                {
                    if (musicRoutine != null)
                        StopCoroutine(musicRoutine);

                    // Rainfall går direkte til 0.75 og vidare til 1
                    phase1Music.volume = 0.75f;
                    musicRoutine = StartCoroutine(StepVolume(phase1Music, 1f));

                    // Alarm startar samtidig
                    phase2Music.volume = 1f;
                    phase2Music.Play();
                }
            }

            if (ScoreScript.Instance.phase2)
                transform.Find("Open Phone").GetComponent<Button>().interactable = false;
            return;
        }

        public void OnNotifButtonClick()
        {
            if (phonePanel != null)
                phonePanel.SetActive(true);
        }

        public void OnPhase2ButtonClick()
        {
            ScoreScript.Instance.phase2 = true;
            phase2button.gameObject.SetActive(false);
        }

        private IEnumerator StepVolume(AudioSource audioSource, float targetVolume)
        {
            while (audioSource.volume < targetVolume)
            {
                audioSource.volume += 0.005f;

                if (audioSource.volume > targetVolume)
                    audioSource.volume = targetVolume;

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}