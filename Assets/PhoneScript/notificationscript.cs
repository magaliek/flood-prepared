using UnityEngine;
using UnityEngine.UI;
using score_system;
using Unity.VisualScripting;
public class notificationscript : MonoBehaviour
{
    [SerializeField] private Image imageApp;
    [SerializeField] private bool notificationOn = false;
    [SerializeField] private Sprite notifOff;
    [SerializeField] private Sprite notifOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageApp = GetComponent<Image>();
        imageApp.sprite = notifOff;

    }
    
     public void notification()
    {
        notificationOn = !notificationOn;

        if (notificationOn)
            imageApp.sprite = notifOn;
        else
          imageApp.sprite = notifOff;

        if (notificationOn)
        {
            ScoreScript.Instance.notificationsDone = true;
        }
        else
        {
            ScoreScript.Instance.notificationsDone = false;
        }
    }
}
