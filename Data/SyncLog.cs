using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetPanda_task.Data
{
    public class SyncLog
    {
        [Key]
        public int SyncID { get; set; }
        public DateTime SyncTime { get; set; }
        public string Changes { get; set; }
    }
}
