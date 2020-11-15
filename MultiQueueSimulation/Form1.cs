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
        Queue<SimulationCase> Queue_wating;
        int max_Queue_wating_length;
         public SimulationSystem simulation = new SimulationSystem();

        string Server_Selection_method = " "; // 3l4an a3raf al user a5tar eh mn al radioButtons bta3at al selection
        string Stopping_Condition = " "; // 3l4an a3raf al user a5tar eh mn al radioButtons bta3at al stopping condition
        string Stopping_Condition_Text_box = " "; // al value aly al user da5lha 3la asas al stopping condition selection
        double Simulation_end_Time=-1; // defult value 3l4an na3raf al user kan alma a5tar mn al stoppping condition a5tar al time w 7ato b value wala la han7tagha fe al performance  
        int customermaxValue = 100; // defult value 3l4an al user lw mad5alt max value ll castomers 

        Random rd1 = new Random();
        Random rd2 = new Random();
        Random rd = new Random();
        List<int> emptyserv = new List<int>();
        int randser;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            ListinterArrrivalTime = new List<TimeDistribution>();
            ListServerTime = new List<TimeDistribution>();
            ListsimulationCases = new List<SimulationCase>();
            Queue_wating = new Queue<SimulationCase>();
            max_Queue_wating_length = 0;
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

            int min = 100000;
            int ind=0;
            bool idleFound=false;
            int notidle = 0;
            for (int id = 1; id <= 100; id++)
                {

                    SimulationCase sc = new SimulationCase();

                    sc.CustomerNumber = id;
            
                if (id == 1)
                {
                    sc.RandomInterArrival = 1;
                    sc.ArrivalTime = 0;
                    sc.InterArrival = 0;
                }
                else
                {
                    int rand_num_interarrivalTime = rd1.Next( 1,100);
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
                            sc.TimeInQueue = ListServer[i].FinishTime - sc.ArrivalTime;
                        }

                        if (sc.TimeInQueue < minFinshTime)
                        {
                            minFinshTime = sc.TimeInQueue;
                            selectedServer = i;
                        }
                        if (ListServer[i].FinishTime <= sc.ArrivalTime)
                        {
                            sc.AssignedServer = ListServer[i];
                            assigned = true;
                           
                            break;
                        }

                    }
                    if (!assigned)
                    {
                        sc.AssignedServer = ListServer[selectedServer];
                        ////////////

             
                    }
                   
                }
               
                else
                {
                   
                    for (int i = 0; i < ListServer.Count; i++)
                    {
                        if (ListServer[i].idle)
                        { emptyserv.Add(i); }

                    }
                    
                    randser = rd2.Next(0, emptyserv.Count - 1);
                    if (emptyserv.Count != 0)
                    {
                        if (Server_Selection_method.Equals("random"))
                        {


                            if (ListServer[emptyserv[randser]].FinishTime <= sc.ArrivalTime)
                            {
                                sc.AssignedServer = ListServer[emptyserv[randser]];
                            }
                            simulation.SelectionMethod = MultiQueueModels.Enums.SelectionMethod.Random;

                        }
                    }
                    if (Server_Selection_method.Equals("least utilization"))
                    {
                       

                             min = 100000;  ind=0;
                             idleFound = false;
                              notidle = 0;
                            for (int i = 0; i < ListServer.Count; i++)
                            {
                               
                                if (ListServer[i].idle)
                                {
                                    if (ListServer[i].TotalWorkingTime < min)
                                    {
                                        min = ListServer[i].TotalWorkingTime;
                                        ind = i;
                                        idleFound = true;
                                    }
                                }else
                                {
                                    if (ListServer[i].TotalWorkingTime < min)
                                    {
                                        min = ListServer[i].TotalWorkingTime;
                                        notidle = i;
                                    }
                                }
                            }if (idleFound)
                            {
                                sc.AssignedServer = ListServer[ind];
                                ListServer[ind].idle = false;
                            }
                            else
                            {
                                sc.AssignedServer = ListServer[notidle];
                                ListServer[notidle].idle = false;
                            }
                        }
                       
                        simulation.SelectionMethod = MultiQueueModels.Enums.SelectionMethod.LeastUtilization;
                    }
                   
                
                //////////////////////
                ///select random server value and find its range based on the selected server option
                int rand_num_serverTime = rd.Next(1,101);
                    sc.RandomService = rand_num_serverTime;
                   
                        for (int ii = 0; ii < ListServer[sc.AssignedServer.ID-1].TimeDistribution.Count; ii++)
                        {
                            if ((sc.RandomService >= ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].MinRange) && (sc.RandomService <= ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].MaxRange))
                            {
                                sc.ServiceTime = ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].Time;
                                ListServer[sc.AssignedServer.ID - 1].TotalServiceTime += ListServer[sc.AssignedServer.ID - 1].TimeDistribution[ii].Time;
                                ListServer[sc.AssignedServer.ID - 1].TotalCustomers += 1;
                        break;
                            }
                        }
                if (Server_Selection_method.Equals("priority")) {
                    if (assigned)
                    {
                        ListServer[sc.AssignedServer.ID - 1].TotalWorkingTime += sc.ServiceTime;
                        sc.TimeInQueue = 0;
                        ListServer[sc.AssignedServer.ID - 1].idle = false;
                        sc.StartTime = sc.TimeInQueue + sc.ArrivalTime;
                        sc.EndTime = sc.StartTime + sc.ServiceTime;
                        ListServer[sc.AssignedServer.ID - 1].FinishTime = sc.EndTime;
                    } else
                    {
                        while (Queue_wating.Count != 0)
                        {
                            if (Queue_wating.Peek().StartTime <= sc.ArrivalTime)
                                Queue_wating.Dequeue();
                            else
                                break;
                        }
                        Queue_wating.Enqueue(sc);

                        if (Queue_wating.Count > max_Queue_wating_length)
                            max_Queue_wating_length = Queue_wating.Count;

                        sc.TimeInQueue = Math.Abs(sc.ArrivalTime - ListServer[selectedServer].FinishTime);
                        ListServer[selectedServer].idle = false;
                        ListServer[selectedServer].TotalWorkingTime += sc.ServiceTime;
                        sc.StartTime = sc.TimeInQueue + sc.ArrivalTime;
                        sc.EndTime = sc.StartTime + sc.ServiceTime;
                        ListServer[selectedServer].FinishTime = sc.EndTime;
                    }
                }
                
                     if(Server_Selection_method.Equals("random"))
                {
                    sc.TimeInQueue = 0;
                    ListServer[emptyserv[randser]].TotalWorkingTime += sc.ServiceTime;
                    sc.StartTime = sc.ArrivalTime;
                    sc.EndTime = sc.StartTime + sc.ServiceTime;

                    ListServer[emptyserv[randser]].idle = false;
                }

                if (Server_Selection_method.Equals("least utilization"))
                {
                   
                    if (idleFound)
                    {
                        
                        sc.TimeInQueue = 0;
                        sc.StartTime = sc.ArrivalTime;
                        sc.EndTime = sc.StartTime + sc.ServiceTime;
                        ListServer[ind].FinishTime = sc.EndTime;
                    }
                    else
                    {
                        while (Queue_wating.Count != 0)
                        {
                            if (Queue_wating.Peek().StartTime <= sc.ArrivalTime)
                                Queue_wating.Dequeue();
                            else
                                break;
                        }
                        Queue_wating.Enqueue(sc);

                        if (Queue_wating.Count > max_Queue_wating_length)
                            max_Queue_wating_length = Queue_wating.Count;

                        sc.TimeInQueue = Math.Abs(sc.ArrivalTime - ListServer[notidle].FinishTime);
                        sc.StartTime= sc.TimeInQueue + sc.ArrivalTime;
                        sc.EndTime = sc.StartTime + sc.ServiceTime;
                        ListServer[notidle].FinishTime = sc.EndTime;
                    }
                 
                   
                    ListServer[sc.AssignedServer.ID-1].TotalWorkingTime += sc.ServiceTime;
                }
                ListsimulationCases.Add(sc);

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

            ///////////// call testing Manager


            string result = TestingManager.Test(simulation, Constants.FileNames.TestCase1);
            //string result = TestingManager.Test(simulation, Constants.FileNames.TestCase2);
            //string result = TestingManager.Test(simulation, Constants.FileNames.TestCase3);
            MessageBox.Show(result);

            //// pass all data to Display Form
            Display displayData = new Display(simulation);
            displayData.Show();
        }


        private void Performance_Measures_For_The_System( PerformanceMeasures performanceMeasures)
        {
           
            int totalCus = 0; // calculate total time in waiting queue
            int waitingInqueue = 0;
            for (int i = 0; i < customermaxValue; i++)
            {
                totalCus += ListsimulationCases[i].TimeInQueue;
                if (ListsimulationCases[i].TimeInQueue != 0)
                {
                    waitingInqueue++;
                }
            }
            performanceMeasures.AverageWaitingTime =(decimal) totalCus / (decimal)customermaxValue;
            performanceMeasures.MaxQueueLength = max_Queue_wating_length;//////////////////////
            performanceMeasures.WaitingProbability = (decimal)waitingInqueue / (decimal)customermaxValue;

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
                ListServer[i].IdleProbability =(decimal) (maxEndTime- ListServer[i].TotalServiceTime) / (decimal)maxEndTime;
                if(ListServer[i].TotalCustomers>0)
                ListServer[i].AverageServiceTime = (decimal)ListServer[i].TotalServiceTime / (decimal)ListServer[i].TotalCustomers;
                else
                {
                    ListServer[i].AverageServiceTime = 0;
                }
                ListServer[i].Utilization = (decimal)ListServer[i].TotalWorkingTime / (decimal)maxEndTime;
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
