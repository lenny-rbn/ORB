using UnityEngine;

public static class SaveStats
{
    public static bool playerNoDeath = true;
    public static bool ballNoDeath = true;

    public static int bouceNb = 0;
    public static int deathNb = 0;
    public static int timeMin = 0;
    public static int timeSec = 0;
    public static int timeCen = -1;

    public static float timeS;
    public static float timeA;
    public static float timeB;

    public static int chestNB = 0;
    public static float bestCombo = 0;
    public static float enemyKilled = 0;
    public static float wallsDestroy = 0;
    public static float time = -Time.deltaTime;
    public static Vector3 spawn = Vector3.zero;

    public static void Reload()
    {
        playerNoDeath = true;
        ballNoDeath = true;

        bouceNb = 0;
        deathNb = 0;
        timeMin = 0;
        timeSec = 0;
        timeCen = -1;

        bestCombo = 0;
        enemyKilled = 0;
        wallsDestroy = 0;
        time = -Time.deltaTime;
        spawn = Vector3.zero;
    }
}
