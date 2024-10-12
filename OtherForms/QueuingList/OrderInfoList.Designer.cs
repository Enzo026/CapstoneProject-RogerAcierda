namespace Flowershop_Thesis.OtherForms.QueuingList
{
    partial class OrderInfoList
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
            this.PriceLbl = new System.Windows.Forms.Label();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.IDLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PriceLbl);
            this.panel1.Controls.Add(this.QtyLbl);
            this.panel1.Controls.Add(this.IDLbl);
            this.panel1.Controls.Add(this.NameLbl);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 35);
            this.panel1.TabIndex = 1;
            // 
            // PriceLbl
            // 
            this.PriceLbl.AutoSize = true;
            this.PriceLbl.Location = new System.Drawing.Point(445, 11);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(31, 13);
            this.PriceLbl.TabIndex = 13;
            this.PriceLbl.Text = "1200";
            // 
            // QtyLbl
            // 
            this.QtyLbl.AutoSize = true;
            this.QtyLbl.Location = new System.Drawing.Point(276, 11);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(19, 13);
            this.QtyLbl.TabIndex = 12;
            this.QtyLbl.Text = "12";
            // 
            // IDLbl
            // 
            this.IDLbl.AutoSize = true;
            this.IDLbl.Location = new System.Drawing.Point(12, 11);
            this.IDLbl.Name = "IDLbl";
            this.IDLbl.Size = new System.Drawing.Size(25, 13);
            this.IDLbl.TabIndex = 11;
            this.IDLbl.Text = "001";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(95, 11);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(32, 13);
            this.NameLbl.TabIndex = 10;
            this.NameLbl.Text = "Rose";
            // 
            // OrderInfoList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "OrderInfoList";
            this.Size = new System.Drawing.Size(548, 42);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label IDLbl;
        private System.Windows.Forms.Label NameLbl;
    }
}
