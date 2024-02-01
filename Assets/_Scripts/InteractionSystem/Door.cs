using UnityEngine;
using UnityEngine.Events;

public class Door : InteractOnTrigger
{
    private bool isOpen;

    private void Update()
    {
        if (isOpen)
        {
            transform.parent.Rotate(0f, -75f * Time.deltaTime, 0f);
        }
    }

    public void DoorInteraction()
    {
        isOpen = !isOpen;
        Debug.Log("Door");
    }
}
