using System;
using System.Collections.Generic;
using com.workes.inventory.core;

namespace com.workes.inventory.layout
{
    public class SlotLayout<TKey> : IInventoryLayout<TKey>
    {
        private readonly List<int?> _slotMap;

        public SlotLayout(int slotCount)
        {
            _slotMap = new List<int?>(slotCount);
            for (int i = 0; i < slotCount; i++)
                _slotMap.Add(null);
        }

        public SlotLayout(object? persistentContext)
        {
            _slotMap = (List<int?>)persistentContext!;
        }

        public int GetSlotCount(Inventory<TKey> inventory) => _slotMap.Count;

        public ItemInstance<TKey>? GetAt(Inventory<TKey> inventory, int index)
        {
            if (index < 0 || index >= _slotMap.Count)
                return null;

            var itemIndex = _slotMap[index];
            if (!itemIndex.HasValue)
                return null;

            return inventory.Items[itemIndex.Value];
        }

        public int? GetSlotOfItem(int index)
        {
            for (int i = 0; i < _slotMap.Count; i++)
            {
                if (_slotMap[i] == index)
                    return i;
            }

            return null;
        }

        public bool CanAcceptNewItem(Inventory<TKey> inventory, ItemInstance<TKey> instance, ILayoutContext<TKey>? context, out string? error)
        {
            error = null;

            int slot;
            if (context is SlotLayoutContext<TKey> slotContext)
            {
                slot = slotContext.SlotIndex;
                if (slot < 0 || slot >= _slotMap.Count)
                {
                    error = "Slot index out of range.";
                    return false;
                }
            }
            else
            {
                slot = FindFirstAvailableSlot();
                if (slot < 0)
                {
                    error = "No available slot.";
                    return false;
                }
            }

            if (_slotMap[slot].HasValue)
            {
                error = "Slot already occupied.";
                return false;
            }

            return true;
        }

        private int FindFirstAvailableSlot()
        {
            for (int i = 0; i < _slotMap.Count; i++)
            {
                if (!_slotMap[i].HasValue)
                    return i;
            }
            return -1;
        }

        public void OnItemAdded(Inventory<TKey> inventory, int index, ILayoutContext<TKey>? context)
        {
            int slot = context is SlotLayoutContext<TKey> slotContext
                ? slotContext.SlotIndex
                : FindFirstAvailableSlot();
            _slotMap[slot] = index;
        }

        public void OnItemRemoved(Inventory<TKey> inventory, int removedIndex)
        {
            for (int i = 0; i < _slotMap.Count; i++)
            {
                if (_slotMap[i] == removedIndex)
                    _slotMap[i] = null;
                else if (_slotMap[i] > removedIndex)
                    _slotMap[i]--;
            }
        }

        public void OnInventoryCleared(Inventory<TKey> inventory)
        {
            for (int i = 0; i < _slotMap.Count; i++)
                _slotMap[i] = null;
        }

        public ILayoutPersistentData GetPersistentData()
        {
            return new SlotLayoutPersistentData
            {
                SlotMap = new List<int?>(_slotMap)
            };
        }

        public void RestorePersistentData(ILayoutPersistentData? data)
        {
            if (data is not SlotLayoutPersistentData slotData)
                throw new InvalidOperationException("Invalid layout data");

            _slotMap.Clear();
            _slotMap.AddRange(slotData.SlotMap);
        }
    }

}
