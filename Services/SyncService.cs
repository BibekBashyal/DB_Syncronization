using FleetPanda_task.Data.DBContext;
using FleetPanda_task.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using System.Data;

namespace FleetPanda_task.Services
{
    public class SyncService
    {
        private System.Timers.Timer _syncTimer;
        private bool _isSyncInProgress = false;

        public async Task<List<Customer>> GetCustomersFromSqlServerAsync()
        {
            using (var context = new TaskSqlServerDBContext())
            {
               
                return await context.Customers.Include(c => c.Locations).ToListAsync();
            }
        }

        public async Task SyncDataToSQLiteAsync(List<Customer> customers)
        {
            var changeLog = "";
            try
            {
                using (var context = new TaskSqliteDbContext())
                {
                    
                    var existingCustomerIds = new HashSet<int>(await context.Customers.Select(c => c.CustomerID).ToListAsync());

                    var newCustomers = new List<Customer>();
                    var newLocations = new List<Location>();

                    foreach (var customer in customers)
                    {
                        if (!existingCustomerIds.Contains(customer.CustomerID))
                        {
                            
                            newCustomers.Add(customer);
                            changeLog += $"Added new customer: {customer.Name} (ID: {customer.CustomerID})\n";
                        }
                        else
                        {
                           
                            var existingCustomer = await context.Customers.Include(c => c.Locations)
                                                                           .FirstOrDefaultAsync(c => c.CustomerID == customer.CustomerID);
                            if (existingCustomer != null)
                            {
                                bool customerUpdated = false;
                                if (existingCustomer.Name != customer.Name ||
                                    existingCustomer.Email != customer.Email ||
                                    existingCustomer.Phone != customer.Phone)
                                {
                                    customerUpdated = true;
                                    existingCustomer.Name = customer.Name;
                                    existingCustomer.Email = customer.Email;
                                    existingCustomer.Phone = customer.Phone;
                                    changeLog += $"Updated customer: {customer.Name} (ID: {customer.CustomerID})\n";
                                }

                                
                                foreach (var location in customer.Locations)
                                {
                                    var existingLocation = existingCustomer.Locations
                                                                          .FirstOrDefault(l => l.LocationID == location.LocationID);
                                    if (existingLocation == null)
                                    {
                                        // Add new location
                                        var newLocation = new Location
                                        {
                                            Address = location.Address,
                                            CustomerID = customer.CustomerID,
                                        };
                                        newLocations.Add(newLocation);
                                        changeLog += $"Added new location for customer {customer.Name} (ID: {customer.CustomerID}): {location.Address}\n";
                                    }
                                    else if (existingLocation.Address != location.Address)
                                    {
                                        // Update existing location
                                        existingLocation.Address = location.Address;
                                        changeLog += $"Updated location for customer {customer.Name} (ID: {customer.CustomerID}): {location.Address}\n";
                                    }
                                }
                            }
                        }
                    }

                  
                    if (newCustomers.Count > 0)
                    {
                        await context.Customers.AddRangeAsync(newCustomers);
                    }
                    if (newLocations.Count > 0)
                    {
                        await context.Locations.AddRangeAsync(newLocations);
                    }

                   
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sync operation: {ex.Message}");
            }

           
            changeLog = changeLog == "" ? "Data synced at: " + DateTime.Now : changeLog;
            await LogSyncOperationAsync(changeLog);
        }


        public async Task LogSyncOperationAsync(string changes)
        {
            using (var context = new TaskSqliteDbContext())
            {
                try
                {
                    var logEntry = new SyncLog
                    {
                        SyncTime = DateTime.Now,
                        Changes = changes
                    };

                    await context.SyncLogs.AddAsync(logEntry);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error logging sync operation: {ex.Message}");
                }
            }
        }

        public void StartSyncTimer(int intervalInMilliseconds)
        {
            
            _syncTimer = new System.Timers.Timer(intervalInMilliseconds);
            _syncTimer.Elapsed += async (source, e) => await OnTimedEventAsync();
            _syncTimer.AutoReset = true;
            _syncTimer.Enabled = true;
        }

        private async Task OnTimedEventAsync()
        {
            if (_isSyncInProgress) return; 

            _isSyncInProgress = true; 
            try
            {
                var customers = await GetCustomersFromSqlServerAsync();
                await SyncDataToSQLiteAsync(customers); 
                //await LogSyncOperationAsync("Data synced at: " + DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sync: {ex.Message}");
            }
            finally
            {
                _isSyncInProgress = false; 
            }
        }
    }
}
