using System;
using System.Collections.Generic;
using System.Linq;
using com.workes.inventory.attributes;
using com.workes.inventory.layout;
using com.workes.inventory.stacking;
using com.workes.inventory.capacity;

namespace com.workes.inventory.core
{
    public class Inventory<TKey>
    {
        private readonly List<ItemInstance<TKey>> _items = new();

        private readonly IStackResolver<TKey> _stackResolver;
        private readonly ICapacityPolicy<TKey> _capacityPolicy;
        private readonly IInventoryLayout<TKey> _layout;

        public AttributeContainer Attributes { get; } = new();

        public event Action OnChanged;

        public Inventory(
            IStackResolver<TKey> stackResolver = null,
            ICapacityPolicy<TKey> capacityPolicy = null,
            IInventoryLayout<TKey> layout = null)
        {
            _stackResolver = stackResolver;
            _capacityPolicy = capacityPolicy;
            _layout = layout;
        }

        public IReadOnlyList<ItemInstance<TKey>> Items => _items;
        public int InstanceCount => _items.Count;
        public int TotalItemCount => _items.Sum(i => i.Amount);

        private void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            _layout.OnItemRemoved(this, index);
        }

        private void SetAmountAt(int index, int amount)
        {
            if (amount <= 0)
            {
                RemoveAt(index);
                return;
            }

            _items[index].SetAmount(amount);
        }

        private void AddItem(ItemInstance<TKey> instance, ILayoutContext<TKey>? context)
        {
            _items.Add(instance);
            _layout.OnItemAdded(this, _items.Count - 1, context);
        }

        public bool TryAdd(ItemDefinition<TKey> definition, out string? error, int amount = 1, ILayoutContext<TKey>? context = null)
        {
            error = null;

            if (amount <= 0)
            {
                error = "Amount must be greater than zero.";
                return false;
            }

            var prototype = new ItemInstance<TKey>(definition, 1);
            int maxStack = _stackResolver != null
                ? _stackResolver.ResolveMaxStackSize(this, prototype)
                : 1;

            int remaining = amount;

            // Fill existing stack-compatible slots first (no-op when maxStack == 1)
            for (int i = 0; i < _items.Count && remaining > 0; i++)
            {
                var existing = _items[i];
                if (!existing.IsStackCompatible(prototype))
                    continue;

                int room = maxStack - existing.Amount;
                if (room <= 0)
                    continue;

                int add = Math.Min(remaining, room);
                existing.AddAmount(add);
                remaining -= add;
            }

            bool anyChange = remaining < amount;
            if (remaining <= 0)
            {
                if (anyChange)
                    OnChanged?.Invoke();
                return true;
            }

            // Add new instance(s) for the remainder
            while (remaining > 0)
            {
                int chunk = Math.Min(remaining, maxStack);
                var instance = new ItemInstance<TKey>(definition, chunk);

                if (!_capacityPolicy.CanAdd(this, instance, out error))
                {
                    if (anyChange)
                        OnChanged?.Invoke();
                    return false;
                }

                if (!_layout.CanAcceptNewItem(this, instance, context, out error))
                {
                    if (anyChange)
                        OnChanged?.Invoke();
                    return false;
                }

                AddItem(instance, context);
                remaining -= chunk;
                anyChange = true;
            }

            OnChanged?.Invoke();
            return true;
        }

        public bool TryRemove(ItemInstance<TKey> instance, out string? error, int amount = 1)
        {
            error = null;

            if (amount <= 0)
            {
                error = "Amount must be greater than zero.";
                return false;
            }

            int index = _items.IndexOf(instance);
            if (index == -1)
            {
                error = "Item not found in inventory.";
                return false;
            }

            if (instance.Amount < amount)
            {
                error = "Not enough quantity to remove.";
                return false;
            }

            if (instance.Amount == amount)
                RemoveAt(index);
            else
                instance.ReduceAmount(amount);

            OnChanged?.Invoke();
            return true;
        }

        public bool TryRemoveAtStorageIndex(int index, out string? error, int amount = 1)
        {
            error = null;

            if (index < 0 || index >= _items.Count)
            {
                error = "Index out of range.";
                return false;
            }

            var instance = _items[index];

            if (amount <= 0)
            {
                error = "Amount must be greater than zero.";
                return false;
            }

            if (instance.Amount < amount)
            {
                error = "Not enough quantity to remove.";
                return false;
            }

            if (instance.Amount == amount)
                RemoveAt(index);
            else
                instance.ReduceAmount(amount);

            OnChanged?.Invoke();
            return true;
        }

        public bool TryRemoveByDefinition(ItemDefinition<TKey> definition, int amount, bool ignoreMetadata, out string? error)
        {
            error = null;

            if (definition == null)
            {
                error = "Definition cannot be null.";
                return false;
            }

            if (amount <= 0)
            {
                error = "Amount must be greater than zero.";
                return false;
            }

            int remaining = amount;
            InstanceMetadata? referenceMetadata = null;

            for (int i = 0; i < _items.Count && remaining > 0; i++)
            {
                var instance = _items[i];
                if (!EqualityComparer<TKey>.Default.Equals(instance.Definition.Id, definition.Id))
                    continue;

                if (!ignoreMetadata)
                {
                    if (referenceMetadata == null)
                        referenceMetadata = instance.Metadata;
                    else if (!instance.Metadata.StructuralEquals(referenceMetadata))
                        continue;
                }

                int take = Math.Min(remaining, instance.Amount);
                remaining -= take;

                if (instance.Amount == take)
                    RemoveAt(i--);
                else
                    instance.ReduceAmount(take);
            }

            if (remaining > 0)
            {
                error = "Not enough matching items to remove.";
                return false;
            }

            OnChanged?.Invoke();
            return true;
        }

        public void Clear()
        {
            if (_items.Count == 0)
                return;

            _items.Clear();
            _layout.OnInventoryCleared(this);
            OnChanged?.Invoke();
        }

    }
}
