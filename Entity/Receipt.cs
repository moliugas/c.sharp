namespace RestaurantSystem.Entity
{
    public class Receipt : BaseIdModel
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<ReceiptItem> Items { get; set; }
        public double TotalSum { get; set; }
        public double DrinksSum { get; set; }
        public double FoodsSum { get; set; }
        public double FoodTax { get; set; }
        public double DrinksTax { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public bool IsPayed { get; set; } = false;

        public Receipt(List<ReceiptItem> items, double drinksSum, double foodsSum, double foodTax, double drinksTax, string restaurantId, string restaurantName)
        {
            Items = items;
            TotalSum = drinksSum + foodsSum;
            DrinksSum = drinksSum;
            FoodsSum = foodsSum;
            FoodTax = foodTax;
            DrinksTax = drinksTax;
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
        }

        public Receipt() { }
    }
}
