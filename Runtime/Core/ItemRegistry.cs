using System;
using System.Collections.Generic;

namespace com.workes.inventory.core
{
    public class ItemRegistry<TKey>
    {
        private readonly Dictionary<TKey, ItemDefinition<TKey>> _definitions = new();

        public void Register(ItemDefinition<TKey> definition)
        {
            if (_definitions.ContainsKey(definition.Id))
                throw new InvalidOperationException("Duplicate item ID.");

            definition.Validate();

            _definitions.Add(definition.Id, definition);
        }

        public bool TryGet(TKey id, out ItemDefinition<TKey> definition)
        {
            return _definitions.TryGetValue(id, out definition);
        }
    }
}
