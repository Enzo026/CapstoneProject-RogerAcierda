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
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.DateLbl = new System.Windows.Forms.Label();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.BatchIDLbl = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.DateLbl);
            this.panel3.Controls.Add(this.QtyLbl);
            this.panel3.Controls.Add(this.BatchIDLbl);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(656, 36);
            this.panel3.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Flowershop_Thesis.Properties.Resources.compose;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(613, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DateLbl
            // 
            this.DateLbl.Font = new System.Drawing.Font("Montserrat Medium", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLbl.Location = new System.Drawing.Point(414, 9);
            this.DateLbl.Name = "DateLbl";
            this.DateLbl.Size = new System.Drawing.Size(165, 18);
            this.DateLbl.TabIndex = 8;
            this.DateLbl.Text = "Oct 31, 2024";
            this.DateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QtyLbl
            // 
            this.QtyLbl.Font = new System.Drawing.Font("Montserrat Medium", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QtyLbl.Location = new System.Drawing.Point(287, 9);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(87, 18);
            this.QtyLbl.TabIndex = 7;
            this.QtyLbl.Text = "2";
            this.QtyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BatchIDLbl
            // 
            this.BatchIDLbl.AutoSize = true;
            this.BatchIDLbl.Font = new System.Drawing.Font("Montserrat Medium", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BatchIDLbl.Location = new System.Drawing.Point(12, 9);
            this.BatchIDLbl.Name = "BatchIDLbl";
            this.BatchIDLbl.Size = new System.Drawing.Size(65, 18);
            this.BatchIDLbl.TabIndex = 6;
            this.BatchIDLbl.Text = "B3110241";
            // 
            // StockAdjustmentListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "StockAdjustmentListItems";
            this.Size = new System.Drawing.Size(662, 43);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label BatchIDLbl;
    }
}
