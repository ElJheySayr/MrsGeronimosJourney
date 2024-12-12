using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSoound;
    public GameObject levelScreenObject;

    public MenuState menuState;

    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public enum MenuState
    {
        MainMenu,
        Settings,
        Credits
    }

    public virtual void StartGame()
    {
        clickSound();
        levelScreenObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public virtual void Settings()
    {
        clickSound();
        menuState = MenuState.Settings;
    }

    public virtual void Credits()
    {
        clickSound();
        menuState = MenuState.Credits;
    }

    public virtual void ExitApplication()
    {
        clickSound();
        Application.Quit();
    }

    public virtual void Back()
    {
        clickSound();
        menuState = MenuState.MainMenu;
    }

    public virtual void clickSound()
    {
        if(!clickSoound.isPlaying)
        {
            clickSoound.Play();
        }
    }
}
