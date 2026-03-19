using UnityEngine;

public class backtotablepanel : MonoBehaviour
{
   [SerializeField] private GameObject panel;
    
    
    public void open()
    {
        panel.SetActive(true);
    }
}
