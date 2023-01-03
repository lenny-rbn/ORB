using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] bool buffNeeded;
    [SerializeField] float timeBeforeDestroy;
    [SerializeField] float explosionForce = 100;

    private bool destroy;
    private bool soundPlayed;
    private bool kinematicSet;

    private Vector3 colisionNormal;

    private void Start()
    {
        colisionNormal = Vector3.zero;
    }

    private void CheckDestruct(OrangeMove ball)
    {
        if (buffNeeded) //if the ball need the buff given by the brasero
        {
            if (ball.GetIsBuffed() && !ball.GetinHand())
                destroy = true;
        }
        else if(!ball.GetinHand())
        {
            destroy = true;
        }
    }

    private void OnCollisionEnter(Collision collision) //Throw and Hit
    {
        if (collision.collider.gameObject.GetComponent<OrangeMove>() != null)
        {
            OrangeMove ball = collision.collider.gameObject.GetComponent<OrangeMove>();
            CheckDestruct(ball);
            colisionNormal = collision.GetContact(0).normal;
        }
    }

    private void OnTriggerEnter(Collider other) //Recall
    {
        if (other.gameObject.GetComponentInParent<OrangeMove>() != null && other.gameObject.GetComponentInParent<OrangeMove>().GetIsRecalled())
        {
            OrangeMove ball = other.gameObject.GetComponentInParent<OrangeMove>();
            CheckDestruct(ball);
        }
    }

    private void FixedUpdate()
    {
        if (destroy)
        {
            GetComponent<BoxCollider>().enabled = false;

            // Destroy the wall in multiple wall shards
            if (!kinematicSet)
            {
                foreach (Transform child in transform)
                {
                    Rigidbody body = child.GetComponent<Rigidbody>();
                    if (body != null)
                    {
                        body.isKinematic = false;
                        body.AddForce(new Vector3(colisionNormal.x * Random.value * explosionForce, Random.value * explosionForce, colisionNormal.z * Random.value * explosionForce));
                    }
                    kinematicSet = true;
                }
            }

            // Sound
            if (!soundPlayed)
            {
                GameEvent.wallBreak = true;
                soundPlayed = true;
            }

            // Destruct
            timeBeforeDestroy -= Time.deltaTime;
            if (timeBeforeDestroy < 0)
            {
                SaveStats.wallsDestroy++;
                Destroy(gameObject);
            }
        }
    }
}
