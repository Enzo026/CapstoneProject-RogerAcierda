namespace Flowershop_Thesis.OtherForms.Reports
{
    partial class SalesTransactionListItems
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
            this.TypeLbl = new System.Windows.Forms.Label();
            this.EmpLbl = new System.Windows.Forms.Label();
            this.DateLbl = new System.Windows.Forms.Label();
            this.DetailsBtn = new System.Windows.Forms.Button();
            this.PriceLbl = new System.Windows.Forms.Label();
            this.CustNameLbl = new System.Windows.Forms.Label();
            this.IDLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TypeLbl
            // 
            this.TypeLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeLbl.Location = new System.Drawing.Point(481, 7);
            this.TypeLbl.Name = "TypeLbl";
            this.TypeLbl.Size = new System.Drawing.Size(154, 15);
            this.TypeLbl.TabIndex = 35;
            this.TypeLbl.Text = "Advance Order";
            this.TypeLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EmpLbl
            // 
            this.EmpLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpLbl.Location = new System.Drawing.Point(802, 7);
            this.EmpLbl.Name = "EmpLbl";
            this.EmpLbl.Size = new System.Drawing.Size(109, 15);
            this.EmpLbl.TabIndex = 34;
            this.EmpLbl.Text = "Benjo";
            this.EmpLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DateLbl
            // 
            this.DateLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLbl.Location = new System.Drawing.Point(641, 7);
            this.DateLbl.Name = "DateLbl";
            this.DateLbl.Size = new System.Drawing.Size(155, 15);
            this.DateLbl.TabIndex = 33;
            this.DateLbl.Text = "11/07/24 | 12:34 pm";
            this.DateLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DetailsBtn
            // 
            this.DetailsBtn.BackColor = System.Drawing.Color.LightCoral;
            this.DetailsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DetailsBtn.Font = new System.Drawing.Font("Montserrat Medium", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsBtn.Location = new System.Drawing.Point(911, 3);
            this.DetailsBtn.Name = "DetailsBtn";
            this.DetailsBtn.Size = new System.Drawing.Size(116, 23);
            this.DetailsBtn.TabIndex = 32;
            this.DetailsBtn.Text = "View full details";
            this.DetailsBtn.UseVisualStyleBackColor = false;
            // 
            // PriceLbl
            // 
            this.PriceLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLbl.Location = new System.Drawing.Point(327, 7);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(157, 15);
            this.PriceLbl.TabIndex = 31;
            this.PriceLbl.Text = "1000.00 Php";
            this.PriceLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CustNameLbl
            // 
            this.CustNameLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustNameLbl.Location = new System.Drawing.Point(45, 7);
            this.CustNameLbl.Name = "CustNameLbl";
            this.CustNameLbl.Size = new System.Drawing.Size(276, 15);
            this.CustNameLbl.TabIndex = 30;
            this.CustNameLbl.Text = "Customize Bouquete";
            this.CustNameLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IDLbl
            // 
            this.IDLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLbl.Location = new System.Drawing.Point(5, 7);
            this.IDLbl.Name = "IDLbl";
            this.IDLbl.Size = new System.Drawing.Size(34, 15);
            this.IDLbl.TabIndex = 29;
            this.IDLbl.Text = "01";
            // 
            // SalesTransactionListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.TypeLbl);
            this.Controls.Add(this.EmpLbl);
            this.Controls.Add(this.DateLbl);
            this.Controls.Add(this.DetailsBtn);
            this.Controls.Add(this.PriceLbl);
            this.Controls.Add(this.CustNameLbl);
            this.Controls.Add(this.IDLbl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SalesTransactionListItems";
            this.Size = new System.Drawing.Size(1030, 31);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TypeLbl;
        private System.Windows.Forms.Label EmpLbl;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Button DetailsBtn;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label CustNameLbl;
        private System.Windows.Forms.Label IDLbl;
    }
}
