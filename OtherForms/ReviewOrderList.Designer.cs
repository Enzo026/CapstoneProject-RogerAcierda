namespace Flowershop_Thesis.OtherForms
{
    partial class ReviewOrderList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ItemPrice = new System.Windows.Forms.Label();
            this.ItemQty = new System.Windows.Forms.Label();
            this.ItemName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ItemPrice);
            this.panel1.Controls.Add(this.ItemQty);
            this.panel1.Controls.Add(this.ItemName);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 36);
            this.panel1.TabIndex = 0;
            // 
            // ItemPrice
            // 
            this.ItemPrice.AutoSize = true;
            this.ItemPrice.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemPrice.Location = new System.Drawing.Point(400, 9);
            this.ItemPrice.Name = "ItemPrice";
            this.ItemPrice.Size = new System.Drawing.Size(34, 18);
            this.ItemPrice.TabIndex = 12;
            this.ItemPrice.Text = "300";
            // 
            // ItemQty
            // 
            this.ItemQty.AutoSize = true;
            this.ItemQty.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemQty.Location = new System.Drawing.Point(304, 9);
            this.ItemQty.Name = "ItemQty";
            this.ItemQty.Size = new System.Drawing.Size(16, 18);
            this.ItemQty.TabIndex = 11;
            this.ItemQty.Text = "6";
            // 
            // ItemName
            // 
            this.ItemName.AutoSize = true;
            this.ItemName.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemName.Location = new System.Drawing.Point(18, 9);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(41, 18);
            this.ItemName.TabIndex = 10;
            this.ItemName.Text = "Rose";
            // 
            // ReviewOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ReviewOrderList";
            this.Size = new System.Drawing.Size(480, 42);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ItemPrice;
        private System.Windows.Forms.Label ItemQty;
        private System.Windows.Forms.Label ItemName;
    }
}
