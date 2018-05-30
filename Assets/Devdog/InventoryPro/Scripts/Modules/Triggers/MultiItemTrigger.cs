using System;
using System.Collections.Generic;
using Devdog.General;
using UnityEngine;

namespace Devdog.InventoryPro
{
    public class MultiItemTrigger : TriggerBase
    {
        [NonSerialized]
        [HideInInspector]
        public new bool useWhenPlayerComesInRange = false;

        [ForceCustomObjectPicker]
        public InventoryItemBase itemPrefab;

        [SerializeField]
        private InventoryItemBase[] _items = new InventoryItemBase[0];
        private InventoryItemBase _itemToAddToInventory;


        protected void SetItemToAddToInventory()
        {
            if (_items != null && _itemToAddToInventory != null)
            {
                Destroy(_itemToAddToInventory.gameObject);
            }

            if (_items == null)
            {
                _itemToAddToInventory = GetComponent<InventoryItemBase>();
            }
            else
            {
                foreach (InventoryItemBase item in _items)
                {

                    _itemToAddToInventory = Instantiate<InventoryItemBase>(item);
                    _itemToAddToInventory.currentStackSize = item.currentStackSize;
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

            foreach (InventoryItemBase item in _items)
            {
                if (CanUse(player) == false)
                {
                    if (item != null && InventoryManager.CanAddItem(item) == false)
                    {
                        InventoryManager.langDatabase.collectionFull.Show(_itemToAddToInventory.name, _itemToAddToInventory.description, "Inventory");
                    }

                    return false;
                }

                DoVisuals(); // Incase it's overwritten
                NotifyTriggerUsed(player);

                InventoryManager.AddItem(item);
            }
            // If the item prefab is set we won't need this object anymore, as it's holding the item for us.
            if (_items != null)
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
