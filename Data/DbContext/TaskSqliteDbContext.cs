using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FleetPanda_task.Data.DBContext
{
    public class TaskSqliteDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SyncLog> SyncLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=Fleet_db.db");
            string dbPath = @"C:\Users\bibek\Fleet_db.db"; 
            optionsBuilder.UseSqlite($"Data Source={dbPath}")
           .EnableSensitiveDataLogging()  
        .LogTo(Console.WriteLine, LogLevel.Information); 
        }
    }
}
