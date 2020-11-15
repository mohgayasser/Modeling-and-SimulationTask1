using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueTesting;
using MultiQueueModels;

namespace MultiQueueSimulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // SimulationSystem system = new SimulationSystem();
            /*
            Form1 form = new Form1();
            system.InterarrivalDistribution = form.simulation.InterarrivalDistribution;
            system.NumberOfServers = form.simulation.NumberOfServers;
            system.PerformanceMeasures = form.simulation.PerformanceMeasures;
            system.SelectionMethod = form.simulation.SelectionMethod;
            system.Servers = form.simulation.Servers;
            system.SimulationTable = form.simulation.SimulationTable;
            system.StoppingCriteria = form.simulation.StoppingCriteria;
            system.StoppingNumber = form.simulation.StoppingNumber;
            */

            //string result = TestingManager.Test(system, Constants.FileNames.TestCase1);
            //MessageBox.Show(result);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           
        }
    }
}
