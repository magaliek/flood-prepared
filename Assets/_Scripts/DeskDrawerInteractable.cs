using UnityEngine;

public class DeskDrawerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject drawerTaskUI;

    public string PromptText => "Interact (E)";

    public void Interact()
    {
        Debug.Log("DeskDrawer Interact called");

        if (drawerTaskUI != null)
            drawerTaskUI.SetActive(true);
        else
            Debug.LogError("drawerTaskUI is NULL");
    }
}