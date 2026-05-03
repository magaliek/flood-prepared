using UnityEngine;

public class PowerState : MonoBehaviour
{
    public static PowerState Instance;

    public bool kitchenOff;
    public bool livingRoomOff;
    public bool bathroomOff;
    public bool bedroomOff;
    public bool hallwayOff;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}