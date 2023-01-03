using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI bounceNb;
    [SerializeField] private TextMeshProUGUI bestCombo;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI wallDestroy;
    [SerializeField] private GameObject secondScreen;
    [SerializeField] private GameObject firstScreen;
    [SerializeField] private Image playerDeathFull;
    [SerializeField] private Image ballDeathFull;

    private PlayerInput PlayerInput;
    private InputAction _gallery;
    private InputAction _next;
    private InputAction _hub;

    private Animator animator;

    private static int newIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerInput = GetComponent<PlayerInput>();

        _next = PlayerInput.actions.FindAction("Next");
        _next.performed += _ => Next();

        _gallery = PlayerInput.actions.FindAction("Gallery");
        _gallery.performed += _ => LaunchGallery();

        _hub = PlayerInput.actions.FindAction("Hub");
        _hub.performed += _ => LaunchHub();

        // UI Timer
        if (SaveStats.timeSec < 10 && SaveStats.timeCen < 10)
            time.SetText(string.Format("{0} : 0{1} : 0{2}", SaveStats.timeMin, SaveStats.timeSec, SaveStats.timeCen));
        else if(SaveStats.timeSec < 10)
            time.SetText(string.Format("{0} : 0{1} : {2}", SaveStats.timeMin, SaveStats.timeSec, SaveStats.timeCen));
        else if (SaveStats.timeCen < 10)
            time.SetText(string.Format("{0} : {1} : 0{2}", SaveStats.timeMin, SaveStats.timeSec, SaveStats.timeCen));
        else
            time.SetText(string.Format("{0} : {1} : {2}", SaveStats.timeMin, SaveStats.timeSec, SaveStats.timeCen));

        // Stats 
        bounceNb.SetText(string.Format("{0}", SaveStats.bouceNb));
        wallDestroy.SetText(string.Format("{0}", SaveStats.wallsDestroy));
        enemiesKilled.SetText(string.Format("{0}", SaveStats.enemyKilled));
        bestCombo.SetText(string.Format("{0}", SaveStats.bestCombo));

        // Special icones : no player/ball death
        if (SaveStats.playerNoDeath) 
            playerDeathFull.enabled = true;
        if (SaveStats.ballNoDeath)
            ballDeathFull.enabled = true;
    }

    public static void Load()
    {
        newIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(8);
    }

    public void Next()
    {
        if(firstScreen.activeInHierarchy)
        {
            firstScreen.SetActive(false);
            animator.SetTrigger("secondScreen");
        }
        else
        {
            GatchaLists.Save();
            SaveStats.Reload();
            if (newIndex < 8)
                SceneManager.LoadScene(newIndex);
            else 
                SceneManager.LoadScene(1);
        }
    }

    public void LaunchHub()
    {
        GatchaLists.Save();
        SaveStats.Reload();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(1); 
    }

    public void LaunchGallery()
    {
        GatchaLists.Save();
        SaveStats.Reload();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(9); 
    }
}
