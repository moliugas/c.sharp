namespace RestaurantSystem.Entity
{
    public class Table
    {
        public int Id { get; set; }
        public int GuestsNum { get; set; }
        public int CartId { get; set; }

        Table() { }

        Table(int id, int guestsNum, int cartId)
        {
            Id = id;
            GuestsNum = guestsNum;
            CartId = cartId;
        }
    }
}
