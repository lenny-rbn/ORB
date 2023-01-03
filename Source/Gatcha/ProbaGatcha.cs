using UnityEngine;
using UnityEngine.UI;

public class ProbaGatcha : MonoBehaviour
{
    [SerializeField] public Color festiveColor;
    [SerializeField] public Color explosiveColor;
    [SerializeField] public Color devilishColor;
    [SerializeField] public Color insanelColor;
    [SerializeField] private ParticleSystem particule;

    [SerializeField] private Color festiveSplashColor;
    [SerializeField] private Color explosiveSplashColor;
    [SerializeField] private Color devilishSplashColor;
    [SerializeField] private Color insaneSplashColor;
    [SerializeField] private GameObject splashs;

    public enum Rarity
    { 
        FESTIVE,
        EXPLOSIVE,
        DEVILISH,
        INSANE,
    };

    public int rndChoose;
    private int rndRarity;
    private int festiveNB;
    private int explosiveNB;
    private int devilishNB;
    private int insanelNB;

    public gatchaObject gatchaObject;
    public Rarity rarity;

    private VendingMachine vending;
    private Image[] images;

    void Start()
    {
        festiveNB = GatchaLists.lists.festiveObjects.Length;
        explosiveNB = GatchaLists.lists.explosiveObjects.Length;
        devilishNB = GatchaLists.lists.devilishObjects.Length;
        insanelNB = GatchaLists.lists.insaneObjects.Length;
        vending = GetComponent<VendingMachine>();

        images = splashs.GetComponentsInChildren<Image>(true);
    }

    public void ChooseAward()
    {
        rndRarity = Random.Range(1, 100);

        if (rndRarity < 56)
        {
            rndChoose = Random.Range(0, festiveNB);
            gatchaObject = GatchaLists.lists.festiveObjects[rndChoose];
            GatchaLists.lists.festiveObjects[rndChoose].unlocked = true;

            vending.tapToOpen = 4;
            particule.startColor = festiveColor;
            rarity = Rarity.FESTIVE;

            for (int i = 0; i < images.Length; i++)
                images[i].color = festiveSplashColor;
        }
        else if (rndRarity < 88)
        {
            rndChoose = Random.Range(0, explosiveNB);
            gatchaObject = GatchaLists.lists.explosiveObjects[rndChoose];
            GatchaLists.lists.explosiveObjects[rndChoose].unlocked = true;

            vending.tapToOpen = 5;
            particule.startColor = explosiveColor;
            rarity = Rarity.EXPLOSIVE;

            for (int i = 0; i < images.Length; i++)
                images[i].color = explosiveSplashColor;
        }
        else if (rndRarity < 98)
        {
            rndChoose = Random.Range(0, devilishNB);
            gatchaObject = GatchaLists.lists.devilishObjects[rndChoose];
            GatchaLists.lists.devilishObjects[rndChoose].unlocked = true;

            vending.tapToOpen = 6;
            particule.startColor = devilishColor;
            rarity = Rarity.DEVILISH;

            for (int i = 0; i < images.Length; i++)
                images[i].color = devilishSplashColor;
        }
        else
        {
            rndChoose = Random.Range(0, insanelNB);
            gatchaObject = GatchaLists.lists.insaneObjects[rndChoose];
            GatchaLists.lists.insaneObjects[rndChoose].unlocked = true;

            vending.tapToOpen = 7;
            particule.startColor = insanelColor;
            rarity = Rarity.INSANE;

            for (int i = 0; i < images.Length; i++)
                images[i].color = insaneSplashColor;
        }

        GatchaLists.Save();
    }
}
