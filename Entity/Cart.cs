namespace RestaurantSystem.Entity
{
    public class Cart : BaseIdModel
    {
        public string TableId { get; set; }

        public List<CartItem> Items { get; } = new();

    }
}
