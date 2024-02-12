using UnityEngine;

    public class OutfitSaveView : MonoBehaviour {

        public void HandleSaveButton() {

            CharacterCustomizationFinderManager.GetSaveManager().HandleSaveButton(CharacterCustomizationFinderManager.GetCharacterBuilder());
        }

        public void HandleLoadButton() {

            CharacterCustomizationFinderManager.GetSaveManager().HandleLoadButton();
        }

        public void HandleDeleteSaveButton() {

            CharacterCustomizationFinderManager.GetSaveManager().HandleDeleteSaveButton();
        }
    }

