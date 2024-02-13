using HoaxGames;
using Lean.Touch;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCustomizationMenu : MonoBehaviour
{
    StarterAssetsInputs player;
    private void Start()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.UnlockCursor;
        player = FindObjectOfType<StarterAssetsInputs>();

        player.AddComponent<LeanTwistRotateAxis>();
        player.GetComponent<FootIK>().enabled = false;
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
