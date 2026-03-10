using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace com.workes.inventory.core
{
    public class ItemRegistry<TKey>
    {
        private readonly Dictionary<TKey, ItemDefinition<TKey>> _definitions = new();
        private readonly Dictionary<TKey, TKey> _migrations = new();

        private bool frozen = false;

        public IEnumerable<ItemDefinition<TKey>> Definitions => _definitions.Values;

        public void Register(ItemDefinition<TKey> definition)
        {
            if (frozen)
                throw new InvalidOperationException("Item registry is frozen and cannot be modified.");

            if (definition == null)
                throw new ArgumentNullException("Definition cannot be null");

            if (_definitions.ContainsKey(definition.Id))
                throw new InvalidOperationException("Duplicate item ID.");

            definition.Validate();

            _definitions.Add(definition.Id, definition);
        }

        public void RegisterMigration(TKey oldId, TKey newId)
        {
            if (frozen)
                throw new InvalidOperationException("Item registry is frozen and cannot be modified.");

            if (oldId == null || newId == null)
                throw new ArgumentNullException("Old or new ID cannot be null");

            if (_migrations.ContainsKey(oldId))
                throw new InvalidOperationException("Migration from this ID already exists.");

            if (_definitions.ContainsKey(oldId))
                throw new InvalidOperationException("Can't migrate from a registered definition.");

            _migrations[oldId] = newId;
        }

        public bool TryGet(TKey id, out ItemDefinition<TKey> definition)
        {
            return _definitions.TryGetValue(id, out definition);
        }

        public ItemDefinition<TKey> Resolve(TKey id)
        {
            while (_migrations.TryGetValue(id, out var migratedId)) // Resolve any migrations recursively
                id = migratedId;

            if (!_definitions.TryGetValue(id, out var definition))
                throw new InvalidOperationException(
                    $"Item definition '{id}' could not be resolved.");

            return definition;
        }

        public void Freeze() {frozen = true;}
    }
}
