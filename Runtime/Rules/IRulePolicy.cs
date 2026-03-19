namespace com.workes.inventory.rules
{
    public interface IRulePolicy<TKey>
    {
        bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error);
    }
}