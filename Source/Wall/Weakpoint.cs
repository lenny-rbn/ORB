using UnityEngine;

public class Weakpoint : MonoBehaviour
{
    [SerializeField] bool buffNeeded;
    [SerializeField] float timeBeforeDestroy;
    [SerializeField] float explosionForce = 100;

    private bool destroy = false;
    private bool soundPlayed = false;
    private bool kinematicSet = false;

    private void CheckDestruct(OrangeMove ball)
    {
        if (buffNeeded)
        {
            if (ball.GetIsBuffed() && !ball.GetinHand())
                destroy = true;
        }
        else if(!ball.GetinHand())
        {
            destroy = true;
        }
    }

    private void OnCollisionEnter(Collision collision) //Throw / Hit
    {
        if (collision.collider.gameObject.GetComponent<OrangeMove>() != null)
        {
            OrangeMove ball = collision.collider.gameObject.GetComponent<OrangeMove>();
            CheckDestruct(ball);
        }
    }

    private void OnTriggerEnter(Collider other) // Recall
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
            // Destroy the wall in multiple wall shards
            if (!kinematicSet)
            {
                foreach (Transform child in transform.parent)
                {
                    BoxCollider box = child.GetComponent<BoxCollider>();

                    if (box != null)
                        box.enabled = false;
                    
                    if (child.childCount > 0)
                    {
                        foreach (Transform children in child)
                        {
                            Rigidbody body = children.GetComponent<Rigidbody>();
                            if (body != null)
                            {
                                body.isKinematic = false;
                                body.AddForce(new Vector3(Random.Range(-0.5f, 1) * explosionForce, Random.value * explosionForce, Random.Range(-1, 1) * Random.value * explosionForce));
                            }
                        }
                    }
                }
                kinematicSet = true;
            }

            // Sound
            if (!soundPlayed)
            {
                GameEvent.wallBreak = true;
                soundPlayed = true;
            }

            //Destruct
            timeBeforeDestroy -= Time.deltaTime;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            if (timeBeforeDestroy < 0)
            {
                SaveStats.wallsDestroy++;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
