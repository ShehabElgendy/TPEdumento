using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.GameStates = GameManager.GameState.UnlockCursor;
    }
    public void StartGameState()
    {
        GameManager.Instance.GameStates = GameManager.GameState.GamePlay;
    }
}
