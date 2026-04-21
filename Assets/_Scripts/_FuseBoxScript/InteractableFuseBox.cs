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
        Debug.Log("InteractableFuseBox Start");

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
            Debug.Log("Enter pressed while in fuse box range");

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
        Debug.Log("Fuse box task completed");
        isPowerOff = true;
        taskOpen = false;

        if (promptUI)
            promptUI.Show("Power disconnected");
        
        score_system.ScoreScript.Instance.fuseboxDone = true;
    }

    public void CancelTask()
    {
        Debug.Log("Fuse box task cancelled");
        taskOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        if (!other.CompareTag("Player")) return;

        Debug.Log("Player entered fuse box range");
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Something exited trigger: " + other.name);

        if (!other.CompareTag("Player")) return;

        Debug.Log("Player left fuse box range");
        playerInRange = false;

        if (promptUI) promptUI.Hide();
    }
}