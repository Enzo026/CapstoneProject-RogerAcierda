namespace Flowershop_Thesis.OtherForms.Reports
{
    partial class OIF_List
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
            this.ItemNameLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.PriceLbl);
            this.panel1.Controls.Add(this.QtyLbl);
            this.panel1.Controls.Add(this.ItemNameLbl);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 26);
            this.panel1.TabIndex = 1;
            // 
            // PriceLbl
            // 
            this.PriceLbl.AutoSize = true;
            this.PriceLbl.Location = new System.Drawing.Point(200, 6);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(31, 13);
            this.PriceLbl.TabIndex = 14;
            this.PriceLbl.Text = "1200";
            // 
            // QtyLbl
            // 
            this.QtyLbl.AutoSize = true;
            this.QtyLbl.Location = new System.Drawing.Point(146, 6);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(19, 13);
            this.QtyLbl.TabIndex = 13;
            this.QtyLbl.Text = "12";
            // 
            // ItemNameLbl
            // 
            this.ItemNameLbl.AutoSize = true;
            this.ItemNameLbl.Location = new System.Drawing.Point(13, 6);
            this.ItemNameLbl.Name = "ItemNameLbl";
            this.ItemNameLbl.Size = new System.Drawing.Size(32, 13);
            this.ItemNameLbl.TabIndex = 12;
            this.ItemNameLbl.Text = "Rose";
            // 
            // OIF_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "OIF_List";
            this.Size = new System.Drawing.Size(267, 32);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label ItemNameLbl;
    }
}
