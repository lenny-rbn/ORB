using System.Collections;
using UnityEngine;

public class KillParticule : MonoBehaviour
{
    private int timer = 2;

    void Start()
    {
       StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
