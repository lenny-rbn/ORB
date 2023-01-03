using UnityEngine;

[RequireComponent(typeof(EnemyState))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private bool isAxisX;
    [SerializeField] private bool isAxisZ;
    [SerializeField] private float min;
    [SerializeField] private float max;    
    [SerializeField] private float speed = 3;

    private int dir;

    private EnemyState state;
    private EnemyFollow follow;

    private void Start()
    {
        dir = 1;
        state = GetComponent<EnemyState>();
        follow = GetComponent<EnemyFollow>();
    }

    private void Patrol()
    {
        if (isAxisX) //Move between min and max (Axe X)
        {
            if (transform.position.x < min)
                dir = 1;
            if (transform.position.x > max)
                dir = -1;

            if (dir == 1)
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            transform.position += dir * speed * Time.deltaTime * Vector3.right;
        }
        if (isAxisZ) //Move between min and max (Axe Z)
        {
            if (transform.position.z < min)
                dir = 1;
            if (transform.position.z > max)
                dir = -1;

            if (dir == 1)
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

            transform.position += dir * speed * Time.deltaTime * Vector3.forward;
        }
    }

    private void FixedUpdate()
    {
        if (!state.isDead)
        {
            if (follow != null)
            {
                if (!follow.isFollowing) //Patrol when the enemy doesn't follow the player if he has the script Follow
                    Patrol();
            }
            else
            {
                Patrol();
            }
        }
    }
}
