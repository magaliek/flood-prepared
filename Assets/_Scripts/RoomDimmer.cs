using UnityEngine;

public class RoomDimmer : MonoBehaviour
{
    public enum RoomType
    {
        Kitchen,
        LivingRoom,
        Bathroom,
        Bedroom,
        Hallway
    }

    [SerializeField] private GameObject darkOverlay;
    [SerializeField] private RoomType roomType;

    private void Start()
    {
        ApplyState();
    }

    public void ApplyState()
    {
        if (darkOverlay == null || PowerState.Instance == null)
            return;

        bool isOff = false;

        switch (roomType)
        {
            case RoomType.Kitchen:
                isOff = PowerState.Instance.kitchenOff;
                break;
            case RoomType.LivingRoom:
                isOff = PowerState.Instance.livingRoomOff;
                break;
            case RoomType.Bathroom:
                isOff = PowerState.Instance.bathroomOff;
                break;
            case RoomType.Bedroom:
                isOff = PowerState.Instance.bedroomOff;
                break;
            case RoomType.Hallway:
                isOff = PowerState.Instance.hallwayOff;
                break;
        }

        darkOverlay.SetActive(isOff);
    }
}