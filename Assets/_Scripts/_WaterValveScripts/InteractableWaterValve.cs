using UnityEngine;
using score_system;
using UnityEngine.SocialPlatforms.Impl;

public class InteractableWaterValve : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private InteractPromptUI promptUI;

    [Header("Task")]
    [SerializeField] private WaterValveTask valveTask;

    private bool playerInRange;
    private bool isDone;
    private bool taskOpen;

    private void Start()
    {
        if (promptUI) promptUI.Hide();
        else Debug.LogWarning("PromptUI is missing!");

        if (valveTask == null)
            Debug.LogWarning("FuseBoxTask is missing!");

        if (ScoreScript.Instance != null && ScoreScript.Instance.fuseboxDone)
        {
            isDone = true;
            if (promptUI) promptUI.Show("Water shut off");
        }
    }

    private void Update()
    {
        if (!playerInRange || !ScoreScript.Instance.phase2)
            return;

        UpdatePrompt();

        if (!isDone && !taskOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (valveTask)
            {
                taskOpen = true;
                valveTask.Open(this);
            }
        }
    }

    private void UpdatePrompt()
    {
        if (!promptUI) return;

        if (!isDone)
            promptUI.Show("Press Enter to shut off water");
        else if (!ScoreScript.Instance.phase2) {promptUI.Show("");}
        else
        {
            promptUI.Show("Water shut off");
        }
    }

    public void CompleteTask()
    {
        isDone = true;
        taskOpen = false;

        if (promptUI)
            promptUI.Show("Water shut off");
        ScoreScript.Instance.valveDone = isDone;
    }

    public void CancelTask()
    {
        taskOpen = false;
        UpdatePrompt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (ScoreScript.Instance.phase2)
            UpdatePrompt();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (promptUI) promptUI.Hide();
    }
}