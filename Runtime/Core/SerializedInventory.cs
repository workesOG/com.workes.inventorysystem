using System.Collections.Generic;
using com.workes.inventory.layout;

namespace com.workes.inventory.core
{
    public class SerializedInventory<TKey>
    {
        public List<SerializedItem<TKey>> Items { get; set; } = new();
        public ILayoutPersistentData? LayoutData { get; set; }
    }
}