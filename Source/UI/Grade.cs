using UnityEngine;
using TMPro;

public class Grade : MonoBehaviour
{
    [SerializeField] private Color colorS;
    [SerializeField] private Color colorA;
    [SerializeField] private Color colorB;
    [SerializeField] private Color colorC;

    [SerializeField] private int nbChestS = 10;
    [SerializeField] private int nbChestA = 7;
    [SerializeField] private int nbChestB = 4;
    [SerializeField] private int nbChestC = 2;

    private TextMeshProUGUI grade;
    private int timeInSecond;

    void Start()
    {
        grade = GetComponentInChildren<TextMeshProUGUI>();

        timeInSecond = SaveStats.timeMin * 60 + SaveStats.timeSec;

        if (timeInSecond < SaveStats.timeS)
            SaveStats.chestNB += nbChestS;
        else if (timeInSecond < SaveStats.timeA)
            SaveStats.chestNB += nbChestA;
        else if (timeInSecond < SaveStats.timeB)
            SaveStats.chestNB += nbChestB;
        else
            SaveStats.chestNB += nbChestC;
    }

    void Update()
    {

        if (timeInSecond < SaveStats.timeS)
        {
            grade.SetText("S");
            grade.color = colorS;
            ChestEndMenu.nbChestToOpen = nbChestS; //variable to display the right number of can in the UI of the EndScreen (script ChestEndMenu)
        }
        else if(timeInSecond < SaveStats.timeA)
        {
            grade.SetText("A");
            grade.color = colorA;
            ChestEndMenu.nbChestToOpen = nbChestA;

        }
        else if (timeInSecond < SaveStats.timeB)
        {
            grade.SetText("B");
            grade.color = colorB;
            ChestEndMenu.nbChestToOpen = nbChestB;
        }
        else
        {
            grade.SetText("C");
            grade.color = colorC;
            ChestEndMenu.nbChestToOpen = nbChestC;
        }
    }
}
