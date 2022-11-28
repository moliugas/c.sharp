namespace RestaurantSystem.Entity
{
    public class Order : BaseIdModel
    {
        public string TableId { get; set; }

        public bool Active { get; set; } = true;

        public List<OrderItem> Items { get; } = new();

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string RestaurantId { get;  set; }

        public string Status { get; set; } = "pending";

        public Order(string tableId, string restaurantId)
        {
            TableId = tableId;
            RestaurantId = restaurantId;
        }

        public Order() { }
    }
}
