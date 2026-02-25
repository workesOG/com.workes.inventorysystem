using System;
using System.Collections.Generic;

namespace com.workes.inventory.core
{
    /// <summary>
    /// Semantic representation of inventory change: which items (by definition and metadata) were added and removed.
    /// Same definition with different metadata is distinct (e.g. 90 apples and 10 apples[metadata]).
    /// Used by capacity policies to evaluate an entire transaction.
    /// </summary>
    public class NormalizedInventoryTransaction<TKey>
    {
        private readonly IReadOnlyList<(ItemDefinition<TKey> definition, InstanceMetadata? metadata, int amount)> _added;
        private readonly IReadOnlyList<(ItemDefinition<TKey> definition, InstanceMetadata? metadata, int amount)> _removed;

        internal NormalizedInventoryTransaction(
            IReadOnlyList<(ItemDefinition<TKey> definition, InstanceMetadata? metadata, int amount)> added,
            IReadOnlyList<(ItemDefinition<TKey> definition, InstanceMetadata? metadata, int amount)> removed)
        {
            _added = added ?? Array.Empty<(ItemDefinition<TKey>, InstanceMetadata?, int)>();
            _removed = removed ?? Array.Empty<(ItemDefinition<TKey>, InstanceMetadata?, int)>();
        }

        /// <summary>Items to add: (definition, metadata, total amount) per definition+metadata group.</summary>
        public IReadOnlyList<(ItemDefinition<TKey> definition, InstanceMetadata? metadata, int amount)> Added => _added;

        /// <summary>Items to remove: (definition, metadata, total amount) per definition+metadata group.</summary>
        public IReadOnlyList<(ItemDefinition<TKey> definition, InstanceMetadata? metadata, int amount)> Removed => _removed;

        /// <summary>Whether this normalized transaction has no net change.</summary>
        public bool IsEmpty => _added.Count == 0 && _removed.Count == 0;
    }
}
