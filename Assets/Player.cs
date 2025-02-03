using UnityEngine;

public class Player : Entity
{


    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("Attack Info")]
    [SerializeField]private float comboTime = .8f;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;


    private float xInput;



    

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();


        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;


        FlipController();
        AnimatorControllers();
    }

    public void AttackOver() 
    { 
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2) 
            comboCounter = 0;
     
    }



    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {
        if(!isGrounded)
            return;
        if (comboTimeWindow < 0)
            comboCounter = 0;
        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (isAttacking) {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facing * dashSpeed, rb.linearVelocity.y * fallSpeed);
            //Debug.Log(rb.linearVelocity.y * fallSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }

    private void AnimatorControllers()
    {
        bool isMove = rb.linearVelocity.x != 0;

        anima.SetFloat("yVelocity", rb.linearVelocity.y);
        anima.SetBool("isMove", isMove);
        anima.SetBool("isGrounded", isGrounded);
        anima.SetBool("isDash", dashTime > 0);
        anima.SetBool("isAttacking", isAttacking);
        anima.SetInteger("comboCounter", comboCounter);
    }



    private void FlipController()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight)
            Flip();
    }

}
