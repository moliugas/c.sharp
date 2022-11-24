using System.Text.Json.Serialization;

namespace RestaurantSystem.Entity
{
    public class Table : BaseIdModel
    {
        public int GuestsNum { get; set; } = 0;
        public bool IsTaken { get; set; } = false;
        public string? CurrentOrderId { get; set; }
        public int Number { get; set; }

        public Table(int num) { Number = num; }

    }
}
