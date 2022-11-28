using RestaurantSystem.Entity;

namespace RestaurantSystem.Repository
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(string path) : base(path)
        {
        }

        public double GetTotalSumByOrder(Order order)
        {
            return GetDrinksTotalSumByOrder(order) + GetFoodTotalSumByOrder(order);
        }

        public double GetDrinksTotalSumByOrder(Order order)
        {
            double totalSum = 0;

            List<MenuItem>? drinks = Items.First().Drinks;

            if (drinks == null) { return 999999; }

            foreach (OrderItem item in order.Items)
            {
                MenuItem? drink = drinks.SingleOrDefault(x => x.Id == item.MenuItemId);

                if (drink == null) continue;

                totalSum += drink.Price * item.Amount;

            }

            return totalSum;

        }

        public double GetFoodTotalSumByOrder(Order order)
        {
            double totalSum = 0;
            List<MenuItem>? foods = Items.First().Foods;

            if (foods == null) { return 999999; }

            foreach (OrderItem item in order.Items)
            {
                MenuItem? food = foods.SingleOrDefault(x => x.Id == item.MenuItemId);

                if (food == null) continue;

                totalSum += food.Price * item.Amount;

            }

            return totalSum;

        }

        public string GetGenericMenuItemById(string id)
        {
            Restaurant res = Items.First();

            string? name = res.Foods?.SingleOrDefault(x => x.Id == id)?.Name;

            name ??= res.Drinks?.SingleOrDefault(x => x.Id == id)?.Name;

            if (name == null)
            {
                throw new Exception("No such id exist in drinks or foods repos.");
            }

            return name;
        }

        public double GetItemPriceById(string id)
        {
            Restaurant res = Items.First();

            double? name = res.Foods?.SingleOrDefault(x => x.Id == id)?.Price;

            name ??= res.Drinks?.SingleOrDefault(x => x.Id == id)?.Price;

            if (name == null)
            {
                throw new Exception("No such id exist in drinks or foods repos.");
            }

            return (double)name;
        }

        public double GetFoodPriceById(string id)
        {
            Restaurant res = Items.First();

            return res.Foods.SingleOrDefault(x => x.Id == id)?.Price ?? 0;
        }

        public double GetDrinkPriceById(string id)
        {
            Restaurant res = Items.First();

            return res.Drinks.SingleOrDefault(x => x.Id == id)?.Price ?? 0;
        }
    }
}
