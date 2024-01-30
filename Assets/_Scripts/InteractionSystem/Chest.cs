using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteractable
{

    public UnityEvent ChestEvent;

    private bool isOpen;

    public bool Interact(Interactor _interactor)
    {

        ChestEvent?.Invoke();
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
    public void ChestInteraction()
    {
        Debug.Log("UnityEvent Chest");
    }
}
