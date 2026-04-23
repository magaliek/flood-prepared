using System.Collections;
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
    if (darkOverlay == null || PowerState.Instance == null) return;

    bool isOff = false;
    switch (roomType)
    {
        case RoomType.Kitchen:    isOff = PowerState.Instance.kitchenOff;    break;
        case RoomType.LivingRoom: isOff = PowerState.Instance.livingRoomOff; break;
        case RoomType.Bathroom:   isOff = PowerState.Instance.bathroomOff;   break;
        case RoomType.Bedroom:    isOff = PowerState.Instance.bedroomOff;    break;
        case RoomType.Hallway:    isOff = PowerState.Instance.hallwayOff;    break;
    }

    bool allOff = PowerState.Instance.kitchenOff &&
                  PowerState.Instance.livingRoomOff &&
                  PowerState.Instance.bathroomOff &&
                  PowerState.Instance.bedroomOff &&
                  PowerState.Instance.hallwayOff;

    StopAllCoroutines();

    if (isOff && allOff)
        StartCoroutine(FadeIn());
    else if (!isOff)
        StartCoroutine(FadeOut());
}

IEnumerator FadeIn()
{
    darkOverlay.SetActive(true);
    SpriteRenderer sr = darkOverlay.GetComponent<SpriteRenderer>();
    float duration = 1f;
    float elapsed = 0f;
    float startAlpha = sr.color.a;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(startAlpha, 0.6f, elapsed / duration);
        sr.color = new Color(0f, 0f, 0f, alpha);
        yield return null;
    }
    sr.color = new Color(0f, 0f, 0f, 0.6f);
}

IEnumerator FadeOut()
{
    SpriteRenderer sr = darkOverlay.GetComponent<SpriteRenderer>();
    float duration = 1f;
    float elapsed = 0f;
    float startAlpha = sr.color.a;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
        sr.color = new Color(0f, 0f, 0f, alpha);
        yield return null;
    }
    sr.color = new Color(0f, 0f, 0f, 0f);
    darkOverlay.SetActive(false);
}
}