
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movSpeed = 5f;


    private Rigidbody2D rb;
    private Vector2 lastDirection;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        lastDirection.x = Input.GetAxisRaw("Horizontal");
        lastDirection.y = Input.GetAxisRaw("Vertical");

        lastDirection = lastDirection.normalized;

        animator.SetFloat("Horizontal", lastDirection.x);
        animator.SetFloat("Vertical", lastDirection.y);
        animator.SetFloat("Speed", lastDirection.magnitude);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = lastDirection * movSpeed;
    }
}