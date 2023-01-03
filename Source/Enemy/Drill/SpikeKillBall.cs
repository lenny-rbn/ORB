using System.Collections;
using UnityEngine;

public class SpikeKillBall : MonoBehaviour
{

    [SerializeField] private float timeBallDestroy;

    private OrangeMove ball;
    private Animator anim;

    void Start()
    {
        ball = FindObjectOfType<OrangeMove>();
        anim = FindObjectOfType<MovePlayer>().GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OrangeMove>() != null)
        {
            GameEvent.ballDeath = true;
            GameEvent.posBallDeath = other.gameObject.GetComponent<OrangeMove>().transform.position;
            StartCoroutine(DestroyBall());
        }
    }

    IEnumerator DestroyBall()
    {
        ball.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeBallDestroy);
        ball.gameObject.SetActive(true);
        ball.SetInHandPlayer(true);
        anim.SetTrigger("Catch");
    }
}
