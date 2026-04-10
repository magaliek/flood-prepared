using TMPro;
using UnityEngine;

public class SealMinigame : MonoBehaviour
{
    [SerializeField] private SealCell[] cells;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private GameObject panelToHide;

    private InteractableWindow targetWindow;

    private int filledCount;
    [SerializeField] private int required = 9;

    private bool plasticSelected;
    public bool PlasticSelected => plasticSelected;

    public bool IsDrawing { get; private set; }

    private void Awake()
    {
        if (panelToHide == null)
            panelToHide = gameObject;

        panelToHide.SetActive(false);
    }

    private void Update()
    {
        IsDrawing = Input.GetMouseButton(0);
    }

    public void Open(InteractableWindow window)
    {
        targetWindow = window;
        filledCount = 0;
        plasticSelected = false;

        foreach (var cell in cells)
        {
            if (cell != null)
                cell.ResetCell();
        }

        UpdateText();

        if (instructionText != null)
            instructionText.text = "Step 1: Select plastic covering";

        panelToHide.SetActive(true);
    }

    public void SelectPlastic()
    {
        plasticSelected = true;

        if (instructionText != null)
            instructionText.text = "Step 2: Drag across the window to seal all sections";
    }

    public void Close()
    {
        panelToHide.SetActive(false);

        if (targetWindow != null)
            targetWindow.CancelSealMinigame();
    }

    public void NotifyCellFilled()
    {
        filledCount++;
        UpdateText();

        if (filledCount >= required)
        {
            if (targetWindow != null)
                targetWindow.CompleteSeal();

            if (instructionText != null)
                instructionText.text = "Seal complete";

            if (progressText != null)
                progressText.text = "Seal done";

            panelToHide.SetActive(false);
        }
    }

    private void UpdateText()
    {
        if (progressText != null)
            progressText.text = $"Coverage: {filledCount}/{required}";
    }

    public void CancelMinigame()
    {
        Close();
    }
}