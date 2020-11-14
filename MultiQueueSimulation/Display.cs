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
           // textBox_Maximum_queue_length.Text = simulation.PerformanceMeasures.MaxQueueLength.ToString();
           for (int i=0;i<simulation.Servers.Count; i++)
            {
             
                    dataGridView2.Rows.Add(simulation.Servers[i].ID,simulation.Servers[i].IdleProbability,simulation.Servers[i].AverageServiceTime,simulation.Servers[i].Utilization);


              
            }

           for(int i = 0; i < simulation.SimulationTable.Count; i++)
            {
                dataGridView1.Rows.Add(simulation.SimulationTable[i].CustomerNumber, simulation.SimulationTable[i].RandomInterArrival, simulation.SimulationTable[i].InterArrival, simulation.SimulationTable[i].ArrivalTime, simulation.SimulationTable[i].RandomService, simulation.SimulationTable[i].ServiceTime, simulation.SimulationTable[i].AssignedServer.ID,simulation.SimulationTable[i].StartTime,simulation.SimulationTable[i].EndTime,simulation.SimulationTable[i].TimeInQueue);
            }
          
        }
    }
}
