namespace Flowershop_Thesis.OtherForms.Abuel
{
    partial class restockinglist
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.restockBtnLabel = new System.Windows.Forms.Label();
            this.supplierLabel = new System.Windows.Forms.Label();
            this.stockLevelLabel = new System.Windows.Forms.Label();
            this.itemQuantityLabel = new System.Windows.Forms.Label();
            this.itemNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // restockBtnLabel
            // 
            this.restockBtnLabel.AutoSize = true;
            this.restockBtnLabel.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restockBtnLabel.Location = new System.Drawing.Point(610, 9);
            this.restockBtnLabel.Name = "restockBtnLabel";
            this.restockBtnLabel.Size = new System.Drawing.Size(68, 18);
            this.restockBtnLabel.TabIndex = 9;
            this.restockBtnLabel.Text = "Re-stock";
            // 
            // supplierLabel
            // 
            this.supplierLabel.AutoSize = true;
            this.supplierLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supplierLabel.Location = new System.Drawing.Point(455, 9);
            this.supplierLabel.Name = "supplierLabel";
            this.supplierLabel.Size = new System.Drawing.Size(51, 18);
            this.supplierLabel.TabIndex = 8;
            this.supplierLabel.Text = "Hnezo";
            // 
            // stockLevelLabel
            // 
            this.stockLevelLabel.AutoSize = true;
            this.stockLevelLabel.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockLevelLabel.ForeColor = System.Drawing.Color.Red;
            this.stockLevelLabel.Location = new System.Drawing.Point(305, 9);
            this.stockLevelLabel.Name = "stockLevelLabel";
            this.stockLevelLabel.Size = new System.Drawing.Size(78, 18);
            this.stockLevelLabel.TabIndex = 7;
            this.stockLevelLabel.Text = "Low Stock";
            // 
            // itemQuantityLabel
            // 
            this.itemQuantityLabel.AutoSize = true;
            this.itemQuantityLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemQuantityLabel.Location = new System.Drawing.Point(192, 9);
            this.itemQuantityLabel.Name = "itemQuantityLabel";
            this.itemQuantityLabel.Size = new System.Drawing.Size(20, 18);
            this.itemQuantityLabel.TabIndex = 6;
            this.itemQuantityLabel.Text = "12";
            // 
            // itemNameLabel
            // 
            this.itemNameLabel.AutoSize = true;
            this.itemNameLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemNameLabel.Location = new System.Drawing.Point(7, 9);
            this.itemNameLabel.Name = "itemNameLabel";
            this.itemNameLabel.Size = new System.Drawing.Size(39, 18);
            this.itemNameLabel.TabIndex = 5;
            this.itemNameLabel.Text = "Rose";
            // 
            // restockinglist
            // 
            this.ClientSize = new System.Drawing.Size(686, 37);
            this.Controls.Add(this.restockBtnLabel);
            this.Controls.Add(this.supplierLabel);
            this.Controls.Add(this.stockLevelLabel);
            this.Controls.Add(this.itemQuantityLabel);
            this.Controls.Add(this.itemNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "restockinglist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label restockBtn;
        private System.Windows.Forms.Label restockBtnLabel;
        private System.Windows.Forms.Label supplierLabel;
        private System.Windows.Forms.Label stockLevelLabel;
        private System.Windows.Forms.Label itemQuantityLabel;
        private System.Windows.Forms.Label itemNameLabel;
    }
}