using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentGameState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
    }

    public enum GameState
    {
        None,
        MainMenu,
        UnlockCursor,
        GamePlay
    }

    public void ChangeGameState(GameState state)
    {
        switch (CurrentGameState)
        {
            case GameState.None:
                break;
            case GameState.MainMenu:
                break;
            case GameState.UnlockCursor:
                break;
            case GameState.GamePlay:
                break;
            default:
                break;
        }

        CurrentGameState = state;
    }

    public void SwitchToGamePlay()
    {
        CurrentGameState = GameState.GamePlay;
    }

    public void SwitchToUnlockCursor()
    {
        CurrentGameState = GameState.UnlockCursor;
    }
}
