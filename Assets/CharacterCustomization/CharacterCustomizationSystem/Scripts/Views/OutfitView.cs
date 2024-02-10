using UnityEngine;
using UnityEngine.UI;

    public class OutfitView : MonoBehaviour
    {

        public OutfitScriptableObject outfitScriptableObject;
        public GameObject content;
        public Image hiddenImage;
        public Text nameText;
        public Image outfitImage;
        public Image outfitEquippedImage;
        public Image outfitLockedImage;
        public Button changeButton;
        public Button buyButton;

        public void Initialize(OutfitScriptableObject inOutfitScriptableObject)
        {

            outfitScriptableObject = inOutfitScriptableObject;
            nameText.text = outfitScriptableObject.outfitName;

            Sprite sprite = inOutfitScriptableObject.outfitIcon;
            outfitImage.sprite = sprite;
        }

        public void HandleOutfitButton()
        {
            CharacterCustomizationFinderManager.GetOutfitController().ToggleOutfit(outfitScriptableObject);
        }

        public void HandleBuyButton()
        {
            CharacterCustomizationFinderManager.GetOutfitController().BuyOutfit(outfitScriptableObject);
        }

        private void UpdateIfCategoryDefault(OutfitController outfitController)
        {

            //check if this is a category default and the only outfit equipped for this category
            if (outfitScriptableObject.outfitCategoryScriptableObject == null || outfitScriptableObject.outfitCategoryScriptableObject.defaultOutfitScriptableObject == null)
                return;

            if (outfitScriptableObject != outfitScriptableObject.outfitCategoryScriptableObject.defaultOutfitScriptableObject)
                return;

            //this is the category default
            if (outfitController.equippedOutfits.ContainsKey(outfitScriptableObject))
            {
                //this is equipped
                int numEquippedInThisCategory = 0;
                foreach (OutfitScriptableObject targetOutfitScriptableObject in outfitController.equippedOutfits.Keys)
                {

                    if (targetOutfitScriptableObject.outfitCategoryScriptableObject == outfitScriptableObject.outfitCategoryScriptableObject)
                    {
                        numEquippedInThisCategory++;
                    }
                }

                if (numEquippedInThisCategory == 1)
                {
                    //this is the only one equipped in this category
                    changeButton.gameObject.SetActive(false);
                }
            }
        }

        public void Update()
        {

            if (outfitScriptableObject == null)
                return;

            if (CharacterCustomizationFinderManager.GetOutfitController() == null)
                return;

            //Visility Check
            bool isVisible = true;
            if (outfitScriptableObject.isInvisible)
            {
                isVisible = CharacterCustomizationFinderManager.GetOutfitController().becameVisibleOutfits.Contains(outfitScriptableObject);
            }
            content.SetActive(isVisible);
            hiddenImage.gameObject.SetActive(!isVisible);

            //Locked check
            bool isLocked = false;
            if (outfitScriptableObject.isLocked)
            {
                isLocked = !CharacterCustomizationFinderManager.GetOutfitController().becameUnlockedOutfits.Contains(outfitScriptableObject);
            }
            outfitLockedImage.gameObject.SetActive(isLocked);
            changeButton.gameObject.SetActive(!isLocked);
            if (buyButton != null)
                buyButton.gameObject.SetActive(isLocked);

            //Equip Check
            if (CharacterCustomizationFinderManager.GetOutfitController().equippedOutfits.ContainsKey(outfitScriptableObject))
            {
                outfitEquippedImage.gameObject.SetActive(true);
                changeButton.GetComponentInChildren<Text>().text = "Remove";
            }
            else
            {
                outfitEquippedImage.gameObject.SetActive(false);
                changeButton.GetComponentInChildren<Text>().text = "Add";
            }

            UpdateIfCategoryDefault(CharacterCustomizationFinderManager.GetOutfitController());
        }
    }
