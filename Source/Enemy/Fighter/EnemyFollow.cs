using UnityEngine;

[RequireComponent(typeof(EnemyState))]
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private int followRange = 10;
    [SerializeField] private float speed = 3;

    public bool isFollowing;

    private float distancePlayerEnemy;
    private float rotation;
    private Vector3 dir;

    private EnemyState enemy;
    private PlayerStats player;

    private void Start()
    {
        enemy = GetComponent<EnemyState>();
        player = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        if (!enemy.isDead)
        {
            distancePlayerEnemy = (player.transform.position - enemy.transform.position).magnitude;
            if (distancePlayerEnemy <= followRange)
            {
                isFollowing = true;
                dir = (player.transform.position - enemy.transform.position).normalized;
                dir.y = 0;

                enemy.transform.position += speed * Time.deltaTime * dir;

                rotation = Vector3.Angle(Vector3.right, dir);

                if (dir.z > 0)
                    rotation *= -1;

                Vector3 rot = new Vector3(0, rotation + 180, 0);
                transform.rotation = Quaternion.Euler(rot);
            }
            else
            {
                isFollowing = false;
            }
        }
    }
}
