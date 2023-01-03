using UnityEngine;

public class WeakPointDrill : MonoBehaviour
{
    [SerializeField] float timeBeforeDestroy;

    private bool destroy;

    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.layer == 6 && !FindObjectOfType<OrangeMove>().GetinHand())
             destroy = true;
    }

    private void FixedUpdate()
    {
        if (destroy)
        {
            timeBeforeDestroy -= Time.deltaTime;
            GetComponent<Collider>().enabled = false;

            if (timeBeforeDestroy < 0)
            {
                SaveStats.enemyKilled++;
                GameEvent.drillDeath = true;
                GameEvent.posEnemyDeath = transform.parent.position;

                Destroy(transform.parent.gameObject);
            }
        }
    }
}
