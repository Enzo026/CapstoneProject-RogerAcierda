namespace Flowershop_Thesis.OtherForms.Restocking
{
    partial class RestockingProcessItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestockingProcessItems));
            this.panel4 = new System.Windows.Forms.Panel();
            this.SupplierLbl = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.QtyLbl = new System.Windows.Forms.Label();
            this.ItemNameLbl = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.SupplierLbl);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.QtyLbl);
            this.panel4.Controls.Add(this.ItemNameLbl);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(378, 43);
            this.panel4.TabIndex = 1;
            // 
            // SupplierLbl
            // 
            this.SupplierLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierLbl.Location = new System.Drawing.Point(209, 12);
            this.SupplierLbl.Name = "SupplierLbl";
            this.SupplierLbl.Size = new System.Drawing.Size(128, 18);
            this.SupplierLbl.TabIndex = 18;
            this.SupplierLbl.Text = "Supplier Name";
            this.SupplierLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(336, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(39, 38);
            this.button4.TabIndex = 17;
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // QtyLbl
            // 
            this.QtyLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QtyLbl.Location = new System.Drawing.Point(157, 12);
            this.QtyLbl.Name = "QtyLbl";
            this.QtyLbl.Size = new System.Drawing.Size(46, 18);
            this.QtyLbl.TabIndex = 1;
            this.QtyLbl.Text = "0000";
            this.QtyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemNameLbl
            // 
            this.ItemNameLbl.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNameLbl.Location = new System.Drawing.Point(6, 12);
            this.ItemNameLbl.Name = "ItemNameLbl";
            this.ItemNameLbl.Size = new System.Drawing.Size(142, 18);
            this.ItemNameLbl.TabIndex = 0;
            this.ItemNameLbl.Text = "Item Name";
            this.ItemNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RestockingProcessItems
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel4);
            this.Name = "RestockingProcessItems";
            this.Size = new System.Drawing.Size(385, 49);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label SupplierLbl;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label QtyLbl;
        private System.Windows.Forms.Label ItemNameLbl;
    }
}
