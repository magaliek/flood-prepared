using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
     private float speed = 4f;
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
        animator.SetBool("playerWalking", true);

        if (c.canceled)
        {
            animator.SetBool("playerWalking", false);
            animator.SetFloat("X", movementInput.x);
            animator.SetFloat("Y", movementInput.y);

        }
        movementInput = c.ReadValue<Vector2>();
        animator.SetFloat("inputx", movementInput.x);
        animator.SetFloat("inputy", movementInput.y);

    }
}