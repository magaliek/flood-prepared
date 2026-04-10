using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuseBoxTask : MonoBehaviour
{
    [Header("UI Root")]
    [SerializeField] private GameObject taskPanel;

    [Header("Switches")]
    [SerializeField] private Button kitchenSwitch;
    [SerializeField] private Button livingRoomSwitch;
    [SerializeField] private Button bathroomSwitch;
    [SerializeField] private Button bedroomSwitch;
    [SerializeField] private Button closeButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text kitchenText;
    [SerializeField] private TMP_Text livingRoomText;
    [SerializeField] private TMP_Text bathroomText;
    [SerializeField] private TMP_Text bedroomText;
    [SerializeField] private TMP_Text progressText;

    private InteractableFuseBox currentFuseBox;

    private bool kitchenOff;
    private bool livingRoomOff;
    private bool bathroomOff;
    private bool bedroomOff;

    private void Start()
    {
        Debug.Log("FuseBoxTask Start");

        if (taskPanel) taskPanel.SetActive(false);
        else Debug.LogWarning("TaskPanel is missing!");

        if (kitchenSwitch) kitchenSwitch.onClick.AddListener(ToggleKitchen);
        if (livingRoomSwitch) livingRoomSwitch.onClick.AddListener(ToggleLivingRoom);
        if (bathroomSwitch) bathroomSwitch.onClick.AddListener(ToggleBathroom);
        if (bedroomSwitch) bedroomSwitch.onClick.AddListener(ToggleBedroom);
        if (closeButton) closeButton.onClick.AddListener(CancelTask);

        ResetTask();
    }

    public void Open(InteractableFuseBox fuseBox)
    {
        Debug.Log("FuseBoxTask.Open called");

        currentFuseBox = fuseBox;
        ResetTask();

        if (taskPanel)
        {
            Debug.Log("Showing task panel");
            taskPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("TaskPanel is null, cannot open UI");
        }
    }

    private void ToggleKitchen()
    {
        kitchenOff = !kitchenOff;
        UpdateUI();
        CheckCompletion();
    }

    private void ToggleLivingRoom()
    {
        livingRoomOff = !livingRoomOff;
        UpdateUI();
        CheckCompletion();
    }

    private void ToggleBathroom()
    {
        bathroomOff = !bathroomOff;
        UpdateUI();
        CheckCompletion();
    }

    private void ToggleBedroom()
    {
        bedroomOff = !bedroomOff;
        UpdateUI();
        CheckCompletion();
    }

    private void UpdateUI()
    {
        if (kitchenText) kitchenText.text = kitchenOff ? "Kitchen: OFF" : "Kitchen: ON";
        if (livingRoomText) livingRoomText.text = livingRoomOff ? "Living Room: OFF" : "Living Room: ON";
        if (bathroomText) bathroomText.text = bathroomOff ? "Bathroom: OFF" : "Bathroom: ON";
        if (bedroomText) bedroomText.text = bedroomOff ? "Bedroom: OFF" : "Bedroom: ON";

        int offCount = 0;
        if (kitchenOff) offCount++;
        if (livingRoomOff) offCount++;
        if (bathroomOff) offCount++;
        if (bedroomOff) offCount++;

        if (progressText)
            progressText.text = "Power off: " + offCount + "/4";
    }

    private void CheckCompletion()
    {
        if (kitchenOff && livingRoomOff && bathroomOff && bedroomOff)
        {
            if (taskPanel) taskPanel.SetActive(false);

            if (currentFuseBox)
                currentFuseBox.CompleteTask();
        }
    }

    public void CancelTask()
    {
        if (taskPanel) taskPanel.SetActive(false);

        if (currentFuseBox)
            currentFuseBox.CancelTask();
    }

    private void ResetTask()
    {
        kitchenOff = false;
        livingRoomOff = false;
        bathroomOff = false;
        bedroomOff = false;
        UpdateUI();
    }
}