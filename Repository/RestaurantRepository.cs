using RestaurantSystem.Entity;

namespace RestaurantSystem.Repository
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(string path) : base(path)
        {
        }
    }
}
