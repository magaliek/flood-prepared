using UnityEngine;
using score_system;

public class InteractableWindow : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private GameObject openVisual;
    [SerializeField] private GameObject closedVisual;
    [SerializeField] private GameObject sealedVisual;

    [Header("UI")]
    [SerializeField] private InteractPromptUI promptUI;

    [Header("Minigame")]
    [SerializeField] private SealMinigame sealMinigame;

    private bool playerInRange;
    private bool isClosed;
    private bool isSealed;
    private bool minigameOpen;

    private void Start()
    {
        if (openVisual) openVisual.SetActive(true);
        if (closedVisual) closedVisual.SetActive(false);
        if (sealedVisual) sealedVisual.SetActive(false);

        if (promptUI) promptUI.Hide();
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        UpdatePrompt();

        if (!isClosed && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            CloseWindow();
            return;
        }

        if (isClosed && !isSealed && !minigameOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (sealMinigame)
            {
                minigameOpen = true;
                sealMinigame.Open(this);
            }
        }
    }

    void UpdatePrompt()
    {
        if (!promptUI) return;

        if (!isClosed)
            promptUI.Show("Press Enter to close window");
        else if (!isSealed)
            promptUI.Show("Press Enter to seal window");
        else
            promptUI.Show("Window sealed");
    }

    void CloseWindow()
    {
        isClosed = true;

        if (openVisual) openVisual.SetActive(false);
        if (closedVisual) closedVisual.SetActive(true);
    }

    public void CompleteSeal()
    {
        isSealed = true;
        minigameOpen = false;

        if (closedVisual) closedVisual.SetActive(false);
        if (sealedVisual) sealedVisual.SetActive(true);

        if (promptUI) promptUI.Show("Window sealed");
        ScoreScript.Instance.windowDone = true;
    }

    public void CancelSealMinigame()
    {
        minigameOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ScoreScript.Instance.phase2) return;
        if (!other.CompareTag("Player") || ScoreScript.Instance.windowDone) return;

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