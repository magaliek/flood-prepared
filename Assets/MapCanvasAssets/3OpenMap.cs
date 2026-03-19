using UnityEngine;

public class OpenMap : MonoBehaviour
{
    
    [SerializeField] private GameObject MapPanel;
    
    
    public void ShowMap()
    {
        MapPanel.SetActive(true);
    }
}
