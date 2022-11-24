namespace RestaurantSystem.Entity
{
    public class OrderItem : BaseIdModel
    {
        public string OrderId { get; set; }
        public string ItemId { get; set; }
        public int ItemAmount { get; set; }
    }
}