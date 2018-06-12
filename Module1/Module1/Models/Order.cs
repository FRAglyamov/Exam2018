using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public float Dicsount { get; set; }
        public DateTime TimeStart { get; set; }
        public float Time { get; set; }
        public float TotalCost { get; set; }
    }
}
