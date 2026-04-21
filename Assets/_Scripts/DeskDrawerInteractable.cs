using UnityEngine;
using score_system;

public class DeskDrawerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject drawerTaskUI;

    public string PromptText => "Interact (E)";

    public void Interact()
    {
        if (ScoreScript.Instance.phase2) return;
        Debug.Log("DeskDrawer Interact called");

        if (drawerTaskUI != null && !ScoreScript.Instance.drawerDone)
            drawerTaskUI.SetActive(true);
        else
            Debug.LogError("drawerTaskUI is NULL");
    }
}