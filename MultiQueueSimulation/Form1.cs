using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class Form1 : Form
    {
        List<Server> ListServer;
        List<TimeDistribution> ListinterArrrivalTime;
        List<TimeDistribution> ListServerTime;
        List<SimulationCase> ListsimulationCases;
        string Server_Selection_method = "";
        string Stopping_Condition = "";
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ListinterArrrivalTime = new List<TimeDistribution>();
            ListServerTime = new List<TimeDistribution>();
            ListsimulationCases = new List<SimulationCase>();
            ListServer = new List<Server>();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//Done
        {
            int numServer = Int32.Parse(textBox1.Text);
            
            if (radioButton1.Checked)
            {
                Server_Selection_method = "priority";
            }
            if (radioButton2.Checked)
            {
                Server_Selection_method = "random";

            }
            if (radioButton3.Checked)
            {
                Server_Selection_method = "least utilization";

            }
            if (radioButton5.Checked)
            {
                Stopping_Condition = "Maximum Number of customers";

            }
            if (radioButton6.Checked)
            {
                Stopping_Condition = "Simulation end time";

            }
            {
                //// get the data that the user entered into the  dataGridView and calculate 
                ///CummProbability
                ///MinRange
                ///MaxRange
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    TimeDistribution td = new TimeDistribution();
                    td.Time = Int32.Parse(row.Cells[0].Value.ToString());
                    td.Probability = Int32.Parse(row.Cells[1].Value.ToString());
                    if (ListinterArrrivalTime.Count == 0)
                    {
                        td.CummProbability = cumprobability(0, td.Probability);
                        td.MinRange = 0;
                        td.MaxRange = Convert.ToInt32(td.CummProbability * 100);
                    }
                    else
                    {
                        td.CummProbability = cumprobability(ListinterArrrivalTime[ListinterArrrivalTime.Count - 1].CummProbability, td.Probability);
                        td.MinRange = ListinterArrrivalTime[ListinterArrrivalTime.Count - 1].MaxRange + 1;
                        td.MaxRange = Convert.ToInt32(td.CummProbability * 100);
                    }
                    ListinterArrrivalTime.Add(td);

                }


            }
            ////// opertaions for each customer in the server by defult 100 customer
            ///CustomerNumber
            ///RandomInterArrival
            ///InterArrival
            ///ArrivalTime
            ///rand_num_serverTime
            ///ServiceTime
            
            for (int id = 1; id <= 100; id++)
            {
                SimulationCase sc = new SimulationCase();
                sc.CustomerNumber = id;
                Random rd = new Random();
                int rand_num_interarrivalTime = rd.Next(0,100);
                sc.RandomInterArrival = rand_num_interarrivalTime;
                for (int i=0;i < ListinterArrrivalTime.Count; i++){
                    if (sc.RandomInterArrival >= ListinterArrrivalTime[i].MinRange&& sc.RandomInterArrival <= ListinterArrrivalTime[i].MaxRange)
                    {
                        sc.InterArrival = ListinterArrrivalTime[i].Time;
                    }
                   }
                if (id == 1)
                {
                    sc.ArrivalTime = 0;
                }
                else
                {
                    sc.ArrivalTime = ListsimulationCases[ListsimulationCases.Count - 1].ArrivalTime + sc.InterArrival;
                }
                int rand_num_serverTime = rd.Next(0, 100);
                sc.RandomService = rand_num_serverTime;
                for (int j = 0; j < numServer; j++)
                {
                    for (int ii = 0; ii < ListServer[j].TimeDistribution.Count; ii++)
                    {  
                        if (sc.RandomService >= ListServer[j].TimeDistribution[ii].MinRange && sc.RandomService <= ListServer[j].TimeDistribution[ii].MaxRange)
                        {
                            sc.ServiceTime = ListServer[j].TimeDistribution[ii].Time;
                        }
                    }
                }

                /// condition of Assigned Server 
            }

        }

        private decimal cumprobability(decimal cumvalue,decimal probabilityvalue)
        {
            return cumvalue + probabilityvalue; ;
        }

        // TimeDistribution of the server and assign the server id &TimeDistribution list into the list of servers 
        int IDserver =1;
        private void button1_Click(object sender, EventArgs e) //clear  when the user click on it that wil be meaning the user enter 1 server 
        {
            Server server = new Server();
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    TimeDistribution td = new TimeDistribution();
                    td.Time = Int32.Parse(row.Cells[0].Value.ToString());
                    td.Probability = Int32.Parse(row.Cells[1].Value.ToString());
                    if (ListServerTime.Count == 0)
                    {
                        td.CummProbability = cumprobability(0, td.Probability);
                        td.MinRange = 0;
                        td.MaxRange = Convert.ToInt32(td.CummProbability * 100);
                    }
                    else
                    {
                        td.CummProbability = cumprobability(ListServerTime[ListServerTime.Count - 1].CummProbability, td.Probability);
                        td.MinRange = ListServerTime[ListServerTime.Count - 1].MaxRange + 1;
                        td.MaxRange = Convert.ToInt32(td.CummProbability * 100); /// multiply fe 100 to not loss the numbers when convert it to integer
                    }
                    ListServerTime.Add(td);
                  
                    
                }
                server.TimeDistribution = ListServerTime;
                server.ID = IDserver++;
                ListServer.Add(server);

            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
