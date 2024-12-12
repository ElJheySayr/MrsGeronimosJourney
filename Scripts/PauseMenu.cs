using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource clickSoound;

    public MenuState menuState;
    public GameObject pausePanel;

    protected virtual void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && menuState == MenuState.None && GameManager.instance.gameState == GameManager.GameState.Gameplay)
        {
            Pause();
        }
    }

    public enum MenuState
    {
        None,
        PauseMenu,
        Settings
    }

    public virtual void Pause()
    {
        clickSound();
        menuState = MenuState.PauseMenu;
        GameManager.instance.gameState = GameManager.GameState.Pause;
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
    }

    public virtual void Resume()
    {
        clickSound();
        menuState = MenuState.None;
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        Time.timeScale = 1f;
    }

    public virtual void Settings()
    {
        clickSound();
        menuState = MenuState.Settings;
    }

    public virtual void MainMenu()
    {
        clickSound();
        SceneManager.LoadScene("MainMenu");
    }

    public virtual void Back()
    {
        clickSound();
        menuState = MenuState.PauseMenu;
    }

    public virtual void TryAgain()
    {
        clickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public virtual void clickSound()
    {
        if(!clickSoound.isPlaying)
        {
            clickSoound.Play();
        }
    }
}
