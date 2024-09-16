namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    partial class StockAdjustmentListItems
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
            this.panel24 = new System.Windows.Forms.Panel();
            this.AdjustBtn = new System.Windows.Forms.Button();
            this.ItmQtyLbl = new System.Windows.Forms.Label();
            this.ItmNameLbl = new System.Windows.Forms.Label();
            this.ItmIDLbl = new System.Windows.Forms.Label();
            this.panel24.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel24.Controls.Add(this.AdjustBtn);
            this.panel24.Controls.Add(this.ItmQtyLbl);
            this.panel24.Controls.Add(this.ItmNameLbl);
            this.panel24.Controls.Add(this.ItmIDLbl);
            this.panel24.Location = new System.Drawing.Point(2, 5);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(670, 38);
            this.panel24.TabIndex = 19;
            // 
            // AdjustBtn
            // 
            this.AdjustBtn.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdjustBtn.Location = new System.Drawing.Point(539, 6);
            this.AdjustBtn.Name = "AdjustBtn";
            this.AdjustBtn.Size = new System.Drawing.Size(122, 27);
            this.AdjustBtn.TabIndex = 3;
            this.AdjustBtn.Text = "Adjust Quantity";
            this.AdjustBtn.UseVisualStyleBackColor = true;
            this.AdjustBtn.Click += new System.EventHandler(this.AdjustBtn_Click);
            // 
            // ItmQtyLbl
            // 
            this.ItmQtyLbl.AutoSize = true;
            this.ItmQtyLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItmQtyLbl.Location = new System.Drawing.Point(357, 10);
            this.ItmQtyLbl.Name = "ItmQtyLbl";
            this.ItmQtyLbl.Size = new System.Drawing.Size(20, 18);
            this.ItmQtyLbl.TabIndex = 2;
            this.ItmQtyLbl.Text = "12";
            // 
            // ItmNameLbl
            // 
            this.ItmNameLbl.AutoSize = true;
            this.ItmNameLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItmNameLbl.Location = new System.Drawing.Point(99, 10);
            this.ItmNameLbl.Name = "ItmNameLbl";
            this.ItmNameLbl.Size = new System.Drawing.Size(39, 18);
            this.ItmNameLbl.TabIndex = 1;
            this.ItmNameLbl.Text = "Rose";
            // 
            // ItmIDLbl
            // 
            this.ItmIDLbl.AutoSize = true;
            this.ItmIDLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItmIDLbl.Location = new System.Drawing.Point(23, 10);
            this.ItmIDLbl.Name = "ItmIDLbl";
            this.ItmIDLbl.Size = new System.Drawing.Size(13, 18);
            this.ItmIDLbl.TabIndex = 0;
            this.ItmIDLbl.Text = "1";
            // 
            // StockAdjustmentListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel24);
            this.Name = "StockAdjustmentListItems";
            this.Size = new System.Drawing.Size(674, 48);
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Button AdjustBtn;
        private System.Windows.Forms.Label ItmQtyLbl;
        private System.Windows.Forms.Label ItmNameLbl;
        private System.Windows.Forms.Label ItmIDLbl;
    }
}
