using UnityEngine;

public class OpenMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject MapPanel;
    
    // Update is called once per frame
    public void ShowMap()
    {
        MapPanel.SetActive(true);
    }
}
