namespace Flowershop_Thesis.OtherForms.AdvanceOrder
{
    partial class AdvanceOrderListContents
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
            this.panel18 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.OrderTypeLbl = new System.Windows.Forms.Label();
            this.PickupDateLbl = new System.Windows.Forms.Label();
            this.TotalAmountLbl = new System.Windows.Forms.Label();
            this.CustomerNameLbl = new System.Windows.Forms.Label();
            this.panel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel18.Controls.Add(this.button1);
            this.panel18.Controls.Add(this.OrderTypeLbl);
            this.panel18.Controls.Add(this.PickupDateLbl);
            this.panel18.Controls.Add(this.TotalAmountLbl);
            this.panel18.Controls.Add(this.CustomerNameLbl);
            this.panel18.Location = new System.Drawing.Point(3, 1);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(592, 29);
            this.panel18.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(514, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OrderTypeLbl
            // 
            this.OrderTypeLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderTypeLbl.Location = new System.Drawing.Point(437, 3);
            this.OrderTypeLbl.Name = "OrderTypeLbl";
            this.OrderTypeLbl.Size = new System.Drawing.Size(76, 26);
            this.OrderTypeLbl.TabIndex = 13;
            this.OrderTypeLbl.Text = "Type";
            this.OrderTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PickupDateLbl
            // 
            this.PickupDateLbl.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.PickupDateLbl.Font = new System.Drawing.Font("Montserrat SemiBold", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickupDateLbl.Location = new System.Drawing.Point(301, 3);
            this.PickupDateLbl.Name = "PickupDateLbl";
            this.PickupDateLbl.Size = new System.Drawing.Size(112, 26);
            this.PickupDateLbl.TabIndex = 12;
            this.PickupDateLbl.Text = "Pick Up Date";
            this.PickupDateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TotalAmountLbl
            // 
            this.TotalAmountLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalAmountLbl.Location = new System.Drawing.Point(186, 3);
            this.TotalAmountLbl.Name = "TotalAmountLbl";
            this.TotalAmountLbl.Size = new System.Drawing.Size(109, 26);
            this.TotalAmountLbl.TabIndex = 11;
            this.TotalAmountLbl.Text = "Total Amount";
            this.TotalAmountLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomerNameLbl
            // 
            this.CustomerNameLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerNameLbl.Location = new System.Drawing.Point(3, 3);
            this.CustomerNameLbl.Name = "CustomerNameLbl";
            this.CustomerNameLbl.Size = new System.Drawing.Size(177, 26);
            this.CustomerNameLbl.TabIndex = 10;
            this.CustomerNameLbl.Text = "Customer Name";
            this.CustomerNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AdvanceOrderListContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel18);
            this.Name = "AdvanceOrderListContents";
            this.Size = new System.Drawing.Size(598, 30);
            this.Load += new System.EventHandler(this.AdvanceOrderListContents_Load);
            this.panel18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label OrderTypeLbl;
        private System.Windows.Forms.Label PickupDateLbl;
        private System.Windows.Forms.Label TotalAmountLbl;
        private System.Windows.Forms.Label CustomerNameLbl;
        private System.Windows.Forms.Button button1;
    }
}
