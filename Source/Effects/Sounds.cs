using UnityEngine;

public class Sounds : MonoBehaviour
{
    #region Sound list
    [Header("Main music")]
    [SerializeField] private AudioSource music;

    [Header("Jump")]
    [SerializeField] private AudioSource audioJump;

    [Header("dash")]
    [SerializeField] private AudioSource audiodash;

    [Header("Trow Ball")]
    [SerializeField] private AudioSource audioThrow;

    [Header("Walk")]
    [SerializeField] private AudioSource audioWalk;

    [Header("Jenga shoot")]
    [SerializeField] private AudioSource audioJengaShoot;

    [Header("Enemy Death")]
    [SerializeField] private AudioSource audioEnemyDeath;

    [Header("Drill Death")]
    [SerializeField] private AudioSource audioDrillDeath;

    [Header("Drill Charge")]
    [SerializeField] private AudioSource audioDrillCharge;

    [Header("Catch")]
    [SerializeField] private AudioSource audioCatch;

    [Header("Recall")]
    [SerializeField] private AudioSource audioRecall;
    [SerializeField] private AudioSource audioRecallWhenSticked;
    [SerializeField] private AudioSource audioOutOfSticky;

    [Header("Bounce")]
    [SerializeField] private AudioSource audioBounce;

    [Header("Ball Death")]
    [SerializeField] private AudioSource audioBallDeath;

    [Header("Wall Break")]
    [SerializeField] private AudioSource audioBreak;

    [Header("Hit")]
    [SerializeField] private AudioSource audioHit;

    [Header("Player Death")]
    [SerializeField] private AudioSource audioDeath;

    [Header("Player Jump On Bumpers")]
    [SerializeField] private AudioSource audioBumper;

    [Header("Pendulum")]
    [SerializeField] private AudioSource audioPendulum;

    [Header("Menu")]
    [SerializeField] private AudioSource audioOpenPause;
    [SerializeField] private AudioSource audioClosePause;

    [Header("Checkpoint")]
    [SerializeField] private AudioSource audioCheckpoint;

    [Header("Pipe")]
    [SerializeField] private AudioSource audioPipe;
    #endregion

    void Start()
    {
        if (!music.isPlaying)
            music.Play();
    }

    void Update()
    {
        if (audiodash != null)
            if (GameEvent.dash)
                audiodash.Play();

        if (audioWalk != null)
        {
            if (GameEvent.walk)
            {
                if (!audioWalk.isPlaying)
                    audioWalk.Play();
            }
            else
            { 
                audioWalk.Stop();
            }
        }

        if (audioRecall != null)
        {
            if (audioRecallWhenSticked != null)
            {
                if (GameEvent.recallBall && OrangeMove.sticked)
                    audioRecallWhenSticked.Play();
                else if (GameEvent.recallBall)
                        audioRecall.Play();
            }
            else
            {
                if (GameEvent.recallBall)
                        audioRecall.Play();
            }
        }

        if (audioJump != null)
            if (GameEvent.jump)
                audioJump.Play();

        if (audioThrow != null)
            if (GameEvent.shoot)
                audioThrow.Play();

        if (audioJengaShoot != null)
            if (GameEvent.jengashoot)
            {
                audioJengaShoot.Play();
                GameEvent.jengashoot = false;   
            }

        if (audioEnemyDeath != null)
            if (GameEvent.enemyDeath)
                audioEnemyDeath.Play();

        if(audioDrillDeath != null)
            if (GameEvent.drillDeath)
                audioDrillDeath.Play();

        if (audioCatch != null)
            if (GameEvent.catchBall)
                audioCatch.Play();

        if (audioBounce != null)
            if (GameEvent.ballBounce)
                audioBounce.Play();

        if (audioHit != null)
            if (GameEvent.playerHitBall)
                audioHit.Play();

        if(audioBallDeath != null)
            if(GameEvent.ballDeath)
                audioBallDeath.Play();

        if (audioBreak != null)
            if (GameEvent.wallBreak)
                audioBreak.Play();

        if (audioOutOfSticky != null)
            if (GameEvent.outOfSticky)
                audioOutOfSticky.Play();

        if (audioDeath != null)
            if (GameEvent.playerDeath)
                audioDeath.Play();

        if (audioBumper != null)
            if (GameEvent.playerJumpOnBumper)
                audioBumper.Play();

        if (audioPendulum != null)
            if (GameEvent.pendulumTriggered)
                audioPendulum.Play();

        if (audioOpenPause != null)
            if (GameEvent.openPause)
                audioOpenPause.Play();

        if (audioClosePause != null)
            if (GameEvent.closePause)
                audioClosePause.Play();

        if (audioCheckpoint != null)
            if (GameEvent.reachCheckpoint)
                audioCheckpoint.Play();

        if (audioPipe != null)
            if (GameEvent.ballGoInPipe)
                audioPipe.Play();

        if (audioDrillCharge != null)
            if (GameEvent.drillCharge)
            {
                audioDrillCharge.Play();
                GameEvent.drillCharge = false;
            }
    }
}
