/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;
using UnityEditor;

namespace ComfortGames.CharacterCustomization {

#if UNITY_EDITOR
    public class PrefabGenerator : EditorWindow {

        public GameObject fullCharacter;
        public GameObject root;
        
        private bool isMale = true;

        private bool showPrefabGenerator = true;

        [MenuItem("CharacterCustomization/PrefabGenerator")]
        static void Init() {
            PrefabGenerator window = (PrefabGenerator)EditorWindow.GetWindow(typeof(PrefabGenerator));
            window.Show();
        }
        
        void OnGUI() {

            showPrefabGenerator = EditorGUILayout.Foldout(showPrefabGenerator, "PrefabGenerator");

            if (showPrefabGenerator) {

                fullCharacter = (GameObject)EditorGUILayout.ObjectField("Full Character ", fullCharacter, typeof(GameObject), true);
                root = (GameObject)EditorGUILayout.ObjectField("Character Root ", root, typeof(GameObject), true);

                //Generate Pieces
                if (GUILayout.Button("Generate Pieces")) {
                    GeneratePieces();
                }

                //Generate Characters
                isMale = EditorGUILayout.Toggle("Is Male", isMale);

                if (GUILayout.Button("Generate Base Character")) {
                    GenerateBaseCharacter();
                }
            }
        }

        private bool IsSkinnedMeshRendererInBones(SkinnedMeshRenderer targetSkinnedMeshRenderer) {

            SkinnedMeshRenderer[] allRootSkinnedMeshRenderers = root.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer rootChildSkinnedMeshRenderer in allRootSkinnedMeshRenderers) {
                if (rootChildSkinnedMeshRenderer.gameObject.name == targetSkinnedMeshRenderer.gameObject.name) {
                    return true;
                }
            }

            return false;
        }

        private void DisableAllSkinnedMeshRendererAndMeshRendererInBones(GameObject duplicateCharacter) {

            string rootName = root.gameObject.name;
            GameObject duplicateRot = null;
            Transform[] duplicateTransforms = duplicateCharacter.GetComponentsInChildren<Transform>();
            foreach(Transform child in duplicateTransforms) {
                if(child.gameObject.name == rootName) {
                    duplicateRot = child.gameObject;
                    break;
                }
            }
            if (duplicateRot == null)
                return;

            SkinnedMeshRenderer[] skinnedMeshRenderers = duplicateRot.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach(SkinnedMeshRenderer smr in skinnedMeshRenderers) {
                smr.enabled = false;
            }
            MeshRenderer[] meshRenderers = duplicateRot.transform.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mr in meshRenderers) {
                mr.enabled = false;
            }
        }

        private void GeneratePieces() {

            if (fullCharacter == null || root == null) {
                Debug.Log("You need to assign a full character and a root before generating pieces.");
                return;
            }

            SkinnedMeshRenderer[] skinnedMeshRenderers = fullCharacter.GetComponentsInChildren<SkinnedMeshRenderer>();
            Debug.Log("Generating " + skinnedMeshRenderers.Length + " pieces...");

            for (int i = 0; i < skinnedMeshRenderers.Length; i++) {

                Debug.Log(skinnedMeshRenderers[i].gameObject.name + " isSkinnedMeshRendererInBones= " + IsSkinnedMeshRendererInBones(skinnedMeshRenderers[i]));
                if (IsSkinnedMeshRendererInBones(skinnedMeshRenderers[i])) {
                    Debug.Log("Skinned Mesh Renderer in Bones, skipping making a prefab for it.");
                    continue;
                }

                GameObject duplicateCharacter = Instantiate(fullCharacter);
                duplicateCharacter.transform.position = Vector3.zero;
                DisableAllSkinnedMeshRendererAndMeshRendererInBones(duplicateCharacter);

                SkinnedMeshRenderer[] duplicateSkinnedMeshRenderers = duplicateCharacter.GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int j = 0; j < duplicateSkinnedMeshRenderers.Length; j++) {
                    
                    if (IsSkinnedMeshRendererInBones(duplicateSkinnedMeshRenderers[j])) {
                        Debug.Log("duplicateSkinnedMeshRenderers[j].gameObject.name= " + duplicateSkinnedMeshRenderers[j].gameObject.name);
                        duplicateSkinnedMeshRenderers[j].enabled = false;
                        continue;
                    }

                    if (duplicateSkinnedMeshRenderers[j].gameObject.name != skinnedMeshRenderers[i].gameObject.name) {
                        DestroyImmediate(duplicateSkinnedMeshRenderers[j].gameObject);
                    }
                    else {
                        duplicateSkinnedMeshRenderers[j].updateWhenOffscreen = true;
                    }
                }

                duplicateCharacter.name = skinnedMeshRenderers[i].gameObject.name;

                SavePrefab(duplicateCharacter);

                DestroyImmediate(duplicateCharacter);
            }

            Debug.Log("Done Generating pieces.");
        }

        private void GenerateBaseCharacter() {

            if (fullCharacter == null || root == null) {
                Debug.Log("You need to assign a full character and a root before generating a base character.");
                return;
            }

            Debug.Log("Generating a base character " + (isMale ? CharacterBuilder.CharacterType.MALE.ToString() : CharacterBuilder.CharacterType.FEMALE.ToString()) + "...");

            //Container
            GameObject characterBase = new GameObject(isMale ? "CharacterBuilder_Male" : "CharacterBuilder_Female");
            characterBase.transform.position = Vector3.zero;
            //Animator
            UnityEditorInternal.ComponentUtility.CopyComponent(fullCharacter.GetComponent<Animator>());
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(characterBase);
            characterBase.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
            //Bones
            GameObject duplicateRoot = Instantiate(root);
            duplicateRoot.name = root.name;
            duplicateRoot.transform.SetParent(characterBase.transform);
            //Character Builder
            CharacterBuilder characterBuilder = characterBase.AddComponent<CharacterBuilder>();
            characterBuilder.characterType = isMale ? CharacterBuilder.CharacterType.MALE : CharacterBuilder.CharacterType.FEMALE;
            //Root
            characterBuilder.root = duplicateRoot;
            SavePrefab(characterBase);

            DestroyImmediate(characterBase);

            Debug.Log("Done Generating Base Character.");
        }

        private void SavePrefab(GameObject targetObject) {

            PrefabUtility.SaveAsPrefabAsset(targetObject, CharacterCustomizationAssetManager.GetPrefabGeneratorPath(targetObject.name));
        }
    }
#endif
}