using System.Text.Json.Serialization;

namespace RestaurantSystem.Entity
{
    public class Restaurant : BaseIdModel
    {
        string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<MenuItem> Drinks { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<MenuItem> Foods { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Table[] Tables { get; set; }

        public List<Cart> Carts { get; set; } = new List<Cart>();

        public Restaurant() { }
        public Restaurant(int tablesCount)
        {
            Tables = new Table[tablesCount];

            for (int i = 0; i < tablesCount; i++)
            {
                Tables[i] = new Table(0);
            }
        }
    }

}
