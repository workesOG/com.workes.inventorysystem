using com.workes.inventory.core;

namespace com.workes.inventory.events.dto
{
    public class ItemModified<TKey>
    {
        public ItemInstance<TKey> Item { get; set; }
        public int Index { get; set; }

        public ItemModified(ItemInstance<TKey> item, int index)
        {
            Item = item;
            Index = index;
        }
    }
}
