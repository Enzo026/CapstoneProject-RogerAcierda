namespace Flowershop_Thesis.OtherForms.Restocking
{
    partial class BatchListItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchListItems));
            this.panel15 = new System.Windows.Forms.Panel();
            this.DateLbl = new System.Windows.Forms.Label();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.BatchIDLbl = new System.Windows.Forms.Label();
            this.button16 = new System.Windows.Forms.Button();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.DateLbl);
            this.panel15.Controls.Add(this.button16);
            this.panel15.Controls.Add(this.QtyLbl);
            this.panel15.Controls.Add(this.BatchIDLbl);
            this.panel15.Location = new System.Drawing.Point(3, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(634, 43);
            this.panel15.TabIndex = 2;
            // 
            // DateLbl
            // 
            this.DateLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLbl.Location = new System.Drawing.Point(405, 12);
            this.DateLbl.Name = "DateLbl";
            this.DateLbl.Size = new System.Drawing.Size(142, 18);
            this.DateLbl.TabIndex = 19;
            this.DateLbl.Text = "19/10/2024";
            this.DateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QtyLbl
            // 
            this.QtyLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QtyLbl.Location = new System.Drawing.Point(291, 13);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(53, 18);
            this.QtyLbl.TabIndex = 1;
            this.QtyLbl.Text = "00";
            this.QtyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BatchIDLbl
            // 
            this.BatchIDLbl.AutoSize = true;
            this.BatchIDLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BatchIDLbl.Location = new System.Drawing.Point(26, 13);
            this.BatchIDLbl.Name = "BatchIDLbl";
            this.BatchIDLbl.Size = new System.Drawing.Size(82, 18);
            this.BatchIDLbl.TabIndex = 0;
            this.BatchIDLbl.Text = "B191020241";
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.White;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.ForeColor = System.Drawing.Color.White;
            this.button16.Image = ((System.Drawing.Image)(resources.GetObject("button16.Image")));
            this.button16.Location = new System.Drawing.Point(577, 3);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(39, 38);
            this.button16.TabIndex = 18;
            this.button16.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button16.UseVisualStyleBackColor = false;
            // 
            // BatchListItems
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel15);
            this.Name = "BatchListItems";
            this.Size = new System.Drawing.Size(641, 49);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label BatchIDLbl;
    }
}
