using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] private float speedLerp;
    [SerializeField] private Vector3 canEnd;
    [SerializeField] private Vector3 canStart;
    [SerializeField] private Animator splash;
    [SerializeField] private GameObject can;
    [SerializeField] private Animator anim;
    [SerializeField] private Image cross;
    [SerializeField] private GameObject shake;

    [System.NonSerialized] public int tapToOpen;

    private bool isOpening;
    private bool canShowed;

    private int tapDone;

    private float lerpCan;
    private Vector3 saveCamPos;

    private Camera cam;
    private OrangeMove ball;
    private MovePlayer player;
    private ProbaGatcha proba;
    private GatchaAsset asset;
    private Galery gallery;

    void Start()
    {
        player = FindObjectOfType<MovePlayer>();
        ball = FindObjectOfType<OrangeMove>();
        proba = GetComponent<ProbaGatcha>();
        cam = FindObjectOfType<Camera>();
        asset = GetComponent<GatchaAsset>();
        gallery = GetComponent<Galery>();

        tapDone = 0;
        lerpCan = 0;

        StartCoroutine(Tap());
        cross.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isOpening)
        {
            player.OnPause = true;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (tapDone >= tapToOpen)
            {
                StartCoroutine(Open());
                SaveStats.chestNB--;
                tapDone = 0;
            }
        }
    }

    IEnumerator CanArrive()
    {
        while(lerpCan < 1)
        {
            can.transform.position = Vector3.Lerp(canStart, canEnd, lerpCan);
            lerpCan += speedLerp * Time.deltaTime;
            yield return null;
        }
        canShowed = true;
    }

    IEnumerator Tap()
    {
        while(true)
        {
            if (isOpening)
                shake.SetActive(true);

            if (player.GetHit() && isOpening && canShowed)
            {
                tapDone++;
                player.SetHit(false);
                anim.SetTrigger("Shake");
                SoundGatcha.playShake = true;
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    IEnumerator Open()
    {
        shake.SetActive(false);
        anim.SetTrigger("Open");
        isOpening = false;
        yield return new WaitForSeconds(0.7f);
        SoundGatcha.playOpen = true;
        yield return new WaitForSeconds(1.3f);
        splash.SetTrigger("Splash");
        yield return new WaitForSeconds(3f);
        SoundGatcha.playRoll = true;
        asset.ShowStatue(proba.rndChoose);
        yield return new WaitForSeconds(6f);

        while(!player.GetHit())
            yield return null;

        player.SetHit(false);
        can.transform.position = canStart;
        asset.UnShowStatue(proba.rndChoose);
        gallery.UpdateGallery();
        lerpCan = 0;
        player.OnPause = false;
        cam.transform.position = saveCamPos;
        canShowed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovePlayer>() != null)
            player.blockInputBall = true;

        if (!ball.GetinHand())
            ball.SetIsRecalled(true);

        cross.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        player.blockInputBall = true;
        if (player.GetHit() && SaveStats.chestNB > 0 && !player.OnPause)
        {
            isOpening = true;
            proba.ChooseAward();
            player.SetHit(false);
            StartCoroutine(CanArrive());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<MovePlayer>() != null)
            player.blockInputBall = false;

        cross.gameObject.SetActive(false);
    }
}
