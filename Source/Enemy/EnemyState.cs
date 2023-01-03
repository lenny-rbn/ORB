using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public bool isDead = false;
    public bool hitting = false;
    private bool deathSoundPlayed = false;

    public void DisableCollliders()
    {
        if(GetComponent<Collider>() != null)
            GetComponent<Collider>().enabled = false;

        if(GetComponentInChildren<SphereCollider>() != null)
            GetComponentInChildren<SphereCollider>().enabled = false;
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            DisableCollliders();

            if (!deathSoundPlayed)
            {
                GameEvent.enemyDeath = true;
                GameEvent.posEnemyDeath = this.transform.position;
                deathSoundPlayed = true;
            }
        }
    }
}
