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
            this.panel27 = new System.Windows.Forms.Panel();
            this.button28 = new System.Windows.Forms.Button();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.ItemNameLbl = new System.Windows.Forms.Label();
            this.panel27.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.button28);
            this.panel27.Controls.Add(this.StatusLbl);
            this.panel27.Controls.Add(this.QtyLbl);
            this.panel27.Controls.Add(this.ItemNameLbl);
            this.panel27.Location = new System.Drawing.Point(3, 3);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(636, 42);
            this.panel27.TabIndex = 4;
            // 
            // button28
            // 
            this.button28.FlatAppearance.BorderSize = 0;
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button28.Image = global::Flowershop_Thesis.Properties.Resources.add_product;
            this.button28.Location = new System.Drawing.Point(590, 3);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(43, 36);
            this.button28.TabIndex = 34;
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // StatusLbl
            // 
            this.StatusLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLbl.ForeColor = System.Drawing.Color.Crimson;
            this.StatusLbl.Location = new System.Drawing.Point(396, 12);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(170, 18);
            this.StatusLbl.TabIndex = 33;
            this.StatusLbl.Text = "Out of Stock";
            this.StatusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QtyLbl
            // 
            this.QtyLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QtyLbl.Location = new System.Drawing.Point(265, 12);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(115, 18);
            this.QtyLbl.TabIndex = 32;
            this.QtyLbl.Text = "100";
            this.QtyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemNameLbl
            // 
            this.ItemNameLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNameLbl.Location = new System.Drawing.Point(16, 12);
            this.ItemNameLbl.Name = "ItemNameLbl";
            this.ItemNameLbl.Size = new System.Drawing.Size(241, 18);
            this.ItemNameLbl.TabIndex = 31;
            this.ItemNameLbl.Text = "FlowerName";
            // 
            // RestockList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel27);
            this.Name = "RestockList";
            this.Size = new System.Drawing.Size(643, 48);
            this.Load += new System.EventHandler(this.RestockList_Load);
            this.panel27.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label ItemNameLbl;
    }
}
