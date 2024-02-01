using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Transform interactionPoint;

    [SerializeField]
    private float interactionPointRadius = 0.5f;

    [SerializeField]
    private LayerMask interactableMask;

    [SerializeField]
    private int interactablesFound;

    private readonly Collider[] colliders = new Collider[3];

    private IInteractable interactable;

    public virtual void Interact()
    {
        interactablesFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (interactablesFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            interactable?.Interact(this);
        }

        interactable = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
