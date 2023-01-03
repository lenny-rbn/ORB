using UnityEngine;

public class KillZone : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = FindObjectOfType<MovePlayer>().GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerDeath>() != null)
            collision.gameObject.GetComponent<PlayerDeath>().Restart();

        if (collision.gameObject.GetComponent<OrangeMove>() != null && !collision.gameObject.GetComponent<OrangeMove>().GetinHand())
        {
            SaveStats.ballNoDeath = false;
            GameEvent.ballDeath = true;
            GameEvent.posBallDeath = collision.gameObject.GetComponent<OrangeMove>().transform.position;
            collision.gameObject.GetComponent<OrangeMove>().SetInHandPlayer(true);
            anim.SetTrigger("Catch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerDeath>() != null)
            other.gameObject.GetComponent<PlayerDeath>().Restart();

        if (other.gameObject.GetComponent<OrangeMove>() != null && !other.gameObject.GetComponent<OrangeMove>().GetinHand())
        {
            SaveStats.ballNoDeath = false;
            GameEvent.ballDeath = true;
            GameEvent.posBallDeath = other.gameObject.GetComponent<OrangeMove>().transform.position;
            other.gameObject.GetComponent<OrangeMove>().SetInHandPlayer(true);
            anim.SetTrigger("Catch");
        }
    }
}
