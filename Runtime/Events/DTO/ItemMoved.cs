using com.workes.inventory.core;
using com.workes.inventory.layout;

namespace com.workes.inventory.events.dto
{
    public class ItemMoved<TKey>
    {
        public ItemInstance<TKey> Instance { get; }
        public ILayoutContext<TKey> FromPosition { get; }
        public ILayoutContext<TKey> ToPosition { get; }

        public ItemMoved(ItemInstance<TKey> instance, ILayoutContext<TKey> fromPosition, ILayoutContext<TKey> toPosition)
        {
            Instance = instance;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }
}
