using UnityEngine;
using UnityEngine.UI;
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
    }
}
