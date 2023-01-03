using System.Collections;
using UnityEngine;

public class MoveDrill : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedAccel;
    [SerializeField] private float speedDecell;
    [SerializeField] private float waitSeconds;
    [SerializeField] private float speedRotate;

    private bool detection;
    private bool isCharging;

    private float angle;
    private float speed;
    private float lerpSpeed;
    private float lerpAngle;
    private float startAngle;
    private float endAngle;
    private Vector3 dir;

    private MovePlayer player;
    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        player = FindObjectOfType<MovePlayer>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        detection = false;
        lerpSpeed = 0;
        lerpAngle = 0;
        StartCoroutine(ChargeSpeed());
    }

    void Update()
    {
        if(!isCharging)
        {
            //Direction and rotation
            dir = player.transform.position - transform.position;

            float angle = Vector3.Angle(Vector3.right, dir);
            if (dir.z > 0)
                angle *= -1;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!detection)
            speed = 0;

        rb.velocity = speed * Time.deltaTime * dir.normalized;
    }

    IEnumerator ChargeSpeed()
    {
        //Precharge
        anim.SetTrigger("Precharge");
        yield return new WaitForSeconds(waitSeconds / 2);

        while (true)
        {
            if (detection)
            {
                //Charge
                anim.SetTrigger("Charge");
                GameEvent.drillCharge = true;
                isCharging = true;

                //Acceleration
                while (lerpSpeed < 1)
                {
                    speed = Mathf.Lerp(0, maxSpeed, lerpSpeed);
                    lerpSpeed += Time.deltaTime * speedAccel;
                    yield return null;
                }

                //Deceleration
                while (lerpSpeed > 0)
                {
                    speed = Mathf.Lerp(0, maxSpeed, lerpSpeed);
                    lerpSpeed -= Time.deltaTime * speedDecell;
                    yield return null;
                }

                speed = 0;
                lerpAngle = 0;

                //Precharge and Rotate
                anim.SetTrigger("Precharge");
                isCharging = false;
                StartCoroutine(Rotate());
                yield return new WaitUntil(() => lerpAngle > 1);
            }
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    IEnumerator Rotate()
    {
        startAngle = Vector3.Angle(Vector3.right, dir); 
        if (dir.z > 0)
            startAngle *= -1;

        endAngle = Vector3.Angle(Vector3.right, player.transform.position - transform.position);
        if ((player.transform.position - transform.position).z > 0)
            endAngle *= -1;

        while(lerpAngle < 1)
        {
            endAngle = Vector3.Angle(Vector3.right, player.transform.position - transform.position); //Actualize endAngle with the position of the player
            if ((player.transform.position - transform.position).z > 0)
                endAngle *= -1;

            angle = Mathf.Lerp(startAngle, endAngle, lerpAngle);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
            lerpAngle += Time.deltaTime * speedRotate;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) //player
            detection = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3) //player
            detection = false;
    }
}
