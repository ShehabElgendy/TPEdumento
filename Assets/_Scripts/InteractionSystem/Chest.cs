using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string prompt;
    public string InteractionPrompt => prompt;

    private bool isOpen;

    public bool Interact(Interactor _interactor)
    {
        if (!isOpen)
        {
            Debug.Log("OpeningChest");
            isOpen = true;
        }
        return true;
    }
}
