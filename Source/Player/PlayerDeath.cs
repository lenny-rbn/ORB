using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Animator trasi;

    private Animator animator;
    private PlayerStats player;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<PlayerStats>();
    }

    public void Restart()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        player.canMove = false;
        player.isAlive = false;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.5f);
        trasi.SetTrigger("End");
        yield return new WaitForSeconds(1);

        SaveStats.spawn = player.spawnPos;
        SaveStats.playerNoDeath = false;
        GameEvent.playerDeath = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
