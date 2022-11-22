using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantSystem.Entity
{
    public class Restaurant : BaseIdModel
    {
        string Name { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<MenuItem> Drinks { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<MenuItem> Foods { get; set; }

        public Table[] Tables { get; set; }

        public List<Cart> Carts { get; set; } = new List<Cart>();
    }
}
