using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scriptables
{
    
    [CreateAssetMenu(fileName = "Slot Item Container", menuName = "Slot/ItemContainer")]
    public class SlotItems : ScriptableObject
    {
        public List<Item> Items;

        public Item GetRandomItem()
        {
            var index = Random.Range(0, Items.Count);
            return Items[index];
        }

        public (Item, Item) GetOppositeItems()
        {
            return (Items[0], Items[1]);
        }
    }
}
