namespace Flowershop_Thesis.OtherForms.Restocking
{
    partial class BatchInfoList
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
            this.ExpLbl = new System.Windows.Forms.Label();
            this.SuppLbl = new System.Windows.Forms.Label();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ExpLbl);
            this.panel1.Controls.Add(this.SuppLbl);
            this.panel1.Controls.Add(this.QtyLbl);
            this.panel1.Controls.Add(this.NameLbl);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 34);
            this.panel1.TabIndex = 1;
            // 
            // ExpLbl
            // 
            this.ExpLbl.AutoSize = true;
            this.ExpLbl.Location = new System.Drawing.Point(511, 10);
            this.ExpLbl.Name = "ExpLbl";
            this.ExpLbl.Size = new System.Drawing.Size(49, 13);
            this.ExpLbl.TabIndex = 13;
            this.ExpLbl.Text = "Exp date";
            // 
            // SuppLbl
            // 
            this.SuppLbl.AutoSize = true;
            this.SuppLbl.Location = new System.Drawing.Point(362, 10);
            this.SuppLbl.Name = "SuppLbl";
            this.SuppLbl.Size = new System.Drawing.Size(32, 13);
            this.SuppLbl.TabIndex = 12;
            this.SuppLbl.Text = "Supp";
            // 
            // QtyLbl
            // 
            this.QtyLbl.AutoSize = true;
            this.QtyLbl.Location = new System.Drawing.Point(216, 10);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(23, 13);
            this.QtyLbl.TabIndex = 11;
            this.QtyLbl.Text = "Qty";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(21, 10);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(58, 13);
            this.NameLbl.TabIndex = 10;
            this.NameLbl.Text = "Item Name";
            // 
            // BatchInfoList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "BatchInfoList";
            this.Size = new System.Drawing.Size(620, 40);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ExpLbl;
        private System.Windows.Forms.Label SuppLbl;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label NameLbl;
    }
}
