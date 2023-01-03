using System.Collections;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool inverseRotation;

    private bool hasRotated;

    private float lerp;
    private float angleEnd;
    private float ballForce;
    private float angleStart;
    private float speedForce;
    private float rotate;
    private float direction;


    private Vector3 ballDirection;

    private OrangeMove ball;

    void Start()
    {
        ball = FindObjectOfType<OrangeMove>();
        angleStart = transform.rotation.eulerAngles.z;
        hasRotated = false;
        rotate = 0;
        direction = 0;
    }

    void Update()
    {
        ballDirection = ball.GetDirection();
        ballForce = ball.ballForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == ball.gameObject.layer && !hasRotated && !ball.GetinHand())
        {
            StartCoroutine(RotatePendulum());
            hasRotated = true;
            GameEvent.pendulumTriggered = true;
        }
    }

    IEnumerator RotatePendulum()
    {
        lerp = 0;
        speedForce = speed * Mathf.Pow(1.1f, ballForce);

        //Choose the right ball direction depending of the orientation of the pendulum
        if((transform.parent.rotation.eulerAngles.y >= 45 && transform.parent.rotation.eulerAngles.y <= 135) || (transform.parent.rotation.eulerAngles.y >= 225 && transform.parent.rotation.eulerAngles.y <= 315))
            direction = ballDirection.z;
        else
            direction = ballDirection.x;

        //Choose angleEnd
        if ((angleStart >= 270 || angleStart <= 90))
        {
            if ((direction > 0 && !inverseRotation) || (direction < 0 && inverseRotation) )
                angleEnd = angleStart - 90;
            else if( (direction > 0 && inverseRotation) || (direction < 0 && !inverseRotation))
                angleEnd = angleStart + 90;
        }
        else
        {
            if ((direction > 0 && !inverseRotation) || (direction < 0 && inverseRotation))
                angleEnd = angleStart + 90;
            else if((direction > 0 && inverseRotation) || (direction < 0 && !inverseRotation))
                angleEnd = angleStart - 90;
        }
        
        //Rotation
        while (true)
        {
            rotate = Mathf.Lerp(angleStart, angleEnd, lerp);
            transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, rotate);
            lerp += Time.deltaTime * speedForce;

            if (lerp < 1)
            {
                yield return null;
            }
            else
            {
                rotate = Mathf.Lerp(angleStart, angleEnd, 1);
                transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, rotate);
                yield break;
            }
        }
    }
}
