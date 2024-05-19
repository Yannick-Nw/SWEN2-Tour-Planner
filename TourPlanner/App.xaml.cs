using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;


namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Get the path to the project's directory (two levels up from the output directory)
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            // Define the Logs directory path
            string logsDirectory = Path.Combine(projectDirectory, "Logs");

            // Ensure the Logs directory exists
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            // Set the log file path dynamically
            string logFilePath = Path.Combine(logsDirectory, "logfile.log");
            log4net.GlobalContext.Properties["LogFileName"] = logFilePath;

            // Configure log4net
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
           
        }

    }
}
