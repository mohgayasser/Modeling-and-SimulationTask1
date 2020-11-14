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
        List<Server> ListServer; // list bta3at al servers aly 3andy fe al system
        List<TimeDistribution> ListinterArrrivalTime; /// al list aly bn7ot fiha al interarrival time w al probability aly gayeen mn al dataGradViwe
        List<TimeDistribution> ListServerTime; // al list aly bn7ot fiha al service time w al probability aly gayeen mn al dataGradViwe
        List<SimulationCase> ListsimulationCases; // list b7ot feha al system kolo row by row
        string Server_Selection_method = " "; // 3l4an a3raf al user a5tar eh mn al radioButtons bta3at al selection
        string Stopping_Condition = " "; // 3l4an a3raf al user a5tar eh mn al radioButtons bta3at al stopping condition
        string Stopping_Condition_Text_box = " "; // al value aly al user da5lha 3la asas al stopping condition selection
        double Simulation_end_Time=-1; // defult value 3l4an na3raf al user kan alma a5tar mn al stoppping condition a5tar al time w 7ato b value wala la han7tagha fe al performance  
        int customermaxValue = 100; // defult value 3l4an al user lw mad5alt max value ll castomers 

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
            if (radioButton6.Checked)
            {
                Stopping_Condition = "Maximum Number of customers";

            }
            if (radioButton5.Checked)
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
                    if (row.Cells[0].Value == null) break;
                    TimeDistribution td = new TimeDistribution();
                    td.Time = Int32.Parse(row.Cells[0].Value.ToString());
                    td.Probability = Decimal.Parse(row.Cells[1].Value.ToString());
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

            SimulationSystem simulation = new SimulationSystem();
           
            for (int id = 1; id <= 100; id++)
                {

                    SimulationCase sc = new SimulationCase();

                    sc.CustomerNumber = id;
            
                if (id == 1)
                {
                    sc.RandomInterArrival = 0;
                    sc.ArrivalTime = 0;
                    sc.InterArrival = 0;
                }
                else
                {
                    Random rd1 = new Random();
                    int rand_num_interarrivalTime = rd1.Next( 101);
                    sc.RandomInterArrival = rand_num_interarrivalTime;
                    for (int i = 0; i < ListinterArrrivalTime.Count; i++)
                    {
                        if (sc.RandomInterArrival >= ListinterArrrivalTime[i].MinRange && sc.RandomInterArrival <= ListinterArrrivalTime[i].MaxRange)
                        {
                            sc.InterArrival = ListinterArrrivalTime[i].Time;
                            break;
                        }
                    }
                    sc.ArrivalTime = ListsimulationCases[ListsimulationCases.Count - 1].ArrivalTime + sc.InterArrival;
                }
                   bool assigned = false;
  int minFinshTime = 10000000;
                    int selectedServer = -1; 
                /// condition of Assigned Server 
                if (Server_Selection_method.Equals("priority"))
                {
                   // sc = prioritySelection(sc);
                    simulation.SelectionMethod = MultiQueueModels.Enums.SelectionMethod.HighestPriority;

                    for (int i = 0; i < ListServer.Count; i++)
                    {
                        if (sc.CustomerNumber == 1)
                        {
                            sc.TimeInQueue = 0;
                        }
                        else
                        {
                            sc.TimeInQueue = ListServer[i].FinishTime - sc.InterArrival;
                        }

                        if (sc.TimeInQueue < minFinshTime)
                        {
                            minFinshTime = sc.TimeInQueue;
                            selectedServer = i;
                        }
                        if (ListServer[i].FinishTime <= sc.InterArrival)
                        {
                            sc.AssignedServer = ListServer[i];
                            assigned = true;
                           
                            break;
                        }

                    }
                    if (!assigned)
                    {
                        sc.AssignedServer = ListServer[selectedServer];
             
                    }
               
                }
                else
                {
                    List<int> emptyserv = new List<int>();
                    for (int i = 0; i < ListServer.Count; i++)
                    {
                        if (ListServer[i].idle)
                        { emptyserv.Add(i); }

                    }
                    if (emptyserv.Count != 0)
                    {
                        if (Server_Selection_method.Equals("random"))
                        {

                            sc = randomSelection(sc, emptyserv);
                            simulation.SelectionMethod = MultiQueueModels.Enums.SelectionMethod.Random;

                        }
                        if (Server_Selection_method.Equals("least utilization"))
                        {

                            sc = least_utilizationSelection(sc, emptyserv);
                            simulation.SelectionMethod = MultiQueueModels.Enums.SelectionMethod.LeastUtilization;
                        }

                    }
                }
                //////////////////////
                ///select random server value and find its range based on the selected server option
                Random rd = new Random();
                int rand_num_serverTime = rd.Next(100);
                    sc.RandomService = rand_num_serverTime;
                   
                        for (int ii = 0; ii < ListServer[sc.AssignedServer.ID-1].TimeDistribution.Count; ii++)
                        {
                            if ((sc.RandomService >= ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].MinRange) && (sc.RandomService <= ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].MaxRange))
                            {
                                sc.ServiceTime = ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].Time;
                        break;
                            }
                        }

                if (assigned)
                {
                    ListServer[sc.AssignedServer.ID-1].TotalWorkingTime += sc.ServiceTime;
                    sc.TimeInQueue = 0;
                    ListServer[sc.AssignedServer.ID-1].idle = false;
                    ListServer[sc.AssignedServer.ID-1].FinishTime += sc.ServiceTime;
                }else
                {
                    sc.TimeInQueue = Math.Abs(sc.InterArrival - ListServer[selectedServer].FinishTime);
                    ListServer[selectedServer].idle = false;
                    ListServer[selectedServer].TotalWorkingTime += sc.ServiceTime;
                    ListServer[selectedServer].FinishTime += sc.ServiceTime;
                }
                sc.EndTime = sc.InterArrival + sc.ServiceTime;
                sc.StartTime = sc.TimeInQueue + sc.InterArrival;

        
              
                if (Stopping_Condition.Equals("Simulation end time"))
                {
                    simulation.StoppingCriteria = MultiQueueModels.Enums.StoppingCriteria.SimulationEndTime;
                    if (sc.StartTime >= Convert.ToInt32(Stopping_Condition_Text_box))
                    {
                        
                        Simulation_end_Time = Convert.ToInt32(Stopping_Condition_Text_box);
                        break;
                    }
                }

                if (Stopping_Condition.Equals("Maximum Number of customers") && Convert.ToInt32(Stopping_Condition_Text_box) <= id)
                {
                    customermaxValue = Convert.ToInt32(Stopping_Condition_Text_box);
                    simulation.StoppingCriteria = MultiQueueModels.Enums.StoppingCriteria.NumberOfCustomers;
                    break;
                }

                ListsimulationCases.Add(sc);
            }

            /// Performance  Measures for the system
            PerformanceMeasures performanceMeasures = new PerformanceMeasures();
            Performance_Measures_For_The_System(performanceMeasures);

            /// Performance  Measures Per Server:
           
            Performance_Measures_Per_Server();

          
            ////// set all data in simulation system  variable 
            simulation.NumberOfServers = ListServer.Count;
            simulation.Servers = ListServer;
            simulation.InterarrivalDistribution = ListinterArrrivalTime;
            simulation.StoppingNumber = 100;
            simulation.PerformanceMeasures = performanceMeasures;
            simulation.SimulationTable = ListsimulationCases;
            //// pass all data to Display Form
            Display displayData = new Display(simulation);
            displayData.Show();
        }


        private void Performance_Measures_For_The_System( PerformanceMeasures performanceMeasures)
        {
           
            int totalCus = 0;
            int waitingInqueue = 0;
            for (int i = 0; i < customermaxValue-1; i++)
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

        }


        private void Performance_Measures_Per_Server()
        {
            for (int i = 0; i < ListServer.Count; i++)
            {

                int newend = -1;
                int maxEndTime = 0;
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

                        ListServer[i].idleTime += ListsimulationCases[j].StartTime - newend;
                        newend = ListsimulationCases[j].EndTime;
                    }


                }
                ListServer[i].IdleProbability = ListServer[i].idleTime / maxEndTime;
                ListServer[i].AverageServiceTime = ListServer[i].TotalServiceTime / customermaxValue;
                ListServer[i].Utilization = ListServer[i].TotalWorkingTime / maxEndTime;
            }

        }


        //private SimulationCase prioritySelection(SimulationCase sc)
        //{
        //    int minFinshTime = 10000000;
        //    int selectedServer = -1;
        //    bool assigned = false;
        //    for (int i = 0; i < ListServer.Count; i++)
        //    { if (sc.CustomerNumber== 1)
        //        {
        //            sc.TimeInQueue = 0;
        //        }
        //    else
        //        {
        //            sc.TimeInQueue = ListServer[i].FinishTime - sc.InterArrival;
        //        }

        //        if (sc.TimeInQueue < minFinshTime)
        //        {
        //            minFinshTime = sc.TimeInQueue;
        //            selectedServer = i;
        //        }
        //        if (ListServer[i].FinishTime <= sc.InterArrival)
        //        {
        //            sc.AssignedServer = ListServer[i];
        //            assigned = true;
        //            ListServer[i].TotalWorkingTime += sc.ServiceTime;
        //            sc.TimeInQueue = 0;
        //            ListServer[i].idle = false;
        //            ListServer[i].FinishTime += sc.ServiceTime;
        //            break;
        //        }
            
        //    }
        //    if (!assigned)
        //    {
        //        sc.AssignedServer = ListServer[selectedServer];
        //        sc.TimeInQueue = Math.Abs(sc.InterArrival - ListServer[selectedServer].FinishTime);
        //        ListServer[selectedServer].idle = false;
        //        ListServer[selectedServer].TotalWorkingTime += sc.ServiceTime;
        //        ListServer[selectedServer].FinishTime += sc.ServiceTime;
        //    }
        //    sc.EndTime = sc.InterArrival + sc.ServiceTime;
        //    sc.StartTime = sc.TimeInQueue + sc.InterArrival;
        //    return sc;
        //}


        private SimulationCase randomSelection(SimulationCase sc ,List<int> emptyserv)
        {
            Random rd2 = new Random();
            int randser = rd2.Next(0, emptyserv.Count - 1);
            if (ListServer[emptyserv[randser]].FinishTime <= sc.InterArrival)
            {
                sc.AssignedServer = ListServer[emptyserv[randser]];
            }

            sc.TimeInQueue = 0;
            ListServer[emptyserv[randser]].TotalWorkingTime += sc.ServiceTime;
            sc.EndTime = sc.InterArrival + sc.ServiceTime;
            sc.StartTime = sc.InterArrival;
            ListServer[emptyserv[randser]].idle = false;
            return sc;
        }



        private SimulationCase least_utilizationSelection(SimulationCase sc, List<int> emptyserv)
        {
            int min = 100000; int ind = 0;
            for (int i = 0; i < emptyserv.Count; i++)
            {
                if (ListServer[emptyserv[i]].TotalWorkingTime < min)
                {
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
            return sc;
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
          
            
                server.TotalServiceTime = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value == null) break;
                TimeDistribution td = new TimeDistribution();

                td.Time = Int32.Parse(row.Cells[0].Value.ToString());
                td.Probability = Decimal.Parse(row.Cells[1].Value.ToString());
                if (server.TimeDistribution.Count == 0)
                {
                    td.CummProbability = cumprobability(0, td.Probability);
                    td.MinRange = 0;
                    td.MaxRange = Convert.ToInt32(td.CummProbability * 100);
                }
                else
                {
                    td.CummProbability = cumprobability(server.TimeDistribution[server.TimeDistribution.Count - 1].CummProbability, td.Probability);
                    td.MinRange = server.TimeDistribution[server.TimeDistribution.Count - 1].MaxRange + 1;
                    td.MaxRange = Convert.ToInt32(td.CummProbability * 100); /// multiply fe 100 to not loss the numbers when convert it to integer
                }
                server.TimeDistribution.Add(td);
                server.TotalServiceTime += td.Time;


            }
                
                server.ID = IDserver++;
              
                server.TotalWorkingTime=0;
                ListServer.Add(server);
            dataGridView2.Rows.Clear();

            }
           
        

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
