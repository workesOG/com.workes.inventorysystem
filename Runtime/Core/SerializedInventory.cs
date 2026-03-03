using System.Collections.Generic;

namespace com.workes.inventory.core
{
    /// <summary>
    /// Serializable snapshot of an inventory for persistence.
    /// </summary>
    [System.Serializable]
    public class SerializedInventory<TKey>
    {
        public List<SerializedItem<TKey>> Items { get; set; } = new();
        public object LayoutData { get; set; }
    }
}
