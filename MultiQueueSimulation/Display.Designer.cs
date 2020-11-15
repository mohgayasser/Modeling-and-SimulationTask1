namespace MultiQueueSimulation
{
    partial class Display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Customer_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Random_Digit_for_interarrival_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interarrival_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arrival_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Random_Digit_for_service_duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service_duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Server_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_Service_Begins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_Service_Ends = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_in_queue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Average_waiting_time = new System.Windows.Forms.Label();
            this.Maximum_queue_length = new System.Windows.Forms.Label();
            this.WaitingProbability = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_WaitingProbability = new System.Windows.Forms.TextBox();
            this.textBox_Maximum_queue_length = new System.Windows.Forms.TextBox();
            this.txtBox_Average_WaitingTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ServerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdleProbability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageServiceTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Utilization = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Customer_No,
            this.Random_Digit_for_interarrival_time,
            this.Interarrival_time,
            this.Arrival_time,
            this.Random_Digit_for_service_duration,
            this.Service_duration,
            this.Server_Index,
            this.Time_Service_Begins,
            this.Time_Service_Ends,
            this.Time_in_queue});
            this.dataGridView1.Location = new System.Drawing.Point(2, 146);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1045, 374);
            this.dataGridView1.TabIndex = 0;
            // 
            // Customer_No
            // 
            this.Customer_No.HeaderText = "Customer No";
            this.Customer_No.Name = "Customer_No";
            // 
            // Random_Digit_for_interarrival_time
            // 
            this.Random_Digit_for_interarrival_time.HeaderText = "Random Digit for interarrival time";
            this.Random_Digit_for_interarrival_time.Name = "Random_Digit_for_interarrival_time";
            // 
            // Interarrival_time
            // 
            this.Interarrival_time.HeaderText = "Inter-arrival time";
            this.Interarrival_time.Name = "Interarrival_time";
            // 
            // Arrival_time
            // 
            this.Arrival_time.HeaderText = "Arrival time (Clock Time)";
            this.Arrival_time.Name = "Arrival_time";
            // 
            // Random_Digit_for_service_duration
            // 
            this.Random_Digit_for_service_duration.HeaderText = "Random Digit for service duration";
            this.Random_Digit_for_service_duration.Name = "Random_Digit_for_service_duration";
            // 
            // Service_duration
            // 
            this.Service_duration.HeaderText = " Service duration";
            this.Service_duration.Name = "Service_duration";
            // 
            // Server_Index
            // 
            this.Server_Index.HeaderText = "Server Index";
            this.Server_Index.Name = "Server_Index";
            // 
            // Time_Service_Begins
            // 
            this.Time_Service_Begins.HeaderText = "Time Service Begins ";
            this.Time_Service_Begins.Name = "Time_Service_Begins";
            // 
            // Time_Service_Ends
            // 
            this.Time_Service_Ends.HeaderText = "Time Service Ends (Departure)";
            this.Time_Service_Ends.Name = "Time_Service_Ends";
            // 
            // Time_in_queue
            // 
            this.Time_in_queue.HeaderText = "total delay time(Time in queue)";
            this.Time_in_queue.Name = "Time_in_queue";
            // 
            // Average_waiting_time
            // 
            this.Average_waiting_time.AutoSize = true;
            this.Average_waiting_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Average_waiting_time.Location = new System.Drawing.Point(16, 35);
            this.Average_waiting_time.Name = "Average_waiting_time";
            this.Average_waiting_time.Size = new System.Drawing.Size(239, 18);
            this.Average_waiting_time.TabIndex = 1;
            this.Average_waiting_time.Text = "Average waiting time (in the queue).";
            this.Average_waiting_time.Click += new System.EventHandler(this.label1_Click);
            // 
            // Maximum_queue_length
            // 
            this.Maximum_queue_length.AutoSize = true;
            this.Maximum_queue_length.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Maximum_queue_length.Location = new System.Drawing.Point(16, 65);
            this.Maximum_queue_length.Name = "Maximum_queue_length";
            this.Maximum_queue_length.Size = new System.Drawing.Size(168, 18);
            this.Maximum_queue_length.TabIndex = 2;
            this.Maximum_queue_length.Text = "Maximum queue length. ";
            // 
            // WaitingProbability
            // 
            this.WaitingProbability.AutoSize = true;
            this.WaitingProbability.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaitingProbability.Location = new System.Drawing.Point(16, 96);
            this.WaitingProbability.Name = "WaitingProbability";
            this.WaitingProbability.Size = new System.Drawing.Size(296, 18);
            this.WaitingProbability.TabIndex = 3;
            this.WaitingProbability.Text = "Probability that a customer wait in the queue";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_WaitingProbability);
            this.panel1.Controls.Add(this.textBox_Maximum_queue_length);
            this.panel1.Controls.Add(this.txtBox_Average_WaitingTime);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Average_waiting_time);
            this.panel1.Controls.Add(this.Maximum_queue_length);
            this.panel1.Controls.Add(this.WaitingProbability);
            this.panel1.Location = new System.Drawing.Point(27, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 128);
            this.panel1.TabIndex = 8;
            // 
            // textBox_WaitingProbability
            // 
            this.textBox_WaitingProbability.Location = new System.Drawing.Point(330, 97);
            this.textBox_WaitingProbability.Name = "textBox_WaitingProbability";
            this.textBox_WaitingProbability.Size = new System.Drawing.Size(100, 20);
            this.textBox_WaitingProbability.TabIndex = 13;
            // 
            // textBox_Maximum_queue_length
            // 
            this.textBox_Maximum_queue_length.Location = new System.Drawing.Point(330, 63);
            this.textBox_Maximum_queue_length.Name = "textBox_Maximum_queue_length";
            this.textBox_Maximum_queue_length.Size = new System.Drawing.Size(100, 20);
            this.textBox_Maximum_queue_length.TabIndex = 12;
            // 
            // txtBox_Average_WaitingTime
            // 
            this.txtBox_Average_WaitingTime.Location = new System.Drawing.Point(330, 36);
            this.txtBox_Average_WaitingTime.Name = "txtBox_Average_WaitingTime";
            this.txtBox_Average_WaitingTime.Size = new System.Drawing.Size(100, 20);
            this.txtBox_Average_WaitingTime.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(417, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Calculate the Measures of Performance for the system";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(478, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(476, 128);
            this.panel2.TabIndex = 9;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServerID,
            this.IdleProbability,
            this.AverageServiceTime,
            this.Utilization});
            this.dataGridView2.Location = new System.Drawing.Point(3, 24);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(444, 101);
            this.dataGridView2.TabIndex = 12;
            // 
            // ServerID
            // 
            this.ServerID.HeaderText = "Server ID";
            this.ServerID.Name = "ServerID";
            // 
            // IdleProbability
            // 
            this.IdleProbability.HeaderText = "IdleProbability";
            this.IdleProbability.Name = "IdleProbability";
            // 
            // AverageServiceTime
            // 
            this.AverageServiceTime.HeaderText = "AverageServiceTime ";
            this.AverageServiceTime.Name = "AverageServiceTime";
            // 
            // Utilization
            // 
            this.Utilization.HeaderText = "Utilization";
            this.Utilization.Name = "Utilization";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(-4, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(384, 19);
            this.label8.TabIndex = 11;
            this.label8.Text = "Calculate the Measures of Performance per server";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(960, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 64);
            this.button1.TabIndex = 10;
            this.button1.Text = "Server Busy Time ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1046, 520);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Display";
            this.Text = "Display";
            this.Load += new System.EventHandler(this.Display_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label Average_waiting_time;
        private System.Windows.Forms.Label Maximum_queue_length;
        private System.Windows.Forms.Label WaitingProbability;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_WaitingProbability;
        private System.Windows.Forms.TextBox textBox_Maximum_queue_length;
        private System.Windows.Forms.TextBox txtBox_Average_WaitingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Random_Digit_for_interarrival_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interarrival_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arrival_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Random_Digit_for_service_duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service_duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Server_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_Service_Begins;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_Service_Ends;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_in_queue;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdleProbability;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageServiceTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Utilization;
        private System.Windows.Forms.Button button1;
    }
}