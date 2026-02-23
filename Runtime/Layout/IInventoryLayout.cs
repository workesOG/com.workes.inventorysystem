using System.Collections.Generic;
using com.workes.inventory.core;

namespace com.workes.inventory.layout
{
    public interface IInventoryLayout<TKey>
    {
        int GetSlotCount(Inventory<TKey> inventory);

        ItemInstance<TKey> GetAt(Inventory<TKey> inventory, int index);

        int? GetSlotOfItem(int itemIndex);

        bool CanAcceptNewItem(Inventory<TKey> inventory, ItemInstance<TKey> instance, ILayoutContext<TKey>? context, out string? error);

        void OnItemAdded(Inventory<TKey> inventory, int index, ILayoutContext<TKey>? context);

        void OnItemRemoved(Inventory<TKey> inventory, int index);

        void OnInventoryCleared(Inventory<TKey> inventory);

        ILayoutPersistentData<TKey> GetPersistentData();

        void RestorePersistentData(ILayoutPersistentData<TKey> persistentData);
    }
}
