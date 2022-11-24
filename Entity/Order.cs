namespace RestaurantSystem.Entity
{
    public class Order : BaseIdModel
    {
        public string TableId { get; set; }

        public bool Active { get; set; } = true;

        public List<OrderItem> Items { get; } = new();

        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
