/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEditor;

namespace ComfortGames.CharacterCustomization {

#if UNITY_EDITOR
    public class FemaleCharacterMatrix : CharacterMatrix {

        [MenuItem("CharacterCustomization/FemaleCharacterMatrix")]
        static void Init() {
            FemaleCharacterMatrix window = (FemaleCharacterMatrix)EditorWindow.GetWindow(typeof(FemaleCharacterMatrix));
            window.Show();
        }

        protected override string GetMatrixName() {
            return "Female Outfits Collision Matrix";
        }

        protected override CharacterBuilder.CharacterType GetCharacterType() {
            return CharacterBuilder.CharacterType.FEMALE;
        }
    }
#endif
}

