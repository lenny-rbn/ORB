using UnityEngine;

public class Particules : MonoBehaviour
{
    #region Particle list
    [SerializeField] private ParticleSystem pJump;
    [SerializeField] private ParticleSystem pDash;
    [SerializeField] private ParticleSystem pBounce;
    [SerializeField] private ParticleSystem pDeathBall;
    [SerializeField] private ParticleSystem pThrow;
    [SerializeField] private ParticleSystem pRecall;
    [SerializeField] private ParticleSystem pHit;
    [SerializeField] private ParticleSystem pCatch;
    [SerializeField] private ParticleSystem pEnemyDeath;
    [SerializeField] private ParticleSystem pDrillDeath;

    #endregion

    private MovePlayer player;
    private OrangeMove ball;

    private void Start()
    {
        player = FindObjectOfType<MovePlayer>();
        ball = FindObjectOfType<OrangeMove>();
    }

    void Update()
    {
        if (pJump != null)
        {
            if (GameEvent.jump)
            {
                ParticleSystem jumpPart = Instantiate<ParticleSystem>(pJump, player.transform.position, Quaternion.identity);
                jumpPart.gameObject.AddComponent<KillParticule>();
            }
        }

        if (pDash != null)
        {
            if (GameEvent.dash)
            {
                ParticleSystem dashPart = Instantiate(pDash, player.transform.position, Quaternion.Euler(0, -player.rotation, 0));
                dashPart.gameObject.AddComponent<KillParticule>();
            }
        }

        if (pRecall != null)
        {
            if (GameEvent.recallBall)
            {
                ParticleSystem recallPart = Instantiate<ParticleSystem>(pRecall, ball.transform.position, Quaternion.identity);
                recallPart.gameObject.AddComponent<KillParticule>();
            }
        }


        if (pThrow != null)
        {
            if (GameEvent.shoot)
            {
                ParticleSystem throwPart = Instantiate<ParticleSystem>(pThrow, ball.transform.position, Quaternion.identity);
                throwPart.gameObject.AddComponent<KillParticule>();
            }
        }

        if (pCatch != null)
        {
            if (GameEvent.catchBall)
            {
                ParticleSystem catchPart = Instantiate<ParticleSystem>(pCatch, ball.transform.position, Quaternion.identity);
                catchPart.gameObject.AddComponent<KillParticule>();
            }
        }


        if (pBounce != null)
        {
            if (GameEvent.ballBounce)
            {
                ParticleSystem bouncePart = Instantiate<ParticleSystem>(pBounce, ball.transform.position, Quaternion.identity);
                bouncePart.gameObject.AddComponent<KillParticule>();
            }
        }

        if (pHit != null)
        {
            if (GameEvent.playerHitBall)
            {
                ParticleSystem hitPart = Instantiate<ParticleSystem>(pHit, ball.gameObject.transform.position, Quaternion.identity);
                hitPart.gameObject.AddComponent<KillParticule>();
            }
        }

        if(pEnemyDeath)
        {
            if (GameEvent.enemyDeath)
            {
                ParticleSystem hitPart = Instantiate<ParticleSystem>(pEnemyDeath, GameEvent.posEnemyDeath, Quaternion.identity);
                hitPart.gameObject.AddComponent<KillParticule>();
            }
        }

        if (pDeathBall != null)
        {
            if (GameEvent.ballDeath)
            {
                ParticleSystem ballDeathPart = Instantiate<ParticleSystem>(pDeathBall, GameEvent.posBallDeath, Quaternion.identity);
                ballDeathPart.gameObject.AddComponent<KillParticule>();
            }
        }

        if(pDrillDeath != null)
        {
            if(GameEvent.drillDeath)
            {
                ParticleSystem drillDeath = Instantiate<ParticleSystem>(pDrillDeath, GameEvent.posEnemyDeath, Quaternion.identity);
                drillDeath.gameObject.AddComponent<KillParticule>();
            }
        }
    }
}
