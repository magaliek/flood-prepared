using UnityEngine;
using score_system;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] public EndingScript endingScript;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;

    void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.CompareTag("Player") && ScoreScript.Instance.phase2)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            endingScript.TriggerEnding();
        }
    }
}