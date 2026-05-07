using UnityEngine;
using score_system;

namespace _Scripts
{
    public class GameTimer : MonoBehaviour
    {
        public static GameTimer Instance;

        [Header("Timer")]
        [SerializeField] private float totalTimeSeconds = 300f;

        public float TimeLeft { get; private set; }
        public bool FloodStarted { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                transform.SetParent(null);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            TimeLeft = totalTimeSeconds;
        }

        private void Update()
        {
            if (FloodStarted || !ScoreScript.Instance.phase2)
                return;

            if (TimeLeft > 0f)
            {
                TimeLeft -= Time.deltaTime;

                if (TimeLeft <= 0f)
                {
                    TimeLeft = 0f;
                    StartFlood();
                }
            }
        }

        public void StartFlood()
        {
            if (FloodStarted || !ScoreScript.Instance.phase2)
                return;

            FloodStarted = true;
            //Floood audio
            FloodRise flood = FindFirstObjectByType<FloodRise>();
            if (flood != null)
                flood.StartFlood();
        }
    }
}