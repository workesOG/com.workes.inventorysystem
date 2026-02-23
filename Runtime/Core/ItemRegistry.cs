using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace com.workes.inventory.core
{
    public class ItemRegistry<TKey>
    {
        private readonly Dictionary<TKey, ItemDefinition<TKey>> _definitions = new();

        private readonly Dictionary<TKey, TKey> _migrations = new();

        public void Register(ItemDefinition<TKey> definition)
        {
            if (definition == null)
                throw new ArgumentNullException("Definition cannot be null");

            if (_definitions.ContainsKey(definition.Id))
                throw new InvalidOperationException("Duplicate item ID.");

            definition.Validate();

            _definitions.Add(definition.Id, definition);
        }

        public void RegisterMigration(TKey oldId, TKey newId)
        {
            if (oldId == null || newId == null)
                throw new ArgumentNullException("Old or new ID cannot be null");

            if (_migrations.ContainsKey(oldId))
                throw new InvalidOperationException("Duplicate migration ID.");

            _migrations[oldId] = newId;
        }

        public bool TryGet(TKey id, out ItemDefinition<TKey> definition)
        {
            return _definitions.TryGetValue(id, out definition);
        }

        public ItemDefinition<TKey> Resolve(TKey id)
        {
            if (_migrations.TryGetValue(id, out var migratedId))
                id = migratedId;

            if (!_definitions.TryGetValue(id, out var definition))
                throw new InvalidOperationException(
                    $"Item definition '{id}' could not be resolved.");

            return definition;
        }
    }
}
