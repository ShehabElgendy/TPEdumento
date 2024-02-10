/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComfortGames.CharacterCustomization {

    public class CharacterCustomizationSceneManagementView : MonoBehaviour
    {
        [SerializeField]
        string characterCustomizationGameSceneName = "CharacterCustomization01_Game";
        [SerializeField]
        string characterCustomizationGenderSceneName = "CharacterCustomization02_Gender";
        [SerializeField]
        string characterCustomizationMaleSceneName = "CharacterCustomization03_Male";
        [SerializeField]
        string characterCustomizationFemaleSceneName = "CharacterCustomization04_Female";

        public GameObject sceneMessagePopup;

        public void CustomizeCharacterScene() {

            CharacterCustomizerSaveData data = CharacterCustomizationFinderManager.GetSaveManager().LoadFile();
            if (data == null) {
                ChangeGenderScene();
            }
            else {
                if(data.gender == CharacterCustomizerSaveData.Gender.NOT_SET) {
                    ChangeGenderScene();
                    return;
                }
                if (data.gender == CharacterCustomizerSaveData.Gender.MALE) {
                    CustomizeMaleScene();
                    return;
                }
                if (data.gender == CharacterCustomizerSaveData.Gender.FEMALE) {
                    CustomizeFemaleScene();
                    return;
                }
            }
        }

        public void ChangeGenderScene() {

            if(Application.CanStreamedLevelBeLoaded(characterCustomizationGenderSceneName)) {
                SceneManager.LoadScene(characterCustomizationGenderSceneName);
            }
            else {
                Debug.Log("You need to add the scene '" + characterCustomizationGenderSceneName + "' to your build setings.");
                if (sceneMessagePopup != null)
                    sceneMessagePopup.SetActive(true);
            }
        }

        public void CustomizeMaleScene() {

            if (Application.CanStreamedLevelBeLoaded(characterCustomizationMaleSceneName)) {
                CharacterCustomizationFinderManager.GetSaveManager().HandleSetMaleButton();
                SceneManager.LoadScene(characterCustomizationMaleSceneName);
            }
            else {
                Debug.Log("You need to add the scene '" + characterCustomizationMaleSceneName + "' to your build setings.");
                if (sceneMessagePopup != null)
                    sceneMessagePopup.SetActive(true);
            }
        }

        public void CustomizeFemaleScene() {

            if (Application.CanStreamedLevelBeLoaded(characterCustomizationFemaleSceneName)) {
                CharacterCustomizationFinderManager.GetSaveManager().HandleSetFemaleButton();
                SceneManager.LoadScene(characterCustomizationFemaleSceneName);
            }
            else {
                Debug.Log("You need to add the scene '" + characterCustomizationFemaleSceneName + "' to your build setings.");
                if (sceneMessagePopup != null)
                    sceneMessagePopup.SetActive(true);
            }
        }

        public void ChangeGameScene() {

            if (Application.CanStreamedLevelBeLoaded(characterCustomizationGameSceneName)) {
                SceneManager.LoadScene(characterCustomizationGameSceneName);
            }
            else {
                Debug.Log("You need to add the scene '" + characterCustomizationGameSceneName + "' to your build setings.");
                if(sceneMessagePopup != null)
                    sceneMessagePopup.SetActive(true);
            }
        }
    }
}
