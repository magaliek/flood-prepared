using UnityEngine;

public class FloodRiser : MonoBehaviour
{
    [Header("Flood Settings")]
    public float riseSpeed = 1f;       // kor fort vatnet veks
    public float maxHeight = 8f;       // maks høgd på vatnet
    public float startHeight = 1f;     // start-høgd

    private float fixedBottomY;

    void Start()
    {
        fixedBottomY = transform.position.y - (transform.localScale.y / 2f);

        Vector3 scale = transform.localScale;
        scale.y = startHeight;
        transform.localScale = scale;

        UpdateWaterPosition();
    }

    void Update()
    {
        if (transform.localScale.y < maxHeight)
        {
            Vector3 scale = transform.localScale;
            scale.y += riseSpeed * Time.deltaTime;
            scale.y = Mathf.Min(scale.y, maxHeight);
            transform.localScale = scale;

            UpdateWaterPosition();
        }
    }

    void UpdateWaterPosition()
    {
        float newY = fixedBottomY + transform.localScale.y / 2f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}