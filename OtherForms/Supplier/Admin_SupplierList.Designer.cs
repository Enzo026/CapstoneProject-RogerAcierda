namespace Flowershop_Thesis.OtherForms.Supplier
{
    partial class Admin_SupplierList
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.SuppTypeLbl = new System.Windows.Forms.Label();
            this.SuppName = new System.Windows.Forms.Label();
            this.SuppIDLbl = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel5.Controls.Add(this.button6);
            this.panel5.Controls.Add(this.SuppTypeLbl);
            this.panel5.Controls.Add(this.SuppName);
            this.panel5.Controls.Add(this.SuppIDLbl);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(559, 30);
            this.panel5.TabIndex = 1;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(432, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(124, 23);
            this.button6.TabIndex = 22;
            this.button6.Text = "See Full Details";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // SuppTypeLbl
            // 
            this.SuppTypeLbl.AutoSize = true;
            this.SuppTypeLbl.Location = new System.Drawing.Point(334, 8);
            this.SuppTypeLbl.Name = "SuppTypeLbl";
            this.SuppTypeLbl.Size = new System.Drawing.Size(31, 13);
            this.SuppTypeLbl.TabIndex = 21;
            this.SuppTypeLbl.Text = "Type";
            // 
            // SuppName
            // 
            this.SuppName.AutoSize = true;
            this.SuppName.Location = new System.Drawing.Point(106, 8);
            this.SuppName.Name = "SuppName";
            this.SuppName.Size = new System.Drawing.Size(76, 13);
            this.SuppName.TabIndex = 20;
            this.SuppName.Text = "Supplier Name";
            // 
            // SuppIDLbl
            // 
            this.SuppIDLbl.AutoSize = true;
            this.SuppIDLbl.Location = new System.Drawing.Point(10, 8);
            this.SuppIDLbl.Name = "SuppIDLbl";
            this.SuppIDLbl.Size = new System.Drawing.Size(18, 13);
            this.SuppIDLbl.TabIndex = 19;
            this.SuppIDLbl.Text = "ID";
            // 
            // Admin_SupplierList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Name = "Admin_SupplierList";
            this.Size = new System.Drawing.Size(564, 36);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label SuppTypeLbl;
        private System.Windows.Forms.Label SuppName;
        private System.Windows.Forms.Label SuppIDLbl;
    }
}
