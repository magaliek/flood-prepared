using UnityEngine;

public abstract class WindowInteractable : MonoBehaviour
{
        public abstract string GetPrompt();
    public abstract void Interact();

       public virtual void HoldInteract(float dt) { }

    public virtual void ResetHold() { }
}