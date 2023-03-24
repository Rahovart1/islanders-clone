using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; } 
    public InGameState CurrentInGameState { get; private set; }
    private int _currentPoints = 0;
    private int _maxPoints = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetGameState(GameState.MainMenu);
        SetInGameState(InGameState.Normal);
    }

    private void Update()
    {
        switch (CurrentGameState) 
        {
            case GameState.MainMenu:
                break;
            case GameState.InGame:
                switch (CurrentInGameState)
                {
                    case InGameState.Normal:
                        break;
                    case InGameState.Place:
                        break;
                    default:
                        break;
                }
                break;
            case GameState.PauseMenu:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }    
    }

    #region Private Methods
    #endregion

    #region Public Methods
    public void AddPoints(int points)
    {
        _currentPoints += points;
        Debug.Log("Points: " + _currentPoints);
    }
    public void SetGameState(GameState gameState)
    {
        CurrentGameState  = gameState;
    }

    public void SetInGameState(InGameState inGameState)
    {
        CurrentInGameState = inGameState;
    }
    #endregion
}
