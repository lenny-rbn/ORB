using UnityEngine;

public class Bumper : MonoBehaviour
{
    public int bumpForce;
    private Rigidbody player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            player.velocity = new Vector3(player.velocity.x, 0, player.velocity.z);
            player.AddForce(new Vector3(player.velocity.x, bumpForce), ForceMode.Impulse);
            GameEvent.playerJumpOnBumper = true;
        }
    }
}