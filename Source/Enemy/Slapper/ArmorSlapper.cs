using UnityEngine;
using UnityEngine.UI;

public class ArmorSlapper : MonoBehaviour
{
    [SerializeField] private int armor;
    [SerializeField] private float speedReturn;
    [SerializeField] private float timeBeforeDestruct = 0;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Text armorText;

    private bool destruct;
    private bool isEnemy = false;
    private bool slapperHasHit;

    private OrangeMove ball;
    private EnemyState enemy;
    private MovePlayer player;

    private void Start()
    {
        enemy = GetComponent<EnemyState>();
        ball = FindObjectOfType<OrangeMove>();
        player = FindObjectOfType<MovePlayer>();

        if (enemy != null)
            isEnemy = true;

        slapperHasHit = false;
    }

    public void CheckDestruct()
    {
        if (ball.ballForce >= armor && !ball.GetinHand() && slapperHasHit) //If the slapper has already hit the ball
        {
            if (isEnemy)
                enemy.isDead = true;

            enemy.GetComponent<Collider>().enabled = false;
            destruct = true;
            ball.HurtEnemy();
        }
        else if(!slapperHasHit && !ball.GetinHand()) //Slapper hit the ball one time
        {
            slapperHasHit = true;
            ball.isSlapping = true;
            ball.gameObject.layer = 0; //Change the layer of the ball (the ball can now hurt us)
            ball.SetSpeed(speedReturn);
        }
    }

    private void OnCollisionEnter(Collision collision) //Ball Throw/Hit
    {
        if (collision.gameObject.layer == 6)
            CheckDestruct();
    }

    private void OnTriggerEnter(Collider other) //Ball Recalled
    {
        if (other.gameObject.layer == 6 && ball.GetIsRecalled())
            CheckDestruct();
    }

    private void Update()
    {
        //Rotation
        Vector3 dir = player.transform.position - transform.position;
        float angle = Vector3.Angle(Vector3.right, dir);
        if (dir.z > 0)
            angle *= -1;
        transform.rotation = Quaternion.Euler(0, angle + 90, 0);

        //Time Before Destruct
        if (destruct)
        {
            GameEvent.posEnemyDeath = this.transform.position;

            if (timeBeforeDestruct > 0)
            {
                timeBeforeDestruct -= Time.deltaTime;
            }
            else
            {
                SaveStats.enemyKilled++;
                Destroy(transform.gameObject);
            }
        }

        //Armor UI
        if (armor == 0)
            GetComponentInChildren<Canvas>().enabled = false;
        else
            GetComponentInChildren<Canvas>().enabled = true;
        if (armorText != null)
        {
            armorText.transform.LookAt(cameraTransform);
            armorText.text = $"{armor}";
        }
    }
}
