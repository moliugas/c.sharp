using System.Text.Json.Serialization;

namespace RestaurantSystem.Entity
{
    public class Restaurant : BaseIdModel
    {
        string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<MenuItem>? Drinks { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<MenuItem>? Foods { get; set; }
        public Table[] Tables { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public Restaurant() { }

        public Restaurant(int tablesCount)
        {
            Tables = new Table[tablesCount];

            for (int i = 0; i < tablesCount; i++)
            {
                Tables[i] = new Table(i+1);
            }
        }
    }

}
