using UnityEngine;
using score_system;

public class doneScriptMap : MonoBehaviour
{

    [SerializeField] private GameObject mapPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void open()
    {
        mapPanel.SetActive(true);
    }

    // Update is called once per frame
    public void onDoneClick()
    {
        mapPanel.SetActive(false);
        ScoreScript.Instance.mapDone = true;
    }
}
