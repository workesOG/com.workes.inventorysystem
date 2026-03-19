namespace com.workes.inventory.rules
{
    public class RuleContainer<TKey>
    {
        private readonly List<IRulePolicy<TKey>> _rules;

        public RuleContainer(params IRulePolicy<TKey>[] rules)
        {
            _rules = rules?.ToList() ?? new List<IRulePolicy<TKey>>();
        }

        public bool CanApply(
            Inventory<TKey> inventory,
            NormalizedInventoryTransaction<TKey> transaction,
            out string? error)
        {
            foreach (var rule in _rules)
            {
                if (!rule.CanApply(inventory, transaction, out error))
                    return false;
            }

            error = null;
            return true;
        }

        public void Add(IRulePolicy<TKey> rule)
        {
            _rules.Add(rule);
        }
    }
}
