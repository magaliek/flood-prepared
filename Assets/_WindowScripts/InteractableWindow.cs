using UnityEngine;

public class InteractableWindow : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private GameObject openVisual;
    [SerializeField] private GameObject closedVisual;
    [SerializeField] private GameObject sealVisual;     // optional overlay
    [SerializeField] private GameObject sealedVisual;   // final sealed sprite

    [Header("Seal")]
    [SerializeField] private float sealTime = 2f;

    [Header("UI")]
    [SerializeField] private InteractPromptUI promptUI;
    [SerializeField] private SealProgressUI sealUI;

    private bool playerInRange;
    private bool isClosed;
    private bool isSealed;
    private float sealProgress;

    // Prevents instant sealing when closing
    private bool requireReleaseAfterClose = false;

    private void Start()
    {
        isClosed = false;
        isSealed = false;
        sealProgress = 0f;

        if (openVisual) openVisual.SetActive(true);
        if (closedVisual) closedVisual.SetActive(false);
        if (sealVisual) sealVisual.SetActive(false);
        if (sealedVisual) sealedVisual.SetActive(false);

        if (promptUI) promptUI.Hide();

        if (sealUI)
        {
            sealUI.Hide();
            sealUI.SetProgress01(0f);
        }
    }

    private void Update()
    {
        if (!playerInRange)
            return;

        // Wait until player releases E after closing
        if (requireReleaseAfterClose && Input.GetKeyUp(KeyCode.E))
        {
            requireReleaseAfterClose = false;
        }

        UpdatePrompt();

        // --- Close window ---
        if (!isClosed && Input.GetKeyDown(KeyCode.E))
        {
            CloseWindow();
            requireReleaseAfterClose = true;
            return;
        }

        // --- Seal window ---
        if (isClosed && !isSealed && !requireReleaseAfterClose)
        {
            if (Input.GetKey(KeyCode.E))
            {
                SealTick(Time.deltaTime);
            }
            else
            {
                ResetSealProgress();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                ResetSealProgress();
            }
        }
    }

    private void UpdatePrompt()
    {
        if (!promptUI) return;

        if (!isClosed)
            promptUI.Show("Press E to close window");
        else if (!isSealed)
            promptUI.Show("Hold E to seal");
        else
            promptUI.Show("Window sealed");
    }

    private void CloseWindow()
    {
        isClosed = true;

        if (openVisual) openVisual.SetActive(false);
        if (closedVisual) closedVisual.SetActive(true);
    }

    private void SealTick(float dt)
    {
        sealProgress += dt;

        float t01 = Mathf.Clamp01(sealProgress / Mathf.Max(0.0001f, sealTime));

        if (sealUI)
        {
            sealUI.Show();
            sealUI.SetProgress01(t01);
        }

        if (sealProgress >= sealTime)
        {
            isSealed = true;

            // Switch visuals to sealed state
            if (closedVisual) closedVisual.SetActive(false);
            if (sealVisual) sealVisual.SetActive(false);
            if (sealedVisual) sealedVisual.SetActive(true);

            if (sealUI)
            {
                sealUI.SetProgress01(1f);
                // Optional: hide after success
                // sealUI.Hide();
            }
        }
    }

    private void ResetSealProgress()
    {
        if (isSealed) return;

        sealProgress = 0f;

        if (sealUI)
        {
            sealUI.SetProgress01(0f);
            sealUI.Hide();
        }
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
        sealProgress = 0f;
        requireReleaseAfterClose = false;

        if (promptUI) promptUI.Hide();

        if (sealUI)
        {
            sealUI.SetProgress01(0f);
            sealUI.Hide();
        }
    }
}