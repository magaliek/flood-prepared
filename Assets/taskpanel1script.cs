using UnityEngine;
using UnityEngine.UI;
using score_system;
public class taskpanel1script : MonoBehaviour
{

     
    public Toggle togglewindow;
    public Toggle toggledrawer;
    public Toggle togglemap;
    public Toggle togglemobile;
    public Toggle togglebackpack;
    public Toggle togglenotebook;
    
    
    public GameObject panel1;  
    public GameObject panel2; 
    // Update is called once per frame
    void Update()
    {
       togglewindow.isOn=ScoreScript.Instance.windowDone;
       toggledrawer.isOn=ScoreScript.Instance.drawerDone;
       togglemap.isOn=ScoreScript.Instance.mapDone;
       togglemobile.isOn=ScoreScript.Instance.notificationsDone;
       togglebackpack.isOn=ScoreScript.Instance.packingDone;
       togglenotebook.isOn=ScoreScript.Instance.notebookDone;

       if (ScoreScript.Instance.phase2)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
        }



    }

}
