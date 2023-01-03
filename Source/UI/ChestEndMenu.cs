using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEndMenu : MonoBehaviour
{
    static public int nbChestToOpen;
    private List<GameObject> can = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            if(!child.gameObject.activeInHierarchy) //Get every can child 
                can.Add(child.gameObject);
        }
        StartCoroutine(AnimCan());
    }

    IEnumerator AnimCan()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < nbChestToOpen; i++)
        {
            if (!can[i].activeInHierarchy)
                can[i].SetActive(true);
            yield return new WaitForSeconds(0.5f); //Active each can one by one
        }
    }
}
