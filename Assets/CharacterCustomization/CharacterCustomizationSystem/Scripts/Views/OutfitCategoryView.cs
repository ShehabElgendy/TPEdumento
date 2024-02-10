/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace ComfortGames.CharacterCustomization {

    public class OutfitCategoryView : MonoBehaviour {

        public OutfitCategoryScriptableObject outfitCategoryScriptableObject;
        public OutfitCategoryContainerView targetOutfitCategoryContainerView;

        public Text nameText;
        public Toggle changeToggle;
    
        public void Initialize(OutfitCategoryScriptableObject inOutfitCategoryScriptableObject) {

            outfitCategoryScriptableObject = inOutfitCategoryScriptableObject;
            nameText.text = outfitCategoryScriptableObject.categoryName;
        }

        public void HandleCategoryToggle(bool isOn) {

            targetOutfitCategoryContainerView.gameObject.SetActive(isOn);

            if (isOn) {
                OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();
                outfitController.currentOutfitCategoryScriptableObject = outfitCategoryScriptableObject;
            }
        }
    }
}

