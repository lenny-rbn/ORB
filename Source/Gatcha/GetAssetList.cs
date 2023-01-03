using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAssetList : MonoBehaviour
{
    public List<GameObject> gachaList;

    void Awake()
    {
        foreach (Transform child in transform)
            gachaList.Add(child.gameObject);
    }
}
