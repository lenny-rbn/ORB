using UnityEngine;
using TMPro;


public class ComboUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerStats player;
    private Animator anim;

    private int saveCombo;


    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<PlayerStats>();
        anim = transform.parent.GetComponent<Animator>();
        saveCombo = 0;
    }

    void Update()
    {

        if((int)player.combo != saveCombo)
        {
            if(player.combo % 10 == 0 && player.combo != 0)
            {
                anim.SetTrigger("bigcombo");
            }
            else
            {
                anim.SetTrigger("combo");
            }

            saveCombo = (int)player.combo;
        }

        if (player.combo > 0)
            text.SetText(string.Format("x{0}", player.combo));
        else
            text.SetText(string.Format(""));
    }
}
