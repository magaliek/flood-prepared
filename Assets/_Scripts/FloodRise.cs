using UnityEngine;

public class FloodRise : MonoBehaviour
{
    [SerializeField] private Transform water;

    [Header("Speed")]
    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float ySpeed = 1f;

    [Header("Limits")]
    [SerializeField] private float maxWidth = 36f;
    [SerializeField] private float maxHeight = 3f;

    [Header("Random Spread")]
    [SerializeField] private float horizontalBiasStrength = 0.5f;

    [Header("Wobble (optional)")]
    [SerializeField] private bool enableWobble = true;
    [SerializeField] private float wobbleSpeed = 2f;
    [SerializeField] private float wobbleAmount = 0.01f;

    private bool flooding;
    private float bias;

    private void Start()
    {
        bias = Random.Range(-1f, 1f);

        if (GameTimer.Instance != null && GameTimer.Instance.FloodStarted)
            StartFlood();
    }

    public void StartFlood()
    {
        flooding = true;
    }

    private void Update()
    {
        if (!flooding || water == null) return;

        Vector3 scale = water.localScale;
        Vector3 pos = water.localPosition;

        if (scale.x < maxWidth)
        {
            float deltaX = xSpeed * Time.deltaTime;
            scale.x = Mathf.Min(scale.x + deltaX, maxWidth);
            pos.x += bias * horizontalBiasStrength * deltaX;
        }

        if (scale.y < maxHeight)
        {
            scale.y = Mathf.Min(scale.y + ySpeed * Time.deltaTime, maxHeight);
        }

        if (enableWobble)
        {
            pos.x += Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        }

        water.localScale = scale;
        water.localPosition = pos;
    }
}