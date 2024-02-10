using UnityEngine;

public class DiscoverOutfitTrigger : InteractOnTrigger
{
    public void DiscoverBelt()
    {
        FindObjectOfType<DiscoverOutfit>().HandleDiscoverOutfitButton();
        Debug.Log("Doscover Belt !");
    }
}
