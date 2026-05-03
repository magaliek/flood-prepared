using UnityEngine;
using TMPro;
using score_system;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private float warningThreshold = 60f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (GameTimer.Instance == null || timerText == null || ScoreScript.Instance == null || !ScoreScript.Instance.phase2) {
            return;
        }
        else {
        float time = GameTimer.Instance.TimeLeft;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
        timerText.color = time <= warningThreshold ? warningColor : normalColor;
        }
    }
}