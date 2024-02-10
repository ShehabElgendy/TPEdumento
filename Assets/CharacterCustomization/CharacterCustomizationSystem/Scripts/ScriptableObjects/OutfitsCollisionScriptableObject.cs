using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "NewOutfitsCollision", menuName = "ScriptableObjects/OutfitsCollisionScriptableObject", order = 3)]
    public class OutfitsCollisionScriptableObject : ScriptableObject {
    
        public List<bool> outfitCollisions = new List<bool>();
    }

