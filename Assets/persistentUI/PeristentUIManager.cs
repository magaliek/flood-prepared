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

    public string[] tasksToDisable = {"backpack", "studytable", "DeskDrawer", "bookdrawer", "window"};

    public string[] tasksToEnable = {"WaterValve", "FuseBox"};

    private static PersistentUIManager _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        notification.onClick.AddListener(OnButton1Clicked);
        phase2button.onClick.AddListener(OnButton2Clicked);

        if (phonePanel != null)
            phonePanel.SetActive(false);
    }

    public void OnButton1Clicked()
    {
        if (phonePanel != null)
            phonePanel.SetActive(true);  

    }

    public void OnButton2Clicked()
    {
        ScoreScript.Instance.phase2 = true;
        phase2button.gameObject.SetActive(false);
    }
}
}