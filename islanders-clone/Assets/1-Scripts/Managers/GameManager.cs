using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameState _currentState;
    public enum GameState
    {
        MainMenu,
        InGame,
        GameOver
    }
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
    }

    private void Update()
    {
        switch (_currentState)
        {
            case GameState.MainMenu:
                break;
            case GameState.InGame:
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
    public void SetGameState(GameState gameState)
    {
        _currentState = gameState;
    }
    #endregion
}
