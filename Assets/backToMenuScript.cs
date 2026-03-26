using UnityEngine;

public class backToMenuScript : MonoBehaviour
{
   [SerializeField] private GameObject panel;
    public void open()
    {
        panel.SetActive(true);
    }    
}
