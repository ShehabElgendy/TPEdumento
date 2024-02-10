/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

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
}

