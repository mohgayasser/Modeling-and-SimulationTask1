﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class Server
    {
        public Server()
        {
            this.TimeDistribution = new List<TimeDistribution>();
            this.idle = true;
            this.FinishTime = 0;
            this.TotalServiceTime = 0;
            this.TotalCustomers = 0;
        }

        public int ID { get; set; }
        public decimal IdleProbability { get; set; }
        public decimal AverageServiceTime { get; set; } 
        public decimal Utilization { get; set; }

        public List<TimeDistribution> TimeDistribution;

        //optional if needed use them
        public int FinishTime { get; set; }
        public int TotalWorkingTime { get; set; }
        public int TotalServiceTime { get; set; }
        public int TotalCustomers { get; set; }
        public bool idle{ get; set;}
        public int idleTime { get; set; }
    }
}
