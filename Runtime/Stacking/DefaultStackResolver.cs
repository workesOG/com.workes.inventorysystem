using com.workes.inventory.core;

namespace com.workes.inventory.stacking
{
    public class DefaultStackResolver<TKey> : IStackResolver<TKey>
    {
        private readonly int _defaultMaxStack;

        public DefaultStackResolver(int defaultMaxStack)
        {
            _defaultMaxStack = defaultMaxStack;
        }

        public int ResolveMaxStackSize(Inventory<TKey> inventory, ItemInstance<TKey> instance) => _defaultMaxStack;
    }
}
