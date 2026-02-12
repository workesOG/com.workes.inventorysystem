using com.workes.inventory.core;

namespace com.workes.inventory.layout
{
    public class SlotLayoutContext<TKey> : ILayoutContext<TKey>
    {
        public int SlotIndex { get; }

        public SlotLayoutContext(int slotIndex)
        {
            SlotIndex = slotIndex;
        }
    }
}
