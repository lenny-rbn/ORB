using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    private OrangeMove ball;
    private HitAndRecall hit;
    private MovePlayer player;
    private Animator animator;
    private PlayerStats playerStats;

    private void Start()
    {
        ball = FindObjectOfType<OrangeMove>();
        player = FindObjectOfType<MovePlayer>();
        hit = player.GetComponent<HitAndRecall>();
        playerStats = player.GetComponent<PlayerStats>();
        animator = player.GetComponentInChildren<Animator>();
    }

    public void ThrowAndHit()
    {
        if(ball.GetinHand() && !player.GetIsFacingWall() && playerStats.isAlive && !player.blockInputBall)
        {
            ball.SetInHandPlayer(false);
            Vector3 dir = new Vector3(Mathf.Cos(player.rotation * Mathf.Deg2Rad), 0, Mathf.Sin(player.rotation * Mathf.Deg2Rad));
            ball.SetDirection(dir);
            ball.AutoAim();
            hit.hasHited = true;

            CatchBall.InitThrowTimer();
            GameEvent.shoot = true;
            animator.SetTrigger("Throw");
        }
    }
}
