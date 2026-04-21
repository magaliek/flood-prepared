using UnityEngine;
using UnityEngine.UI;
using score_system;

namespace persistentUI
{

public class PersistentUIManager : MonoBehaviour
{
    public Button notification;
    public GameObject phonePanel;

    public Button phase2button;

    [SerializeField] public AudioSource phase1Music;
    [SerializeField] public AudioSource phase2Music;

    public static PersistentUIManager Instance;

    public bool gameEnded = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        notification.onClick.AddListener(OnNotifButtonClick);
        phase2button.onClick.AddListener(OnPhase2ButtonClick);

        if (phonePanel != null)
            phonePanel.SetActive(false);

        phase1Music.Play();
    }

    public void Update()
    {
        if (gameEnded)
            return;
            
        if (ScoreScript.Instance.phase2)
        {
            if (!phase2Music.isPlaying)
            {
                phase2Music.Play();
            }
        }

        if (ScoreScript.Instance.phase2)
            transform.Find("Open Phone").GetComponent<Button>().interactable = false;
            return;
    }

        public void OnNotifButtonClick()
    {
        if (phonePanel != null)
            phonePanel.SetActive(true);
    }

    public void OnPhase2ButtonClick()
    {
        ScoreScript.Instance.phase2 = true;
        phase2button.gameObject.SetActive(false);
    }
}
}