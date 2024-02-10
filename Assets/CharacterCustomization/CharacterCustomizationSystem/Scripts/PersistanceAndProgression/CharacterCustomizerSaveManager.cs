using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

    [System.Serializable]
    public class GenderSaveData {

        public List<int> becameVisibleOutfits = new List<int>();
        public List<int> becameUnlockedOutfits = new List<int>();
        public List<int> equippedOutfits = new List<int>();

        public GenderSaveData(List<int> newInvisibleOutfits, List<int> newLockedOutfits, List<int> newEquippedOutfits) {

            becameVisibleOutfits = newInvisibleOutfits;
            becameUnlockedOutfits = newLockedOutfits;
            equippedOutfits = newEquippedOutfits;
        }
    }

    [System.Serializable]
    public class CharacterCustomizerSaveData {

        //SaveVersion can be added here

        public GenderSaveData maleSaveData;
        public GenderSaveData femaleSaveData;

        [System.Serializable]
        public enum Gender {
            NOT_SET,
            MALE,
            FEMALE
        }
        public Gender gender = Gender.NOT_SET;

        //Colors can be added here

        public CharacterCustomizerSaveData(Gender targetGender) {

            if (targetGender == Gender.NOT_SET) {
                Debug.Log("CharacterCustomizerSaveData targetGender NOT_SET");
                return;
            }

            gender = targetGender;
        }

        public CharacterCustomizerSaveData(Gender targetGender, GenderSaveData genderSaveData) {

            if(targetGender == Gender.NOT_SET) {
                Debug.Log("CharacterCustomizerSaveData targetGender NOT_SET");
                return;
            }

            gender = targetGender;

            if (targetGender == Gender.MALE) {
                maleSaveData = genderSaveData;
            }
            if (targetGender == Gender.FEMALE) {
                femaleSaveData = genderSaveData;
            }
        }
    }

    public class CharacterCustomizerSaveManager : MonoBehaviour {

        private const string saveFileName = "characterCustomizer.txt";

        public void SaveUnlockAndVisible(CharacterBuilder targetCharacterBuilder) {

            Debug.Log("SaveFile");
            //Debug.Log("Application.persistentDataPath= " + Application.persistentDataPath);

            CharacterCustomizerSaveData priorSaveData = LoadFile();

            OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();

            string destination = Application.persistentDataPath + "/"+ saveFileName;
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            CharacterCustomizerSaveData data;
            if (priorSaveData == null) {
                CharacterCustomizerSaveData.Gender gender = CharacterCustomizerSaveData.Gender.NOT_SET;
                if (targetCharacterBuilder.characterType == CharacterBuilder.CharacterType.MALE) {
                    gender = CharacterCustomizerSaveData.Gender.MALE;
                }
                if (targetCharacterBuilder.characterType == CharacterBuilder.CharacterType.FEMALE) {
                    gender = CharacterCustomizerSaveData.Gender.FEMALE;
                }

                GenderSaveData genderSaveData = null;
                if (outfitController == null) {
                    Debug.Log("No outfit Controller");
                }
                else {
                    genderSaveData = new GenderSaveData(outfitController.GetOutfitIndexListFromOutfitlist(outfitController.becameVisibleOutfits), outfitController.GetOutfitIndexListFromOutfitlist(outfitController.becameUnlockedOutfits), outfitController.GetOutfitIndexListFromOutfitDictioanry(outfitController.equippedOutfits));
                }

                data = new CharacterCustomizerSaveData(gender, genderSaveData);
            }
            else {
                GenderSaveData genderSaveData = null;
                if (targetCharacterBuilder.characterType == CharacterBuilder.CharacterType.MALE) {
                    genderSaveData = priorSaveData.maleSaveData;
                }
                if (targetCharacterBuilder.characterType == CharacterBuilder.CharacterType.FEMALE) {
                    genderSaveData = priorSaveData.femaleSaveData;
                }

                if (outfitController == null) {
                    Debug.Log("No outfit Controller");
                }
                else {
                    if(genderSaveData != null) {
                        genderSaveData.becameVisibleOutfits = outfitController.GetOutfitIndexListFromOutfitlist(outfitController.becameVisibleOutfits);
                        genderSaveData.becameUnlockedOutfits = outfitController.GetOutfitIndexListFromOutfitlist(outfitController.becameUnlockedOutfits);
                    }
                }

                data = priorSaveData;
                if (targetCharacterBuilder.characterType == CharacterBuilder.CharacterType.MALE) {
                    data.maleSaveData = genderSaveData;
                }
                if (targetCharacterBuilder.characterType == CharacterBuilder.CharacterType.FEMALE) {
                    data.femaleSaveData = genderSaveData;
                }
            }

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public void HandleSetMaleButton() {

            SaveGender(CharacterCustomizerSaveData.Gender.MALE);
        }

        public void HandleSetFemaleButton() {

            SaveGender(CharacterCustomizerSaveData.Gender.FEMALE);
        }

        private void SaveGender(CharacterCustomizerSaveData.Gender gender) {

            CharacterCustomizerSaveData data = LoadFile();
            if(data == null) {
                Debug.Log("SaveGender new data");
                data = new CharacterCustomizerSaveData(gender);
            }
            else {
                Debug.Log("SaveGender update data");
                data.gender = gender;
            }

            string destination = Application.persistentDataPath + "/" + saveFileName;
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public void HandleSaveButton(CharacterBuilder targetCharacterBuilder) {

            CharacterCustomizerSaveData data = LoadFile();
            SaveFile(data, targetCharacterBuilder.characterType);
        }

        private void SaveFile(CharacterCustomizerSaveData data, CharacterBuilder.CharacterType characterType) { /*List<bool> inCollisionMatrix*/

            Debug.Log("SaveFile");
            //Debug.Log("Application.persistentDataPath= " + Application.persistentDataPath);

            OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();

            string destination = Application.persistentDataPath + "/" + saveFileName;
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            GenderSaveData genderSaveData = null;
            if (outfitController == null) {
                Debug.Log("No outfit Controller");
            }
            else {
                genderSaveData = new GenderSaveData(outfitController.GetOutfitIndexListFromOutfitlist(outfitController.becameVisibleOutfits), outfitController.GetOutfitIndexListFromOutfitlist(outfitController.becameUnlockedOutfits), outfitController.GetOutfitIndexListFromOutfitDictioanry(outfitController.equippedOutfits) /*inCollisionMatrix*/);
            }
        
            CharacterCustomizerSaveData.Gender gender = CharacterCustomizerSaveData.Gender.NOT_SET;
            if (characterType == CharacterBuilder.CharacterType.MALE) {
                gender = CharacterCustomizerSaveData.Gender.MALE;
            }
            if (characterType == CharacterBuilder.CharacterType.FEMALE) {
                gender = CharacterCustomizerSaveData.Gender.FEMALE;
            }

            if(data == null) {
                if (genderSaveData == null) {
                    data = new CharacterCustomizerSaveData(gender);
                }
                else {
                    data = new CharacterCustomizerSaveData(gender, genderSaveData);
                }
            }
            else {
                data.gender = gender;
                if (characterType == CharacterBuilder.CharacterType.MALE) {
                    data.maleSaveData = genderSaveData;
                }
                if (characterType == CharacterBuilder.CharacterType.FEMALE) {
                    data.femaleSaveData = genderSaveData;
                }
            }
        
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public void HandleLoadButton() {

            CharacterCustomizerSaveData data = LoadFile();
            if(data != null) {
                ApplyLoadedData(data);
            }
        }

        public CharacterCustomizerSaveData LoadFile() {

            string destination = Application.persistentDataPath + "/" + saveFileName;
            Debug.Log("LoadFile" + destination);

            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else {
                Debug.Log("File not found");
                return null; //new List<bool>();
            }

            BinaryFormatter bf = new BinaryFormatter();
            CharacterCustomizerSaveData data = (CharacterCustomizerSaveData)bf.Deserialize(file);
            file.Close();

            if(data == null) {
                Debug.LogError("Could not cast data");
                return null;
            }

            Debug.Log("LoadFile Gender = " + data.gender);

            //outfitController.invisibleOutfits = data.m_invisibleOutfits;
            //outfitController.lockedOutfits = data.m_lockedOutfits;
            //outfitController.equippedOutfits = data.m_equippedOutfits;

            return data;
        }

        public void ApplyLoadedData(CharacterCustomizerSaveData data) {

            OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();

            Debug.Log("ApplyLoadedData gender = " + data.gender);
            if (data.gender == CharacterCustomizerSaveData.Gender.NOT_SET) {
                Debug.LogError("ApplyLoadedData targetGender NOT_SET");
                return;
            }
            GenderSaveData genderSaveData = genderSaveData = null;
            if (data.gender == CharacterCustomizerSaveData.Gender.MALE) {
                genderSaveData = data.maleSaveData;
            }
            if (data.gender == CharacterCustomizerSaveData.Gender.FEMALE) {
                genderSaveData = data.femaleSaveData;
            }

            if(genderSaveData == null) {
                Debug.Log("genderSaveData = null");
                return;
            }

            outfitController.UpdateVisibleOutfitListFromOutfitIndexList(genderSaveData.becameVisibleOutfits);

            outfitController.UpdateUnlockedOutfitListFromOutfitIndexList(genderSaveData.becameUnlockedOutfits);

            outfitController.RemoveAllOutfits();
            outfitController.UpdateEquippedOutfitDictioanryFromOutfitIndexList(genderSaveData.equippedOutfits);

        }

        public void HandleDeleteSaveButton() {

            DeleteSave();
        }

        private void DeleteSave() {
        
            string destination = Application.persistentDataPath + "/" + saveFileName;

            if (File.Exists(destination)) {
                File.Delete(destination);
            }
        }
    }

