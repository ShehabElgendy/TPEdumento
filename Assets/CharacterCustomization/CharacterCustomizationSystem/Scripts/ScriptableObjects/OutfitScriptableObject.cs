using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "NewOutfit", menuName = "ScriptableObjects/OutfitScriptableObject", order = 1)]
    public class OutfitScriptableObject : ScriptableObject {

        public string outfitName;

        public Sprite outfitIcon;

        [Tooltip("If IsInvisible then the Outfit will appear as a question mark and the player will need to Discover the Outfit before it can be Equipped.")]
        public bool isInvisible;
        [Tooltip("If IsLocked then the player will not own the Outfit and will need to Buy it before the Outfit can be equipped.")]
        public bool isLocked;

        [Tooltip("Sorting order determines which Outfit appears above or below the others in the same Category in the view. The higher the sorting order the higher up the Outfit will be.")]
        public int sortingOrder;

        public OutfitCategoryScriptableObject outfitCategoryScriptableObject;

        [Tooltip("List of prefabs that are instantiated when the outfit is equipped or destroyed when the outfit is unequipped. This list cannot have null references. Used if the list is not empty.")]
        public List<GameObject> outfitPieces;

        [Tooltip("List of names of objects inside the root that have renderer components (SkinnedMeshRenderers or MeshRenderers) which need to be enabled or disabled when the outfit is equipped/unequipped. Used if the list is not empty.")]
        public List<string> rendererObjectNames;

        [Tooltip("If on, RendererObjectNames can also include renderers outside the root to toggle enable/disable. Only needed if you want to hide or show without instantiating/destroying.")]
        public bool checkOutsideRoot = false;

        public bool isEquipped { get; internal set; }
    }

