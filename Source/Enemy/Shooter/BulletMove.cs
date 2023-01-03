using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float autoDestroyDistance;

    private Vector3 direction;

    private Rigidbody bullet;
    private MovePlayer player;

    private void Start()
    {
        bullet = GetComponent<Rigidbody>();
        player = FindObjectOfType<MovePlayer>();
        direction = (player.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        bullet.velocity = speed * Time.deltaTime * direction;

        if(Vector3.Distance(player.transform.position, transform.position) > autoDestroyDistance)
            Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3) // 3 = Player
            collision.gameObject.GetComponent<PlayerStats>().TakeHit();

        Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 dir) { direction = dir; }
}
