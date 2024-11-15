using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flowershop_Thesis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.IO;
using System.Text.Json;

namespace Flowershop_Thesis.MainForms
{
    public partial class SystemSettings : Form
    {

        private const string configFilePath = "SystemStoredConfiguration.json";
        private AppConfig appConfig;
        public SystemSettings()
        {
            InitializeComponent();
            LoadConfig();
  
        
        }
        public void getdirectory()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Build the file name to search for
            string fileName = "SystemStoredConfiguration.json";

            // Search for the file (including subdirectories)
            string[] files = Directory.GetFiles(executableDirectory, fileName, SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                // If the file is found, extract its directory path
              //   configFilePath = Path.GetDirectoryName(files[0]);
      
                // Store the directory path in a string variable
               // Console.WriteLine($"File found. Directory: {configFilePath}");
            }
            else
            {
                // If the file is not found, print a message
                MessageBox.Show("File not found.");
            }
        }

        private void LoadConfig()
        {
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                appConfig = JsonSerializer.Deserialize<AppConfig>(json);
            }
            else
            {
                // Default values
                appConfig = new AppConfig
                {
                    IP = "192.168.1.100",
                    Port = "1433",
                    MainPC = true
                };
            }

            // Populate UI elements
            //textBox5.Text = appConfig.IP;
            //textBox6.Text = appConfig.Port;
            //checkBox1.Checked = appConfig.MainPC;
            //label8.Text = appConfig.CreateConnectionString();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppConfig.TestConnectionString(appConfig.ConnectionString) == true)
                {
                    MessageBox.Show("Connected");
                }
                else
                {
                    MessageBox.Show("Cannot Connect");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
