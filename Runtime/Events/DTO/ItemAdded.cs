using com.workes.inventory.core;

namespace com.workes.inventory.events.dto
{
    public class ItemAdded<TKey>
    {
        public ItemInstance<TKey> Item { get; set; }
        public int Index { get; set; }

        public ItemAdded(ItemInstance<TKey> item, int index)
        {
            Item = item;
            Index = index;
        }
    }
}
