﻿namespace Flowershop_Thesis.OtherForms
{
    partial class ReviewOrder
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NameIndicator = new System.Windows.Forms.Label();
            this.OrderContents_lbl = new System.Windows.Forms.Label();
            this.Amount_lbl = new System.Windows.Forms.Label();
            this.CustName_txtbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProceedBtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.counterlbl = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat ExtraBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "ORDER DETAILS";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 58);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.NameIndicator);
            this.panel2.Controls.Add(this.OrderContents_lbl);
            this.panel2.Controls.Add(this.Amount_lbl);
            this.panel2.Controls.Add(this.CustName_txtbox);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(520, 264);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(314, 273);
            this.panel2.TabIndex = 2;
            // 
            // NameIndicator
            // 
            this.NameIndicator.AutoSize = true;
            this.NameIndicator.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameIndicator.Location = new System.Drawing.Point(23, 227);
            this.NameIndicator.Name = "NameIndicator";
            this.NameIndicator.Size = new System.Drawing.Size(29, 15);
            this.NameIndicator.TabIndex = 23;
            this.NameIndicator.Text = "Null";
            // 
            // OrderContents_lbl
            // 
            this.OrderContents_lbl.AutoSize = true;
            this.OrderContents_lbl.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderContents_lbl.Location = new System.Drawing.Point(159, 91);
            this.OrderContents_lbl.Name = "OrderContents_lbl";
            this.OrderContents_lbl.Size = new System.Drawing.Size(71, 18);
            this.OrderContents_lbl.TabIndex = 14;
            this.OrderContents_lbl.Text = "0000000";
            // 
            // Amount_lbl
            // 
            this.Amount_lbl.AutoSize = true;
            this.Amount_lbl.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Amount_lbl.Location = new System.Drawing.Point(159, 126);
            this.Amount_lbl.Name = "Amount_lbl";
            this.Amount_lbl.Size = new System.Drawing.Size(71, 18);
            this.Amount_lbl.TabIndex = 11;
            this.Amount_lbl.Text = "0000000";
            // 
            // CustName_txtbox
            // 
            this.CustName_txtbox.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustName_txtbox.Location = new System.Drawing.Point(20, 201);
            this.CustName_txtbox.Name = "CustName_txtbox";
            this.CustName_txtbox.Size = new System.Drawing.Size(281, 23);
            this.CustName_txtbox.TabIndex = 10;
            this.CustName_txtbox.TextChanged += new System.EventHandler(this.CustName_txtbox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 18);
            this.label11.TabIndex = 9;
            this.label11.Text = "Customer name :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "Order Contents :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Amount :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Order Amount";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ProceedBtn
            // 
            this.ProceedBtn.Location = new System.Drawing.Point(520, 543);
            this.ProceedBtn.Name = "ProceedBtn";
            this.ProceedBtn.Size = new System.Drawing.Size(314, 48);
            this.ProceedBtn.TabIndex = 3;
            this.ProceedBtn.Text = "Proceed";
            this.ProceedBtn.UseVisualStyleBackColor = true;
            this.ProceedBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.counterlbl);
            this.panel3.Controls.Add(this.CancelBtn);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.flowLayoutPanel1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.ProceedBtn);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(13, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(837, 656);
            this.panel3.TabIndex = 4;
            // 
            // counterlbl
            // 
            this.counterlbl.AutoSize = true;
            this.counterlbl.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counterlbl.Location = new System.Drawing.Point(106, 67);
            this.counterlbl.Name = "counterlbl";
            this.counterlbl.Size = new System.Drawing.Size(43, 22);
            this.counterlbl.TabIndex = 7;
            this.counterlbl.Text = "000";
            this.counterlbl.TextChanged += new System.EventHandler(this.counterlbl_TextChanged);
            this.counterlbl.Click += new System.EventHandler(this.counterlbl_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(520, 597);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(314, 48);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Contents";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 92);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(511, 561);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Flowershop_Thesis.Properties.Resources.undraw_order_flowers_scc0;
            this.pictureBox1.Location = new System.Drawing.Point(520, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 191);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // ReviewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(862, 681);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReviewOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReviewOrder";
            this.Load += new System.EventHandler(this.ReviewOrder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ProceedBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox CustName_txtbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label OrderContents_lbl;
        private System.Windows.Forms.Label Amount_lbl;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label counterlbl;
        private System.Windows.Forms.Label NameIndicator;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}