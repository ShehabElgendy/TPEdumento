using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string prompt;

    public string InteractionPrompt => prompt;


    public bool Interact(Interactor _interactor)
    {
        Debug.Log("OpeningDoor");
        return true;
    }
}
