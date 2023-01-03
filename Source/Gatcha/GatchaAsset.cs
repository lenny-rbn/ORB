using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatchaAsset : MonoBehaviour
{
    [SerializeField] private GameObject can;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDescription;

    private Galery galery;

    private ProbaGatcha proba;
    public List<GameObject> festive;
    public List<GameObject> explosive;
    public List<GameObject> devilish;
    public List<GameObject> insane;

    private void Start()
    {
        galery = GetComponent<Galery>();
        festive = galery.festiveOpening;
        explosive = galery.explosiveOpening;
        devilish = galery.devilishOpening;
        insane = galery.insaneOpening;

        proba = GetComponent<ProbaGatcha>();
    }

    public void ShowStatue(int id)
    {
        can.SetActive(false);
        textName.color = Color.white;
        textDescription.color = Color.white;

        switch (proba.rarity)
        {
            case (ProbaGatcha.Rarity.FESTIVE):
                festive[id].SetActive(true);

                textName.text = festive[id].GetComponent<Info>().name;
                textDescription.text = festive[id].GetComponent<Info>().description;

                textName.color = proba.festiveColor;
                textDescription.color = proba.festiveColor;
                break;

            case (ProbaGatcha.Rarity.EXPLOSIVE):
                explosive[id].SetActive(true);

                textName.text = explosive[id].GetComponent<Info>().name;
                textDescription.text = explosive[id].GetComponent<Info>().description;

                textName.color = proba.explosiveColor;
                textDescription.color = proba.explosiveColor;
                break;

            case (ProbaGatcha.Rarity.DEVILISH):
                devilish[id].SetActive(true);

                textName.text = devilish[id].GetComponent<Info>().name;
                textDescription.text = devilish[id].GetComponent<Info>().description;

                textName.color = proba.devilishColor;
                textDescription.color = proba.devilishColor;
                break;

            case (ProbaGatcha.Rarity.INSANE):
                insane[id].SetActive(true);

                textName.text = insane[id].GetComponent<Info>().name;
                textDescription.text = insane[id].GetComponent<Info>().description;

                textName.color = proba.insanelColor;
                textDescription.color = proba.insanelColor;
                break;

            default:
                break;
        }
        textName.gameObject.SetActive(true);
        textDescription.gameObject.SetActive(true);
    }

    public void UnShowStatue(int id)
    {
        if (proba.rarity == ProbaGatcha.Rarity.FESTIVE)
        {
            festive[id].SetActive(false);
        }
        else if (proba.rarity == ProbaGatcha.Rarity.EXPLOSIVE)
        {
            explosive[id].SetActive(false);
        }
        else if (proba.rarity == ProbaGatcha.Rarity.DEVILISH)
        {
            devilish[id].SetActive(false);
        }
        else if (proba.rarity == ProbaGatcha.Rarity.INSANE)
        {
            insane[id].SetActive(false);
        }
        can.SetActive(true);
        textName.gameObject.SetActive(false);
        textDescription.gameObject.SetActive(false);
        textName.color = Color.white;
        textDescription.color = Color.white;
    }
}
