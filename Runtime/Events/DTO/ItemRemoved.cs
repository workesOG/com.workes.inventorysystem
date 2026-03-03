using com.workes.inventory.core;

namespace com.workes.inventory.events.dto
{
    public class ItemRemoved<TKey>
    {
        public ItemInstance<TKey> Instance { get; }
        public int Index { get; }

        public ItemRemoved(ItemInstance<TKey> instance, int index)
        {
            Instance = instance;
            Index = index;
        }
    }
}
