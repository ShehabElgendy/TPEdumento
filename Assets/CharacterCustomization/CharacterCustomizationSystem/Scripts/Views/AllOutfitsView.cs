using UnityEngine;

    public class AllOutfitsView : MonoBehaviour {

        //Removes all outfits, but any defaults will be automatically added.
        public void RemoveAllOutfits() {

            OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();
            outfitController.RemoveAllOutfits();
        }
    }


