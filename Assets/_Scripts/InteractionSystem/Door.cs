using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{

    public UnityEvent DoorEvent;

    public UnityEvent Event { get => DoorEvent; }

    public bool Interact(Interactor _interactor)
    {
        DoorEvent?.Invoke();
        return true;
    }

    public void DoorInteraction()
    {
        Debug.Log("UnityEvent Door");
    }
}
