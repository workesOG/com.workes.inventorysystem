using System;

namespace com.workes.inventory.tags
{
    public sealed class TagKey
    {
        public string Id { get; }

        public TagKey(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("TagKey id cannot be null or empty.");

            Id = id;
        }

        public override string ToString() => Id;
    }
}