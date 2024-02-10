/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ComfortGames.CharacterCustomization {

#if UNITY_EDITOR
    public class CharacterMatrix : EditorWindow {
    
        protected List<bool> outfitMatrixFlags = new List<bool>();

        public bool showOutfitCollisionMatrix = true;

        [MenuItem("CharacterCustomization/CharacterMatrix")]
        static void Init() {
            CharacterMatrix window = (CharacterMatrix)EditorWindow.GetWindow(typeof(CharacterMatrix));
            window.Show();
        }

        private void OnEnable() {
            LoadSave();    
        }

        protected virtual string GetMatrixName() {
            return "Outfits Collision Matrix";
        }

        protected virtual CharacterBuilder.CharacterType GetCharacterType() {
            return CharacterBuilder.CharacterType.MALE;
        }

        void OnGUI() {
        
            showOutfitCollisionMatrix = EditorGUILayout.Foldout(showOutfitCollisionMatrix, GetMatrixName());

            if(showOutfitCollisionMatrix){

                OutfitScriptableObject[] outfitScriptableObjects = GetOutfitScriptableObjects();
                int numberOfOutfits = outfitScriptableObjects.Length;
                int numberOfFlags = numberOfOutfits * (numberOfOutfits + 1) / 2;
            
                int flagsToAdd = numberOfFlags - outfitMatrixFlags.Count;

                for (int i = 0; i < flagsToAdd; i++) {
                    outfitMatrixFlags.Add(false);
                }
            
                GUIStyle topLabelStyle = new GUIStyle();
                topLabelStyle.alignment = TextAnchor.MiddleRight;

                //turn sideways to write labels
                EditorGUIUtility.RotateAroundPivot(-90, new Vector2(100, 100));

                int topLabelStartOffset = 0;
                if (Application.unityVersion.ToString().Contains("2018")) {
                    topLabelStartOffset = 19;
                }
                
                int topLabelSpread = 30;
                for (int i = 0; i < outfitScriptableObjects.Length; i++) {
                    EditorGUI.LabelField(new Rect(100, 150 + topLabelStartOffset + (topLabelSpread * i), 150, 50), outfitScriptableObjects[outfitScriptableObjects.Length - i - 1].outfitName);
                }
                //turn back
                EditorGUIUtility.RotateAroundPivot(90, new Vector2(100, 100));

                int sideLabelStartOffset = 100;
                if (Application.unityVersion.ToString().Contains("2018")) {
                    sideLabelStartOffset = 92;
                }
                int k = 0;
                for (int i = 0; i < outfitScriptableObjects.Length; i++) { //i is the column

                    EditorGUI.LabelField(new Rect(0, sideLabelStartOffset + 30 * i, 150, 50), outfitScriptableObjects[i].outfitName, topLabelStyle);

                    for (int j = 0; j < outfitScriptableObjects.Length - i; j++) {
                        if(j == outfitScriptableObjects.Length - i - 1)
                        {
                            EditorGUI.LabelField(new Rect(170 + 30 * j, 110 + 30 * i, 30, 30), " "); // "\u2713");
                            outfitMatrixFlags[k] = false;
                        }
                        else {
                            outfitMatrixFlags[k] = EditorGUI.Toggle(new Rect(170 + 30 * j, 110 + 30 * i, 30, 30), "", outfitMatrixFlags[k]);
                        }

                        k++;
                    }
                }

                outfitMatrixFlags = outfitMatrixFlags.GetRange(0, k);
            }
        }

        private void LoadSave() {

            OutfitsCollisionScriptableObject outfitsCollisionScriptableObject = GetOutfitsCollisionScriptableObject();
            //Debug.Log("Loading " + GetMatrixName() + " , outfitCollisions:" + outfitsCollisionScriptableObject.outfitCollisions.Count + " , outfitMatrixFlags: " + outfitMatrixFlags.Count);
            int oldCount = outfitsCollisionScriptableObject.outfitCollisions.Count;
            int newCount = outfitMatrixFlags.Count;
        
            if (newCount < oldCount && newCount > 0 && oldCount > 0) {
                Debug.LogWarning("A OUTFIT MODEL WAS DELETED IN THE EDITOR, CLEARING COLLISION MATRIX.");
                outfitMatrixFlags = outfitsCollisionScriptableObject.outfitCollisions;
                outfitsCollisionScriptableObject.outfitCollisions.Clear();
            }
            else {
                outfitMatrixFlags = outfitsCollisionScriptableObject.outfitCollisions;
            }
        }

        private void Save() {
        
            OutfitsCollisionScriptableObject outfitsCollisionScriptableObject = GetOutfitsCollisionScriptableObject();
        
            //Debug.Log("Saving " + GetMatrixName() +" , "+ outfitMatrixFlags.Count + " , " + outfitsCollisionScriptableObject.outfitCollisions.Count);
        
            outfitsCollisionScriptableObject.outfitCollisions = outfitMatrixFlags;

            EditorUtility.SetDirty(outfitsCollisionScriptableObject);
        }

        private OutfitScriptableObject[] GetOutfitScriptableObjects() {

            return CharacterCustomizationAssetManager.GetOutfitScriptableObjects(GetCharacterType());
        }

        private OutfitsCollisionScriptableObject GetOutfitsCollisionScriptableObject() {

            return CharacterCustomizationAssetManager.GetOutfitsCollisionScriptableObject(GetCharacterType());
        }

        private void OnFocus() {
            LoadSave();
        }

        private void OnLostFocus() {
            Save();
        }

        private void OnDestroy() {
            Save();
        }
    }
#endif
}
