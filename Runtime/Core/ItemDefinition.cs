using com.workes.inventory.attributes;
using com.workes.inventory.tags;

namespace com.workes.inventory.core
{
    public class ItemDefinition<TKey>
    {
        public TKey Id { get; }

        public AttributeContainer Attributes { get; } = new();
        public TagContainer Tags { get; } = new();

        public string Category { get; set; }

        public ItemDefinition(TKey id)
        {
            Id = id;
        }
    }
}
