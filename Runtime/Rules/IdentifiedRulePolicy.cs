using com.workes.inventory.core;
using System;

namespace com.workes.inventory.rules
{
    /// <summary>
    /// Wraps a rule and overrides its identity for stable dictionary-key based management.
    /// </summary>
    public sealed class IdentifiedRulePolicy<TKey> : IRulePolicy<TKey>
    {
        private readonly IRulePolicy<TKey> _inner;

        public string Id { get; }

        public IdentifiedRulePolicy(string id, IRulePolicy<TKey> inner)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Rule id cannot be null/empty.", nameof(id));
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            Id = id;
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            return _inner.CanApply(inventory, transaction, out error);
        }
    }
}

