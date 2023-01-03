using UnityEngine;

public class Sticky : MonoBehaviour
{
    [SerializeField] int recallToUnstick = 1;

    private bool ballStick = false;
    private int recallCounter;
    private Vector3 enterVelocity;

    private GameObject ballSticked;
    private OrangeMove ball;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<OrangeMove>() != null && !collision.gameObject.GetComponent<OrangeMove>().GetinHand())
        {
            ballSticked = collision.gameObject;
            ball = ballSticked.GetComponent<OrangeMove>();

            Vector3 velocity = ballSticked.GetComponent<Rigidbody>().velocity;
            enterVelocity = velocity;
            ballStick = true;
            recallCounter = 0;
        }
    }

    private void Update()
    {
        if (ballStick)
        {
            OrangeMove.sticked = true;

            if (ball.GetIsRecalled())
            {
                recallCounter++;
                ball.SetIsRecalled(false);
            }

            if (recallCounter == recallToUnstick)
            {
                ballSticked.GetComponent<Rigidbody>().velocity = enterVelocity;
                OrangeMove.sticked = false;
                ballStick = false;
                ball.SetIsRecalled(true);
                GameEvent.outOfSticky = true;
            }
        }
    }
}
