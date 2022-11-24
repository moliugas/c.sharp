using RestaurantSystem.Repository;
using RestaurantSystem.Entity;

namespace RestaurantSystem
{
    internal class UI
    {
        private RestaurantRepository restaurantRepository;
        private ReceiptRepository receiptRepository;
        private Restaurant restaurant;
        private double FoodTax;
        private double DrinkTax;


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

            receiptRepository = new ReceiptRepository("receipt.txt");

            restaurant = restaurantRepository.Items.First();
            restaurant.Drinks = drinksRepo.Items;
            restaurant.Foods = foodRepo.Items;

            FoodTax = 5;
            DrinkTax = 21;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("[1] Manage Tables");
                Console.WriteLine("[2] Manage Orders");
                Console.WriteLine("[3]List Receipts");
                Console.WriteLine("[99]Save & Quit");

                switch (GetChoice())
                {
                    case 1:
                        ManageTables();
                        break;
                    case 2:
                        ManageOrders();
                        break;
                    case 99:
                        restaurantRepository.Update(restaurant);
                        Environment.Exit(0);
                        break;
                }
            }
        }


        private void ManageTables()
        {
            List<Table> tables = restaurant.Tables.ToList();

            Table? table = GetGenericObjectFromDynamicList(tables, (i, t) => { return $"Table [{i}] ({(t.IsTaken ? "Taken" : "Empty")})"; });
            if (table == null)
            {
                Console.WriteLine("Table not chosen.");
                return;
            }
            InitTable(table);

            Order currentOrder = restaurant.Orders.Single(x => x.Id == table.CurrentOrderId);

            Console.WriteLine("[1] Add Items To Order");
            Console.WriteLine("[2] Close Table");
            Console.WriteLine("[3]Checkout");
            Console.WriteLine("[0] Back");

            switch (GetChoice())
            {
                case 1:
                    AddItemsToOrder(currentOrder);
                    break;
                case 2:
                    table.CurrentOrderId = null;
                    table.IsTaken = false;
                    table.GuestsNum = 0;
                    break;
                case 3:
                    Checkout(currentOrder);
                    break;
                case 0:
                    break;
            }

        }

        private void Checkout(Order order)
        {
            Console.WriteLine("[0] Back");
            Console.WriteLine("[1] Print receipt");
            Console.WriteLine("[2] Confirm payment");

            switch (GetChoice())
            {
                case 1:
                   
                    if (order == null)
                    {
                        Console.WriteLine("This has no active order.");
                        return;
                    }
                    Console.WriteLine($"Total Food: {restaurantRepository.GetFoodTotalSumByOrder(order)}");
                    Console.WriteLine($"Total Drinks: {restaurantRepository.GetDrinksTotalSumByOrder(order)}");
                    break;
                case 2:
                    Console.WriteLine("[2] Confirm payment");
                    break;
                case 0:
                    break;
            }
        }

        private void GenerateReceipt(Order order)
        {
            List<ReceiptItem> receiptItems = new ();
            double sum = 0;

            foreach(var item in order.Items)
            {
                double itemPrice = restaurantRepository.GetItemPriceById(item.Id);
                receiptItems.Add(new(restaurantRepository.GetMenuItemName(item), item.Amount, itemPrice, item.Id));
                sum += item.Amount * itemPrice;
            }

            Receipt receipt = new(receiptItems, FoodTax, DrinkTax, restaurant.Id, restaurant.Name ?? "");

            receiptRepository.Add(receipt);
        }

        private void ManageOrders()
        {
            List<Order> orders = restaurant.Orders.ToList();

            Order? order = GetGenericObjectFromDynamicList(orders, (i, t) => { return $"Order [{i}] current sum: {restaurantRepository.GetTotalSumByOrderId(t.Id)}"; });

            if (order == null)
            {
                Console.WriteLine("Order not chosen.");
                return;
            }
            Console.WriteLine("[1] Edit order");
            Console.WriteLine("[2] Delete order");
            Console.WriteLine("[0] Back");

            switch (GetChoice())
            {

                case 1:
                    EditOrder(order);
                    break;
                case 2:
                    Table table = restaurant.Tables.Single(x => x.CurrentOrderId == order.Id);
                    table.CurrentOrderId = null;
                    restaurant.Orders.Remove(order);
                    break;
                case 0:
                    break;
            }
        }

        private static void EditOrder(Order order)
        {
            OrderItem? item = GetGenericObjectFromDynamicList(order.Items, (i, t) => { return $"Item [{i}] {t.Name} x {t.Amount}"; });

            if (item == null)
            {
                Console.WriteLine("Item not chosen.");
                return;
            }
            Console.WriteLine("[1] Edit Item amount");
            Console.WriteLine("[2] Delete Item");
            Console.WriteLine("[0] Back");

            switch (GetChoice())
            {
                case 1:
                    item.Amount = GetChoice();
                    break;
                case 2:
                    order.Items.Remove(item);
                    break;
                case 0:
                    break;
            }

        }

        private void InitTable(Table table)
        {
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
                    Console.WriteLine("Failed to initialize order.");
                    return;
                }
            }
        }
        private void AddItemsToOrder(Order order)
        {

            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            bool repeat = true;
            while (order != null && repeat)
            {

                Console.WriteLine("[0] Back");
                Console.WriteLine("Choose what to add:");
                Console.WriteLine("[1] Food");
                Console.WriteLine("[2] Drinks");
                MenuItem? item = null;
                bool isDrink = false;
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
                        isDrink = true;
                        break;
                }
                if (item != null)
                {
                    OrderItem? currentItem = order.Items.FirstOrDefault(x => x.MenuItemId == item.Id);
                    if (currentItem == null)
                    {
                        currentItem = new OrderItem() { OrderId = order.Id, Amount = 0, MenuItemId = item.Id, isDrink = isDrink };
                        order.Items.Add(currentItem);
                    }
                    currentItem.Amount++;
                }
            }

        }

        private static T? GetChoiceFromDynamicList<T>(List<T> list, Func<int, T, string> print) where T : class
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

            while (!int.TryParse(Console.ReadLine(), out choise)) ;

            return choise;
        }

        private static T? GetGenericObjectFromDynamicList<T>(List<T> items, Func<int, T, string> print) where T : class
        {
            if (items.Count == 0)
            {
                Console.WriteLine($"No {typeof(T)} found in system.");
                return default;
            }

            return GetChoiceFromDynamicList(items, print);

        }
    }

}
