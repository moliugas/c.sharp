using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestaurantSystem.Entity
{
    public class Table : BaseIdModel
    {
        public int GuestsNum { get; set; }
        public bool IsTaken { get; set; } = false;
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? CurrentOrderId { get; set; }

        public Table() { }

        public Table(int guestsNum)
        {
            GuestsNum = guestsNum;
        }
    }
}
