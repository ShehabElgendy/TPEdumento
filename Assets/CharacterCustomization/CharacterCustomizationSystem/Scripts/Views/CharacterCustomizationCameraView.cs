/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    public class CharacterCustomizationCameraView : MonoBehaviour {

        private Camera characterCustomizationCamera;
        private OutfitController outfitController;
        private CharacterBuilder characterBuilder;
        private Transform[] bones;

        private void Start() {

            characterCustomizationCamera = GetComponent<Camera>();
            outfitController = CharacterCustomizationFinderManager.GetOutfitController();
            characterBuilder = CharacterCustomizationFinderManager.GetCharacterBuilder();
            bones = characterBuilder.GetComponentsInChildren<Transform>();
        }

        private void Update() {

            if (outfitController.currentOutfitCategoryScriptableObject.targetBoneName == null)
                return;

            Transform child = null;
            for (int i = 0; i < bones.Length; i++) {
                if(bones[i] != null && bones[i].gameObject.name == outfitController.currentOutfitCategoryScriptableObject.targetBoneName) {
                    child = bones[i];
                    break;
                }
            }

            if (child != null) {
                characterCustomizationCamera.transform.LookAt(child);
                characterCustomizationCamera.transform.position = new Vector3(child.position.x, child.position.y, characterCustomizationCamera.transform.position.z);
                characterCustomizationCamera.fieldOfView = outfitController.currentOutfitCategoryScriptableObject.categoryZoom;
            }
        }
    }
}