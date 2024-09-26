namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    partial class ProductMaintenanceListItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductMaintenanceListItem));
            this.panel4 = new System.Windows.Forms.Panel();
            this.SeemoreBtn = new System.Windows.Forms.Label();
            this.Price = new System.Windows.Forms.Label();
            this.Qty = new System.Windows.Forms.Label();
            this.Name = new System.Windows.Forms.Label();
            this.Img = new System.Windows.Forms.PictureBox();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.SeemoreBtn);
            this.panel4.Controls.Add(this.Price);
            this.panel4.Controls.Add(this.Qty);
            this.panel4.Controls.Add(this.Name);
            this.panel4.Controls.Add(this.Img);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(195, 272);
            this.panel4.TabIndex = 9;
            // 
            // SeemoreBtn
            // 
            this.SeemoreBtn.AutoSize = true;
            this.SeemoreBtn.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeemoreBtn.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.SeemoreBtn.Location = new System.Drawing.Point(45, 239);
            this.SeemoreBtn.Name = "SeemoreBtn";
            this.SeemoreBtn.Size = new System.Drawing.Size(102, 22);
            this.SeemoreBtn.TabIndex = 7;
            this.SeemoreBtn.Text = "See More....";
            this.SeemoreBtn.Click += new System.EventHandler(this.SeemoreBtn_Click);
            // 
            // Price
            // 
            this.Price.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Price.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.Price.Location = new System.Drawing.Point(3, 192);
            this.Price.Name = "Price";
            this.Price.Size = new System.Drawing.Size(189, 22);
            this.Price.TabIndex = 6;
            this.Price.Text = "50 php";
            this.Price.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Qty
            // 
            this.Qty.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Qty.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.Qty.Location = new System.Drawing.Point(22, 217);
            this.Qty.Name = "Qty";
            this.Qty.Size = new System.Drawing.Size(142, 22);
            this.Qty.TabIndex = 3;
            this.Qty.Text = "100";
            this.Qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Name
            // 
            this.Name.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.Name.Location = new System.Drawing.Point(3, 18);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(189, 22);
            this.Name.TabIndex = 1;
            this.Name.Text = "ProductMaintenanceListItem";
            this.Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Img
            // 
            this.Img.Image = ((System.Drawing.Image)(resources.GetObject("Img.Image")));
            this.Img.Location = new System.Drawing.Point(22, 53);
            this.Img.Name = "Img";
            this.Img.Size = new System.Drawing.Size(142, 136);
            this.Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Img.TabIndex = 0;
            this.Img.TabStop = false;
            // 
            // ProductMaintenanceListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.panel4);
       //     this.Name = "ProductMaintenanceListItem";
            this.Size = new System.Drawing.Size(202, 278);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label SeemoreBtn;
        private System.Windows.Forms.Label Price;
        private System.Windows.Forms.Label Qty;
        private System.Windows.Forms.Label Name;
        private System.Windows.Forms.PictureBox Img;
    }
}
