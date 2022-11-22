namespace RestaurantSystem.Entity
{
    public class CartItem : BaseIdModel
    {
        public string CartId { get; set; }
        public string ItemId { get; set; }
        public int ItemAmount { get; set; }

    }
}