/******************************************************************************************************

Copyright (c) Comfort Games and its affiliates. All rights reserved.
Unless required by applicable law or agreed to in writing,
the code is provided "AS IS" WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

******************************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace ComfortGames.CharacterCustomization {

    [CreateAssetMenu(fileName = "NewOutfitsCollision", menuName = "ScriptableObjects/OutfitsCollisionScriptableObject", order = 3)]
    public class OutfitsCollisionScriptableObject : ScriptableObject {
    
        public List<bool> outfitCollisions = new List<bool>();
    }
}

