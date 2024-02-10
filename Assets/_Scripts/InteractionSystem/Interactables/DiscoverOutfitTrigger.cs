using UnityEngine;

public class DiscoverOutfitTrigger : InteractOnTrigger
{
    public void DiscoverBelt()
    {
        FindObjectOfType<DiscoverOutfit>().HandleDiscoverOutfitButton();
        Destroy(GetComponentInParent<BoxCollider>().gameObject);
    }
}
