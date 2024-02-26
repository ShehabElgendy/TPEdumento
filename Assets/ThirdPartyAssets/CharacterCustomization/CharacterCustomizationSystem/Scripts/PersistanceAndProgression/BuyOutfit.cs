
using UnityEngine;

public class BuyOutfit : MonoBehaviour
{

    public OutfitScriptableObject outfitScriptableObject;

    private void Start()
    {

        if (outfitScriptableObject == null)
        {
            Debug.Log("BuyOutfit - outfitScriptableObject = null");
            return;
        }

        if (outfitScriptableObject.isLocked)
        {
            if (CharacterCustomizationFinderManager.GetOutfitController().becameUnlockedOutfits.Contains(outfitScriptableObject))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void HandleBuyOutfitButton()
    {
            HandleBuyOutfit();
            gameObject.SetActive(false);
    }

    private void HandleBuyOutfit()
    {

        if (outfitScriptableObject == null)
        {
            Debug.Log("BuyOutfit - outfitScriptableObject = null");
            return;
        }

        if (!outfitScriptableObject.isLocked)
        {
            Debug.Log("BuyOutfit - outfitScriptableObject must be configured to be Locked before it can be bought.");
            return;
        }

        CharacterCustomizationFinderManager.GetOutfitController().BuyOutfit(outfitScriptableObject);
    }
}

