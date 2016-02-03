namespace PresidentsN
{
    partial class PlayerForm
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
            this.Play_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Play_Btn
            // 
            this.Play_Btn.Location = new System.Drawing.Point(102, 152);
            this.Play_Btn.Name = "Play_Btn";
            this.Play_Btn.Size = new System.Drawing.Size(75, 23);
            this.Play_Btn.TabIndex = 0;
            this.Play_Btn.Text = "Play Hand";
            this.Play_Btn.UseVisualStyleBackColor = true;
            this.Play_Btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Play_Btn);
            this.Name = "PlayerForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Play_Btn;
    }
}