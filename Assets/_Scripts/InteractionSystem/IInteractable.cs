using UnityEngine.Events;

public interface IInteractable
{
    public bool Interact(Interactor _interactor);

    public UnityEvent Event { get; }
}
