namespace Flowershop_Thesis.OtherForms
{
    partial class PriceListContents
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
            this.DetailsBtn = new System.Windows.Forms.Label();
            this.QuantityLbl = new System.Windows.Forms.Label();
            this.PriceLbl = new System.Windows.Forms.Label();
            this.TypeLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.IdLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DetailsBtn);
            this.panel1.Controls.Add(this.QuantityLbl);
            this.panel1.Controls.Add(this.PriceLbl);
            this.panel1.Controls.Add(this.TypeLbl);
            this.panel1.Controls.Add(this.NameLbl);
            this.panel1.Controls.Add(this.IdLbl);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 30);
            this.panel1.TabIndex = 0;
            // 
            // DetailsBtn
            // 
            this.DetailsBtn.AutoSize = true;
            this.DetailsBtn.Font = new System.Drawing.Font("Montserrat SemiBold", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsBtn.Location = new System.Drawing.Point(553, 7);
            this.DetailsBtn.Name = "DetailsBtn";
            this.DetailsBtn.Size = new System.Drawing.Size(78, 15);
            this.DetailsBtn.TabIndex = 11;
            this.DetailsBtn.Text = "View Details";
            this.DetailsBtn.Click += new System.EventHandler(this.DetailsBtn_Click);
            // 
            // QuantityLbl
            // 
            this.QuantityLbl.AutoSize = true;
            this.QuantityLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityLbl.Location = new System.Drawing.Point(474, 8);
            this.QuantityLbl.Name = "QuantityLbl";
            this.QuantityLbl.Size = new System.Drawing.Size(25, 15);
            this.QuantityLbl.TabIndex = 10;
            this.QuantityLbl.Text = "100";
            // 
            // PriceLbl
            // 
            this.PriceLbl.AutoSize = true;
            this.PriceLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLbl.Location = new System.Drawing.Point(367, 8);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(18, 15);
            this.PriceLbl.TabIndex = 9;
            this.PriceLbl.Text = "10";
            // 
            // TypeLbl
            // 
            this.TypeLbl.AutoSize = true;
            this.TypeLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeLbl.Location = new System.Drawing.Point(244, 8);
            this.TypeLbl.Name = "TypeLbl";
            this.TypeLbl.Size = new System.Drawing.Size(59, 15);
            this.TypeLbl.TabIndex = 8;
            this.TypeLbl.Text = "Individual";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLbl.Location = new System.Drawing.Point(127, 7);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(34, 15);
            this.NameLbl.TabIndex = 7;
            this.NameLbl.Text = "Rose";
            // 
            // IdLbl
            // 
            this.IdLbl.AutoSize = true;
            this.IdLbl.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdLbl.Location = new System.Drawing.Point(15, 7);
            this.IdLbl.Name = "IdLbl";
            this.IdLbl.Size = new System.Drawing.Size(18, 15);
            this.IdLbl.TabIndex = 6;
            this.IdLbl.Text = "01";
            // 
            // PriceListContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PriceListContents";
            this.Size = new System.Drawing.Size(657, 39);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label DetailsBtn;
        private System.Windows.Forms.Label QuantityLbl;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label TypeLbl;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Label IdLbl;
    }
}
