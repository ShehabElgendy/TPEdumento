/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    public class AllOutfitsView : MonoBehaviour {

        //Removes all outfits, but any defaults will be automatically added.
        public void RemoveAllOutfits() {

            OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();
            outfitController.RemoveAllOutfits();
        }
    }
}

