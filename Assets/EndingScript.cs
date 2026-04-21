using UnityEngine;
using score_system;
using TMPro;
using System.Collections;
using persistentUI;

public class EndingScript : MonoBehaviour
{
    [SerializeField] private TimerUI TimerScript;
    [SerializeField] private TMP_Text EndingText;
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
            while (fadeOverlay.alpha < 1f)
            {
                fadeOverlay.alpha += Mathf.Clamp(Time.deltaTime * 2f, 0, 1);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            EndingPanel.SetActive(true);

            if (PersistentUIManager.Instance != null)
            {
                PersistentUIManager.Instance.phase1Music.Stop();
                PersistentUIManager.Instance.phase2Music.Stop();
                PersistentUIManager.Instance.phase2Music.Stop();
            }

            endingMusic.Play();
            alarmSound.Stop();
        }

    public void TriggerEnding()
    {
        PersistentUIManager.Instance.gameEnded = true;
        if (GameTimer.Instance != null && GameTimer.Instance.TimeLeft > 0f)
            ScoreScript.Instance.leftOnTime = true;

        EndingText.text = ScoreScript.Instance.GetEndingText();
        ScoreScript.Instance.TotalScore();
        ScoreScript.Instance.SaveToSupabase();
        StartCoroutine(FadeIn());
    }
}
