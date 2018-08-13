using System;
using System.Collections.Generic;
using Devdog.General;
using UnityEngine;

namespace Devdog.InventoryPro
{
    [System.Serializable]
    public struct LootCollection
    {
        public InventoryItemBase item;
        public uint amount;
    }

    public class MultiItemTrigger : TriggerBase
    {
        [NonSerialized]
        [HideInInspector]
        public new bool useWhenPlayerComesInRange = false;

        [ForceCustomObjectPicker]
        public InventoryItemBase itemPrefab;

        private InventoryItemBase _itemToAddToInventory;

        [SerializeField]
        private LootCollection[] _loot = new LootCollection[0];


        protected void SetItemToAddToInventory()
        {
            if (_loot != null && _itemToAddToInventory != null)
            {
                Destroy(_itemToAddToInventory.gameObject);
            }

            if (_loot == null)
            {
                _itemToAddToInventory = GetComponent<InventoryItemBase>();
            }
            else
            {
                foreach (LootCollection loot in _loot)
                {
                        _itemToAddToInventory = Instantiate<InventoryItemBase>(loot.item);
                        _itemToAddToInventory.currentStackSize = loot.item.currentStackSize;
                        _itemToAddToInventory.gameObject.SetActive(false);
                        _itemToAddToInventory.transform.SetParent(transform);
                }
            }
        }

        public override bool CanUse(Player player)
        {
            var canUse = base.CanUse(player);
            if (canUse == false)
            {
                return false;
            }

            if (_itemToAddToInventory == null)
            {
                return false;
            }

            return InventoryManager.CanAddItem(_itemToAddToInventory);
        }

        public override bool CanUnUse(Player player)
        {
            return false; // It's not possible to un-use an ItemTrigger
        }

        public override bool Use(Player player)
        {
            SetItemToAddToInventory();
            List<InventoryItemBase> myLoot = new List<InventoryItemBase>();
            foreach (LootCollection loot in _loot)
            {
                for (int i = 0; i < loot.amount; i++)
                {
                    myLoot.Add(loot.item);
                }
                if (CanUse(player) == false)
                {
                    if (loot.item != null && InventoryManager.CanAddItem(loot.item) == false)
                    {
                        InventoryManager.langDatabase.collectionFull.Show(_itemToAddToInventory.name, _itemToAddToInventory.description, "Inventory");
                    }

                    return false;
                }

                DoVisuals(); // Incase it's overwritten
                NotifyTriggerUsed(player);
                
                InventoryManager.AddItems(myLoot.ToArray());
                myLoot.Clear();
            }
            // If the item prefab is set we won't need this object anymore, as it's holding the item for us.
            if (_loot != null)
            {
                Destroy(gameObject);
            }

            return true;
        }

        public override bool UnUse(Player player)
        {
            return false; // Can't un-use an item
        }

        public override void DoVisuals()
        { }

        public override void UndoVisuals()
        { }
    }
}
