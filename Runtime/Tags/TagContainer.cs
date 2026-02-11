using System.Collections.Generic;

namespace com.workes.inventory.tags
{
    public class TagContainer
    {
        private readonly HashSet<TagKey> _tags = new();

        public void Add(TagKey tag)
        {
            _tags.Add(tag);
        }

        public bool Has(TagKey tag)
        {
            return _tags.Contains(tag);
        }

        public IEnumerable<TagKey> All() => _tags;
    }
}