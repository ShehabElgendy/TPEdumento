/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    public class CharacterCustomizationAssetManager {

        public static OutfitScriptableObject[] GetOutfitScriptableObjects(CharacterBuilder.CharacterType characterType) {

            string modelPath = "OutfitModels_Male";
            if (characterType == CharacterBuilder.CharacterType.FEMALE) {
                modelPath = "OutfitModels_Female";
            }

            return Resources.LoadAll<OutfitScriptableObject>(modelPath);
        }

        public static OutfitCategoryScriptableObject[] GetOutfitCategoryScriptableObject(CharacterBuilder.CharacterType characterType) {

            string outfitCategoryModelPath = "OutfitsCategoryData_Male";
            if (characterType == CharacterBuilder.CharacterType.FEMALE) {
                outfitCategoryModelPath = "OutfitsCategoryData_Female";
            }

            return Resources.LoadAll<OutfitCategoryScriptableObject>(outfitCategoryModelPath);
        }

        public static OutfitsCollisionScriptableObject GetOutfitsCollisionScriptableObject(CharacterBuilder.CharacterType characterType) {

            string collisionPath = "OutfitsCollisionData/OutfitsCollision_Male";
            if (characterType == CharacterBuilder.CharacterType.FEMALE) {
                collisionPath = "OutfitsCollisionData/OutfitsCollision_Female";
            }

            return Resources.Load<OutfitsCollisionScriptableObject>(collisionPath);
        }

        public static string GetIconGeneratorPath(string iconName) {

            return "Assets/CharacterCustomization/GeneratedAssets/" + iconName + ".png";
        }

        public static string GetPrefabGeneratorPath(string prefabName) {

            return "Assets/CharacterCustomization/GeneratedAssets/" + prefabName + ".prefab";
        }
    }
}

