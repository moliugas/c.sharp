namespace RestaurantSystem.Entity
{
    public class OrderItem : BaseIdModel
    {
        public string OrderId { get; set; }
        public string MenuItemId { get; set; }

        public bool isDrink { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}