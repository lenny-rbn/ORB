using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private EnemyState enemyState;
    private Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
    }

    private void Update()
    {
        if (enemyState.isDead)
            enemyAnimator.SetBool("Death", true);

        if (enemyState.hitting)
        {
            enemyAnimator.SetTrigger("Hit");
            enemyState.hitting = false;
        }
    }
}
