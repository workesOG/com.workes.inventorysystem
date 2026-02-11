using System;
using System.Collections.Generic;
using com.workes.inventory.attributes;
using com.workes.inventory.stacking;

namespace com.workes.inventory.core
{
    public class Inventory<TKey>
    {
        private readonly List<ItemInstance<TKey>> _items = new();

        private readonly IStackResolver<TKey> _stackResolver;

        public AttributeContainer Attributes { get; } = new();

        public int Capacity { get; }

        public event Action OnChanged;

        public Inventory(
            int capacity,
            IStackResolver<TKey> stackResolver = null)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            Capacity = capacity;
            _stackResolver = stackResolver;
        }

        public IReadOnlyList<ItemInstance<TKey>> Items => _items;

        public bool TryAdd(ItemDefinition<TKey> definition, int amount = 1)
        {
            if (definition == null || amount <= 0)
                return false;

            if (_stackResolver == null)
                return TryAddWithoutStacking(definition, amount);

            return TryAddWithStacking(definition, amount);
        }

        private bool TryAddWithoutStacking(ItemDefinition<TKey> definition, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (_items.Count >= Capacity)
                    return false;

                _items.Add(new ItemInstance<TKey>(definition));
            }

            OnChanged?.Invoke();
            return true;
        }

        private bool TryAddWithStacking(ItemDefinition<TKey> definition, int amount)
        {
            int remaining = amount;
            int maxStack = _stackResolver.ResolveMaxStack(definition, this);

            foreach (var item in _items)
            {
                if (!EqualityComparer<TKey>.Default.Equals(item.Definition.Id, definition.Id))
                    continue;

                if (item.Amount >= maxStack)
                    continue;

                int space = maxStack - item.Amount;
                int toAdd = Math.Min(space, remaining);

                item.AddAmount(toAdd);
                remaining -= toAdd;

                if (remaining == 0)
                {
                    OnChanged?.Invoke();
                    return true;
                }
            }

            while (remaining > 0)
            {
                if (_items.Count >= Capacity)
                    return false;

                int toCreate = Math.Min(maxStack, remaining);
                _items.Add(new ItemInstance<TKey>(definition, toCreate));
                remaining -= toCreate;
            }

            OnChanged?.Invoke();
            return true;
        }
    }
}
