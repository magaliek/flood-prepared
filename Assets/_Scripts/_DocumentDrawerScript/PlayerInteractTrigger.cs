using UnityEngine;

public class PlayerInteractTrigger : MonoBehaviour
{
    [SerializeField] private InteractionSystem interactionSystem;

    private IInteractable FindInteractable(Collider2D other)
    {
        // 1. Same object
        var mono = other.GetComponent<MonoBehaviour>();
        if (mono is IInteractable interactable1)
            return interactable1;

        // 2. Parent
        foreach (var mb in other.GetComponentsInParent<MonoBehaviour>())
        {
            if (mb is IInteractable interactable2)
                return interactable2;
        }

        // 3. Children
        foreach (var mb in other.GetComponentsInChildren<MonoBehaviour>())
        {
            if (mb is IInteractable interactable3)
                return interactable3;
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = FindInteractable(other);

        if (interactable != null)
            interactionSystem.SetCurrent(interactable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = FindInteractable(other);

        if (interactable != null)
            interactionSystem.ClearCurrent(interactable);
    }
}