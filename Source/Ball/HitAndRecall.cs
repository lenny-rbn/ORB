using UnityEngine;

public class HitAndRecall : MonoBehaviour
{ 
    [SerializeField] private float distanceHitRecall = 5f;
    [SerializeField] private float maxDistanceBall = 100f;

    public bool hasHited;

    private float rotation;
    private Vector3 dir;

    private OrangeMove ball;
    private MovePlayer player;
    private Animator animator;
    private PlayerStats playerStats;

    private void Start()
    {
        player = GetComponent<MovePlayer>();
        ball = FindObjectOfType<OrangeMove>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponentInChildren<Animator>();
    }

    void Hit()
    {
        if (Vector3.Distance(ball.transform.position, player.transform.position) < distanceHitRecall && !ball.GetinHand() && !hasHited && !OrangeMove.sticked && !player.blockInputBall && playerStats.isAlive)
        {
            rotation = player.rotation;
            dir = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), 0, Mathf.Sin(rotation * Mathf.Deg2Rad));

            if (player.GetHit() && !player.GetIsFacingWall()) //Can't hit if the player is facing a wall
            {
                //Ball direction and update variables
                ball.currentCombo++;
                ball.isSlapping = false;
                ball.transform.position = (player.transform.position + Vector3.up * ball.offSetYInHand) + dir;
                ball.SetAimbot(false);
                ball.HitBall(dir);
                ball.AutoAim();
                if (ball.gameObject.layer != 6) //The slapper change the layer of the ball (the ball can hurt the player if he doesn't hit it)
                    ball.gameObject.layer = 6;

                //Animation and Sound
                GameEvent.playerHitBall = true;
                CatchBall.InitThrowTimer(Time.deltaTime);
                animator.SetTrigger("Throw");

                Collider ballCollider = ball.GetComponent<Collider>();
                ballCollider.enabled = true;
                hasHited = true;
            }
        }
        else if (Vector3.Distance(ball.transform.position, player.transform.position) > distanceHitRecall && !player.GetHit())
        {
            hasHited = false;
        }
    }

    void Recall()
    {
        float distancePlayerBall = (transform.position - ball.transform.position).magnitude;
        if (((player.GetHit() && distancePlayerBall > distanceHitRecall) || (distancePlayerBall > maxDistanceBall)) && !hasHited && ball.gameObject.layer == 6 && playerStats.isAlive && !player.blockInputBall)
        {
            //Ball direction and update variables
            ball.SetDirection((transform.position - ball.transform.position).normalized);
            ball.SetIsRecalled(true);
            hasHited = true;

            Collider ballCollider = ball.GetComponent<Collider>();
            ballCollider.enabled = false;
            CatchBall.InitThrowTimer(0f);

            //Animation and Sound
            animator.SetTrigger("Recall");
            GameEvent.recallBall = true;

        }
        else if (Vector3.Distance(ball.transform.position, player.transform.position) < distanceHitRecall && !player.GetHit())
        {
            hasHited = false;
        }
    }

    private void Update()
    {
        Hit();
        Recall();
    }
}
