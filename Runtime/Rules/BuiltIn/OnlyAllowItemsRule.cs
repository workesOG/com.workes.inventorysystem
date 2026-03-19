using com.workes.inventory.core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.workes.inventory.rules
{
    public class OnlyAllowItemsRule<TKey> : IRulePolicy<TKey>
    {
        private readonly HashSet<ItemDefinition<TKey>> _allowed;

        public OnlyAllowItemsRule(params ItemDefinition<TKey>[] allowed)
        {
            _allowed = new HashSet<ItemDefinition<TKey>>(allowed);
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            foreach (var (definition, _, _) in transaction.Added)
            {
                if (!_allowed.Contains(definition))
                {
                    error = $"Item is not allowed in inventory. OnlyAllowItemsRule allows these items: {string.Join(", ", _allowed.Select(x => x.Id.ToString()))}";
                    return false;
                }
            }

            error = null;
            return true;
        }
    }
}