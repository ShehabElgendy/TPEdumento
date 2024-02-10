/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    public class BuyOutfit : MonoBehaviour {

        public OutfitScriptableObject outfitScriptableObject;

        private void Start() {

            if (outfitScriptableObject == null) {
                Debug.Log("BuyOutfit - outfitScriptableObject = null");
                return;
            }

            if (outfitScriptableObject.isLocked) {
                if (CharacterCustomizationFinderManager.GetOutfitController().becameUnlockedOutfits.Contains(outfitScriptableObject)) {
                    gameObject.SetActive(false);
                }
            }
        }

        public void HandleBuyOutfitButton() {

            HandleBuyOutfit();
            gameObject.SetActive(false);
        }

        private void HandleBuyOutfit() {

            if (outfitScriptableObject == null) {
                Debug.Log("BuyOutfit - outfitScriptableObject = null");
                return;
            }

            if (!outfitScriptableObject.isLocked) {
                Debug.Log("BuyOutfit - outfitScriptableObject must be configured to be Locked before it can be bought.");
                return;
            }

            CharacterCustomizationFinderManager.GetOutfitController().BuyOutfit(outfitScriptableObject);
        }
    }
}

