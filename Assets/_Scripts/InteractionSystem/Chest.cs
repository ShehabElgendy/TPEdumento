using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteractable
{

    public UnityEvent ChestEvent;

    public UnityEvent Event { get => ChestEvent; }

    private bool isOpen;

    public bool Interact(Interactor _interactor)
    {
        if (!isOpen)
        {
            ChestEvent?.Invoke();
            isOpen = true;
        }
        return true;
    }

    public void ChestInteraction()
    {
        Debug.Log("UnityEvent Chest");
    }
}
