using UnityEngine;

public class openUserNameScript : MonoBehaviour
{


    [SerializeField] private GameObject userNamePanel;
    [SerializeField] private GameObject gameInstrcutionsPanel;
    [SerializeField] private GameObject bPreparedPanel;

    public void showUserName()
    {
        userNamePanel.SetActive(true);
    }
    public void showGameInstructions()
    {
        gameInstrcutionsPanel.SetActive(true);
    }
     public void showbPrepared()
    {
        bPreparedPanel.SetActive(true);
    }
    
  
}
