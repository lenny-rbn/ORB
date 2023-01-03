using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Galery : MonoBehaviour
{
    [SerializeField] private GetAssetList festiveList;
    [SerializeField] private GetAssetList explosiveList;
    [SerializeField] private GetAssetList devilishList;
    [SerializeField] private GetAssetList insaneList;

    [SerializeField] private GetAssetList festiveListOpening;
    [SerializeField] private GetAssetList explosiveListOpening;
    [SerializeField] private GetAssetList devilishListOpening;
    [SerializeField] private GetAssetList insaneListOpening;

    [SerializeField] private TextMeshProUGUI textCanCounter;

    public List<GameObject> festive;
    public List<GameObject> explosive;
    public List<GameObject> devilish;
    public List<GameObject> insane;

    public List<GameObject> festiveOpening;
    public List<GameObject> explosiveOpening;
    public List<GameObject> devilishOpening;
    public List<GameObject> insaneOpening;

    private void Start()
    {
        festive = festiveList.gachaList;
        explosive = explosiveList.gachaList;
        devilish = devilishList.gachaList;
        insane = insaneList.gachaList;

        festiveOpening = festiveListOpening.gachaList;
        explosiveOpening = explosiveListOpening.gachaList;
        devilishOpening = devilishListOpening.gachaList;
        insaneOpening = insaneListOpening.gachaList;

        GatchaLists.Load();

        #region lists
        #region Normal
        for (int i = 0; i < festive.Count; i++)
        {
            festive[i].AddComponent<Info>();
            festive[i].GetComponent<Info>().name = GatchaLists.lists.festiveObjects[i].name;
            festive[i].GetComponent<Info>().description = GatchaLists.lists.festiveObjects[i].description;
        }
        for (int i = 0; i < explosive.Count; i++)
        {
            explosive[i].AddComponent<Info>();
            explosive[i].GetComponent<Info>().name = GatchaLists.lists.explosiveObjects[i].name;
            explosive[i].GetComponent<Info>().description = GatchaLists.lists.explosiveObjects[i].description;
        }
        for (int i = 0; i < devilish.Count; i++)
        {
            devilish[i].AddComponent<Info>();
            devilish[i].GetComponent<Info>().name = GatchaLists.lists.devilishObjects[i].name;
            devilish[i].GetComponent<Info>().description = GatchaLists.lists.devilishObjects[i].description;
        }
        for (int i = 0; i < insane.Count; i++)
        {
            insane[i].AddComponent<Info>();
            insane[i].GetComponent<Info>().name = GatchaLists.lists.insaneObjects[i].name;
            insane[i].GetComponent<Info>().description = GatchaLists.lists.insaneObjects[i].description;
        }
        #endregion

        #region Opening
        for (int i = 0; i < festiveOpening.Count; i++)
        {
            festiveOpening[i].AddComponent<Info>();
            festiveOpening[i].GetComponent<Info>().name = GatchaLists.lists.festiveObjects[i].name;
            festiveOpening[i].GetComponent<Info>().description = GatchaLists.lists.festiveObjects[i].description;
        }
        for (int i = 0; i < explosiveOpening.Count; i++)
        {
            explosiveOpening[i].AddComponent<Info>();
            explosiveOpening[i].GetComponent<Info>().name = GatchaLists.lists.explosiveObjects[i].name;
            explosiveOpening[i].GetComponent<Info>().description = GatchaLists.lists.explosiveObjects[i].description;
        }
        for (int i = 0; i < devilishOpening.Count; i++)
        {
            devilishOpening[i].AddComponent<Info>();
            devilishOpening[i].GetComponent<Info>().name = GatchaLists.lists.devilishObjects[i].name;
            devilishOpening[i].GetComponent<Info>().description = GatchaLists.lists.devilishObjects[i].description;
        }
        for (int i = 0; i < insaneOpening.Count; i++)
        {
            insaneOpening[i].AddComponent<Info>();
            insaneOpening[i].GetComponent<Info>().name = GatchaLists.lists.insaneObjects[i].name;
            insaneOpening[i].GetComponent<Info>().description = GatchaLists.lists.insaneObjects[i].description;
        }

        #endregion
        #endregion
        UpdateGallery();
    }
    public void UpdateGallery()
    {
        for(int i = 0; i < festive.Count; i++)
            if(GatchaLists.lists.festiveObjects[i].unlocked)
                festive[i].SetActive(true);

        for (int i = 0; i < explosive.Count; i++)
            if (GatchaLists.lists.explosiveObjects[i].unlocked)
                explosive[i].SetActive(true);

        for (int i = 0; i < devilish.Count; i++)
            if (GatchaLists.lists.devilishObjects[i].unlocked)
                devilish[i].SetActive(true);

        for (int i = 0; i < insane.Count; i++)
            if (GatchaLists.lists.insaneObjects[i].unlocked)
                insane[i].SetActive(true);

        if (textCanCounter != null)
            textCanCounter.text = SaveStats.chestNB.ToString();
    }

    private void OnDestroy()
    {
        GatchaLists.Save();
    }
}
