using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadEndMenu : MonoBehaviour
{
    [SerializeField] float timeScoreS = 500;
    [SerializeField] float timeScoreA = 400;
    [SerializeField] float timeScoreB = 300;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerStats>() != null)
        {
            SaveStats.timeS = timeScoreS;
            SaveStats.timeA = timeScoreA;
            SaveStats.timeB = timeScoreB;

            EndMenu.Load();
        }
    }
}
