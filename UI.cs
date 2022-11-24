using RestaurantSystem.Repository;
using RestaurantSystem.Entity;

namespace RestaurantSystem
{
    internal class UI
    {
        private RestaurantRepository restaurantRepository;
        private ReceiptRepository receiptRepository;
        private Restaurant restaurant;

        public UI()
        {
            Initialize();
        }

        private void Initialize()
        {
            //File.Delete("repo.txt");
            var drinksRepo = new Repository<MenuItem>("drinks.txt");
            if (!drinksRepo.Items.Any())
            {
                drinksRepo.Add(new MenuItem { Name = "Beer", Price = 3 });
                drinksRepo.Add(new MenuItem { Name = "Whiskey", Price = 5 });
                drinksRepo.Add(new MenuItem { Name = "Whiskey *", Price = 15 });
                drinksRepo.Add(new MenuItem { Name = "Whiskey **", Price = 25 });
                drinksRepo.Add(new MenuItem { Name = "Whiskey ***", Price = 35 });
            }

            var foodRepo = new Repository<MenuItem>("food.txt");

            if (!foodRepo.Items.Any())
            {
                foodRepo.Add(new MenuItem { Name = "Fried bread squared", Price = 5 });
                foodRepo.Add(new MenuItem { Name = "Deez Nuts", Price = 3 });
                foodRepo.Add(new MenuItem { Name = "Kodletukai", Price = 3.3 });
                foodRepo.Add(new MenuItem { Name = "Soup", Price = 0.69 });
                foodRepo.Add(new MenuItem { Name = "ShalTB", Price = 4.2 });
                foodRepo.Add(new MenuItem { Name = "Pitsa", Price = 13.3 });
            }

            restaurantRepository = new RestaurantRepository("repo.txt");
            if (!restaurantRepository.Items.Any())
            {
                restaurantRepository.Add(new Restaurant(20));
            }


            restaurant = restaurantRepository.Items.First();
            restaurant.Drinks = drinksRepo.Items;
            restaurant.Foods = foodRepo.Items;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("[1]Create/Edit Table");
                Console.WriteLine("[2]Close Table");
                Console.WriteLine("[3]Manage Orders");
                Console.WriteLine("[99]Quit");

                switch (GetChoice())
                {
                    case 1:
                        EditTable();
                        break;
                    case 2:
                        CloseTable();
                        break;
                    case 3:
                        OrdersList();
                        break;
                    case 99:
                        restaurantRepository.Update(restaurant);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void OrdersList()
        {
            List<Order> orders = restaurant.Orders.Where(x => x.Active).ToList();

            if (orders.Count == 0)
            {
                Console.WriteLine("No active orders found.");
                return;
            }

            Order? order = GetChoiceFromDynamicList(orders, (i, t) => { return $"Order [{i}] current sum: {restaurantRepository.GetTotalSumByOrderId(t.Id)}"; });

            if (order == null)
            {
                Console.WriteLine("Order not chosen.");
                return;
            }

            Console.WriteLine("[1]Edit order");
            Console.WriteLine("[2]Delete order");
            Console.WriteLine("[0]Back");

            switch (GetChoice())
            {
                case 1:
                    ManageOrder(order.Id);
                    break;
                case 2:
                    Table table = restaurant.Tables.Single(x => x.CurrentOrderId == order.Id);
                    table.CurrentOrderId = null;
                    restaurantRepository.DeleteOrderByOrder(order);
                    break;
                case 0:
                    break;
            }
        }

        private void EditTable()
        {
            List<Table> tables = restaurant.Tables.ToList();

            Table? table = GetChoiceFromDynamicList(tables, (i, t) => { return $"Table [{i}] ({(t.IsTaken ? "Taken" : "Empty")})"; });

            Order? order = null;

            if (table != null)
            {
                if (table.IsTaken)
                {
                    order = restaurant.Orders.FirstOrDefault(x => x.Id == table.CurrentOrderId);
                }

                if (order == null)
                {
                    Console.WriteLine("Enter number of guests");
                    table.GuestsNum = GetChoice();
                    table.IsTaken = true;
                    order = new Order();
                    table.CurrentOrderId = order.Id;
                    order.TableId = table.Id;
                    restaurant.Orders.Add(order);
                }

                if (table.CurrentOrderId == null)
                {
                    Console.WriteLine("Failed to create order.");
                    return;
                }

                ManageOrder(table.CurrentOrderId);
            }

            Console.WriteLine("Table init unsuccessfull :(");
        }

        private void CloseTable()
        {
            List<Table> tables = restaurant.Tables.Where(x => x.IsTaken).ToList();

            if (tables.Count == 0)
            {
                Console.WriteLine("No active tables found.");
                return;
            }

            if (tables.Count == 0) return;

            Table? table = GetChoiceFromDynamicList(tables, (i, t) => { return $"Table [{i}] ({(t.IsTaken ? "Taken" : "Empty")})"; });

            if (table == null)
            {
                Console.WriteLine("Table not found.");
                return;
            }

            Console.WriteLine("0 - Back");
            Console.WriteLine("1 - print receipt");
            Console.WriteLine("2 - confirm payment");

            switch (GetChoice())
            {
                case 1:
                    string? orderId = table.CurrentOrderId;
                    if (orderId == null)
                    {
                        Console.WriteLine("This has no active order.");
                        return;
                    }
                    Console.WriteLine($"Total Food: {restaurantRepository.GetFoodTotalSumByOrderId(orderId)}");
                    Console.WriteLine($"Total Drinks: {restaurantRepository.GetDrinksTotalSumByOrderId(orderId)}");
                    break;
                case 2:
                    Console.WriteLine("2 - confirm payment");
                    break;
                case 0:
                    break;
            }


        }
        private void ManageOrder(string id)
        {
            Order? order = restaurant.Orders.SingleOrDefault(x => x.Id == id);

            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            bool repeat = true;
            while (order != null && repeat)
            {
                Console.WriteLine("0 - Back");
                Console.WriteLine("1 - Food");
                Console.WriteLine("2 - Drinks");
                MenuItem? item = null;
                switch (GetChoice())
                {
                    case 0:
                        repeat = false;
                        break;
                    case 1:
                        item = GetChoiceFromDynamicList(restaurant.Foods ?? new(), (i, f) => $"[{i}] {f.Name} - {f.Price}");
                        break;
                    case 2:
                        item = GetChoiceFromDynamicList(restaurant.Drinks ?? new(), (i, f) => $"[{i}] {f.Name} - {f.Price}");
                        break;
                }
                if (item != null)
                {
                    OrderItem? currentItem = order.Items.FirstOrDefault(x => x.ItemId == item.Id);
                    if (currentItem == null)
                    {
                        currentItem = new OrderItem() { OrderId = order.Id, ItemAmount = 0, ItemId = item.Id };
                        order.Items.Add(currentItem);
                    }
                    currentItem.ItemAmount++;
                }
            }

        }

        private static T? GetChoiceFromDynamicList<T>(List<T> list, Func<int, T, string> print)
            where T : class
        {
            Console.WriteLine("Back 0");

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(print(i + 1, list[i]));
            }

            int choice;

            do
            {
                choice = GetChoice() - 1;
            }
            while (choice < -1 || choice >= list.Count);

            return choice == -1 ? null : list[choice];
        }

        private static int GetChoice()
        {
            int choise;

            while (!int.TryParse(Console.ReadLine(), out choise));

            return choise;
        }
    }


}
