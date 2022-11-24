namespace RestaurantSystem.Entity
{
    public struct ReceiptItem
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string Id { get; set; }

        public ReceiptItem(string name, int amount, double price, string id)
        {
            Name = name;
            Amount = amount;
            Price = price;
            Id = id;
        }
    }
}