/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    [CreateAssetMenu(fileName = "NewOutfitCategory", menuName = "ScriptableObjects/OutfitCategoryScriptableObject", order = 2)]
    public class OutfitCategoryScriptableObject : ScriptableObject {

        public string categoryName;

        [Tooltip("Sorting Order determines which Category appears above or below another one. The higher the sorting order the higher up the Category will be.")]
        public int sortingOrder;

        [Tooltip("IsInvisible is optional and used to prevent the Category from being seen.")]
        public bool isInvisible;

        [Tooltip("DefaultOutfitScriptableObject is optional and determines what Outfit is equipped if nothing in the category is Equipped.")]
        public OutfitScriptableObject defaultOutfitScriptableObject;

        [Tooltip("TargetBoneName is an optional string matching the name of a bone in your Character Base. If you have it set then the CharacterCustomizationCameraView in your male or female scene will position itself at the X,Y position of the bone.")]
        public string targetBoneName;

        [Tooltip("CategoryZoom is an optional float that sets the FieldOfView of the camera when the Category is selected (if there is a TargetBoneName).")]
        public float categoryZoom;
    }
}
