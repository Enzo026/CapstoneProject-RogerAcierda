namespace Flowershop_Thesis.MainForms
{
    partial class CustomRoleList
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
            this.FormBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FormBtn
            // 
            this.FormBtn.FlatAppearance.BorderSize = 0;
            this.FormBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FormBtn.Font = new System.Drawing.Font("Montserrat SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FormBtn.Location = new System.Drawing.Point(1, 2);
            this.FormBtn.Name = "FormBtn";
            this.FormBtn.Size = new System.Drawing.Size(286, 56);
            this.FormBtn.TabIndex = 7;
            this.FormBtn.Text = "History Logs";
            this.FormBtn.UseVisualStyleBackColor = true;
            this.FormBtn.Click += new System.EventHandler(this.button5_Click);
            // 
            // CustomRoleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FormBtn);
            this.Name = "CustomRoleList";
            this.Size = new System.Drawing.Size(289, 77);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FormBtn;
    }
}
