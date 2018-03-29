using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

namespace THPerfMon.Methods
{
    class TaskRunner
    {

        public static void Run()
        {
            XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            var config = XDocument.Load(File.OpenRead("THPerMon.exe.config"));
            var section = config.Descendants("tasklist");

            log.Info("List of tasks configured:");

            foreach (var element in section.Descendants())
            {
                log.Info("\t\t" + element.Attribute("definition").Value);
            }

            ProcessTasks(section, log);

        }

        public static void ProcessTasks(IEnumerable<XElement> section, ILog log)
        {
            log.Info("Processing tasks....");

            foreach (var element in section.Descendants())
            {
                var definition = element.Attribute("definition").Value;

                switch (definition)
                {
                    case Constants.Performance.CPUUSAGETOTAL:
                        Performance.GetCpuUsage();
                        break;
                    case Constants.Performance.DISKUSUAGETOTAL:
                        Performance.GetDiskUsage();
                        break;
                    case Constants.Performance.RAMUSUAGETOTAL:
                        Performance.GetAvailableRam();
                        break;
                    default:
                        log.InfoFormat("Task type not implemented ({0}). Check the task list.", definition);
                        break;
                }
            }
        }

    }
}
