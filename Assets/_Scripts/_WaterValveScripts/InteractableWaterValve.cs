using UnityEngine;

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
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        UpdatePrompt();

        if (!isDone && !taskOpen && Input.GetKeyDown(KeyCode.E))
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
            promptUI.Show("Press E to shut off water");
        else
            promptUI.Show("Water shut off");
    }

    public void CompleteTask()
    {
        isDone = true;
        taskOpen = false;

        if (promptUI)
            promptUI.Show("Water shut off");
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
        UpdatePrompt();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (promptUI) promptUI.Hide();
    }
}