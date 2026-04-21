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
    [SerializeField] private Button hallwaySwitch;
    [SerializeField] private Button closeButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text kitchenText;
    [SerializeField] private TMP_Text livingRoomText;
    [SerializeField] private TMP_Text bathroomText;
    [SerializeField] private TMP_Text bedroomText;
    [SerializeField] private TMP_Text hallwayText;
    [SerializeField] private TMP_Text progressText;

    private InteractableFuseBox currentFuseBox;

    private bool kitchenOff;
    private bool livingRoomOff;
    private bool bathroomOff;
    private bool bedroomOff;
    private bool hallwayOff;

    private void Start()
    {
        if (taskPanel) taskPanel.SetActive(false);

        if (kitchenSwitch) kitchenSwitch.onClick.AddListener(ToggleKitchen);
        if (livingRoomSwitch) livingRoomSwitch.onClick.AddListener(ToggleLivingRoom);
        if (bathroomSwitch) bathroomSwitch.onClick.AddListener(ToggleBathroom);
        if (bedroomSwitch) bedroomSwitch.onClick.AddListener(ToggleBedroom);
        if (hallwaySwitch) hallwaySwitch.onClick.AddListener(ToggleHallway);
        if (closeButton) closeButton.onClick.AddListener(CancelTask);

        ResetTask();
    }

    public void Open(InteractableFuseBox fuseBox)
    {
        currentFuseBox = fuseBox;
        LoadFromPowerState();

        if (taskPanel)
            taskPanel.SetActive(true);
    }

    private void ToggleKitchen()
    {
        kitchenOff = !kitchenOff;
        if (PowerState.Instance != null) PowerState.Instance.kitchenOff = kitchenOff;
        UpdateUI();
        RefreshCurrentSceneDimming();
        CheckCompletion();
    }

    private void ToggleLivingRoom()
    {
        livingRoomOff = !livingRoomOff;
        if (PowerState.Instance != null) PowerState.Instance.livingRoomOff = livingRoomOff;
        UpdateUI();
        RefreshCurrentSceneDimming();
        CheckCompletion();
    }

    private void ToggleBathroom()
    {
        bathroomOff = !bathroomOff;
        if (PowerState.Instance != null) PowerState.Instance.bathroomOff = bathroomOff;
        UpdateUI();
        RefreshCurrentSceneDimming();
        CheckCompletion();
    }

    private void ToggleBedroom()
    {
        bedroomOff = !bedroomOff;
        if (PowerState.Instance != null) PowerState.Instance.bedroomOff = bedroomOff;
        UpdateUI();
        RefreshCurrentSceneDimming();
        CheckCompletion();
    }

    private void ToggleHallway()
    {
        hallwayOff = !hallwayOff;
        if (PowerState.Instance != null) PowerState.Instance.hallwayOff = hallwayOff;
        UpdateUI();
        RefreshCurrentSceneDimming();
        CheckCompletion();
    }

    private void UpdateUI()
    {
        if (kitchenText) kitchenText.text = kitchenOff ? "Kitchen: OFF" : "Kitchen: ON";
        if (livingRoomText) livingRoomText.text = livingRoomOff ? "Living Room: OFF" : "Living Room: ON";
        if (bathroomText) bathroomText.text = bathroomOff ? "Bathroom: OFF" : "Bathroom: ON";
        if (bedroomText) bedroomText.text = bedroomOff ? "Bedroom: OFF" : "Bedroom: ON";
        if (hallwayText) hallwayText.text = hallwayOff ? "Hallway: OFF" : "Hallway: ON";

        int offCount = 0;
        if (kitchenOff) offCount++;
        if (livingRoomOff) offCount++;
        if (bathroomOff) offCount++;
        if (bedroomOff) offCount++;
        if (hallwayOff) offCount++;

        if (progressText)
            progressText.text = "Power off: " + offCount + "/5";
    }

    private void CheckCompletion()
    {
        if (kitchenOff && livingRoomOff && bathroomOff && bedroomOff && hallwayOff)
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
        hallwayOff = false;
        UpdateUI();
    }

    private void LoadFromPowerState()
    {
        if (PowerState.Instance != null)
        {
            kitchenOff = PowerState.Instance.kitchenOff;
            livingRoomOff = PowerState.Instance.livingRoomOff;
            bathroomOff = PowerState.Instance.bathroomOff;
            bedroomOff = PowerState.Instance.bedroomOff;
            hallwayOff = PowerState.Instance.hallwayOff;
        }

        UpdateUI();
    }

    private void RefreshCurrentSceneDimming()
    {
        RoomDimmer dimmer = FindFirstObjectByType<RoomDimmer>();
        if (dimmer != null)
            dimmer.ApplyState();
    }
}