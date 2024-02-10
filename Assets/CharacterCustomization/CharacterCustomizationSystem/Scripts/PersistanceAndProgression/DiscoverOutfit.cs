using UnityEngine;

    public class DiscoverOutfit : MonoBehaviour {

        public OutfitScriptableObject outfitScriptableObject;

        private void Start() {

            if (outfitScriptableObject == null) {
                Debug.Log("DiscoverOutfit - outfitScriptableObject = null");
                return;
            }
            
            if(outfitScriptableObject.isInvisible) {
                if (CharacterCustomizationFinderManager.GetOutfitController().becameVisibleOutfits.Contains(outfitScriptableObject)) {
                    gameObject.SetActive(false);
                }
            }
        }

        public void HandleDiscoverOutfitButton() {

            HandleDiscoverOutfit();
            gameObject.SetActive(false);
        }

        private void HandleDiscoverOutfit() {

            if(outfitScriptableObject == null) {
                Debug.Log("DiscoverOutfit - outfitScriptableObject = null");
                return;
            }

            if (!outfitScriptableObject.isInvisible) {
                Debug.Log("DiscoverOutfit - outfitScriptableObject must be configured to be invisible to be discoverable.");
                return;
            }

            CharacterCustomizationFinderManager.GetOutfitController().ShowOutfit(outfitScriptableObject);
        }
    }
