using UnityEngine;
using UnityEngine.UI;
public class Circle : MonoBehaviour
{


    [SerializeField] private Image imageCircle;
    [SerializeField] private bool ticked = false;

    [SerializeField] private Sprite  NotTickedCircle;
    [SerializeField] private Sprite TickedCircle;
   
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
