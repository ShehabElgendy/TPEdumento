using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{

    public UnityEvent DoorEvent;

    private bool isOpen;

    public bool Interact(Interactor _interactor)
    {

        DoorEvent?.Invoke();
        isOpen = !isOpen;

        return true;
    }

    private void Update()
    {
        if (isOpen)
        {
            transform.Rotate(0f, 75f * Time.deltaTime, 0f);
        }
    }

    public void DoorInteraction()
    {
        Debug.Log("UnityEvent Door");
    }
}
