using com.workes.inventory.core;

namespace com.workes.inventory.events.dto
{
    public class ItemMoved<TKey>
    {
        public ItemInstance<TKey> Item { get; set; }
        public object FromPosition { get; set; }
        public object ToPosition { get; set; }

        public ItemMoved(ItemInstance<TKey> item, object fromPosition, object toPosition)
        {
            Item = item;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }
}
