using com.workes.inventory.core;

namespace com.workes.inventory.events.dto
{
    public class ItemMoved<TKey>
    {
        public ItemInstance<TKey> Instance { get; }
        public object FromPosition { get; }
        public object ToPosition { get; }

        public ItemMoved(ItemInstance<TKey> instance, object fromPosition, object toPosition)
        {
            Instance = instance;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }
}
