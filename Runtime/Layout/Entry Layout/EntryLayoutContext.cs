using com.workes.inventory.core;

namespace com.workes.inventory.layout
{
    /// <summary>
    /// Layout context for entry-based layouts, representing a target position
    /// in the structural ordering for move operations.
    /// </summary>
    public class EntryLayoutContext<TKey> : ILayoutContext<TKey>
    {
        public int TargetIndex { get; }

        public EntryLayoutContext(int targetIndex)
        {
            TargetIndex = targetIndex;
        }
    }
}

