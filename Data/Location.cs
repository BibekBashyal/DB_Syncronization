using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetPanda_task.Data
{
    public class Location
    {
        public int LocationID { get; set; }
        public int CustomerID { get; set; }
        public string Address { get; set; }

        public Customer Customer { get; set; }
    }
}
