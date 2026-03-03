using com.workes.inventory.core;

namespace com.workes.inventory.events.dto
{
    public class ItemModified<TKey>
    {
        public ItemInstance<TKey> Instance { get; }
        public int Index { get; }

        public ItemModified(ItemInstance<TKey> instance, int index)
        {
            Instance = instance;
            Index = index;
        }
    }
}
