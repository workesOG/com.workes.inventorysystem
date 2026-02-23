using System;
using System.Collections.Generic;
using com.workes.inventory.core;

namespace com.workes.inventory.layout
{
    public class EntryLayout<TKey> : IInventoryLayout<TKey>
    {
        public int GetSlotCount(Inventory<TKey> inventory) => inventory.Items.Count;

        public ItemInstance<TKey>? GetAt(Inventory<TKey> inventory, int index)
        {
            if (index < 0 || index >= inventory.Items.Count)
                return null;

            return inventory.Items[index];
        }

        public int? GetSlotOfItem(int index)
        {
            return index;
        }

        public IEnumerable<int> GetMergeCandidates(Inventory<TKey> inventory, ItemInstance<TKey> prototype, ILayoutContext<TKey>? context)
        {
            for (int i = 0; i < inventory.Items.Count; i++)
                yield return i;
        }

        public bool CanSatisfyPlacement(Inventory<TKey> inventory, ItemInstance<TKey> prototype, int requiredNewInstanceCount, ILayoutContext<TKey>? context)
        {
            return requiredNewInstanceCount >= 0;
        }

        public bool CanAcceptNewItem(Inventory<TKey> inventory, ItemInstance<TKey> instance, ILayoutContext<TKey>? context, out string? error)
        {
            error = null;
            return true;
        }

        public void OnItemAdded(Inventory<TKey> inventory, int index, ILayoutContext<TKey>? context)
        {

        }

        public void OnItemRemoved(Inventory<TKey> inventory, int index)
        {

        }

        public void OnInventoryCleared(Inventory<TKey> inventory)
        {

        }

        public ILayoutPersistentData GetPersistentData() => null;

        public void RestorePersistentData(ILayoutPersistentData? data) { }
    }
}
