using RestaurantSystem.Entity;
using System.Collections.Generic;

namespace RestaurantSystem.Repository
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(string path) : base(path)
        {
        }

        public double GetTotalSumByOrderId(string orderId)
        {            
            return GetDrinksTotalSumByOrderId(orderId) + GetFoodTotalSumByOrderId(orderId);
        }

        public double GetDrinksTotalSumByOrderId(string orderId)
        {
            double totalSum = 0;

            List<OrderItem> orderItems = Items.First().Orders.Single(x => x.Id == orderId).Items;
            List<MenuItem> drinks = Items.First().Drinks;

            foreach (OrderItem item in orderItems)
            {
                MenuItem? drink = drinks.SingleOrDefault(x => x.Id == item.ItemId);

                if (drink == null) continue;

                totalSum += drink.Price * item.ItemAmount;

            }

            return totalSum;

        }

        public double GetFoodTotalSumByOrderId(string orderId)
        {
            double totalSum = 0;

            List<OrderItem> orderItems = Items.First().Orders.Single(x => x.Id == orderId).Items;
            List<MenuItem> foods = Items.First().Foods;

            foreach (OrderItem item in orderItems)
            {
                MenuItem? food = foods.SingleOrDefault(x => x.Id == item.ItemId);

                if (food == null) continue;

                totalSum += food.Price * item.ItemAmount;

            }

            return totalSum;

        }
    }
}
