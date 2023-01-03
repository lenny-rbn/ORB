using UnityEngine;

public class DrillKillPlayer : MonoBehaviour
{
    private PlayerDeath player;

    void Start()
    {
        player = FindObjectOfType<PlayerDeath>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3) // 3 : player
            player.Restart();
    }
}
