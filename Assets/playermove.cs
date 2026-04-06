using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 12f;
     private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Enable Collider here
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementInput * speed;   // 2D standard
        //Check if collider is enabled here every frame 
    }

    public void Move(InputAction.CallbackContext c)
{
    Vector2 input = c.ReadValue<Vector2>();

    if (c.canceled)
    {
        animator.SetBool("playerWalking", false);
        // don't update inputx/inputy here — keep last direction
    }
    else
    {
        animator.SetBool("playerWalking", true);
        animator.SetFloat("inputx", input.x);
        animator.SetFloat("inputy", input.y);
    }

    movementInput = input;
}
}