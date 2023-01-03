using UnityEngine;

[RequireComponent(typeof(EnemyState))]
public class EnemyHit : MonoBehaviour
{
    private Animator anim;
    private EnemyState state;

    private void Start()
    {
        state = GetComponent<EnemyState>();
        anim = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            if (this.CompareTag("JHit"))
                anim.SetTrigger("Hit");

            state.hitting = true;

            Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;
            float rotation = Vector3.Angle(Vector3.right, dir);
            if (dir.z > 0)
                rotation *= -1;
            Vector3 rot = new Vector3(0, rotation + 180, 0);
            transform.rotation = Quaternion.Euler(rot);

            collision.gameObject.GetComponent<PlayerStats>().TakeHit();
        }
    }
}


