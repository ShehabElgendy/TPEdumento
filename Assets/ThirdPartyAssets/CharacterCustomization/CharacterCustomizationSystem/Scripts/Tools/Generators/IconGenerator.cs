using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class IconGenerator : MonoBehaviour {

        public GameObject fullCharacter;
        public GameObject root;

        public Camera iconCamera;

        public void HandleGenerateIconsButton() {

            StartCoroutine(GenerateIcons());

            GenerateIcons();
        }

        IEnumerator GenerateIcons() {
            SkinnedMeshRenderer[] skinnedMeshRenderers = fullCharacter.GetComponentsInChildren<SkinnedMeshRenderer>();
            Debug.Log("Generating " + skinnedMeshRenderers.Length + " icons.");

            List<GameObject> tempPieces = new List<GameObject>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++) {

                GameObject duplicateCharacter = Instantiate(fullCharacter);
                duplicateCharacter.transform.position = Vector3.zero;

                SkinnedMeshRenderer[] duplicateSkinnedMeshRenderers = duplicateCharacter.GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int j = 0; j < duplicateSkinnedMeshRenderers.Length; j++) {

                    if (IsSkinnedMeshRendererInBones(duplicateSkinnedMeshRenderers[j])) {
                        Debug.Log("duplicateSkinnedMeshRenderers[j].gameObject.name= " + duplicateSkinnedMeshRenderers[j].gameObject.name);
                        continue;
                    }

                    if (duplicateSkinnedMeshRenderers[j].gameObject.name != skinnedMeshRenderers[i].gameObject.name) {
                        DestroyImmediate(duplicateSkinnedMeshRenderers[j].gameObject);
                    }
                }

                duplicateCharacter.name = skinnedMeshRenderers[i].gameObject.name;

                yield return new WaitForSeconds(1f);

                GenerateIcons(duplicateCharacter.name);

                Destroy(duplicateCharacter);

                duplicateCharacter.SetActive(false);
            }

            Debug.Log("Done Generating Icons.");
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

        private void GenerateIcons(string iconName) {

            if (fullCharacter == null || root == null) {
                Debug.Log("You need to assign a full character and a root before generating icons.");
                return;
            }

            if (iconCamera == null) {
                Debug.Log("You need to assign a camera generating icons.");
                return;
            }

            RenderTexture.active = iconCamera.targetTexture;
            Texture2D targetTexture = new Texture2D(iconCamera.targetTexture.width, iconCamera.targetTexture.height, TextureFormat.RGB24, false);
            targetTexture.ReadPixels(new Rect(0, 0, iconCamera.targetTexture.width, iconCamera.targetTexture.height), 0, 0);
            RenderTexture.active = null;

            System.IO.File.WriteAllBytes(CharacterCustomizationAssetManager.GetIconGeneratorPath(iconName), targetTexture.EncodeToPNG());
        }
    }