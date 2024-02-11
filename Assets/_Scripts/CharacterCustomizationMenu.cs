using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationMenu : MonoBehaviour
{
    StarterAssetsInputs player;
    private void Start()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.UnlockCursor;
        player = FindObjectOfType<StarterAssetsInputs>();
    }
    public void StartGameState()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.GamePlay;
    }

    private void Update()
    {
        //Rotate();
    }

    public void Rotate()
    {
        player.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
    }
}
