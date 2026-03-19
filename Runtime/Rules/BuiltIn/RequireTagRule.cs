using com.workes.inventory.core;
using com.workes.inventory.tags;
using System;

namespace com.workes.inventory.rules
{
    public class RequireTagRule<TKey> : IRulePolicy<TKey>
    {
        private readonly TagKey _tag;

        public RequireTagRule(TagKey tag)
        {
            _tag = tag;
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            foreach (var (definition, _, _) in transaction.Added)
            {
                if (!definition.HasTag(_tag))
                {
                    error = $"Item must have tag '{_tag}'.";
                    return false;
                }
            }

            error = null;
            return true;
        }
    }
}