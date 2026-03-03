using System;
using System.Collections.Generic;
using com.workes.inventory.layout;

namespace com.workes.inventory.core
{
    /// <summary>
    /// Builds a bulk transaction by operating on a simulated inventory state.
    /// Use <see cref="Inventory{TKey}.CreateTransactionBuilder"/> to create.
    /// Add/remove operations update the simulated state; call <see cref="ToInventoryTransaction"/>
    /// to get the merged transaction, then <see cref="Inventory{TKey}.CommitTransaction"/> on the original inventory.
    /// </summary>
    public class InventoryTransactionBuilder<TKey>
    {
        private readonly Inventory<TKey> _targetInventory;
        private readonly Inventory<TKey> _simulation;
        private InventoryTransaction<TKey>? _accumulatedTransaction;

        internal InventoryTransactionBuilder(Inventory<TKey> targetInventory, Inventory<TKey> simulation)
        {
            _targetInventory = targetInventory ?? throw new ArgumentNullException(nameof(targetInventory));
            _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        }

        /// <summary>Adds items to the simulated state. Returns false if the operation would fail.</summary>
        public bool TryAdd(ItemDefinition<TKey> definition, out string? error, int amount = 1, ILayoutContext<TKey>? context = null)
        {
            return TryAdd(definition, amount, context, null, out error);
        }

        /// <summary>Adds items with optional metadata. Returns false if the operation would fail.</summary>
        public bool TryAdd(ItemDefinition<TKey> definition, int amount, ILayoutContext<TKey>? context, InstanceMetadata? metadata, out string? error)
        {
            error = null;
            if (!_simulation.TryFormulateAdd(definition, amount, context, metadata, out var tx, out error) || tx == null)
                return false;

            MergeAndApply(tx);
            return true;
        }

        /// <summary>Removes items from the simulated state. Returns false if the operation would fail.
        /// The instance may be from the original inventory; it will be resolved to the matching instance in the simulation.</summary>
        public bool TryRemove(ItemInstance<TKey> instance, out string? error, int amount = 1)
        {
            error = null;
            var simulationInstance = ResolveToSimulationInstance(instance);
            if (simulationInstance == null)
            {
                error = "Item not found in inventory.";
                return false;
            }
            if (!_simulation.TryFormulateRemove(simulationInstance, amount, out var tx, out error) || tx == null)
                return false;

            MergeAndApply(tx);
            return true;
        }

        /// <summary>Removes items at the given storage index. Returns false if the operation would fail.</summary>
        public bool TryRemoveAtStorageIndex(int index, out string? error, int amount = 1)
        {
            error = null;
            if (!_simulation.TryFormulateRemoveAt(index, amount, out var tx, out error) || tx == null)
                return false;

            MergeAndApply(tx);
            return true;
        }

        /// <summary>Removes items by definition. Returns false if the operation would fail.</summary>
        public bool TryRemoveByDefinition(ItemDefinition<TKey> definition, int amount, bool ignoreMetadata, out string? error)
        {
            error = null;
            if (!_simulation.TryFormulateRemoveByDefinition(definition, amount, ignoreMetadata, out var tx, out error) || tx == null)
                return false;

            MergeAndApply(tx);
            return true;
        }

        /// <summary>
        /// Produces an <see cref="InventoryTransaction{TKey}"/> targeting the original inventory.
        /// Call <see cref="Inventory{TKey}.CommitTransaction"/> with the result.
        /// </summary>
        public InventoryTransaction<TKey> ToInventoryTransaction()
        {
            if (_accumulatedTransaction == null)
            {
                return new InventoryTransaction<TKey>(
                    _targetInventory,
                    new List<(int index, int delta)>(),
                    new List<(int index, ItemInstance<TKey> instance)>(),
                    new List<(ItemInstance<TKey> instance, ILayoutContext<TKey>? context)>());
            }

            return _accumulatedTransaction.ForInventory(_targetInventory);
        }

        private ItemInstance<TKey>? ResolveToSimulationInstance(ItemInstance<TKey> instance)
        {
            if (instance == null) return null;
            foreach (var simInst in _simulation.Items)
            {
                if (EqualityComparer<TKey>.Default.Equals(simInst.Definition.Id, instance.Definition.Id) &&
                    simInst.Metadata.StructuralEquals(instance.Metadata))
                    return simInst;
            }
            return null;
        }

        private void MergeAndApply(InventoryTransaction<TKey> tx)
        {
            if (_accumulatedTransaction == null)
                _accumulatedTransaction = tx;
            else
                _accumulatedTransaction = Inventory<TKey>.MergeTransactions(_accumulatedTransaction, tx);

            _simulation.ApplyTransactionSilent(tx);
        }
    }
}
