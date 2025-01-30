using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anima;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float xInput;
    private int facing=1;
    private bool facingRight = true;

    [Header("Collison info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
        CheckInput();
        CollisonChecks();
        FlipController();
        AnimatorControllers();
    }

    private void CollisonChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (isGrounded) { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
        }
        
    }

    private void AnimatorControllers()
    {
        bool isMove = rb.linearVelocity.x != 0;

        anima.SetFloat("yVelocity", rb.linearVelocity.y);
        anima.SetBool("isMove", isMove);
        anima.SetBool("isGrounded",isGrounded);
    }

    private void Flip()
    {
        facing = facing * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight) 
            Flip();   
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y - groundCheckDistance));
    }
}
