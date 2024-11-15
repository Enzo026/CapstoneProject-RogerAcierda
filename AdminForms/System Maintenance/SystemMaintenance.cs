using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flowershop_Thesis.MainForms;
using Microsoft.Win32.TaskScheduler;

namespace Capstone_Flowershop.AdminForms.System_Maintenance
{
    public partial class SystemMaintenance : Form
    {
        public SystemMaintenance()
        {
            InitializeComponent();
            LoadSystemSettings();



        }
        private void CreateDailyBackupTask()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    // Check if the task already exists
                    Microsoft.Win32.TaskScheduler.Task existingTask = null;

                    try
                    {
                        existingTask = ts.GetTask("DailyDatabaseBackup");
                    }
                    catch (Exception)
                    {
                        // Task does not exist, that's fine
                        existingTask = null;
                    }

                    if (existingTask != null)
                    {
                        // If the task already exists, notify the user and do not create a new one
                        return;
                    }

                    // Define the task
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Run daily database backup at the end of the day";

                    // Trigger: run every day at 11:59 PM
                    td.Triggers.Add(new DailyTrigger() { StartBoundary = DateTime.Today.AddDays(1).AddMinutes(-1) });

                    // Action: run sqlcmd to execute the stored procedure
                    string sqlCmdArgs = "-S 192.168.8.205,2626 -d master -Q \"EXEC dbo.BackupDatabase;\"";

                    td.Actions.Add(new ExecAction("sqlcmd", sqlCmdArgs));

