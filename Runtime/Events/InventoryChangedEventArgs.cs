using System;
using com.workes.inventory.events.dto;
using System.Collections.Generic;

namespace com.workes.inventory.events
{
    public class InventoryChangedEventArgs<TKey> : EventArgs
    {
        public List<ItemAdded<TKey>> Added { get; set; }
        public List<ItemRemoved<TKey>> Removed { get; set; }
        public List<ItemModified<TKey>> Modified { get; set; }
        public List<ItemMoved<TKey>> Moved { get; set; }

        public bool Cleared { get; set; }

        public InventoryChangedEventArgs()
        {
            Added = new List<ItemAdded<TKey>>();
            Removed = new List<ItemRemoved<TKey>>();
            Modified = new List<ItemModified<TKey>>();
            Moved = new List<ItemMoved<TKey>>();
            Cleared = false;
        }

        public InventoryChangedEventArgs(List<ItemAdded<TKey>> added = null, List<ItemRemoved<TKey>> removed = null, List<ItemModified<TKey>> modified = null, List<ItemMoved<TKey>> moved = null, bool cleared = false)
        {
            Added = added;
            Removed = removed;
            Modified = modified;
            Moved = moved;
            Cleared = false;
        }
    }
}
