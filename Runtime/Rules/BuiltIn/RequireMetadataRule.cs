using com.workes.inventory.core;
using System;

namespace com.workes.inventory.rules
{
    public class RequireMetadataRule<TKey> : IRulePolicy<TKey>
    {
        private readonly string _key;
        private readonly object _value;
        public string Id { get; }

        public RequireMetadataRule(string key, object value)
        {
            _key = key;
            _value = value;
            Id = $"RequireMetadata[{_key}={_value?.ToString()}]";
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            foreach (var (_, metadata, _) in transaction.Added)
            {
                if (metadata == null || !metadata.TryGet<object>(_key, out var val) || !val.Equals(_value))
                {
                    error = $"Expected item metadata '{_key}' to equal '{_value}'.";
                    return false;
                }
            }

            error = null;
            return true;
        }
    }
}