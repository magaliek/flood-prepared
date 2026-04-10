using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaterValveTask : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Button closeButton;

    [Header("Valve")]
    [SerializeField] private RectTransform valvePivot;
    [SerializeField] private float targetRotation = 90f;
    [SerializeField] private float turnSpeed = 45f;
    [SerializeField] private bool rotateClockwise = true;

    [Header("Text")]
    [SerializeField] private string defaultTitle = "Shut Off Water";
    [SerializeField] private string defaultInstruction = "Hold E to turn valve";
    [SerializeField] private string completeText = "Water shut off";

    private InteractableWaterValve currentValve;
    private float currentRotation;
    private bool isCompleted;

    private void Start()
    {
        if (taskPanel)
            taskPanel.SetActive(false);

        if (closeButton)
            closeButton.onClick.AddListener(CancelTask);

        SetupStaticText();
        ResetTaskState();
    }

    public void Open(InteractableWaterValve valve)
    {
        currentValve = valve;
        isCompleted = false;

        SetupStaticText();
        ResetTaskState();

        if (taskPanel)
            taskPanel.SetActive(true);
    }

    private void Update()
    {
        if (!taskPanel || !taskPanel.activeSelf || isCompleted)
            return;

        if (Input.GetKey(KeyCode.E))
        {
            RotateValve();
        }
    }

    private void RotateValve()
    {
        currentRotation += turnSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation, 0f, targetRotation);

        if (valvePivot)
        {
            float zRotation = rotateClockwise ? -currentRotation : currentRotation;
            valvePivot.localRotation = Quaternion.Euler(0f, 0f, zRotation);
        }

        UpdateProgressText();

        if (currentRotation >= targetRotation)
        {
            CompleteTask();
        }
    }

    private void CompleteTask()
    {
        isCompleted = true;

        if (progressText)
            progressText.text = completeText;

        if (taskPanel)
            taskPanel.SetActive(false);

        if (currentValve)
            currentValve.CompleteTask();
    }

    public void CancelTask()
    {
        if (taskPanel)
            taskPanel.SetActive(false);

        if (currentValve)
            currentValve.CancelTask();
    }

    private void ResetTaskState()
    {
        currentRotation = 0f;

        if (valvePivot)
            valvePivot.localRotation = Quaternion.identity;

        UpdateProgressText();
    }

    private void UpdateProgressText()
    {
        if (!progressText)
            return;

        int percent = Mathf.RoundToInt((currentRotation / targetRotation) * 100f);
        progressText.text = "Turning: " + percent + "%";
    }

    private void SetupStaticText()
    {
        if (titleText)
            titleText.text = defaultTitle;

        if (instructionText)
            instructionText.text = defaultInstruction;
    }
}