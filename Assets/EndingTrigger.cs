using UnityEngine;
using score_system;
using System;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] public EndingScript endingScript;

    void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.CompareTag("Player") && ScoreScript.Instance.phase2)
            endingScript.TriggerEnding();
    }
}