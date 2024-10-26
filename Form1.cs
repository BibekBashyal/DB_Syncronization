using FleetPanda_task.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace FleetPanda_task
{
    public partial class Form1 : Form
    {
        private readonly SyncService _syncService;

        // Constructor with dependency injection
        public Form1(SyncService syncService)
        {
            InitializeComponent();
            _syncService = syncService;
        }

        private async void btnFetch_Click(object sender, EventArgs e)
        {
            var customers = await _syncService.GetCustomersFromSqlServerAsync();


            var dataTable = new DataTable();
            dataTable.Columns.AddRange(new[]
            {
                   new DataColumn("CustomerID", typeof(int)),
                   new DataColumn("CustomerName", typeof(string)),
                   new DataColumn("CustomerPhone", typeof(string)),
                   new DataColumn("Address", typeof(string))
             });

            // Populate rows
            foreach (var customer in customers.SelectMany(c => c.Locations.Select(l => new
            {
                CustomerID = c.CustomerID,
                CustomerName = c.Name,
                CustomerPhone = c.Phone,
                Address = l.Address
            })))
            {
                dataTable.Rows.Add(customer.CustomerID, customer.CustomerName, customer.CustomerPhone, customer.Address);
            }

            // Set the data source
            dgvCustomers.DataSource = dataTable;
        }


        private void btnStartSync_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtInterval.Text, out int interval))
            {
                _syncService.StartSyncTimer(interval);
                MessageBox.Show($"Synchronization started with an interval of {interval} ms.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a valid number for the interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
