using System;

namespace com.workes.inventory.attributes
{
    public sealed class AttributeKey<T>
    {
        public string Id { get; }

        public AttributeKey(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("AttributeKey id cannot be null or empty");

            Id = id;
        }

        public override string ToString() => Id;
    }
}
