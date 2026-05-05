using UnityEngine;
using UnityEngine.UI;
using score_system;

public class taskpanel2script : MonoBehaviour

{


    public Toggle togglewater;
    public Toggle togglefusebox;
    public Toggle togglepickupbackpack;
    

    public GameObject panel2; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         panel2.SetActive(false);
    }

    // Update is called once per frame
   void Update()
    {
       togglewater.isOn=ScoreScript.Instance.valveDone;
       togglefusebox.isOn=ScoreScript.Instance.fuseboxDone;
       togglepickupbackpack.isOn=ScoreScript.Instance.tookBackpack;
       


    }
}
