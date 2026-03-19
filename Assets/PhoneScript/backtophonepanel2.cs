using UnityEngine;

public class backtophonepanel2 : MonoBehaviour
{
   [SerializeField] private GameObject panel;
    
    
    public void open()
    {
        panel.SetActive(true);
    }
}
