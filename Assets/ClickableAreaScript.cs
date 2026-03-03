using UnityEngine;

public class ClickableAreaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mapPanel;

    public void ShowMap()
    {
        mapPanel.SetActive(true);
    }

    // Update is called once per frame
}
