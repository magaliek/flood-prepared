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

    StopAllCoroutines();

    if (isOff)
        StartCoroutine(FadeIn());
    else
        StartCoroutine(FadeOut());
}

IEnumerator FadeIn()
{
    darkOverlay.SetActive(true); // enable first so it's visible to fade in
    SpriteRenderer sr = darkOverlay.GetComponent<SpriteRenderer>();
    float duration = 1f;
    float elapsed = 0f;
    Color initialColor = sr.color;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
        sr.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        yield return null;
    }
    sr.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
}

IEnumerator FadeOut()
{
    SpriteRenderer sr = darkOverlay.GetComponent<SpriteRenderer>();
    float duration = 1f;
    float elapsed = 0f;
    Color initialColor = sr.color;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        sr.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        yield return null;
    }
    sr.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    darkOverlay.SetActive(false); // disable AFTER fade completes, not before
}
}