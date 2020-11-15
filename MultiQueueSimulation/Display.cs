using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueTesting;
using MultiQueueModels;

namespace MultiQueueSimulation
{

    public partial class Display : Form
    {
        SimulationSystem simulation;
        public Display(SimulationSystem simsystem)
        {
            this.simulation = simsystem;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Display_Load(object sender, EventArgs e)
        {
            txtBox_Average_WaitingTime.Text = simulation.PerformanceMeasures.AverageWaitingTime.ToString();
            textBox_WaitingProbability.Text = simulation.PerformanceMeasures.WaitingProbability.ToString();
           textBox_Maximum_queue_length.Text = simulation.PerformanceMeasures.MaxQueueLength.ToString();
           for (int i=0;i<simulation.Servers.Count; i++)
            {
             
                    dataGridView2.Rows.Add(simulation.Servers[i].ID,simulation.Servers[i].IdleProbability,simulation.Servers[i].AverageServiceTime,simulation.Servers[i].Utilization);
                
            }

           for(int i = 0; i < simulation.SimulationTable.Count; i++)
            {
                dataGridView1.Rows.Add(simulation.SimulationTable[i].CustomerNumber, simulation.SimulationTable[i].RandomInterArrival, simulation.SimulationTable[i].InterArrival, simulation.SimulationTable[i].ArrivalTime, simulation.SimulationTable[i].RandomService, simulation.SimulationTable[i].ServiceTime, simulation.SimulationTable[i].AssignedServer.ID,simulation.SimulationTable[i].StartTime,simulation.SimulationTable[i].EndTime,simulation.SimulationTable[i].TimeInQueue);
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < simulation.NumberOfServers ; i++)
            {
                Form form = new Form();

                form.Size = new Size(1600, 600);
                var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chart.Size = new Size(1600, 600);
                chart.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());

                chart.Titles.Add("Chart of server " + (i + 1).ToString());
                chart.ChartAreas[0].AxisX.Title = "Time";
                chart.ChartAreas[0].AxisX.Minimum = 0;

                chart.ChartAreas[0].AxisX.Interval = 2;
                chart.ChartAreas[0].AxisX.IsMarginVisible = false;

                chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;


                var series = new System.Windows.Forms.DataVisualization.Charting.Series();

                for (int j = 0; j < simulation.SimulationTable.Count; j++)
                {
                    if (simulation.SimulationTable[j].AssignedServer.ID == i+1)
                    {
                        for (int time = simulation.SimulationTable[j].StartTime; time <= simulation.SimulationTable[j].EndTime; time++)
                        {
                            series.Points.AddXY(time, 1);
                        }
                    }
                }
                    chart.Series.Add(series);
                chart.Series[0]["PointWidth"] = "1";

                form.Controls.Add(chart);
                
                    form.Show();
            }
        }
    }
}
