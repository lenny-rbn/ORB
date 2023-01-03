using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speedWithoutBall = 20f;
    [SerializeField] private float maxSpeedWithoutBall = 10f;
    [SerializeField] private float speedWithBall = 20f;
    [SerializeField] private float maxSpeedWithBall = 5f;
    [SerializeField] private float slownDown = 0.5f;

    [Header("Jump")]
    [SerializeField] private int jumpVelocity = 10;
    [SerializeField] private float lowJumpMult = 3f;
    [SerializeField] private float fallMult = 1.5f;
    [SerializeField] private float coyoteInit = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashDistance = 1.5f;
    [SerializeField] private float dashCoolDown = 0.2f;
    [SerializeField] private float coefDecelDash = 1.75f;

    [System.NonSerialized] public bool quit;
    [System.NonSerialized] public bool pause;
    [System.NonSerialized] public bool OnPause = false;
    [System.NonSerialized] public bool blockInputBall;
    [System.NonSerialized] public bool isFacingWall;
    [System.NonSerialized] public float rotation;

    private bool hit;
    private bool isJumping;
    private bool isDashing;
    private bool isGrounded;
    private bool canDashInAir;

    private float coyote;
    private float speed;
    private float LerpDash;
    private float maxSpeed;
    private float dashTimer;
    private Vector3 move;

    private Rigidbody player;
    private Animator animator;
    private ThrowBall throwBall;
    private OrangeMove ballMove;
    private PlayerStats playerStats;

    #region Input System
    private PlayerInput PlayerInput;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _throw;
    private InputAction _dash;
    private InputAction _pause;
    private InputAction _quit;
    #endregion

    private void Start()
    {
        if (SaveStats.spawn != Vector3.zero)
            transform.position = SaveStats.spawn;

        player = GetComponent<Rigidbody>();
        PlayerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
        throwBall = FindObjectOfType<ThrowBall>();
        ballMove = FindObjectOfType<OrangeMove>();
        animator = GetComponentInChildren<Animator>();

        LerpDash = 0;
        dashTimer = -1;
        blockInputBall = false;

        #region Input Actions
        _move = PlayerInput.actions.FindAction("Move");
        _move.performed += ctx => move = ctx.ReadValue<Vector2>();
        _move.canceled += _ => move = Vector2.zero;

        _jump = PlayerInput.actions.FindAction("Jump");
        _jump.Enable();
        _jump.performed += _ => Jump();
        _jump.canceled += _ => isJumping = true;

        _throw = PlayerInput.actions.FindAction("Throw");
        _throw.performed += _ => throwBall.ThrowAndHit();
        _throw.performed += _ => hit = true;
        _throw.canceled += _ => hit = false;

        _dash = PlayerInput.actions.FindAction("Dash");
        _dash.Enable();
        _dash.performed += _ => Dash();

        _pause = PlayerInput.actions.FindAction("Pause");
        _pause.performed += _ => pause = true;

        _quit = PlayerInput.actions.FindAction("Quit");
        _quit.performed += _ => quit = true;
        #endregion
    }


    private void Update()
    {
        isGrounded = IsGrounded();
        IsFacingWall();

        if (dashTimer > 0)
            dashTimer -= Time.deltaTime;

        if (dashTimer < dashCoolDown - dashTime)
            isDashing = false;
    }

    void FixedUpdate()
    {
        //Choose the right speed
        speed = ballMove.GetinHand() ? speedWithBall : speedWithoutBall;
        maxSpeed = ballMove.GetinHand() ? maxSpeedWithBall : maxSpeedWithoutBall;
        coyote -= Time.deltaTime;

        Move();

        // Jump gravity
        if (player.velocity.y > 0)
            player.velocity += fallMult * Physics.gravity.y * Time.deltaTime * Vector3.up;
        else if (player.velocity.y < 0 || isJumping)
            player.velocity += lowJumpMult * Physics.gravity.y * Time.deltaTime * Vector3.up;
    }

    private void LateUpdate()
    {
        pause = false;
        quit = false;
    }

    private bool IsGrounded()
    {
        LayerMask mask = (1 << 8); // Ignore the AutoAim layer
        mask = ~mask;
        isGrounded = false;
        if (Physics.Raycast(transform.position + Vector3.right * (-0.2f), Vector3.down, 1.2f, mask) || Physics.Raycast(transform.position + Vector3.right * (0.2f), Vector3.down, 1.2f, mask) || Physics.Raycast(transform.position, Vector3.down, 1.2f, mask) || Physics.Raycast(transform.position + Vector3.forward * (-0.2f), Vector3.down, 1.2f, mask) || Physics.Raycast(transform.position + Vector3.forward * (0.2f), Vector3.down, 1.2f, mask))
        {
            isGrounded = true;
            isJumping = false;
            canDashInAir = true;
            coyote = coyoteInit;
        }
        return isGrounded;
    }
     
    public void Move()
    {
        if(playerStats.canMove && !OnPause && playerStats.isAlive)
        {
            if (move != Vector3.zero)
            {
                //Animation and Sound
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);

                if (isGrounded)
                    GameEvent.walk = true;
                else
                    GameEvent.walk = false;

                //player Rotation
                rotation = Vector3.Angle(Vector3.right, move);
                if (move.y < 0)
                    rotation *= -1;

                //player Movement/Velocity
                player.velocity += new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad) * speed, 0, Mathf.Sin(rotation * Mathf.Deg2Rad) * speed);

                if (player.velocity.magnitude > maxSpeed && !isDashing && LerpDash <= 0) //Cap velocity with maxSpeed if the player isn't dashing
                {
                    player.velocity = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad) * maxSpeed, player.velocity.y, Mathf.Sin(rotation * Mathf.Deg2Rad) * maxSpeed); 
                }
                else if(player.velocity.magnitude > maxSpeed && LerpDash > 0) //Dash Velocity if the player is dashing
                {
                    player.velocity = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad) * Mathf.Lerp(maxSpeed, dashDistance / dashTime,LerpDash), player.velocity.y, Mathf.Sin(rotation * Mathf.Deg2Rad) * Mathf.Lerp(maxSpeed, dashDistance / dashTime, LerpDash));
                    LerpDash -= Time.deltaTime * coefDecelDash;
                }

                Vector3 rot = new Vector3(0, -rotation, 0);
                transform.rotation = Quaternion.Euler(rot);
            }
            else
            {
                //Animation and Sound
                animator.SetBool("Running", false);
                animator.SetBool("Idle", true);
                GameEvent.walk = false;

                //Deceleration
                LerpDash = 0;

                if (player.velocity.x > 0.5f)
                    player.velocity = new Vector3(player.velocity.x - slownDown, player.velocity.y, player.velocity.z);
                else if (player.velocity.x < -0.5f)
                    player.velocity = new Vector3(player.velocity.x + slownDown, player.velocity.y, player.velocity.z);
                else
                    player.velocity = new Vector3(0, player.velocity.y, player.velocity.z);

                if (player.velocity.z > 0.5f)
                    player.velocity = new Vector3(player.velocity.x, player.velocity.y, player.velocity.z - slownDown);
                else if (player.velocity.z < -0.5f)
                    player.velocity = new Vector3(player.velocity.x, player.velocity.y, player.velocity.z + slownDown);
                else
                    player.velocity = new Vector3(player.velocity.x, player.velocity.y, 0);
            }
        }
    }

    public void Jump()
    {
        if (OnPause)
            return;

        if ((isGrounded || coyote > 0) && !isJumping && playerStats.isAlive)
        {
            player.velocity = Vector3.up * jumpVelocity;
            coyote = 0;

            GameEvent.jump = true;
            animator.SetTrigger("Jump");
        }
    }

    public void Dash()
    {
        if (OnPause)
            return;

        if (canDashInAir && dashTimer < 0 && move != Vector3.zero && playerStats.isAlive)
        {
            isDashing = true;
            player.velocity = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad) * dashDistance / dashTime, player.velocity.y, Mathf.Sin(rotation * Mathf.Deg2Rad) * dashDistance / dashTime);
            dashTimer = dashCoolDown;

            GameEvent.dash = true;
            animator.SetTrigger("Dash");

            LerpDash = 1;

            if (!isGrounded)
                canDashInAir = false;
        }
    }
    public void IsFacingWall()
    {
        isFacingWall = false;
        var layerMask = (1 << 6) + (1 << 1) + (1 << 7) + (1 << 8) + (1 << 11); //Ignore layer ball, player, enemy, autoaim, wallshard
        layerMask = ~layerMask;

        Vector3 ray = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), 0, Mathf.Sin(rotation * Mathf.Deg2Rad));
        isFacingWall = Physics.Raycast(transform.position, ray, 1.1f * (ballMove.GetSpeed() / 900), layerMask);
    }

    public void SetHit(bool _hit) { hit = _hit; }
    public bool GetHit() { return hit; }
    public bool GetIsFacingWall() { return isFacingWall; }
    public bool GetIsGrounded() { return isGrounded; }
    public Vector2 GetMoveVector() { return move; }
}   