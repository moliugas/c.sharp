namespace RestaurantSystem.Entity
{
    public class CartItems
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ItemSdk { get; set; }
        public int ItemAmount { get; set; }

    }
}