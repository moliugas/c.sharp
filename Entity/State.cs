using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Entity
{
    public class State
    {   
        public int Id { get; set; }
        private int TablesAmount { get; set; }
        private int[] TablesInUse { get; set; }

        private int CartId { get; }
    }
}
