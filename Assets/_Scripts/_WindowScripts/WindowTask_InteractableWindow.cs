using UnityEngine;

public class WindowTask_InteractableWindow : WindowInteractable
{
    [Header("Visuals")]
    [SerializeField] private GameObject openVisual;
    [SerializeField] private GameObject closedVisual;
    [SerializeField] private GameObject sealVisual;

    [Header("Seal")]
    [SerializeField] private float sealTime = 2f;

    private bool isClosed;
    private bool isSealed;
    private float sealProgress;

    private void Start()
    {
        if (openVisual) openVisual.SetActive(true);
        if (closedVisual) closedVisual.SetActive(false);
        if (sealVisual) sealVisual.SetActive(false);
    }

    public override string GetPrompt()
    {
        if (!isClosed) return "Close window (E)";
        if (!isSealed) return "Hold E to seal";
        return "Window sealed";
    }

    public override void Interact()
    {
        if (isClosed) return;

        isClosed = true;
        if (openVisual) openVisual.SetActive(false);
        if (closedVisual) closedVisual.SetActive(true);
    }

    public override void HoldInteract(float dt)
    {
        if (!isClosed || isSealed) return;

        sealProgress += dt;
        if (sealProgress >= sealTime)
        {
            isSealed = true;
            if (sealVisual) sealVisual.SetActive(true);
        }
    }

    public override void ResetHold()
    {
        if (!isSealed) sealProgress = 0f;
    }
}