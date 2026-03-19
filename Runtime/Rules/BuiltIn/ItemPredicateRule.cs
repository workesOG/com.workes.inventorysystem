using com.workes.inventory.core;
using System;

namespace com.workes.inventory.rules
{
    public class ItemPredicateRule<TKey> : IRulePolicy<TKey>
    {
        private readonly Func<ItemDefinition<TKey>, InstanceMetadata?, bool> _predicate;
        private readonly string _errorMessage;

        public ItemPredicateRule(
            Func<ItemDefinition<TKey>, InstanceMetadata?, bool> predicate,
            string errorMessage = "Item violates predicate rule")
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _errorMessage = errorMessage;
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            foreach (var (definition, metadata, _) in transaction.Added)
            {
                if (!_predicate(definition, metadata))
                {
                    error = _errorMessage;
                    return false;
                }
            }

            error = null;
            return true;
        }
    }
}