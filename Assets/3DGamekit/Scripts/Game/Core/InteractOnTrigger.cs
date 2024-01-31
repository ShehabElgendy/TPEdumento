using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractOnTrigger : MonoBehaviour, IInteractable
{
    public LayerMask layers;

    public UnityEvent OnEnter, OnExit, OnInteract;

    private new Collider collider;

    private void Reset()
    {
        layers = LayerMask.NameToLayer("Everything");
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var renderer = GetComponent<InteractOnTrigger>().GetComponentInParent<Renderer>().material;
        renderer.EnableKeyword("_EMISSION");
        ExecuteOnEnter(other);
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        var renderer = GetComponent<InteractOnTrigger>().GetComponentInParent<Renderer>().material;
        renderer.DisableKeyword("_EMISSION");
        ExecuteOnExit(other);
    }

    protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }

    public bool Interact(Interactor _interactor)
    {
        OnInteract.Invoke();
        return _interactor;
    }
}
