using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [System.NonSerialized] public bool canMove;
    [System.NonSerialized] public bool isAlive;
    [System.NonSerialized] public int deathNB;
    [System.NonSerialized] public float combo;
    [System.NonSerialized] public Vector3 spawnPos;

    private PlayerDeath death;
    private int life = 1;

    void Start()
    {
        death = this.gameObject.GetComponent<PlayerDeath>();
        canMove = true;
        isAlive = true;

        spawnPos = SaveStats.spawn;
        deathNB = SaveStats.deathNb;
    }

    void Update()
    {
        if (life <= 0)
        {
            SaveStats.deathNb++;
            death.Restart();
        }
    }

    public void TakeHit()
    {
        life--;
    }
}
