using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.PerformanceData;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace THPerfMon.Methods
{
    class Performance
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void GetAvailableRam()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            var currentAvailableRam = ramCounter.NextValue() + "Mb";

            log.InfoFormat("\tAvailable RAM: {0}", currentAvailableRam);


        }

        public static void GetCpuUsage()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            CounterSample cs1 = cpuCounter.NextSample();
            System.Threading.Thread.Sleep(100);
            CounterSample cs2 = cpuCounter.NextSample();
            float finalCpuCounter = CounterSample.Calculate(cs1, cs2);

            log.InfoFormat("\tCPU Usage: {0}", finalCpuCounter);
        }

        public static void GetDiskUsage()
        {
            PerformanceCounter diskCounter = new PerformanceCounter("PhysicalDisk", "Avg. Disk Queue Length", "_Total");
            log.InfoFormat("\tAverage Disk Queue Length: {0}", diskCounter.NextValue());
        }
    }
}
