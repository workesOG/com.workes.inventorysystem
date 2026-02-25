using com.workes.inventory.attributes;
using com.workes.inventory.tags;

namespace com.workes.inventory.core
{
    public class ItemDefinition<TKey>
    {
        public TKey Id { get; }

        public DefinitionSchema Schema { get; } = new();
        public AttributeContainer Attributes { get; } = new();
        public TagContainer Tags { get; } = new();

        public ItemDefinition(TKey id, AttributeContainer attributes = null, TagContainer tags = null)
        {
            Id = id;
            Attributes = attributes ?? new AttributeContainer();
            Tags = tags ?? new TagContainer();
        }

        public void Validate()
        {
            Schema.Validate(Attributes);
        }
    }
}
