/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    public class CharacterBuilder : MonoBehaviour {
        
        public OutfitController outfitController { get; private set; }

        public enum CharacterType {
            MALE,
            FEMALE
        }
        public CharacterType characterType;

        public GameObject root;

        private List<GameObject> piecesList = new List<GameObject>();

        private void Awake() {

            if(outfitController == null) {
                GameObject outfitControllerObject = new GameObject("OutfitController");
                outfitControllerObject.transform.SetParent(transform);
                outfitController = outfitControllerObject.AddComponent<OutfitController>();
                outfitController.Initialize(this);
            }
        }

        private void ChangeBones(SkinnedMeshRenderer skinnedMeshRenderer) {

            if (skinnedMeshRenderer.rootBone == null) {
                return;
            }

            List<string> boneNames = new List<string>();
            for(int i = 0; i< skinnedMeshRenderer.bones.Length; i++)
            {
                boneNames.Add(skinnedMeshRenderer.bones[i].name);
            }

            Transform[] children = transform.GetComponentsInChildren<Transform>();

            Transform[] newBones = new Transform[skinnedMeshRenderer.bones.Length];

            for (int i = 0; i < boneNames.Count; i++) {

                Transform targetBone = null;
                foreach (var child in children) {

                    if (child.name == boneNames[i]) {
                        targetBone = child;
                        newBones[i] = targetBone;
                        break;
                    }
                }
            }

            Transform oldRootBone = skinnedMeshRenderer.rootBone;
            skinnedMeshRenderer.bones = newBones;
            
            Destroy(oldRootBone.gameObject);
        }
    
        public void AddOutfitPiece(GameObject piece) {

            if (piece == null)
                return;

            SkinnedMeshRenderer[] skinnedMeshRenderers = piece.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
                ChangeBones(skinnedMeshRenderers[i]);
            }

            piece.transform.SetParent(transform, false);
            piece.transform.localPosition = Vector3.zero;
            piece.transform.localRotation = Quaternion.identity;

            piecesList.Add(piece);
        }
    
        public void RemoveOutfitPiece(GameObject piece) {

            if (piece == null)
                return;

            if (piecesList.Contains(piece)) {
                piecesList.Remove(piece);
                GameObject.Destroy(piece.gameObject);
            }
        }
    }
}

