using com.workes.inventory.core;
using System;

namespace com.workes.inventory.rules
{
    /// <summary>
    /// Wraps a snapshot-capable rule and overrides its identity.
    /// </summary>
    public sealed class IdentifiedSnapshotRulePolicy<TKey> : IRulePolicy<TKey>, IInventorySnapshotRulePolicy<TKey>
    {
        private readonly IInventorySnapshotRulePolicy<TKey> _innerSnapshot;
        private readonly IRulePolicy<TKey> _innerRule;

        public string Id { get; }

        public IdentifiedSnapshotRulePolicy(string id, IInventorySnapshotRulePolicy<TKey> innerSnapshot)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Rule id cannot be null/empty.", nameof(id));
            _innerSnapshot = innerSnapshot ?? throw new ArgumentNullException(nameof(innerSnapshot));
            _innerRule = innerSnapshot as IRulePolicy<TKey>
                ?? throw new ArgumentException("Snapshot rule must also implement IRulePolicy.", nameof(innerSnapshot));
            Id = id;
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            return _innerRule.CanApply(inventory, transaction, out error);
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            InventoryRuleSnapshot<TKey> snapshot,
            out string? error)
        {
            return _innerSnapshot.CanApply(inventory, transaction, snapshot, out error);
        }
    }
}

