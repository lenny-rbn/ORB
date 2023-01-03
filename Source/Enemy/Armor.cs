using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestruct = 0;

    private bool isEnemy = false;
    private bool destruct;

    private EnemyState enemy;
    private OrangeMove ball;

    private void Start()
    {
        enemy = GetComponent<EnemyState>();
        ball = FindObjectOfType<OrangeMove>();

        if (enemy != null)
            isEnemy = true;
    }

    public void CheckDestruct()
    {
        if (!ball.GetinHand()) //just check if !GetinHand because armor has been killed
        {
            if (isEnemy)
                enemy.isDead = true;

            destruct = true;
            ball.HurtEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            CheckDestruct();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && ball.GetIsRecalled())
            CheckDestruct();
    }

    private void Update()
    {
        if (destruct)
        {
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
    }
}
