using System.Collections.Generic;

namespace com.workes.inventory.core
{
    [System.Serializable]
    /// <summary>
    /// Serialized representation of a single item instance.
    /// </summary>
    public class SerializedItem<TKey>
    {
        public TKey DefinitionId { get; set; }
        public int Amount { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}

