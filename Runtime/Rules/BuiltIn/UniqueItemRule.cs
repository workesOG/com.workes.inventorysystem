using com.workes.inventory.core;
using System;

namespace com.workes.inventory.rules
{
    /// <summary>
    /// Limits how many instances of each item definition may exist in the inventory.
    /// For example, maxInstancesPerItem = 1 enforces classic "unique item" semantics.
    /// </summary>
    public class UniqueItemRule<TKey> : InventorySnapshotRulePolicy<TKey>
    {
        private readonly int _maxInstancesPerItem;

        public UniqueItemRule(int maxInstancesPerItem)
        {
            if (maxInstancesPerItem <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxInstancesPerItem), "Max instances must be greater than zero.");
            _maxInstancesPerItem = maxInstancesPerItem;
            Id = $"UniqueItemRule[{_maxInstancesPerItem}]";
        }

        protected override bool CanApplyWithSnapshot(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            InventoryRuleSnapshot<TKey> snapshot,
            out string? error)
        {
            foreach (var (definition, _, _) in transaction.Added)
            {
                var total = snapshot.GetQuantity(definition);
                if (total > _maxInstancesPerItem)
                {
                    error = $"Expected inventory to contain at most {_maxInstancesPerItem} instance(s) of item '{definition.Id}' after the transaction, but it would contain {total}.";
                    return false;
                }
            }

            error = null;
            return true;
        }
    }
}
