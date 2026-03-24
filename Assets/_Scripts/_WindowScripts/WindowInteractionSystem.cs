using UnityEngine;

public class WindowInteractionSystem : MonoBehaviour
{
    [SerializeField] private InteractPromptUI promptUI;

    private WindowInteractable current;

    private void Awake()
    {
        if (!promptUI) promptUI = FindFirstObjectByType<InteractPromptUI>();
        if (promptUI) promptUI.Hide();
    }

    private void Update()
    {
        if (current == null)
        {
            if (promptUI) promptUI.Hide();
            return;
        }

        if (promptUI) promptUI.Show(current.GetPrompt());


        if (Input.GetKeyDown(KeyCode.E))
            current.Interact();

        if (Input.GetKey(KeyCode.E))
            current.HoldInteract(Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.E))
            current.ResetHold();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var w = other.GetComponentInParent<WindowInteractable>();
        if (w != null)
            current = w;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var w = other.GetComponentInParent<WindowInteractable>();
        if (w != null && w == current)
            current = null;
    }
}