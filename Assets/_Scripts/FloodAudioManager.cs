/**
using UnityEngine;

public class FloodAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource rainSource;
    [SerializeField] private AudioSource thunderSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip rainClip;
    [SerializeField] private AudioClip stormClip;

    [Header("Rain Volume Over Time")]
    [SerializeField, Range(0f, 1f)] private float startRainVolume = 0.2f;
    [SerializeField, Range(0f, 1f)] private float midRainVolume = 0.5f;
    [SerializeField, Range(0f, 1f)] private float maxRainVolume = 0.9f;

    [SerializeField] private float phase1Duration = 60f;
    [SerializeField] private float phase2Duration = 60f;

    [Header("Thunder Settings")]
    [SerializeField] private float thunderStartsWhenTimeLeftIs = 30f;
    [SerializeField] private float thunderInterval = 8f;
    [SerializeField, Range(0f, 1f)] private float thunderVolume = 0.5f;

    private float playTime;
    private float thunderTimer;
    private bool audioStarted;
    private bool thunderEnabled;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (rainSource != null)
        {
            rainSource.playOnAwake = false;
            rainSource.loop = true;
        }

        if (thunderSource != null)
        {
            thunderSource.playOnAwake = false;
            thunderSource.loop = false;
        }
    }

    private void Update()
    {
        if (!audioStarted || rainSource == null || !rainSource.isPlaying)
            return;

        playTime += Time.deltaTime;
        UpdateRainVolume();

        if (thunderEnabled)
        {
            thunderTimer -= Time.deltaTime;

            if (thunderTimer <= 0f)
            {
                PlayThunder();
                thunderTimer = thunderInterval;
            }
        }
    }

    private void UpdateRainVolume()
    {
        if (playTime <= phase1Duration)
        {
            float t = playTime / phase1Duration;
            rainSource.volume = Mathf.Lerp(startRainVolume, midRainVolume, t);
        }
        else if (playTime <= phase1Duration + phase2Duration)
        {
            float t = (playTime - phase1Duration) / phase2Duration;
            rainSource.volume = Mathf.Lerp(midRainVolume, maxRainVolume, t);
        }
        else
        {
            rainSource.volume = maxRainVolume;
        }
    }

    public void PlayFloodAudio()
    {
        if (rainSource == null)
        {
            Debug.LogWarning("FloodAudioManager: Rain Source mangler.");
            return;
        }

        if (rainClip == null)
        {
            Debug.LogWarning("FloodAudioManager: Rain Clip mangler.");
            return;
        }

        rainSource.clip = rainClip;
        rainSource.volume = startRainVolume;
        rainSource.loop = true;

        if (!rainSource.isPlaying)
            rainSource.Play();

        audioStarted = true;
        playTime = 0f;
        thunderTimer = thunderInterval;
        thunderEnabled = false;

        Debug.Log("FloodAudioManager: Regn startet.");
    }

    public void UpdateTimer(float timeLeft)
    {
        if (!audioStarted)
            return;

        if (timeLeft <= thunderStartsWhenTimeLeftIs)
            thunderEnabled = true;
    }

    private void PlayThunder()
    {
        if (stormClip == null)
            return;

        if (thunderSource != null)
        {
            thunderSource.PlayOneShot(stormClip, thunderVolume);
        }
        else if (rainSource != null)
        {
            rainSource.PlayOneShot(stormClip, thunderVolume);
        }
    }
    private void Start()
{
    PlayFloodAudio();
}
}
**/