using Capstone_Flowershop;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {  
            
            //System Configurations
            SystemInfo.SecurityCode = "Admin1233";
            SystemInfo.discount = 0.10m;
            SystemInfo.MinimumOrder = 10;


            //Proceeding Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
