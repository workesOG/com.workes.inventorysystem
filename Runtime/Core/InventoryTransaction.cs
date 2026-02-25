using System;
using System.Collections.Generic;
using com.workes.inventory.layout;

namespace com.workes.inventory.core
{
    /// <summary>
    /// Inventory-specific structural change set (merge deltas, added/removed instances). Used internally
    /// for atomic operations and cross-inventory; custom capacity and layout policies can use it when
    /// implementing <see cref="IInventoryLayout{TKey}.CanSatisfyPlacement"/>.
    /// </summary>
    public class InventoryTransaction<TKey>
    {
        private readonly Inventory<TKey> _inventory;
        private readonly IReadOnlyList<(int index, int delta)> _amountDeltas;
        private readonly IReadOnlyList<(int index, ItemInstance<TKey> instance)> _removed;
        private readonly IReadOnlyList<(ItemInstance<TKey> instance, ILayoutContext<TKey>? context)> _added;
        private bool _applied;

        internal InventoryTransaction(
            Inventory<TKey> inventory,
            IReadOnlyList<(int index, int delta)> amountDeltas,
            IReadOnlyList<(int index, ItemInstance<TKey> instance)> removed,
            IReadOnlyList<(ItemInstance<TKey> instance, ILayoutContext<TKey>? context)> added)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _amountDeltas = amountDeltas ?? Array.Empty<(int, int)>();
            _removed = removed ?? Array.Empty<(int, ItemInstance<TKey>)>();
            _added = added ?? Array.Empty<(ItemInstance<TKey>, ILayoutContext<TKey>?)>();
        }

        /// <summary>Origin inventory this transaction applies to.</summary>
        public Inventory<TKey> Inventory => _inventory;

        /// <summary>Amount deltas (index, delta): merge adds or partial removes.</summary>
        public IReadOnlyList<(int index, int delta)> AmountDeltas => _amountDeltas;

        /// <summary>Full removals (index, instance).</summary>
        public IReadOnlyList<(int index, ItemInstance<TKey> instance)> Removed => _removed;

        /// <summary>New instances to add (instance, layout context).</summary>
        public IReadOnlyList<(ItemInstance<TKey> instance, ILayoutContext<TKey>? context)> Added => _added;

        /// <summary>Whether this transaction has been applied by the origin inventory.</summary>
        public bool IsApplied => _applied;

        internal void MarkApplied() => _applied = true;
    }
}
