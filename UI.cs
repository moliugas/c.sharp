using RestaurantSystem.Repository;
using RestaurantSystem.Entity;

namespace RestaurantSystem
{
    internal class UI
    {
        private RestaurantRepository mainRepo;
        private Restaurant res;
        private string[] lines = {
        "Pasirinkite veiksmą:",
        "[1] Klientas atėjo",
        "[2] Klientas išeina",
        "[3] Keisti staliukų rezervacijas",
        "Įveskite staliuko numerį:",
        "Įveskite svečių kiekį:",
        "[1]Spasdinti kvita"
        };

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

            mainRepo = new RestaurantRepository("repo.txt");
            if (!mainRepo.Items.Any())
            {
                mainRepo.Add(new Restaurant(20));
            }

            res = mainRepo.Items.First();
            res.Drinks = drinksRepo.Items;
            res.Foods = foodRepo.Items;
        }

        //        − Restorano sistema:
        //− 1. Padavėja turi galėti užregistruoti žmogaus užsakymą:
        //− 1.1 Prekės pavadinimas + kaina eurais
        //− 1.2 Kuriant užsakymą pirmas pasirinkimas yra staliukas, prie kurio sėdi žmonės.
        //− 1.2 Pasirinkus staliuką sistemoje jis pažymimas kaip užimtas, taip pat turi būti galimybė
        //pažymėti jog staliukas atsilaisvino
        //− 1.3 Sąraše užimti staliukai turi būti pažymėti kaip užimti.
        //− 1.4 Prekės ir gėrimai imami iš dviejų skirtingų failų pvz food.txt/csv drinks.txt/csv
        //−
        //− 2. Užsakyme yra staliuko informacija:
        //− 2.1 Staliuko numeris, kiek sėdimų vietų
        //− 2.2 Užsakyti patiekalai/gėrimai ir bendra mokama suma.
        //− 2.3 Data ir laikas
        //Egzaminas
        //− 3. Iš užsakymo sudaromi 2 čekiai:
        //− 3.1 Vienas restoranui kitas klientui.(Pagalvokite kuom skirtųsi informacija patiekiama
        //restoranui ir klientui, kuri informacija persidengia?)
        //−
        //− 4. Abu čekiai turi būti galimi išsiųsti email'u(panaudoti interface).
        //− 5. Priklausomai nuo kliento norų, jam čekis gali būti nespausdinamas, bet čekis restoranui
        //spausdinamas visada ir taip pat čekio skirto restoranui informacija įrašoma į failą.
        public void Start()
        {
            while (true)
            {
                Console.WriteLine(lines[0]);
                Console.WriteLine(lines[1]);
                Console.WriteLine(lines[2]);
                switch (GetChoice())
                {
                    case 1:
                        OpenTable();
                        break;
                    case 2:
                        CloseTable();
                        break;
                    case 99:
                        mainRepo.Update(res);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void OpenTable()
        {
            var tables = res.Tables.Where(x => !x.IsTaken).ToList();

            var table = GetChoiceFromDynamicList(tables, (i, t) => { return $"Table [{i}] ({(t.IsTaken ? "Taken" : "Empty")})"; });
            if (table != null)
            {
                ManageOrder(table);
            }

        }

        private void CloseTable()
        {
            var tables = res.Tables.Where(x => x.IsTaken).ToList();

            var table = GetChoiceFromDynamicList(tables, (i, t) => { return $"Table [{i}] ({(t.IsTaken ? "Taken" : "Empty")})"; });

            Console.WriteLine("0 - Back");
            Console.WriteLine("1 - spausdinti kvita");
            Console.WriteLine("2 - sumoketa");

            switch (GetChoice())
            {
                case 1:
                    string cartId = table.CurrentCartId;
                    Console.WriteLine($"checkis {mainRepo.CountTotalByCartId(cartId)}");
                    break;
                case 2:
                    CloseTable();
                    break;
                case 0:
                    break;
            }


        }
        private void ManageOrder(Table table)
        {
            Cart? order = null;
            if (table.IsTaken)
            {
                order = res.Carts.FirstOrDefault(x => x.Id == table.CurrentCartId);
            }
            if (order == null)
            {
                table.IsTaken = true;
                order = new Cart();
                table.CurrentCartId = order.Id;
                order.TableId = table.Id;
                res.Carts.Add(order);
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
                    case 0: repeat = false; break;
                    case 1:
                        item = GetChoiceFromDynamicList(res.Foods, (i, f) => $"[{i}] {f.Name} - {f.Price}");
                        break;
                    case 2:
                        item = GetChoiceFromDynamicList(res.Drinks, (i, f) => $"[{i}] {f.Name} - {f.Price}");
                        break;
                }
                if (item != null)
                {
                    var ci = order.Items.FirstOrDefault(x => x.ItemId == item.Id);
                    if (ci == null)
                    {
                        ci = new CartItem() { CartId = order.Id, ItemAmount = 0, ItemId = item.Id };
                        order.Items.Add(ci);
                    }
                    ci.ItemAmount++;
                }
            }

        }

        private T? GetChoiceFromDynamicList<T>(List<T> list, Func<int, T, string> print)
            where T : class
        {
            Console.WriteLine("Back 0");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(print(i + 1, list[i]));
            }
            int choice = int.MinValue;
            do
            {
                choice = GetChoice() - 1;
            } while (choice < -1 || choice >= list.Count);
            return choice == -1 ? null : list[choice];
        }

        public void AddTable()
        {
            Console.WriteLine(lines[3]);
            int tableNum = GetChoice();

            Console.WriteLine(lines[1]);
            Console.WriteLine(lines[2]);

            switch (GetChoice())
            {
                case 1:

                    break;
                case 2:

                    break;
                case 99:
                    mainRepo.Update(res);
                    Environment.Exit(0);
                    break;
            }

        }

        private int GetChoice()
        {
            int choise;
            while (!int.TryParse(Console.ReadLine(), out choise)) ;
            return choise;
        }
    }


}
