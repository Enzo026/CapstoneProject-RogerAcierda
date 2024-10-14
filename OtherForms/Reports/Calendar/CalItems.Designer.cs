namespace Flowershop_Thesis.OtherForms.Reports.Calendar
{
    partial class CalItems
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
            this.panel22 = new System.Windows.Forms.Panel();
            this.button24 = new System.Windows.Forms.Button();
            this.PriceLbl = new System.Windows.Forms.Label();
            this.CustNameLbl = new System.Windows.Forms.Label();
            this.panel22.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel22.Controls.Add(this.button24);
            this.panel22.Controls.Add(this.PriceLbl);
            this.panel22.Controls.Add(this.CustNameLbl);
            this.panel22.Location = new System.Drawing.Point(3, 3);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(216, 59);
            this.panel22.TabIndex = 1;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.Snow;
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button24.Location = new System.Drawing.Point(6, 29);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(93, 23);
            this.button24.TabIndex = 25;
            this.button24.Text = "See Items...";
            this.button24.UseVisualStyleBackColor = false;
            // 
            // PriceLbl
            // 
            this.PriceLbl.AutoSize = true;
            this.PriceLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLbl.Location = new System.Drawing.Point(141, 33);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(72, 18);
            this.PriceLbl.TabIndex = 24;
            this.PriceLbl.Text = "20000.00";
            // 
            // CustNameLbl
            // 
            this.CustNameLbl.AutoSize = true;
            this.CustNameLbl.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustNameLbl.Location = new System.Drawing.Point(83, 7);
            this.CustNameLbl.Name = "CustNameLbl";
            this.CustNameLbl.Size = new System.Drawing.Size(63, 18);
            this.CustNameLbl.TabIndex = 23;
            this.CustNameLbl.Text = "P-Diddy";
            // 
            // CalItems
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel22);
            this.Name = "CalItems";
            this.Size = new System.Drawing.Size(221, 64);
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label CustNameLbl;
    }
}
