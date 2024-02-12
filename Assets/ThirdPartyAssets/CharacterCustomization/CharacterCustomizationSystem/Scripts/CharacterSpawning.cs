using UnityEngine;

    public class CharacterSpawning : MonoBehaviour {

        public enum SpawnType {
            DATA_DRIVEN,
            MALE,
            FEMALE
        }
        public SpawnType spawnType;

        public GameObject characterBuilderFemale;
        public GameObject characterBuilderMale;
        
        private void Awake() {

            CharacterCustomizerSaveData data = CharacterCustomizationFinderManager.GetSaveManager().LoadFile();

            if (data == null)
                return;

            GameObject characterObject = null;
            switch(spawnType)
            {
                case SpawnType.FEMALE:
                    characterObject = Instantiate(characterBuilderFemale, transform.position, transform.rotation);
                    break;
                case SpawnType.MALE:
                    characterObject = Instantiate(characterBuilderMale, transform.position, transform.rotation);
                    break;
                case SpawnType.DATA_DRIVEN:
                    //Data Driven
                    if (data.gender == CharacterCustomizerSaveData.Gender.FEMALE)
                    {
                        characterObject = Instantiate(characterBuilderFemale, transform.position, transform.rotation);
                    }

                    if (data.gender == CharacterCustomizerSaveData.Gender.MALE)
                    {
                        characterObject = Instantiate(characterBuilderMale, transform.position, transform.rotation);
                    }
                    break;
            }
            
            //If the controller uses cinemachine
            GetComponent<CinemachineAdapter>()?.SetupCinemachine(characterObject);
        }
    }


