using FleetPanda_task.Services;

namespace FleetPanda_task
{
    partial class Form1:Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 
      
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void label1_Click(object sender, EventArgs e)
        {
            // This method can be removed if it's not needed anymore.
        }
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblInterval = new Label();
            txtInterval = new TextBox();
            btnFetch = new Button();
            btnStartSync = new Button();
            dgvCustomers = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // lblInterval
            // 
            lblInterval.AutoSize = true;
            lblInterval.Location = new Point(42, 100);
            lblInterval.Name = "lblInterval";
            lblInterval.Size = new Size(166, 20);
            lblInterval.TabIndex = 0;
            lblInterval.Text = "Enter Sync Interval (ms):";
            // 
            // txtInterval
            // 
            txtInterval.Location = new Point(216, 102);
            txtInterval.Name = "txtInterval";
            txtInterval.Size = new Size(267, 27);
            txtInterval.TabIndex = 1;
            // 
            // btnFetch
            // 
            btnFetch.Location = new Point(508, 104);
            btnFetch.Name = "btnFetch";
            btnFetch.Size = new Size(94, 29);
            btnFetch.TabIndex = 2;
            btnFetch.Text = "Fetch Data";
            btnFetch.UseVisualStyleBackColor = true;
            btnFetch.Click += btnFetch_Click;
            // 
            // btnStartSync
            // 
            btnStartSync.Location = new Point(608, 104);
            btnStartSync.Name = "btnStartSync";
            btnStartSync.Size = new Size(94, 29);
            btnStartSync.TabIndex = 3;
            btnStartSync.Text = "Start Sync";
            btnStartSync.UseVisualStyleBackColor = true;
            btnStartSync.Click += btnStartSync_Click;
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(42, 173);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.Size = new Size(679, 249);
            dgvCustomers.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvCustomers);
            Controls.Add(btnStartSync);
            Controls.Add(btnFetch);
            Controls.Add(txtInterval);
            Controls.Add(lblInterval);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInterval;
        private TextBox txtInterval;
        private Button btnFetch;
        private Button btnStartSync;
        private DataGridView dgvCustomers;
    }
}
