using UnityEngine;

public class GameEvent : MonoBehaviour
{
    #region Booleans
    static public bool jump = false;
    static public bool dash = false;
    static public bool shoot = false;
    static public bool walk = false;
    static public bool wallBreak = false;
    static public bool enemyDeath = false;
    static public bool catchBall = false;
    static public bool recallBall = false;
    static public bool ballBounce = false;
    static public bool playerHitBall = false;
    static public bool playerDeath = false;
    static public bool pendulumTriggered = false;
    static public bool outOfSticky = false;
    static public bool playerJumpOnBumper = false;
    static public bool ballDeath = false;
    static public bool drillDeath = false;
    static public bool openPause = false;
    static public bool closePause = false;
    static public bool jengashoot = false;
    static public bool reachCheckpoint = false;
    static public bool ballGoInPipe = false;
    static public bool drillCharge = false;
    #endregion

    static public Vector3 posEnemyDeath;
    static public Vector3 posBallDeath;

    private void Reset()
    {
        if (jump)
            jump = false;

        if (dash)
            dash = false;

        if (shoot)
            shoot = false;

        if (wallBreak)
            wallBreak = false;

        if (enemyDeath)
            enemyDeath = false;

        if (catchBall)
            catchBall = false;

        if (recallBall)
            recallBall = false;

        if (ballBounce)
            ballBounce = false;

        if (playerHitBall)
            playerHitBall = false;

        if (playerDeath)
            playerDeath = false;

        if (pendulumTriggered)
            pendulumTriggered = false;

        if (outOfSticky)
            outOfSticky = false;

        if (playerJumpOnBumper)
            playerJumpOnBumper = false;

        if (ballDeath)
            ballDeath = false;

        if (drillDeath)
            drillDeath = false;

        if (openPause)
            openPause = false;

        if (closePause)
            closePause = false;

        if (reachCheckpoint)
            reachCheckpoint = false;

        if (ballGoInPipe)
            ballGoInPipe = false;
    }

    private void LateUpdate()
    {
        Reset();
    }
}
