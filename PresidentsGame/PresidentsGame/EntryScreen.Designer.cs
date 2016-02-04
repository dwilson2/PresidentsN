namespace PresidentsGame
{
    partial class EntryScreen
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
            this.OptionsBox = new System.Windows.Forms.GroupBox();
            this.Startbtn = new System.Windows.Forms.Button();
            this.Heading = new System.Windows.Forms.Label();
            this.Heading2 = new System.Windows.Forms.Label();
            this.OptionsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptionsBox
            // 
            this.OptionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsBox.AutoSize = true;
            this.OptionsBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OptionsBox.Controls.Add(this.Startbtn);
            this.OptionsBox.Location = new System.Drawing.Point(139, 95);
            this.OptionsBox.Margin = new System.Windows.Forms.Padding(2);
            this.OptionsBox.Name = "OptionsBox";
            this.OptionsBox.Padding = new System.Windows.Forms.Padding(2);
            this.OptionsBox.Size = new System.Drawing.Size(90, 66);
            this.OptionsBox.TabIndex = 2;
            this.OptionsBox.TabStop = false;
            this.OptionsBox.Text = "Game Options";
            // 
            // Startbtn
            // 
            this.Startbtn.Location = new System.Drawing.Point(12, 28);
            this.Startbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(74, 21);
            this.Startbtn.TabIndex = 2;
            this.Startbtn.Text = "Start Game";
            this.Startbtn.UseVisualStyleBackColor = true;
            this.Startbtn.Click += new System.EventHandler(this.Startbtn_Click);
            // 
            // Heading
            // 
            this.Heading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Heading.AutoSize = true;
            this.Heading.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Heading.Location = new System.Drawing.Point(1, 0);
            this.Heading.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(377, 37);
            this.Heading.TabIndex = 3;
            this.Heading.Text = "Welcome To Presidents";
            this.Heading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Heading2
            // 
            this.Heading2.AutoSize = true;
            this.Heading2.Location = new System.Drawing.Point(123, 59);
            this.Heading2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Heading2.Name = "Heading2";
            this.Heading2.Size = new System.Drawing.Size(133, 13);
            this.Heading2.TabIndex = 4;
            this.Heading2.Text = "Select An Option To Begin";
            this.Heading2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EntryScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(384, 189);
            this.Controls.Add(this.Heading2);
            this.Controls.Add(this.Heading);
            this.Controls.Add(this.OptionsBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 227);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 227);
            this.Name = "EntryScreen";
            this.Text = "EntryScreen";
            this.OptionsBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox OptionsBox;
        private System.Windows.Forms.Label Heading;
        private System.Windows.Forms.Label Heading2;
        private System.Windows.Forms.Button Startbtn;
    }
}

