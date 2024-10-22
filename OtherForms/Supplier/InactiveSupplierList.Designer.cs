namespace Flowershop_Thesis.OtherForms.Supplier
{
    partial class InactiveSupplierList
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
            this.panel7 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.SuppName = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel7.Controls.Add(this.button7);
            this.panel7.Controls.Add(this.SuppName);
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(241, 29);
            this.panel7.TabIndex = 1;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(145, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(93, 23);
            this.button7.TabIndex = 23;
            this.button7.Text = "Activate";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // SuppName
            // 
            this.SuppName.AutoSize = true;
            this.SuppName.Location = new System.Drawing.Point(11, 7);
            this.SuppName.Name = "SuppName";
            this.SuppName.Size = new System.Drawing.Size(76, 13);
            this.SuppName.TabIndex = 21;
            this.SuppName.Text = "Supplier Name";
            // 
            // InactiveSupplierList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel7);
            this.Name = "InactiveSupplierList";
            this.Size = new System.Drawing.Size(247, 35);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label SuppName;
    }
}
