namespace Flowershop_Thesis.OtherForms.Reports.Calendar
{
    partial class CalDays
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lineLbl = new System.Windows.Forms.Panel();
            this.DayLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lineLbl);
            this.panel1.Controls.Add(this.DayLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(54, 41);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lineLbl
            // 
            this.lineLbl.BackColor = System.Drawing.Color.Gray;
            this.lineLbl.Location = new System.Drawing.Point(7, 30);
            this.lineLbl.Name = "lineLbl";
            this.lineLbl.Size = new System.Drawing.Size(40, 5);
            this.lineLbl.TabIndex = 22;
            // 
            // DayLbl
            // 
            this.DayLbl.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayLbl.Location = new System.Drawing.Point(3, 0);
            this.DayLbl.Name = "DayLbl";
            this.DayLbl.Size = new System.Drawing.Size(48, 41);
            this.DayLbl.TabIndex = 21;
            this.DayLbl.Text = "55";
            this.DayLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DayLbl.Click += new System.EventHandler(this.DayLbl_Click);
            // 
            // CalDays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "CalDays";
            this.Size = new System.Drawing.Size(54, 41);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel lineLbl;
        private System.Windows.Forms.Label DayLbl;
    }
}
