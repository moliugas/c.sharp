using RestaurantSystem.Entity;

namespace RestaurantSystem.Repository
{
    public class ReceiptRepository : Repository<Receipt>
    {
        public ReceiptRepository(string path) : base(path)
        {
        }
    }
}
