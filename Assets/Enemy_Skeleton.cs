using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.linearVelocity = new Vector2(moveSpeed * 1.5f * facing, rb.linearVelocity.y);
                isAttacking = false;
                Debug.Log("c u");
            }
            else
            {
                Debug.Log("atac");
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
            Movement();

    }

    private void Movement()
    {
        if (!isAttacking) 
        { 
            rb.linearVelocity = new Vector2(moveSpeed * facing, rb.linearVelocity.y);
        }
    }

    protected override void CollisonChecks()
    {
        base.CollisonChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facing, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x +playerCheckDistance*facing,transform.position.y));
    }
}
