using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetPanda_task.Data.DBContext
{
    public class TaskSqlServerDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SyncLog> SyncLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-I9NFHHK;Database=Fleet_task;User Id=sa;Password=12345;TrustServerCertificate=True;");
        }
        
    }
}
