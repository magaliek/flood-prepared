using UnityEngine;
using score_system;
using TMPro;
using System.Collections;
using persistentUI;
using System;

public class EndingScript : MonoBehaviour
{
    [SerializeField] private TimerUI TimerScript;
    [SerializeField] private TMP_Text EndingText;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private GameObject EndingPanel;
    private CanvasGroup fadeOverlay;

    [SerializeField] private AudioSource endingMusic;
    [SerializeField] private AudioSource alarmSound;

    void Start()
    {
        fadeOverlay = EndingPanel.GetComponent<CanvasGroup>();
        fadeOverlay.alpha = 0f;
    }

    IEnumerator FadeIn()
        {
            EndingPanel.SetActive(true);
            while (fadeOverlay.alpha < 1f)
            {
                fadeOverlay.alpha += Mathf.Clamp(Time.deltaTime * 2f, 0, 1);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            if (PersistentUIManager.Instance != null)
            {
                PersistentUIManager.Instance.phase1Music.Stop();
                PersistentUIManager.Instance.phase2Music.Stop();
                PersistentUIManager.Instance.phase2Music.Stop();
            }

            endingMusic.Play();
            if (alarmSound != null)
            {
                alarmSound.Stop();
            }
        }

    public void TriggerEnding()
    {
        PersistentUIManager.Instance.gameEnded = true;
        
        if (GameTimer.Instance != null && GameTimer.Instance.TimeLeft > 0f)
            ScoreScript.Instance.leftOnTime = true;

        Debug.Log($"GameTimer.Instance: {GameTimer.Instance}, TimeLeft: {(GameTimer.Instance != null ? GameTimer.Instance.TimeLeft.ToString() : "N/A")}");
        Debug.Log($"leftOnTime: {ScoreScript.Instance.leftOnTime}");

        EndingText.text = ScoreScript.Instance.GetEndingText();
        int totalScore = ScoreScript.Instance.TotalScore();
        ScoreText.text = "Your Score: " + totalScore.ToString();
        ScoreScript.Instance.SaveToSupabase();
        StartCoroutine(FadeIn());
    }
}
