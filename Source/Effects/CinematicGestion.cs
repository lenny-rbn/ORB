using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;

public class CinematicGestion : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private TextMeshProUGUI skipText;

    private PlayerInput PlayerInput;
    private InputAction _skip;

    private bool skip;

    private void CheckSkip()
    {
        if (skip)
        {
            StartCoroutine(Skip());
        }
        else
        {
            skipText.gameObject.SetActive(true);
            skip = true;
        }
    }

    private IEnumerator Skip()
    {
        StopCoroutine(DoCinematicGestion());
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    IEnumerator DoCinematicGestion()
    {
        yield return new WaitForSeconds(16f);
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _skip = PlayerInput.actions.FindAction("Skip");
        _skip.performed += _ => CheckSkip();
        StartCoroutine(DoCinematicGestion());
    }
}
