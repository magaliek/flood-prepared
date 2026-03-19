using UnityEngine;

public class phoneoffscript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    
    public void open()
    {
        panel.SetActive(true);
    }
}
