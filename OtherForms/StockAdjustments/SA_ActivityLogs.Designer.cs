namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    partial class SA_ActivityLogs
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.DescLbl = new System.Windows.Forms.Label();
            this.DateLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel4.Controls.Add(this.DescLbl);
            this.panel4.Controls.Add(this.DateLbl);
            this.panel4.Controls.Add(this.NameLbl);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(322, 95);
            this.panel4.TabIndex = 29;
            // 
            // DescLbl
            // 
            this.DescLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescLbl.Location = new System.Drawing.Point(7, 37);
            this.DescLbl.Name = "DescLbl";
            this.DescLbl.Size = new System.Drawing.Size(312, 54);
            this.DescLbl.TabIndex = 26;
            this.DescLbl.Text = "\"Adjusted 70 Tulip from the inventory\"\r\n";
            this.DescLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateLbl
            // 
            this.DateLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLbl.Location = new System.Drawing.Point(222, 10);
            this.DateLbl.Name = "DateLbl";
            this.DateLbl.Size = new System.Drawing.Size(90, 18);
            this.DateLbl.TabIndex = 25;
            this.DateLbl.Text = "Nov 1, 2024";
            this.DateLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLbl.Location = new System.Drawing.Point(7, 10);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(48, 18);
            this.NameLbl.TabIndex = 24;
            this.NameLbl.Text = "Enzoy";
            // 
            // SA_ActivityLogs
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel4);
            this.Name = "SA_ActivityLogs";
            this.Size = new System.Drawing.Size(329, 101);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label DescLbl;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Label NameLbl;
    }
}
