/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ComfortGames.CharacterCustomization {

    public class OutfitInstantiated {

        public List<GameObject> piecesList;

        public OutfitInstantiated(List<GameObject> inPiecesList) {
            piecesList = inPiecesList;
        }
    }

    public class OutfitController : MonoBehaviour {

        public List<OutfitScriptableObject> becameVisibleOutfits = new List<OutfitScriptableObject>();
        public List<OutfitScriptableObject> becameUnlockedOutfits = new List<OutfitScriptableObject>();
        public Dictionary<OutfitScriptableObject, OutfitInstantiated> equippedOutfits = new Dictionary<OutfitScriptableObject, OutfitInstantiated>();
    
        private List<OutfitScriptableObject> outfitScriptableObjects;
        public int numOutfitModels { get { return outfitScriptableObjects.Count; } }

        private List<bool> outfitMatrixFlags = new List<bool>();

        private CharacterBuilder characterBuilder;

        private List<OutfitCategoryScriptableObject> outfitCategoryScriptableObjects;
        public int numOutfitCategoryModels { get { return outfitCategoryScriptableObjects.Count; } }

        public OutfitCategoryScriptableObject currentOutfitCategoryScriptableObject;

        public void Initialize(CharacterBuilder targetCharacterBuilder) {

            characterBuilder = targetCharacterBuilder;

            outfitScriptableObjects = CharacterCustomizationAssetManager.GetOutfitScriptableObjects(characterBuilder.characterType).ToList();

            outfitCategoryScriptableObjects = CharacterCustomizationAssetManager.GetOutfitCategoryScriptableObject(characterBuilder.characterType).ToList();

            OutfitsCollisionScriptableObject outfitsCollisionScriptableObject = CharacterCustomizationAssetManager.GetOutfitsCollisionScriptableObject(characterBuilder.characterType);
            outfitMatrixFlags = outfitsCollisionScriptableObject.outfitCollisions;

            CharacterCustomizerSaveData data = CharacterCustomizationFinderManager.GetSaveManager().LoadFile();
            if(data != null) {
                CharacterCustomizationFinderManager.GetSaveManager().ApplyLoadedData(data);
            }

            for(int i = 0; i< outfitCategoryScriptableObjects.Count; i++) {
                TryApplyDefaultOutfitForCategory(outfitCategoryScriptableObjects[i]);
            }
        }

        public void TryApplyDefaultOutfitForCategory(OutfitCategoryScriptableObject targetOutfitCategoryScriptableObject) {

            if (targetOutfitCategoryScriptableObject.defaultOutfitScriptableObject == null)
                return;

            foreach(OutfitScriptableObject targetOutfitScriptableObject in equippedOutfits.Keys) {
                if(targetOutfitScriptableObject.outfitCategoryScriptableObject == targetOutfitCategoryScriptableObject) {
                    return; //we already have a equipped outfits in this category
                }
            }

            EquipOutfit(targetOutfitCategoryScriptableObject.defaultOutfitScriptableObject);
        }

        public List<int> GetOutfitIndexListFromOutfitlist(List<OutfitScriptableObject> outfitsList) {

            List<int> outfitIndexList = new List<int>();
            foreach (OutfitScriptableObject outfitScriptableObject in outfitsList) {
                int index = GetOutfitModelIndex(outfitScriptableObject);
                outfitIndexList.Add(index);
            }
            return outfitIndexList;
        }

        public List<int> GetOutfitIndexListFromOutfitDictioanry(Dictionary<OutfitScriptableObject, OutfitInstantiated> outfitsDictionary) {

            List<int> outfitIndexList = new List<int>();
            foreach (OutfitScriptableObject outfitScriptableObject in outfitsDictionary.Keys) {
                int index = GetOutfitModelIndex(outfitScriptableObject);
                outfitIndexList.Add(index);
            }
            return outfitIndexList;
        }

        public void UpdateEquippedOutfitDictioanryFromOutfitIndexList(List<int> equippedOutfitIndexList) {

            if (equippedOutfitIndexList == null)
                return;

            foreach (int index in equippedOutfitIndexList) {
                OutfitScriptableObject outfitScriptableObject = GetOutfitModel(index);

                EquipOutfit(outfitScriptableObject);
            }
        }

        public void UpdateVisibleOutfitListFromOutfitIndexList(List<int> becameVisibleOutfitIndexList) {

            if (becameVisibleOutfitIndexList == null)
                return;

            foreach (int index in becameVisibleOutfitIndexList) {
                OutfitScriptableObject outfitScriptableObject = GetOutfitModel(index);

                if (!becameVisibleOutfits.Contains(outfitScriptableObject)) {
                    becameVisibleOutfits.Add(outfitScriptableObject);
                }
            }
        }

        public void UpdateUnlockedOutfitListFromOutfitIndexList(List<int> becameUnlockedOutfitIndexList) {

            if (becameUnlockedOutfitIndexList == null)
                return;

            foreach (int index in becameUnlockedOutfitIndexList) {
                OutfitScriptableObject outfitScriptableObject = GetOutfitModel(index);

                if (!becameUnlockedOutfits.Contains(outfitScriptableObject)) {
                    becameUnlockedOutfits.Add(outfitScriptableObject);
                }
            }
        }

        public OutfitCategoryScriptableObject GetOutfitCategoryModel(int outfitCategoryModelIndex) {
            if (outfitCategoryScriptableObjects == null || outfitCategoryScriptableObjects.Count < 1) {
                Debug.Log("No Outfit Cateory Models");
                return null;
            }

            if (outfitCategoryScriptableObjects.Count < outfitCategoryModelIndex || outfitCategoryModelIndex < 0) {
                Debug.Log("Wrong outfitCategoryModelIndex");
                return null;
            }

            return outfitCategoryScriptableObjects[outfitCategoryModelIndex];
        }

        public OutfitScriptableObject GetOutfitModel(int outfitModelIndex) {
            if(outfitScriptableObjects == null || outfitScriptableObjects.Count < 1) {
                Debug.Log("No Outfits Models");
                return null;
            }

            if(outfitScriptableObjects.Count < outfitModelIndex || outfitModelIndex < 0) {
                Debug.Log("Wrong outfitModelIndex");
                return null;
            }

            return outfitScriptableObjects[outfitModelIndex];
        }

        public int GetOutfitModelIndex(OutfitScriptableObject outfitScriptableObject) {
            return outfitScriptableObjects.IndexOf(outfitScriptableObject);
        }

        public void RemoveAllOutfits() {

            for (int i = 0; i < outfitScriptableObjects.Count; i++) {
                RemoveOutfit(outfitScriptableObjects[i]);
            }
        }

        public void RemoveAllOutfitsBasedOnCollision(OutfitScriptableObject targetOutfit) {

            int targetOutfitIndex = outfitScriptableObjects.IndexOf(targetOutfit);
            //Debug.Log("targetOutfitIndex= " + targetOutfitIndex +" "+ targetOutfit.outfitName);

            int k = 0;
            for (int i = 0; i < outfitScriptableObjects.Count; i++) {
            
                for (int j = outfitScriptableObjects.Count - 1; j >=i ; j--) {

                    if (i == targetOutfitIndex && j == targetOutfitIndex) {
                        //Debug.Log("B    i= " + i + " , j= " + j + " , k=" + k);
                        if (outfitMatrixFlags[k]) {
                            //remove the oufit at row i and remove the oufit at column j
                            RemoveOutfit(outfitScriptableObjects[i]);
                            //RemoveOutfit(outfitScriptableObjects[j]);
                        }
                    }
                    else if (i == targetOutfitIndex) {
                        //Debug.Log("R    i= " + i + " , j= " + j + " , k=" + k);
                        if (outfitMatrixFlags[k]) {
                            //remove the oufit at column j
                            RemoveOutfit(outfitScriptableObjects[j]);
                        }
                    }
                    else if (j == targetOutfitIndex) {
                        //Debug.Log("C    i= " + i + " , j= " + j + " , k=" + k);
                        if (outfitMatrixFlags[k]) {
                            //remove the oufit at row i
                            RemoveOutfit(outfitScriptableObjects[i]);
                        }
                    }
                    else {
                        //Debug.Log("i= " + i + " , j= " + j + " , k=" + k);
                    }

                    k++;
                }
            }
        }

        private void EquipOutfit(OutfitScriptableObject targetOutfit) {

            bool wasEquipped = equippedOutfits.ContainsKey(targetOutfit);

            if (wasEquipped) {
                return;
            }
            else {
                AddOutfit(targetOutfit);
            }
        
            //Remove all outfits based on the collision Matrix
            RemoveAllOutfitsBasedOnCollision(targetOutfit);
        }

        public void ShowOutfit(OutfitScriptableObject targetOutfit) {

            bool isShown = becameVisibleOutfits.Contains(targetOutfit);

            if (isShown) {
                return;
            }
            else {
                becameVisibleOutfits.Add(targetOutfit);

                CharacterCustomizationFinderManager.GetSaveManager().SaveUnlockAndVisible(characterBuilder);
            }
        }

        public void BuyOutfit(OutfitScriptableObject targetOutfit) {
            bool isOwned = becameUnlockedOutfits.Contains(targetOutfit);

            if (isOwned) {
                return;
            }
            else {
                becameUnlockedOutfits.Add(targetOutfit);

                CharacterCustomizationFinderManager.GetSaveManager().SaveUnlockAndVisible(characterBuilder);
            }
        }

        public void ToggleOutfit(OutfitScriptableObject targetOutfit) {
        
            bool wasEquipped = equippedOutfits.ContainsKey(targetOutfit);
            if (wasEquipped) {
                RemoveOutfit(targetOutfit);
            }
            else {
                AddOutfit(targetOutfit);
            }
        
            //Remove all outfits based on the collision Matrix
            RemoveAllOutfitsBasedOnCollision(targetOutfit);
        }

        public void RemoveOutfit(OutfitScriptableObject targetOutfit) {
        
            OutfitInstantiated outfitInstantaited = null;
            equippedOutfits.TryGetValue(targetOutfit, out outfitInstantaited);
            if(outfitInstantaited == null) {
                //Debug.Log("Can't remove the outfit because it is not equipped");
                return;
            }

            if(targetOutfit.rendererObjectNames != null && targetOutfit.rendererObjectNames.Count > 0) {
                ToggleRendererObjects(targetOutfit, false);
            }
            
            if(targetOutfit.outfitPieces != null && targetOutfit.outfitPieces.Count > 0) {
                for (int i = 0; i < outfitInstantaited.piecesList.Count; i++) {
                    characterBuilder.RemoveOutfitPiece(outfitInstantaited.piecesList[i]);
                }
            }
            
            equippedOutfits.Remove(targetOutfit);

            TryApplyDefaultOutfitForCategory(targetOutfit.outfitCategoryScriptableObject);
        }

        private void AddOutfit(OutfitScriptableObject targetOutfit) {

            List<GameObject> piecesList = new List<GameObject>();

            if (targetOutfit.rendererObjectNames != null && targetOutfit.rendererObjectNames.Count > 0)
                ToggleRendererObjects(targetOutfit, true);

            if (targetOutfit.outfitPieces != null && targetOutfit.outfitPieces.Count > 0)
                piecesList = AddOutfitPieces(targetOutfit);

            equippedOutfits.Add(targetOutfit, new OutfitInstantiated(piecesList));
        }

        private List<GameObject> AddOutfitPieces(OutfitScriptableObject targetOutfit) {

            List<GameObject> piecesList = new List<GameObject>();
            for (int i = 0; i < targetOutfit.outfitPieces.Count; i++) {
                GameObject piece = Instantiate(targetOutfit.outfitPieces[i], new Vector3(0, 0, 0), Quaternion.identity);
                characterBuilder.AddOutfitPiece(piece);
                piecesList.Add(piece);
            }
            return piecesList;
        }

        private void ToggleRendererObjects(OutfitScriptableObject targetOutfit, bool isOn) {

            if (targetOutfit.rendererObjectNames.Count < 1)
                return;

            if (characterBuilder.root == null)
                return;

            Transform parentTransform = null;

            if (characterBuilder.root != null)
                parentTransform = characterBuilder.root.transform;

            if (targetOutfit.checkOutsideRoot)
                parentTransform = characterBuilder.transform;
            
            foreach (Transform child in parentTransform.GetComponentsInChildren<Transform>()) {
                for (int i = 0; i < targetOutfit.rendererObjectNames.Count; i++) {
                    if (child.name == targetOutfit.rendererObjectNames[i]) {

                        Renderer renderer = child.GetComponent<Renderer>(); //Checking for SkinnedMeshRenderer or MeshRenderer to toggle
                        if (renderer != null)
                            renderer.enabled = isOn;
                    }
                }
            }
        }
    }
}


