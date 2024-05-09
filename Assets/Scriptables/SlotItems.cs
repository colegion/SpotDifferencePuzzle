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
            List<Item> selected = new List<Item>(2);
            
            if (Items.Count <= 2)
            {
                return (Items[0], Items[1]);
            }

            for (int i = 0; i < 2; i++)
            {
                var randomIndex = Random.Range(0, Items.Count);
                var randomItem = Items[randomIndex];

                while (selected.Contains(randomItem))
                {
                    randomIndex = Random.Range(0, Items.Count);
                    randomItem = Items[randomIndex];
                }
        
                selected.Add(randomItem);
            }

            return (selected[0], selected[1]);
        }

    }
}
