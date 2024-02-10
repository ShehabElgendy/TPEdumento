using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class OutfitSelectionView : MonoBehaviour {

        public GameObject outfitCategoryViewPrefab;
        public Transform outfitCategoryViewsParent;

        public GameObject outfitCategoryContainerPrefab;
        public Transform outfitCategoryContainerParent;

        public GameObject outfitViewPrefab;
        public Transform outfitViewsParent;

        private List<OutfitCategoryView> outfitCategoryViewList = new List<OutfitCategoryView>();

        private List<OutfitView> outfitViewList = new List<OutfitView>();
    private void Start() {
        OutfitController outfitController = CharacterCustomizationFinderManager.GetOutfitController();

            //Outfit Categories
            for (int i = 0; i < outfitController.numOutfitCategoryModels; i++) {

                OutfitCategoryScriptableObject outfitCategoryScriptableObject = outfitController.GetOutfitCategoryModel(i);
                
                OutfitCategoryView outfitCategoryView = Instantiate(outfitCategoryViewPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<OutfitCategoryView>();
                outfitCategoryView.GetComponent<Toggle>().group = outfitCategoryViewsParent.GetComponent<ToggleGroup>();
                outfitCategoryView.Initialize(outfitCategoryScriptableObject);
                outfitCategoryView.transform.SetParent(outfitCategoryViewsParent);

                outfitCategoryView.GetComponent<RectTransform>().localScale = Vector3.one;

                if (!outfitCategoryScriptableObject.isInvisible) {
                    outfitCategoryView.gameObject.SetActive(true);
                }
                
                OutfitCategoryContainerView outOutfitCategoryContainerView = Instantiate(outfitCategoryContainerPrefab).GetComponent<OutfitCategoryContainerView>();
                outOutfitCategoryContainerView.transform.SetParent(outfitCategoryContainerParent, false);
                outfitCategoryView.targetOutfitCategoryContainerView = outOutfitCategoryContainerView;

                outfitCategoryViewList.Add(outfitCategoryView);
            }

            SortCategoryViewsBySortingOrder();

            //activate first toggle
            int firstVisibleCategoryIndex = 0;
            for(int i = 0; i< outfitCategoryViewList.Count; i++) {
                if (outfitCategoryViewList[i].outfitCategoryScriptableObject.isInvisible) {
                    firstVisibleCategoryIndex++;
                }
                else {
                    break;
                }
            }
            if(outfitCategoryViewList != null && outfitCategoryViewList.Count > firstVisibleCategoryIndex) {
                outfitCategoryViewList[firstVisibleCategoryIndex].GetComponent<Toggle>().isOn = true;
            }

            //Outfits
            for (int i = 0; i < outfitController.numOutfitModels; i++) {
                OutfitView outfitView = Instantiate(outfitViewPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<OutfitView>();
                
                outfitView.Initialize(outfitController.GetOutfitModel(i));

                OutfitCategoryView targetOutfitCategoryView = GetOutfitCategoryViewByModel(outfitView.outfitScriptableObject.outfitCategoryScriptableObject);
            
                outfitView.transform.SetParent(targetOutfitCategoryView.targetOutfitCategoryContainerView.outfitCategoryContainerParent);

                outfitView.GetComponent<RectTransform>().localScale = Vector3.one;

                outfitView.gameObject.SetActive(true);

                outfitViewList.Add(outfitView);
            }

            //sort
            SortViewsByVisibilityLockedSortingOrder();

            //SortViewsBySortingOrder();
        }

        private OutfitCategoryView GetOutfitCategoryViewByModel(OutfitCategoryScriptableObject targetOutfitCategoryScriptableObject) {

            for(int i = 0; i< outfitCategoryViewList.Count; i++) {
                if (outfitCategoryViewList[i].outfitCategoryScriptableObject == targetOutfitCategoryScriptableObject) {
                    return outfitCategoryViewList[i];
                }
            }

            return null;
        }

        public void SortViewsByVisibilityLockedSortingOrder() {
        
            outfitViewList.Sort((v1, v2) => {

                //Invisible last
                if (v2.outfitScriptableObject.isInvisible && v1.outfitScriptableObject.isInvisible) {
                    //nothing
                }
                else if (v2.outfitScriptableObject.isInvisible || v1.outfitScriptableObject.isInvisible) {
                    return v1.outfitScriptableObject.isInvisible.CompareTo(v2.outfitScriptableObject.isInvisible);
                }

                //locked second last
                if (v2.outfitScriptableObject.isLocked && v1.outfitScriptableObject.isLocked) {
                    //nothing
                }
                else if (v2.outfitScriptableObject.isLocked || v1.outfitScriptableObject.isLocked) {
                    return v1.outfitScriptableObject.isLocked.CompareTo(v2.outfitScriptableObject.isLocked);
                }

                //Sorting order
                return v2.outfitScriptableObject.sortingOrder.CompareTo(v1.outfitScriptableObject.sortingOrder);
            });

            for (int i = 0; i < outfitViewList.Count; i++) {
                outfitViewList[i].transform.SetAsLastSibling();
            }
        }

        public void SortViewsBySortingOrder() {
        
            //Sort only by sorting order
            outfitViewList.Sort((v1, v2) => v2.outfitScriptableObject.sortingOrder.CompareTo(v1.outfitScriptableObject.sortingOrder));
            for (int i = 0; i < outfitViewList.Count; i++) {
                outfitViewList[i].transform.SetAsLastSibling();
            }
        }

        public void SortCategoryViewsBySortingOrder() {

            //Sort only by sorting order
            outfitCategoryViewList.Sort((v1, v2) => v2.outfitCategoryScriptableObject.sortingOrder.CompareTo(v1.outfitCategoryScriptableObject.sortingOrder));
            for (int i = 0; i < outfitCategoryViewList.Count; i++) {
                outfitCategoryViewList[i].transform.SetAsLastSibling();
            }
        }
    }


