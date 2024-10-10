namespace Flowershop_Thesis.OtherForms.Reports
{
    partial class TransactionsList
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
            this.panel13 = new System.Windows.Forms.Panel();
            this.TypeLbl = new System.Windows.Forms.Label();
            this.EmpLbl = new System.Windows.Forms.Label();
            this.DateLbl = new System.Windows.Forms.Label();
            this.DetailsBtn = new System.Windows.Forms.Button();
            this.PriceLbl = new System.Windows.Forms.Label();
            this.CustNameLbl = new System.Windows.Forms.Label();
            this.IDLbl = new System.Windows.Forms.Label();
            this.panel13.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel13.Controls.Add(this.TypeLbl);
            this.panel13.Controls.Add(this.EmpLbl);
            this.panel13.Controls.Add(this.DateLbl);
            this.panel13.Controls.Add(this.DetailsBtn);
            this.panel13.Controls.Add(this.PriceLbl);
            this.panel13.Controls.Add(this.CustNameLbl);
            this.panel13.Controls.Add(this.IDLbl);
            this.panel13.Location = new System.Drawing.Point(3, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1039, 31);
            this.panel13.TabIndex = 1;
            // 
            // TypeLbl
            // 
            this.TypeLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeLbl.Location = new System.Drawing.Point(480, 7);
            this.TypeLbl.Name = "TypeLbl";
            this.TypeLbl.Size = new System.Drawing.Size(154, 15);
            this.TypeLbl.TabIndex = 28;
            this.TypeLbl.Text = "Advance Order";
            this.TypeLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EmpLbl
            // 
            this.EmpLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpLbl.Location = new System.Drawing.Point(801, 7);
            this.EmpLbl.Name = "EmpLbl";
            this.EmpLbl.Size = new System.Drawing.Size(109, 15);
            this.EmpLbl.TabIndex = 27;
            this.EmpLbl.Text = "Benjo";
            this.EmpLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DateLbl
            // 
            this.DateLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLbl.Location = new System.Drawing.Point(640, 7);
            this.DateLbl.Name = "DateLbl";
            this.DateLbl.Size = new System.Drawing.Size(155, 15);
            this.DateLbl.TabIndex = 26;
            this.DateLbl.Text = "11/07/24 | 12:34 pm";
            this.DateLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DetailsBtn
            // 
            this.DetailsBtn.BackColor = System.Drawing.Color.LightCoral;
            this.DetailsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DetailsBtn.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsBtn.Location = new System.Drawing.Point(916, 4);
            this.DetailsBtn.Name = "DetailsBtn";
            this.DetailsBtn.Size = new System.Drawing.Size(116, 23);
            this.DetailsBtn.TabIndex = 25;
            this.DetailsBtn.Text = "View full details";
            this.DetailsBtn.UseVisualStyleBackColor = false;
            this.DetailsBtn.Click += new System.EventHandler(this.DetailsBtn_Click);
            // 
            // PriceLbl
            // 
            this.PriceLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLbl.Location = new System.Drawing.Point(326, 7);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(157, 15);
            this.PriceLbl.TabIndex = 24;
            this.PriceLbl.Text = "1000.00 Php";
            this.PriceLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CustNameLbl
            // 
            this.CustNameLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustNameLbl.Location = new System.Drawing.Point(44, 7);
            this.CustNameLbl.Name = "CustNameLbl";
            this.CustNameLbl.Size = new System.Drawing.Size(276, 15);
            this.CustNameLbl.TabIndex = 23;
            this.CustNameLbl.Text = "Customize Bouquete";
            this.CustNameLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IDLbl
            // 
            this.IDLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLbl.Location = new System.Drawing.Point(4, 7);
            this.IDLbl.Name = "IDLbl";
            this.IDLbl.Size = new System.Drawing.Size(34, 15);
            this.IDLbl.TabIndex = 22;
            this.IDLbl.Text = "01";
            // 
            // TransactionsList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel13);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TransactionsList";
            this.Size = new System.Drawing.Size(1046, 37);
            this.panel13.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label TypeLbl;
        private System.Windows.Forms.Label EmpLbl;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Button DetailsBtn;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label CustNameLbl;
        private System.Windows.Forms.Label IDLbl;
    }
}
