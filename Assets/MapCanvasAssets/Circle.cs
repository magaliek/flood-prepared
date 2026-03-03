using UnityEngine;
using UnityEngine.UI;
public class Circle : MonoBehaviour
{


    public Image imageCircle;
    public bool ticked = false;

    public Sprite  NotTickedCircle;
    public Sprite TickedCircle;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCircle = GetComponent<Image>();
        imageCircle.sprite = NotTickedCircle;
       


    }

    public void tickingCircle()
    {
        ticked = !ticked;
        if (ticked)
             imageCircle.sprite = TickedCircle;
        else
            imageCircle.sprite = NotTickedCircle;
            
        
    }

  
}
