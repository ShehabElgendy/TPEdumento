using UnityEngine;
using UnityEngine.Events;

public class Chest : InteractOnTrigger
{
    private bool isOpen;

    private void Update()
    {
        if (isOpen)
        {
            transform.parent.Rotate(0f, 75f * Time.deltaTime, 0f);
        }
    }

    public void ChestInteraction()
    {
        isOpen = !isOpen;
        Debug.Log("UnityEvent Chest");
    }

}
