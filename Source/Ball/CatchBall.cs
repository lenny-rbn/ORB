using UnityEngine;

public class CatchBall : MonoBehaviour
{
    [SerializeField] private float catchBallDistance = 0.5f;

    static private float throwTimer = 0.25f;
    private float distBallPlayer;
    private float rotation;

    private Vector3 dir;

    private OrangeMove ball;
    private MovePlayer player;
    private Animator animator;

    void Start()
    {
        rotation = 0;
        ball = FindObjectOfType<OrangeMove>();
        player = GetComponentInParent<MovePlayer>();
        animator = player.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        distBallPlayer = Vector3.Distance(ball.transform.position, player.transform.position);

        if (ball.gameObject.layer == 6 && distBallPlayer < catchBallDistance && !ball.GetinHand() && throwTimer <= 0 && !OrangeMove.sticked)
        {
            //Save Best Combo
            if (ball.currentCombo > SaveStats.bestCombo)
                SaveStats.bestCombo = ball.currentCombo;
            ball.currentCombo = 0;

            //Ball position
            rotation = player.rotation;
            dir = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), 0, Mathf.Sin(rotation * Mathf.Deg2Rad));
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.position = player.transform.position + dir;
            ball.SetInHandPlayer(true);
            ball.SetIsRecalled(false);
            ball.SetBuff(false);

            //Animation and Sound
            GameEvent.catchBall = true;
            animator.SetTrigger("Catch");

            Collider ballCollider = ball.GetComponent<Collider>();
            ballCollider.enabled = true;
        }

        if (throwTimer > 0)
            throwTimer -= Time.deltaTime;
    }
    static public void InitThrowTimer(float time = 0.75f) { throwTimer = time; }
}
