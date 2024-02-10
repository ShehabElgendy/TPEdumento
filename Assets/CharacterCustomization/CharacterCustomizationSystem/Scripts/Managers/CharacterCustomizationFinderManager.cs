/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    public class CharacterCustomizationFinderManager {

        private static CharacterCustomizerSaveManager characterCustomizerSaveManager;

        private static CharacterBuilder characterBuilder;

        private static OutfitController outfitController;

        public static CharacterCustomizerSaveManager GetSaveManager() {

            if (characterCustomizerSaveManager == null) {
                //Try to Find
                characterCustomizerSaveManager = GameObject.FindObjectOfType<CharacterCustomizerSaveManager>();
            }

            if (characterCustomizerSaveManager == null) {
                //Try to Create
                GameObject characterCustomizerSaveManagerObject = new GameObject("CharacterCustomizerSaveManager");
                characterCustomizerSaveManager = characterCustomizerSaveManagerObject.AddComponent<CharacterCustomizerSaveManager>();
            }

            return characterCustomizerSaveManager;
        }

        public static CharacterBuilder GetCharacterBuilder() {

            if (characterBuilder == null) {
                //Try to Find
                characterBuilder = GameObject.FindObjectOfType<CharacterBuilder>();
            }

            if (characterBuilder == null) {
                Debug.Log("CharacterCustomizationFinderManager characterBuilder = null!");
            }

            return characterBuilder;
        }

        public static OutfitController GetOutfitController() {

            if (outfitController == null) {
                //Try to Find
                outfitController = GameObject.FindObjectOfType<OutfitController>();
            }

            if (outfitController == null) {
                Debug.Log("CharacterCustomizationFinderManager outfitController = null!");
            }

            return outfitController;
        }
    }
}

