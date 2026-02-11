using System;

namespace com.workes.inventory.core
{
    public class ItemInstance<TKey>
    {
        public ItemDefinition<TKey> Definition { get; }
        public int Amount { get; private set; }

        public Guid InstanceId { get; }

        public ItemInstance(ItemDefinition<TKey> definition, int amount = 1)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Definition = definition;
            Amount = amount;
            InstanceId = Guid.NewGuid();
        }

        public void SetAmount(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Amount = amount;
        }

        public void AddAmount(int amount)
        {
            SetAmount(Amount + amount);
        }

        public void ReduceAmount(int amount)
        {
            if (amount <= 0 || amount > Amount)
                throw new ArgumentException("Invalid reduction amount.");

            Amount -= amount;
        }
    }
}
