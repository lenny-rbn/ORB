using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HubDoor : MonoBehaviour
{
    [SerializeField] int sceneBuildIndex;
    [SerializeField] Animator animator;

    IEnumerator DoTransitionAndLoad()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovePlayer>() != null)
        {
            if (animator != null)
                StartCoroutine(DoTransitionAndLoad());
            else
                SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
