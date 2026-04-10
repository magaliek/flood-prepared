using UnityEngine;

public class PlayerInteractTrigger : MonoBehaviour
{
    [SerializeField] private InteractionSystem interactionSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactionSystem.SetCurrent(interactable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactionSystem.ClearCurrent(interactable);
    }
}