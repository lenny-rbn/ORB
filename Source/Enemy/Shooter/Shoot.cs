using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float cdShoot;
    [SerializeField] private float detection;
    [SerializeField] private GameObject bullet;

    private float rotation;
    private float distancePlayerShooter;
    private Vector3 direction;

    private Animator anim;
    private MovePlayer player;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        player = FindObjectOfType<MovePlayer>();
        distancePlayerShooter = Vector3.Distance(player.transform.position, transform.position);
        StartCoroutine(Shooting());
    }

    void Update()
    {
        //Rotation 
        direction = (player.transform.position - transform.position).normalized;
        rotation = Vector3.Angle(Vector3.right, direction);
        if (direction.z > 0)
            rotation *= -1;
        Vector3 rot = new Vector3(0, rotation, 0);
        transform.rotation = Quaternion.Euler(rot);

        distancePlayerShooter = Vector3.Distance(player.transform.position, transform.position);
    }

    IEnumerator Shooting()
    {
        while(true)
        {
            if (distancePlayerShooter < detection)
            {
                anim.SetTrigger("Shoot");
                GameObject newBullet = Instantiate<GameObject>(bullet, transform.position + direction * 2, Quaternion.identity);
                GameEvent.jengashoot = true;
                yield return new WaitForSeconds(cdShoot);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
