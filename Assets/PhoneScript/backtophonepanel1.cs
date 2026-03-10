using UnityEngine;

public class backtophonepanel1 : MonoBehaviour
{
    
     [SerializeField] private GameObject panel;
    
    
    public void open()
    {
        panel.SetActive(true);
    }
}
