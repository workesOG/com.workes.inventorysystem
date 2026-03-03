using System.Collections.Generic;
using com.workes.inventory.core;

namespace com.workes.inventory.layout
{
    public interface IInventoryLayout<TKey>
    {
        int GetSlotCount(Inventory<TKey> inventory);

        ItemInstance<TKey> GetAt(Inventory<TKey> inventory, int index);

        int? GetSlotOfItem(int itemIndex);

        IEnumerable<int> GetMergeCandidates(Inventory<TKey> inventory, ItemInstance<TKey> prototype, ILayoutContext<TKey>? context);

        bool CanSatisfyPlacement(Inventory<TKey> inventory, InventoryTransaction<TKey> transaction, ILayoutContext<TKey>? context, out string? error);

        bool CanAcceptNewItem(Inventory<TKey> inventory, ItemInstance<TKey> instance, ILayoutContext<TKey>? context, out string? error);

        bool TryMove(Inventory<TKey> inventory, int storageIndex, ILayoutContext<TKey>? context, out object fromPosition, out object toPosition , out string? error);

        void OnItemAdded(Inventory<TKey> inventory, int index, ILayoutContext<TKey>? context);

        void OnItemRemoved(Inventory<TKey> inventory, int index);

        void OnInventoryCleared(Inventory<TKey> inventory);

        ILayoutPersistentData GetPersistentData();

        void RestorePersistentData(ILayoutPersistentData persistentData);

        /// <summary>Returns a new layout instance with the same state. Used for simulation cloning.</summary>
        IInventoryLayout<TKey> Clone();
    }
}
