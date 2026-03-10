using com.workes.inventory.core;
using com.workes.inventory.layout;

namespace com.workes.inventory.events.dto
{
    public class ItemSwapped<TKey>
    {
        public ILayoutContext<TKey> FromPosition { get; }
        public ItemInstance<TKey> AfterSwapFromPositionInstance { get; }
        public ILayoutContext<TKey> ToPosition { get; }
        public ItemInstance<TKey> AfterSwapToPositionInstance { get; }

        public ItemSwapped(ILayoutContext<TKey> fromPosition, ILayoutContext<TKey> toPosition, ItemInstance<TKey> afterSwapFromPositionInstance, ItemInstance<TKey> afterSwapToPositionInstance)
        {
            FromPosition = fromPosition;
            AfterSwapFromPositionInstance = afterSwapFromPositionInstance;
            ToPosition = toPosition;
            AfterSwapToPositionInstance = afterSwapToPositionInstance;
        }
    }
}
