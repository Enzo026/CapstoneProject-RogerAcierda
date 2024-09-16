namespace Flowershop_Thesis.OtherForms.Abuel
{
    partial class RestockList
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
            this.restockBtnLabel = new System.Windows.Forms.Label();
            this.supplierLabel = new System.Windows.Forms.Label();
            this.itemQuantityLabel = new System.Windows.Forms.Label();
            this.itemNameLabel = new System.Windows.Forms.Label();
            this.stockLevelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // restockBtnLabel
            // 
            this.restockBtnLabel.AutoSize = true;
            this.restockBtnLabel.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restockBtnLabel.Location = new System.Drawing.Point(613, 7);
            this.restockBtnLabel.Name = "restockBtnLabel";
            this.restockBtnLabel.Size = new System.Drawing.Size(68, 18);
            this.restockBtnLabel.TabIndex = 14;
            this.restockBtnLabel.Text = "Re-stock";
            this.restockBtnLabel.Click += new System.EventHandler(this.restockBtnLabel_Click);
            // 
            // supplierLabel
            // 
            this.supplierLabel.AutoSize = true;
            this.supplierLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supplierLabel.Location = new System.Drawing.Point(458, 7);
            this.supplierLabel.Name = "supplierLabel";
            this.supplierLabel.Size = new System.Drawing.Size(51, 18);
            this.supplierLabel.TabIndex = 13;
            this.supplierLabel.Text = "Hnezo";
            // 
            // itemQuantityLabel
            // 
            this.itemQuantityLabel.AutoSize = true;
            this.itemQuantityLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemQuantityLabel.Location = new System.Drawing.Point(195, 7);
            this.itemQuantityLabel.Name = "itemQuantityLabel";
            this.itemQuantityLabel.Size = new System.Drawing.Size(20, 18);
            this.itemQuantityLabel.TabIndex = 11;
            this.itemQuantityLabel.Text = "12";
            // 
            // itemNameLabel
            // 
            this.itemNameLabel.AutoSize = true;
            this.itemNameLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemNameLabel.Location = new System.Drawing.Point(10, 7);
            this.itemNameLabel.Name = "itemNameLabel";
            this.itemNameLabel.Size = new System.Drawing.Size(39, 18);
            this.itemNameLabel.TabIndex = 10;
            this.itemNameLabel.Text = "Rose";
            // 
            // stockLevelLabel
            // 
            this.stockLevelLabel.AutoSize = true;
            this.stockLevelLabel.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockLevelLabel.Location = new System.Drawing.Point(322, 6);
            this.stockLevelLabel.Name = "stockLevelLabel";
            this.stockLevelLabel.Size = new System.Drawing.Size(52, 18);
            this.stockLevelLabel.TabIndex = 15;
            this.stockLevelLabel.Text = "Hnezo";
            // 
            // RestockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stockLevelLabel);
            this.Controls.Add(this.restockBtnLabel);
            this.Controls.Add(this.supplierLabel);
            this.Controls.Add(this.itemQuantityLabel);
            this.Controls.Add(this.itemNameLabel);
            this.Name = "RestockList";
            this.Size = new System.Drawing.Size(694, 36);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label restockBtnLabel;
        private System.Windows.Forms.Label supplierLabel;
        private System.Windows.Forms.Label itemQuantityLabel;
        private System.Windows.Forms.Label itemNameLabel;
        private System.Windows.Forms.Label stockLevelLabel;
    }
}
