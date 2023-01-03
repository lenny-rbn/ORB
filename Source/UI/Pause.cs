using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject quit;

    private MovePlayer player;
    private float saveTimeScale;
    void Start()
    {
        player = FindObjectOfType<MovePlayer>();
        saveTimeScale = Time.timeScale;
    }

    void Update()
    {
        if(player.pause) //Player launches the pause Menu
        {
            LoadPause();
        }

        if (player.quit && help.activeInHierarchy)
        {
            help.SetActive(false);
            quit.SetActive(false);
        }
    }

    public void LoadPause()
    {
        if (!pause.activeInHierarchy)
        {
            GameEvent.openPause = true;
            pause.SetActive(true);
            player.OnPause = true;
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        pause.SetActive(false);
        player.OnPause = false;
        Time.timeScale = saveTimeScale;
        SaveStats.Reload();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        GameEvent.closePause = true;
        pause.SetActive(false);
        player.OnPause = false;
        Time.timeScale = saveTimeScale;
    }

    public void Help()
    {
        help.SetActive(true);
        quit.SetActive(true);
    }

    public void MainMenu()
    {
        pause.SetActive(false);
        player.OnPause = false;
        Time.timeScale = saveTimeScale;
        SaveStats.Reload();
        SceneManager.LoadScene(0);
    }
}
