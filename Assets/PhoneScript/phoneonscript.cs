using UnityEngine;

public class phoneonscript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    
    public void open()
    {
        panel.SetActive(true);
    }
}
