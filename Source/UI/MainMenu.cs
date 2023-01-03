using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject secondScreen;

    private PlayerInput PlayerInput;
    private InputAction _A;
    private InputAction _B;

    void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _A = PlayerInput.actions.FindAction("Launch");
        _A.performed += _ => A_Button();

        _B = PlayerInput.actions.FindAction("Quit");
        _B.performed += _ => B_Button();

    }

    public void A_Button()
    {
        if(secondScreen.activeInHierarchy) // = Screen of confirmation to quit the game
            Application.Quit(); //Quit the Game
        else
            SceneManager.LoadScene(10); //Load the Hub
    }

    public void B_Button()
    {
        if (secondScreen.activeInHierarchy)
            secondScreen.SetActive(false);
        else
            secondScreen.SetActive(true);
    }
}
