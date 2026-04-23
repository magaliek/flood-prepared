using UnityEngine;
using score_system;

public class InteractableFuseBox : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private InteractPromptUI promptUI;

    [Header("Task")]
    [SerializeField] private FuseBoxTask fuseBoxTask;

    private bool playerInRange;
    private bool isPowerOff;
    private bool taskOpen;

    private void Start()
    {
        if (promptUI) promptUI.Hide();
        else Debug.LogWarning("PromptUI is missing!");

        if (fuseBoxTask == null)
            Debug.LogWarning("FuseBoxTask is missing!");
    }

    private void Update()
    {
        if (!playerInRange || !ScoreScript.Instance.phase2)
            return;

        if (!isPowerOff)
        {
            if (promptUI) promptUI.Show("Press Enter to turn off power");
        }
        else
        {
            if (promptUI) promptUI.Show("Power disconnected");
        }

        if (!isPowerOff && !taskOpen &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (fuseBoxTask)
            {
                taskOpen = true;
                fuseBoxTask.Open(this);
            }
            else
            {
                Debug.LogWarning("FuseBoxTask reference is null");
            }
        }
    }

    public void CompleteTask()
    {
        isPowerOff = true;
        taskOpen = false;

        if (promptUI)
            promptUI.Show("Power disconnected");
        
        score_system.ScoreScript.Instance.fuseboxDone = true;
    }

    public void CancelTask()
    {
        taskOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (promptUI) promptUI.Hide();
    }
}