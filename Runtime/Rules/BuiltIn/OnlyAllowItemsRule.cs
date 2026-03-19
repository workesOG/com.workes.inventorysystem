using com.workes.inventory.core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.workes.inventory.rules
{
    public class OnlyAllowItemsRule<TKey> : IRulePolicy<TKey>
    {
        private readonly HashSet<ItemDefinition<TKey>> _allowed;
        public string Id { get; }

        public OnlyAllowItemsRule(params ItemDefinition<TKey>[] allowed)
        {
            _allowed = new HashSet<ItemDefinition<TKey>>(allowed);
            var allowedDescription = allowed == null
                ? string.Empty
                : string.Join(", ", allowed.Select(x => x.Id.ToString()));
            Id = $"OnlyAllowItems[{allowedDescription}]";
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            var allowedDescription = string.Join(", ", _allowed.Select(x => x.Id.ToString()));
            foreach (var (definition, _, _) in transaction.Added)
            {
                if (!_allowed.Contains(definition))
                {
                    error = $"Expected transaction to only add allowed items. OnlyAllowItemsRule allows: {allowedDescription}, but it attempted to add '{definition.Id}'.";
                    return false;
                }
            }

            error = null;
            return true;
        }
    }
}