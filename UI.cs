using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    internal class UI
    {
        private string[] lines = {
        "Pasirinkite veiksmą:",
        "[1] Klientas atėjo",
        "[2] Klientas išeina",
        "[3] Keisti staliukų rezervacijas",
        "Įveskite staliuko numerį:",
        "Įveskite svečių kiekį:",
        };

        public UI()
        {
        }

        public void Start()
        {
            Console.WriteLine(lines[0]);
            Console.WriteLine(lines[1]);
            Console.WriteLine(lines[2]);

            switch (GetChoice())
            {
                case 1:

                    break;
                case 2:

                    break;
                case 99:
                    Environment.Exit(0);
                    break;
            }

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
