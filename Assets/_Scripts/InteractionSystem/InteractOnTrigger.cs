using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractOnTrigger : MonoBehaviour, IInteractable
{
    public LayerMask layers;

    public UnityEvent OnEnter, OnExit, OnInteract, OnInteractFinish;

    protected new Collider collider;

    protected bool isInteracted;

    private void Reset()
    {
        layers = LayerMask.NameToLayer("Everything");
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnableEmission();
        ExecuteOnEnter(other);
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        DisableEmission();
        ExecuteOnExit(other);
    }

    protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }

    public bool Interact(Interactor _interactor)
    {
        if (isInteracted)
        {
            DisableEmission();
        }
        else if (!isInteracted)
        {
            EnableEmission();
        }

        isInteracted = !isInteracted;
        OnInteract.Invoke();

        return true;
    }

    protected virtual void InteractFinish()
    {
        OnInteract.Invoke();
    }

    protected virtual void DisableEmission()
    {
        var renderer = GetComponent<InteractOnTrigger>().GetComponentInParent<Renderer>().material;
        renderer.DisableKeyword("_EMISSION");
    }

    protected virtual void EnableEmission()
    {
        var renderer = GetComponent<InteractOnTrigger>().GetComponentInParent<Renderer>().material;
        renderer.EnableKeyword("_EMISSION");
    }
}
