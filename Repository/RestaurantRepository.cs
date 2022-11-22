using RestaurantSystem.Entity;

namespace RestaurantSystem.Repository
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(string path) : base(path)
        {
        }

        public double CountTotalByCartId(string id)
        {
            var items =  Items.First().Carts.Where(x => x.Id == id);


            

            return items.Count();
        }
    }
}
