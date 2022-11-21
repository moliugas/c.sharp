namespace RestaurantSystem.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public int TableId { get; set; }

        private List<CartItems> Items { get; set; }

    }
}
