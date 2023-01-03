using System.Collections;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private bool wantMin;
    [SerializeField] private bool wantSec;
    [SerializeField] private bool wantCen;

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (wantCen)
            StartCoroutine(TimeIncrease());
    }

    void Update()
    {

        // Display Time //
        if (wantMin)
            text.SetText(string.Format("{0} :", SaveStats.timeMin));

        if (wantSec)
        {
            if (SaveStats.timeSec < 10)
                text.SetText(string.Format("0{0} :", SaveStats.timeSec));
            else
                text.SetText(string.Format("{0} :", SaveStats.timeSec));
        }

        if (wantCen)
        {
            if (SaveStats.timeCen < 10)
                text.SetText(string.Format("0{0}", SaveStats.timeCen));
            else
                text.SetText(string.Format("{0}", SaveStats.timeCen));
        }


        // Update Time //
        int secInt = (int)SaveStats.time; 
        float secFloat = (float)secInt;
        SaveStats.timeCen = (int)((SaveStats.time - secFloat) * 100);  // == (float second - int second) * 100
        SaveStats.timeSec = secInt;

        if (SaveStats.time >= 60) //Increase SaveStats.timeMin
        {
            SaveStats.time = 0;
            SaveStats.timeMin += 1;
        }
    }

    IEnumerator TimeIncrease()
    {
        while(true)
        {
            SaveStats.time += Time.deltaTime;
            yield return null;
        }
    }
}
