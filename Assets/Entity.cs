using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anima;

    [Header("Facing Info")]
    protected int facing = 1;
    protected bool facingRight = true;

    [Header("Collison info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;



    protected bool isWallDetected;
    protected bool isGrounded;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponentInChildren<Animator>();
        if (wallCheck == null)
            wallCheck = transform;
    }

    
    protected virtual void Update()
    {
        CollisonChecks();
    }


    protected virtual void CollisonChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facing, whatIsGround);
    }

    protected virtual void Flip()
    {
        facing = facing * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance *facing, wallCheck.position.y));
    }
}
