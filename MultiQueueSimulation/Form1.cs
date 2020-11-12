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
        string Server_Selection_method = " ";
        string Stopping_Condition = " ";
        string Stopping_Condition_Text_box = " ";
        double Average_waiting_time;
        double Maximum_queue_length;
        double Probability_that_acustomer;
        double Simulation_end_Time=-1;
        int customermaxValue = 100;

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
            Stopping_Condition_Text_box = textBox2.Text;
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
                    int rand_num_interarrivalTime = rd.Next(0, 100);
                    sc.RandomInterArrival = rand_num_interarrivalTime;
                    for (int i = 0; i < ListinterArrrivalTime.Count; i++) {
                        if (sc.RandomInterArrival >= ListinterArrrivalTime[i].MinRange && sc.RandomInterArrival <= ListinterArrrivalTime[i].MaxRange)
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
                    if (Server_Selection_method.Equals("priority")) {
                        int minFinshTime = 10000000;
                        int selectedServer = -1;
                        bool assigned = false;
                        for (int i = 0; i < ListServer.Count; i++)
                        {
                            sc.TimeInQueue = Math.Abs(sc.InterArrival - ListServer[i].FinishTime);
                            if (sc.TimeInQueue < minFinshTime) {
                                minFinshTime = sc.TimeInQueue;
                                selectedServer = i;
                            }
                            if (ListServer[i].FinishTime <= sc.InterArrival) {
                                sc.AssignedServer = ListServer[i];
                                ListServer[i].TotalWorkingTime += sc.ServiceTime;
                                assigned = true;
                                sc.TimeInQueue = 0;
                                ListServer[i].idle = false;
                                break;
                            }

                        }
                        if (!assigned) {
                            sc.AssignedServer = ListServer[selectedServer];
                            sc.TimeInQueue = Math.Abs(sc.InterArrival - ListServer[selectedServer].FinishTime);
                            ListServer[selectedServer].idle = false;
                            ListServer[selectedServer].TotalWorkingTime += sc.ServiceTime;
                        }
                        sc.EndTime = sc.InterArrival + sc.ServiceTime;
                        sc.StartTime = sc.TimeInQueue + sc.InterArrival;
                    }
                    List<int> emptyserv = new List<int>();
                    for (int i = 0; i < ListServer.Count; i++)
                    {
                        if (ListServer[i].idle)
                        { emptyserv.Add(i); }

                    }
                    if (emptyserv.Count != 0) {
                        if (Server_Selection_method.Equals("random")) {

                            Random rd2 = new Random();
                            int randser = rd2.Next(0, emptyserv.Count - 1);
                            if (ListServer[emptyserv[randser]].FinishTime <= sc.InterArrival) {
                                sc.AssignedServer = ListServer[emptyserv[randser]];
                            }

                            sc.TimeInQueue = 0;
                            ListServer[emptyserv[randser]].TotalWorkingTime += sc.ServiceTime;
                            sc.EndTime = sc.InterArrival + sc.ServiceTime;
                            sc.StartTime = sc.InterArrival;
                            ListServer[emptyserv[randser]].idle = false;
                        }
                        if (Server_Selection_method.Equals("least utilization")) {
                            int min = 100000; int ind = 0;
                            for (int i = 0; i < emptyserv.Count; i++) {
                                if (ListServer[emptyserv[i]].TotalWorkingTime < min) {
                                    min = ListServer[emptyserv[i]].TotalWorkingTime;
                                    ind = emptyserv[i];
                                }

                            }
                            sc.AssignedServer = ListServer[ind];
                            sc.TimeInQueue = 0;
                            sc.EndTime = sc.InterArrival + sc.ServiceTime;
                            sc.StartTime = sc.InterArrival;
                            ListServer[emptyserv[ind]].idle = false;
                            ListServer[ind].TotalWorkingTime += sc.ServiceTime;


                        }

                    }
                if (Stopping_Condition.Equals("Simulation end time"))
                {
                    if (sc.StartTime >= Convert.ToInt32(Stopping_Condition_Text_box))
                    {
                      
                        Simulation_end_Time = Convert.ToInt32(Stopping_Condition_Text_box);
                        break;
                    }
                }

                if (Stopping_Condition.Equals("Maximum Number of customers") && Convert.ToInt32(Stopping_Condition_Text_box) <= id)
                {
                    customermaxValue = Convert.ToInt32(Stopping_Condition_Text_box);
                    break;
                }

                ListsimulationCases.Add(sc);
            }





            /// Performance  Measures for the system
            PerformanceMeasures performanceMeasures = new PerformanceMeasures();
            int totalCus = 0;
            int waitingInqueue = 0;
            for (int i=0;i<customermaxValue;i++)
            {
                totalCus += ListsimulationCases[i].TimeInQueue;
                if (ListsimulationCases[i].TimeInQueue != 0)
                {
                    waitingInqueue++;
                }
            }
            performanceMeasures.AverageWaitingTime = totalCus / customermaxValue;
            //////////////////////////// performanceMeasures.MaxQueueLength
            performanceMeasures.WaitingProbability = waitingInqueue / customermaxValue;


            /// Performance  Measures Per Server:
            /// 
            for (int i = 0; i < ListServer.Count; i++)
            {

                int newend = -1;
                int maxEndTime =0;
                for (int j = 0; j < ListsimulationCases.Count; j++)
                {
                    if (maxEndTime < ListsimulationCases[j].EndTime)
                    {
                        maxEndTime = ListsimulationCases[j].EndTime;
                    }
                    if (ListsimulationCases[j].AssignedServer == ListServer[i])
                    {

                        if (newend == -1)
                        {

                            newend = ListsimulationCases[j].EndTime; 
                            ListServer[i].idleTime = 0;
                            continue;
                        }
                        
                        ListServer[i].idleTime += ListsimulationCases[j].StartTime -newend;
                        newend = ListsimulationCases[j].EndTime;
                    }
                   
                    
                }
                ListServer[i].IdleProbability = ListServer[i].idleTime / maxEndTime;
                ListServer[i].AverageServiceTime = ListServer[i].TotalServiceTime / customermaxValue;
                ListServer[i].Utilization = ListServer[i].TotalWorkingTime / maxEndTime;
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
                server.TotalServiceTime = 0;
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
                    server.TotalServiceTime += td.Time;


                }
                server.TimeDistribution = ListServerTime;
                server.ID = IDserver++;
              
                server.TotalWorkingTime=0;
                ListServer.Add(server);

            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
