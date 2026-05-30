using com.workes.inventory.core;

namespace com.workes.inventory.rules
{
    /// <summary>
    /// Optional interface for rules that need the structural inventory transaction,
    /// such as rules based on item instance count rather than item quantity.
    /// </summary>
    public interface IInventoryStructuralRulePolicy<TKey>
    {
        bool CanApply(
            Inventory<TKey> inventory,
            InventoryTransaction<TKey> transaction,
            out string? error);
    }
}
