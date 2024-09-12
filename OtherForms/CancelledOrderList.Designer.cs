namespace Flowershop_Thesis.OtherForms
{
    partial class CancelledOrderList
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
            this.panel21 = new System.Windows.Forms.Panel();
            this.PriceLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.IdLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel21.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.label1);
            this.panel21.Controls.Add(this.PriceLbl);
            this.panel21.Controls.Add(this.NameLbl);
            this.panel21.Controls.Add(this.IdLbl);
            this.panel21.Location = new System.Drawing.Point(3, 3);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(259, 31);
            this.panel21.TabIndex = 25;
            // 
            // PriceLbl
            // 
            this.PriceLbl.AutoSize = true;
            this.PriceLbl.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLbl.Location = new System.Drawing.Point(117, 8);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(50, 15);
            this.PriceLbl.TabIndex = 2;
            this.PriceLbl.Text = "2000.00";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLbl.Location = new System.Drawing.Point(53, 8);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(39, 15);
            this.NameLbl.TabIndex = 1;
            this.NameLbl.Text = "alixes";
            // 
            // IdLbl
            // 
            this.IdLbl.AutoSize = true;
            this.IdLbl.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdLbl.Location = new System.Drawing.Point(5, 8);
            this.IdLbl.Name = "IdLbl";
            this.IdLbl.Size = new System.Drawing.Size(20, 15);
            this.IdLbl.TabIndex = 0;
            this.IdLbl.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat ExtraBold", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.IndianRed;
            this.label1.Location = new System.Drawing.Point(192, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Revert";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // CancelledOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel21);
            this.Name = "CancelledOrderList";
            this.Size = new System.Drawing.Size(265, 38);
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Label IdLbl;
        private System.Windows.Forms.Label label1;
    }
}
