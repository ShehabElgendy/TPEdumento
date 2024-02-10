using UnityEngine;
using UnityEngine.UI;

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

