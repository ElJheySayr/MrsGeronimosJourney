using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        Time.timeScale = 1.0f;
    }

    public enum GameState
    {
        Gameplay,
        Interaction,
        Pause,
        Gameover,
        End
    }
}
