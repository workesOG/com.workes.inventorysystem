using System.Collections.Generic;

namespace com.workes.inventory.layout
{
    public class SlotLayoutPersistentData : ILayoutPersistentData
    {
        public List<int?> SlotMap { get; set; } = new();

        public object? GetPersistentContext() => SlotMap;
    }
}
