using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] public EndingScript endingScript;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            endingScript.TriggerEnding();
    }
}