                    // Register the task in Task Scheduler
                    ts.RootFolder.RegisterTaskDefinition("DailyDatabaseBackup", td);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating scheduled task: {ex.Message}", "Error");
            }
        }

        public void CreateScheduledTask()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    // Check if the task already exists
                    Microsoft.Win32.TaskScheduler.Task existingTask = null;

                    try
                    {
                        existingTask = ts.GetTask("MonthlyDatabaseBackup");
                    }
                    catch (Exception)
                    {
                        // Task does not exist, that's fine
                        existingTask = null;
                    }

                    if (existingTask != null)
                    {
                        // If the task already exists, notify the user and do not create a new one
             
                        return;
                    }

                    // Define the task
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Run monthly database backup on the first day of every month at midnight.";

                    // Trigger: run monthly on the first day of every month at 12:00 AM (midnight)
                    MonthlyTrigger monthlyTrigger = new MonthlyTrigger();
                    monthlyTrigger.StartBoundary = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1, 0, 0, 0);  // Set start date/time to the first day of the next month at midnight
                    monthlyTrigger.DaysOfMonth = new int[] { 1 };  // Run on the 1st of each month
                    td.Triggers.Add(monthlyTrigger);

                    // Action: run sqlcmd to execute the stored procedure
                    string sqlCmdArgs = "-S 192.168.8.205,2626 -d master -Q \"EXEC dbo.BackupDatabase;\"";

                    td.Actions.Add(new ExecAction("sqlcmd", sqlCmdArgs));

                    // Register the task in Task Scheduler
                    ts.RootFolder.RegisterTaskDefinition("MonthlyDatabaseBackup", td);
   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating scheduled task: {ex.Message}", "Error");
            }
        }





        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }


        // Enable the scheduled task
        private void EnableScheduledTask()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    // Get the task definition
                    Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("MonthlyDatabaseBackup");

                    if (task != null)
                    {
                        // Enable the task
                        task.Enabled = true;
                      
                    }
                    else
                    {
                        MessageBox.Show("The task does not exist.", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error enabling the task: {ex.Message}", "Error");
            }
        }

        // Disable the scheduled task
        private void DisableScheduledTask()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    // Get the task definition
                    Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("MonthlyDatabaseBackup");

                    if (task != null)
                    {
                        // Disable the task
                        task.Enabled = false;
                       
                    }
                    else
                    {
                        MessageBox.Show("The task does not exist.", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disabling the task: {ex.Message}", "Error");
            }
        }
        private void EnableScheduledTaskDaily()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    // Get the task by name
                    Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("DailyDatabaseBackup");

                    // Enable the task
                    if (task != null)
                    {
                        task.Definition.Settings.Enabled = true;
                        task.RegisterChanges();  // Apply changes
                  
                    }
                    else
                    {
                        MessageBox.Show("The task does not exist.", "Task Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error enabling the task: {ex.Message}", "Error");
            }
        }
        private void DisableScheduledTaskDaily()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    // Get the task by name
                    Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("DailyDatabaseBackup");

                    // Disable the task
                    if (task != null)
                    {
                        task.Definition.Settings.Enabled = false;
                        task.RegisterChanges();  // Apply changes
                 
                    }
                    else
                    {
                        MessageBox.Show("The task does not exist.", "Task Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disabling the task: {ex.Message}", "Error");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {
                CreateScheduledTask();
                DisableScheduledTaskDaily();
                EnableScheduledTask();
               
                
            }
            if (radioButton6.Checked)
            {
                CreateDailyBackupTask();
                DisableScheduledTask();
                EnableScheduledTaskDaily();
            }
        }
        string oldfilepath;
        private void LoadSystemSettings()
        {
           
            string query = "SELECT Top 1 FilePath, BackupMode, FileName, SecurityCode, Discount, MinimumOrder FROM SystemSettingsTbl";

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Assuming the query returns a single row, read each value and assign to properties
                                string filePath = reader["FilePath"].ToString();
                                oldfilepath = reader["FilePath"].ToString();
                                string backupMode = reader["BackupMode"].ToString();
                                string fileName = reader["FileName"].ToString();
                                string securityCode = reader["SecurityCode"].ToString();
                                string discount = reader["Discount"].ToString();
                                string minimumOrder = reader["MinimumOrder"].ToString();

                                // Forward the gathered values to the textboxes or use them as required
                                textBox2.Text = filePath;
                                textBox1.Text = fileName;
                                textBox6.Text = discount;
                                textBox7.Text = minimumOrder;
                                textBox8.Text = filePath;
                                textBox9.Text = fileName;


                                if(backupMode == "Daily")
                                {
                                    radioButton6.Checked = true;
                                    
                                }
                                else if (backupMode == "Monthly")
                                {
                                    radioButton1.Checked = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("No settings found in the database.", "Error");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading system settings: {ex.Message}", "Error");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (panel3.Enabled)
            {
                panel3.Enabled = false;
            }
            else
            {
                panel3.Enabled= true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (panel4.Enabled)
            {
                panel4.Enabled = false;
            }
            else
            {
                panel4.Enabled = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (panel5.Enabled)
            {
                panel5.Enabled = false;
            }
            else
            {
                panel5.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (panel6.Enabled)
            {
                panel6.Enabled = false;
            }
            else
            {
                panel6.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                CreateScheduledTask();
                DisableScheduledTaskDaily();
                EnableScheduledTask();


            }
            else if (radioButton6.Checked)
            {
                CreateDailyBackupTask();
                DisableScheduledTask();
                EnableScheduledTaskDaily();
            }
            updateMain();

          


        }
        public void updateMaster()
        {
            //Selected Value
            string SelectedMode = "";
            if (radioButton1.Checked) { SelectedMode = "Monthly"; }
            else if (radioButton1.Checked) { SelectedMode = "Daily"; }

            // Retrieve values from textboxes
            string filePath = newFilePath;
            string fileName = textBox1.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(filePath) ||  string.IsNullOrEmpty(fileName))
            {
                
                return;
            }

            // Create SQL query for updating the data
            string updateQuery = @"
        UPDATE BackupInfo
        SET 
            FilePath = @FilePath,
            FileName = @FileName
        WHERE Id = 1";  // Replace @Id with an appropriate condition or parameter

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.csRestore))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters to the query to avoid SQL injection
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@FileName", fileName);

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                                panel3.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. in master Please check the conditions.", "Warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating system settings: {ex.Message}", "Error");
            }
        }
        public void updateMain()
        {
            //Selected Value
            string SelectedMode = "";
            if (radioButton1.Checked) { SelectedMode = "Monthly"; }
            else if (radioButton6.Checked) { SelectedMode = "Daily"; }
            string filePath = newFilePath;
            string backupMode = SelectedMode;
            string fileName = textBox1.Text.Trim();
            // Retrieve values from textboxes
            if (autoFilePath == null || autoFilePath == "")
            {
                filePath = oldfilepath;
            }
            else
            {
                filePath = autoFilePath;
            }
    

            // Validate inputs
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(backupMode) || string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("All fields are required to be filled in.", "Validation Error");
                return;
            }

            // Create SQL query for updating the data
            string updateQuery = @"
        UPDATE SystemSettingsTbl
        SET 
            FilePath = @FilePath,
            BackupMode = @BackupMode,
            FileName = @FileName
        WHERE id = 1";  // Replace @Id with an appropriate condition or parameter

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters to the query to avoid SQL injection
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@BackupMode", backupMode);
                        cmd.Parameters.AddWithValue("@FileName", fileName);

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            updateMaster();
                            MessageBox.Show("System settings updated successfully.", "Success");
                           
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated in main. Please check the conditions.", "Warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating system settings: {ex.Message}", "Error");
            }
        }
        string newFilePath = "";
        string autoFilePath = "";
        private void btnUpdateSettings_Click(object sender, EventArgs e)
        {   //Selected Value
            string SelectedMode= "";
            if (radioButton1.Checked) { SelectedMode = "Monthly"; }
            else if (radioButton1.Checked) { SelectedMode = "Daily"; }

            // Retrieve values from textboxes
            string filePath = newFilePath;
            string backupMode = SelectedMode;
            string fileName = textBox1.Text.Trim();
            string securityCode = textBox4.Text.Trim();
            string discount = textBox6.Text.Trim();
            string minimumOrder = textBox7.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(backupMode) || string.IsNullOrEmpty(fileName) ||
                string.IsNullOrEmpty(securityCode) || string.IsNullOrEmpty(discount) || string.IsNullOrEmpty(minimumOrder))
            {
                MessageBox.Show("All fields are required to be filled in.", "Validation Error");
                return;
            }

            // Create SQL query for updating the data
            string updateQuery = @"
        UPDATE SystemSettingsTbl
        SET 
            FilePath = @FilePath,
            BackupMode = @BackupMode,
            FileName = @FileName,
            SecurityCode = @SecurityCode,
            Discount = @Discount,
            MinimumOrder = @MinimumOrder
        WHERE 
            -- Add a condition to identify the row to update. For example, using an ID or other criteria.
            -- Assuming the table has an ID column as a unique identifier for the row.
            Id = @Id";  // Replace @Id with an appropriate condition or parameter

            try
            {
                using (SqlConnection con = new SqlConnection("your_connection_string_here"))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters to the query to avoid SQL injection
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@BackupMode", backupMode);
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.Parameters.AddWithValue("@SecurityCode", securityCode);
                        cmd.Parameters.AddWithValue("@Discount", discount);
                        cmd.Parameters.AddWithValue("@MinimumOrder", minimumOrder);

                        // Assuming there's an ID or a unique key for identifying the row (like "Id")
                        // If you don't have an ID, you need to modify this logic accordingly
                        cmd.Parameters.AddWithValue("@Id", 1);  // Use the actual Id or unique key

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("System settings updated successfully.", "Success");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Please check the conditions.", "Warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating system settings: {ex.Message}", "Error");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {   
            if(textBox3.Text == SystemInfo.SecurityCode)
            {
                if(textBox4.Text == textBox5.Text)
                {
                    string securityCode = textBox4.Text.Trim();
  

                    // Create SQL query for updating the data
                    string updateQuery = @" UPDATE SystemSettingsTbl SET SecurityCode = @SecurityCode WHERE id = '1';";  // Replace @Id with an appropriate condition or parameter

                    try
                    {
                        using (SqlConnection con = new SqlConnection(Connect.connectionString))
                        {
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                            {
                                // Add parameters to the query to avoid SQL injection
        
                                cmd.Parameters.AddWithValue("@SecurityCode", securityCode);

                                // Execute the update query
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("SecurityCode updated successfully.", "Success");
                                    SystemInfo.SecurityCode = securityCode;
                                    textBox3.Text = null;
                                    textBox4.Text = null;
                                    textBox5.Text = null;
                                    panel4.Enabled = false;
                                }
                                else
                                {
                                    MessageBox.Show("No rows were updated. Please check the conditions.", "Warning");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating system settings: {ex.Message}", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("New Security Code Mismatch");
                }

            }
            else
            {
                MessageBox.Show("Wrong Old Security Code");
            }
       
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string discount = textBox6.Text.Trim();

            // Validate inputs
            if ( string.IsNullOrEmpty(discount))
            {
                MessageBox.Show("All fields are required to be filled in.", "Validation Error");
                return;
            }

            // Create SQL query for updating the data
            string updateQuery = @"
        UPDATE SystemSettingsTbl
        SET 

            Discount = @Discount
        WHERE id=1";  // Replace @Id with an appropriate condition or parameter

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters to the query to avoid SQL injection
                        cmd.Parameters.AddWithValue("@Discount", discount);
                 

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("System settings updated successfully.", "Success");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Please check the conditions.", "Warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating system settings: {ex.Message}", "Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            string minimumOrder = textBox7.Text.Trim();

            // Validate inputs
            if ( string.IsNullOrEmpty(minimumOrder))
            {
                MessageBox.Show("All fields are required to be filled in.", "Validation Error");
                return;
            }

            // Create SQL query for updating the data
            string updateQuery = @"
        UPDATE SystemSettingsTbl
        SET 
            MinimumOrder = @MinimumOrder
        WHERE id = 1";  // Replace @Id with an appropriate condition or parameter

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters to the query to avoid SQL injection
                        cmd.Parameters.AddWithValue("@MinimumOrder", minimumOrder);

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("System settings updated successfully.", "Success");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Please check the conditions.", "Warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating system settings: {ex.Message}", "Error");
            }
        }
      
        public void retrievingfile()
        {
            // Step 1: Open the File Dialog to select the backup file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*|Backup Files (*.bak)|*.bak|SQL Files (*.sql)|*.sql|ZIP Files (*.zip)|*.zip";
            // Only show .bak files
            openFileDialog.Title = "Select Database Backup File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Step 2: Retrieve the full file path and file name
                string filePath = openFileDialog.FileName; // Full path to the selected file
                string fileName = openFileDialog.SafeFileName; // Just the file name
                //MessageBox.Show(filePath);
                ExecuteRestoreDatabaseRestoration(filePath);
            }
            else
            {
                MessageBox.Show("No file selected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void RestoreDatabase(string filePath)
        {
            // SQL queries to disconnect users, restore the database, and reset the database to multi-user mode
            string disconnectUsersQuery = @"
        ALTER DATABASE RogerAcierdaFlowerShop
        SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";

            string restoreQuery = $@"
        RESTORE DATABASE RogerAcierdaFlowerShop
        FROM DISK = @BackupFilePath
        WITH REPLACE;";

            string setMultiUserModeQuery = @"
        ALTER DATABASE RogerAcierdaFlowerShop
        SET MULTI_USER;";

            try
            {
                // Open a SQL connection and execute the queries
                using (SqlConnection con = new SqlConnection(Connect.csRestore))  // Use your connection string
                {
                    con.Open();  // Open the connection

                    // Step 1: Disconnect active users from the database (set to single-user mode)
                    using (SqlCommand cmd = new SqlCommand(disconnectUsersQuery, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Step 2: Perform the restore from the backup file
                    using (SqlCommand cmd = new SqlCommand(restoreQuery, con))
                    {
                        // Add the parameter for the backup file path
                        cmd.Parameters.AddWithValue("@BackupFilePath", filePath);

                        // Execute the restore command
                        cmd.ExecuteNonQuery();
                    }

                    // Step 3: Set the database back to multi-user mode
                    using (SqlCommand cmd = new SqlCommand(setMultiUserModeQuery, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Notify the user that the restore was successful
                    MessageBox.Show("Database restored successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();  // Close the application if necessary
                }
            }
            catch (Exception ex)
            {
                // Handle any errors during the restore process
                MessageBox.Show($"Error restoring database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            retrievingfile();
        }
        string folderPath;
        private void button11_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Set the description or initial folder if needed
                folderDialog.Description = "Select a folder";

                // Show the dialog and check if the user selected a folder
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve the selected folder path
                    autoFilePath = folderDialog.SelectedPath;
                    textBox2.Text = folderDialog.SelectedPath;

                    // Optionally, display the folder path in a label or text box
           
                }
                else
                {
                    MessageBox.Show("No folder selected.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = $"Server=192.168.8.205,2626;Initial Catalog=RogerAcierdaFlowerShop;Persist Security Info=True;User ID=sa;Password=ApplicationDb123;Encrypt=True;TrustServerCertificate=True;";
            string backupDirectory = newFilePath;
            string FileName = " ";
            if (textBox9.Text.Length > 0)
            {
                FileName = textBox9.Text;
            }
            else
            {
                FileName = "RogerAcierdaFlowershopBackup";
            }


            // List of database names to backup
            string[] databases = new string[] { "RogerAcierdaFlowerShop" };

            foreach (string databaseName in databases)
            {


                DateTime date = DateTime.Today;
                string formattedDate = date.ToString("MMM dd, yyyy");

                string backupFile = $@"{backupDirectory}\{FileName + " " + formattedDate}.bak";
                string sql = $@"
                BACKUP DATABASE [{databaseName}]
                TO DISK = '{backupFile}'
                WITH FORMAT,
                     MEDIANAME = 'DbBackup',
                     NAME = '{databaseName} Full Backup';
            ";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show($"Backup completed for {databaseName}.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while backing up {databaseName}: {ex.Message}");
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                // Get all network interfaces on the machine
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                // Look for a wireless LAN (Wi-Fi) network interface
                foreach (var networkInterface in networkInterfaces)
                {
                    // Check if the network interface is up (enabled) and is a Wi-Fi adapter
                    if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                        networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    {
                        // Get the IP properties of the Wi-Fi network interface
                        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                        // Look for an IPv4 address in the unicast addresses
                        foreach (var unicastAddress in ipProperties.UnicastAddresses)
                        {
                            if (unicastAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                // Output the IPv4 address of the wireless LAN adapter
                                
                                if(unicastAddress.Address.ToString() == "192.168.8.205")
                                {
                                    MessageBox.Show("Gumagana boss");
                                }
                              
                                return; // Exit the method after printing the first IPv4 address
                            }
                        }
                    }
                }

                Console.WriteLine("No active Wi-Fi network interface found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Set the description or initial folder if needed
                folderDialog.Description = "Select a folder";

                // Show the dialog and check if the user selected a folder
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve the selected folder path
                    newFilePath = folderDialog.SelectedPath;
                    textBox8.Text = folderDialog.SelectedPath;
                    button1.Enabled = true;
                    // Optionally, display the folder path in a label or text box

                }
                else
                {
                    MessageBox.Show("No folder selected.");
                }
            }
        }
        static void ExecuteRestoreDatabaseRestoration( string backupFilePath)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=192.168.8.205,2626;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=ApplicationDb123;Encrypt=True;TrustServerCertificate=True;"))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("[dbo].[RestoreDB]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the @FilePath parameter to the command
                        command.Parameters.Add(new SqlParameter("@FilePath", SqlDbType.VarChar, 100)).Value = backupFilePath;

                        // Execute the command (could also use ExecuteNonQuery for no results expected)
                        command.ExecuteNonQuery();

                        MessageBox.Show("Database restore started successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may have occurred
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
