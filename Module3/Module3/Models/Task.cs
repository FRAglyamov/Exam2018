using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module3.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
    }
}
