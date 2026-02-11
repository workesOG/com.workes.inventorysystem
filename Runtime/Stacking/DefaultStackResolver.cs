using com.workes.inventory.attributes;
using com.workes.inventory.core;

namespace com.workes.inventory.stacking
{
    public class DefaultStackResolver<TKey> : IStackResolver<TKey>
    {
        private readonly AttributeKey<int> _maxStackKey;

        public DefaultStackResolver(AttributeKey<int> maxStackKey)
        {
            _maxStackKey = maxStackKey;
        }

        public int ResolveMaxStack(
            ItemDefinition<TKey> definition,
            Inventory<TKey> inventory)
        {
            return definition.Attributes.GetOrDefault(_maxStackKey, 1);
        }
    }
}
