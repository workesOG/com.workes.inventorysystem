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

        /// <summary>Evaluates whether the layout can satisfy the full transaction (structure: merge deltas, added instances, removals). Context is the placement context for the operation (e.g. specific slot); null means no constraint. Consulted alongside capacity before committing.</summary>
        bool CanSatisfyPlacement(Inventory<TKey> inventory, InventoryTransaction<TKey> transaction, ILayoutContext<TKey>? context, out string? error);

        bool CanAcceptNewItem(Inventory<TKey> inventory, ItemInstance<TKey> instance, ILayoutContext<TKey>? context, out string? error);

        void OnItemAdded(Inventory<TKey> inventory, int index, ILayoutContext<TKey>? context);

        void OnItemRemoved(Inventory<TKey> inventory, int index);

        void OnInventoryCleared(Inventory<TKey> inventory);

        ILayoutPersistentData GetPersistentData();

        void RestorePersistentData(ILayoutPersistentData persistentData);
    }
}
