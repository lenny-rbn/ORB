using UnityEngine;

public class OrangeMove : MonoBehaviour
{
    [SerializeField] private int speedScale = 275;
    [SerializeField] private int initSpeed = 1100;
    [SerializeField] private float FastRecallDistance = 75f;
    [SerializeField] private GameObject buffFeedBack;

    private bool inHandPlayer;
    private bool isRecalled;
    private bool isBuffed;
    private bool aimbot;

    private float speed;
    private float rotation;
    private float exitPipeTimer = -1;
    private Vector3 dir;

    public bool isSlapping;
    public static bool sticked;
    public int currentCombo;
    public float ballForce;
    public float offSetYInHand = 0;
    public PlayerStats player;

    private Rigidbody ball;
    private OrangeTrail trail;
    private GameObject aimbotGO;
    private MovePlayer playerMove;
    private PlayerDeath playerDeath;

    private void Start()
    {
        ball = GetComponent<Rigidbody>();
        trail = GetComponent<OrangeTrail>();
        player = FindObjectOfType<PlayerStats>();
        playerMove = FindObjectOfType<MovePlayer>();
        playerDeath = FindObjectOfType<PlayerDeath>();

        inHandPlayer = true;
        isRecalled = false;
        aimbot = false;
        sticked = false;
        transform.position = player.transform.position + new Vector3(1, 0, 0);

        currentCombo = 0;
        ballForce = 0;
        speed = initSpeed;
        rotation = playerMove.rotation;
        dir = new Vector3(0, 0, 0);

        trail.DisableTrails();
    }

    public void HitBall(Vector3 vdir)
    {
        dir = vdir;
        isRecalled = false;

        if (ballForce < 12)
        {
            speed += speedScale;
            ballForce++;
        }
    }

    public void HurtEnemy()
    {
        speed += speedScale;
        ballForce -= 4;
        if (ballForce < 0)
            ballForce = 0;

        ball.velocity = speed * Time.deltaTime * dir;
    }

    public void AutoAim()
    {
        Ray ray = new Ray(transform.position, dir);
        LayerMask mask = LayerMask.GetMask("AutoAim");

        if (Physics.Raycast(ray, out RaycastHit hitRaycast, Mathf.Infinity, mask))
        {
            if (hitRaycast.collider.isTrigger)
            {
                dir = (hitRaycast.collider.transform.parent.position - transform.position).normalized;
                aimbot = true;
                aimbotGO = hitRaycast.collider.transform.parent.gameObject;
            }
        }
    }

    private void Update()
    {
        if (inHandPlayer)
        {
            ball.velocity = Vector3.zero;
            dir = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), offSetYInHand, Mathf.Sin(rotation * Mathf.Deg2Rad));
            transform.position = player.transform.position + dir;
            ballForce = 0;
            speed = initSpeed;
            aimbot = false;
        }

        trail.TrailGestion(ballForce);
        player.combo = currentCombo;
    }

    void FixedUpdate()
    {
        rotation = playerMove.rotation;

        if (sticked)
        {
            ball.velocity = Vector3.zero;
        }
        else if ((isRecalled || isSlapping) && !inHandPlayer)
        {
            dir = ((player.transform.position + Vector3.up * offSetYInHand) - transform.position).normalized;
            ball.velocity = speed * Time.deltaTime * dir;

            if (Vector3.Distance(player.transform.position, transform.position) > FastRecallDistance) //Fast speed when the ball is too far
                ball.velocity = 10000 * Time.deltaTime * dir;

            aimbot = false;
        }
        else if(aimbot && !inHandPlayer)
        {
            dir = (aimbotGO.transform.position - transform.position).normalized;
            ball.velocity = speed * Time.deltaTime * dir;
        }
        else if (!inHandPlayer)
        {
            ball.velocity = speed * Time.deltaTime * dir;
        }

        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        if (exitPipeTimer > 0)
            exitPipeTimer -= Time.deltaTime;
    }

    private void ChangeDirectionPente()
    {
        float absX = Mathf.Abs(dir.x);
        float absY = Mathf.Abs(dir.y);
        float absZ = Mathf.Abs(dir.z);

        if (absX  > absY && absX > absZ)
            dir = new Vector3(Mathf.Sign(dir.x) * 1, 0, 0);
        else if (absY > absX && absY > absZ)
            dir = new Vector3(0, Mathf.Sign(dir.y) * 1, 0);
        else if (absZ > absY && absZ > absX)
            dir = new Vector3(0, 0, Mathf.Sign(dir.z) * 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        aimbot = false;

        if (collision.gameObject != player.gameObject && gameObject.layer != 0 && !inHandPlayer)
        {
            currentCombo++;
            CatchBall.InitThrowTimer(0f);
            SaveStats.bouceNb += 1;

            if (ballForce < 12 && collision.gameObject.layer != 7) //7 = enemy
            {
                speed += speedScale;
                ballForce++;
            }

            if(collision.gameObject.layer == LayerMask.NameToLayer("Pente"))
                ChangeDirectionPente();

            dir = Vector3.Reflect(dir, collision.contacts[0].normal);
            ball.velocity = dir * Mathf.Max(speed, 0f);
            AutoAim();

            if (!inHandPlayer)
                GameEvent.ballBounce = true;
        }
        else if(collision.gameObject == player.gameObject)
        {
            playerDeath.Restart();
        }
    }

    public void SetBuff(bool isBuffed)
    {
        this.isBuffed = isBuffed;
        if (buffFeedBack != null)
        {
            if (isBuffed)
                buffFeedBack.SetActive(true);
            else
                buffFeedBack.SetActive(false);
        }
    }

    public void ExitFromPipe()
    {
        exitPipeTimer = 0.2f;
        trail.ClearTrails();
    }

    public void SetAimbot(bool aim) { aimbot = aim; }
    public void SetInHandPlayer(bool inHand) { inHandPlayer = inHand; }
    public void SetIsRecalled(bool recalled) { isRecalled = recalled; }
    public void SetSpeed(float vspeed) { speed = vspeed; }
    public void SetDirection(Vector3 direction) { dir = direction; }
    public bool GetIsBuffed() { return isBuffed; }
    public bool GetinHand() { return inHandPlayer; }
    public bool GetIsRecalled() { return isRecalled; }
    public bool CanTakePipes() { return exitPipeTimer <= 0; }
    public float GetSpeed() { return speed;}
    public Vector3 GetDirection() { return dir; }
}