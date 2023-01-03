using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerStats player;

    bool reached;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // 3 : Player
        {
            if (!reached)
            {
                player.spawnPos = transform.position;
                GameEvent.reachCheckpoint = true;
                reached = true;
            }
        }
    }
}
