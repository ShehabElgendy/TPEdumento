using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameStates;

    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
    }

    public enum GameState
    {
        None,
        MainMenu,
        UnlockCursor,
        GamePlay
    }

    public void Update()
    {
        switch (GameStates)
        {
            case GameState.None:
                break;
            case GameState.MainMenu:
                break;
            case GameState.UnlockCursor:
                DisablePlayerInput();
                break;
            case GameState.GamePlay:
                EnablePlayerInput();
                break;
            default:
                break;
        }
    }

    public void DisablePlayerInput()
    {
            starterAssetsInputs?.SetCursorState(false);
    }

    public void EnablePlayerInput()
    {
            starterAssetsInputs?.SetCursorState(true);
    }
}
