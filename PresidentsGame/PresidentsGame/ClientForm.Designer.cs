﻿namespace PresidentsGame
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.Moves = new System.Windows.Forms.GroupBox();
            this.Play = new System.Windows.Forms.Button();
            this.Pass = new System.Windows.Forms.Button();
            this.PrevH = new System.Windows.Forms.Label();
            this.P1 = new System.Windows.Forms.Panel();
            this.P2 = new System.Windows.Forms.Panel();
            this.P3 = new System.Windows.Forms.Panel();
            this.P4 = new System.Windows.Forms.Panel();
            this.P5 = new System.Windows.Forms.Panel();
            this.C26 = new PresidentsGame.TransparentMessagePanel();
            this.C13 = new PresidentsGame.TransparentMessagePanel();
            this.C25 = new PresidentsGame.TransparentMessagePanel();
            this.C12 = new PresidentsGame.TransparentMessagePanel();
            this.C24 = new PresidentsGame.TransparentMessagePanel();
            this.C11 = new PresidentsGame.TransparentMessagePanel();
            this.C23 = new PresidentsGame.TransparentMessagePanel();
            this.C10 = new PresidentsGame.TransparentMessagePanel();
            this.C22 = new PresidentsGame.TransparentMessagePanel();
            this.C9 = new PresidentsGame.TransparentMessagePanel();
            this.C21 = new PresidentsGame.TransparentMessagePanel();
            this.C8 = new PresidentsGame.TransparentMessagePanel();
            this.C20 = new PresidentsGame.TransparentMessagePanel();
            this.C7 = new PresidentsGame.TransparentMessagePanel();
            this.C19 = new PresidentsGame.TransparentMessagePanel();
            this.C6 = new PresidentsGame.TransparentMessagePanel();
            this.C18 = new PresidentsGame.TransparentMessagePanel();
            this.C5 = new PresidentsGame.TransparentMessagePanel();
            this.C17 = new PresidentsGame.TransparentMessagePanel();
            this.C4 = new PresidentsGame.TransparentMessagePanel();
            this.C16 = new PresidentsGame.TransparentMessagePanel();
            this.C3 = new PresidentsGame.TransparentMessagePanel();
            this.C15 = new PresidentsGame.TransparentMessagePanel();
            this.C2 = new PresidentsGame.TransparentMessagePanel();
            this.C14 = new PresidentsGame.TransparentMessagePanel();
            this.C1 = new PresidentsGame.TransparentMessagePanel();
            this.Moves.SuspendLayout();
            this.SuspendLayout();
            // 
            // Moves
            // 
            this.Moves.Controls.Add(this.Play);
            this.Moves.Controls.Add(this.Pass);
            this.Moves.Location = new System.Drawing.Point(501, 8);
            this.Moves.Margin = new System.Windows.Forms.Padding(2);
            this.Moves.Name = "Moves";
            this.Moves.Padding = new System.Windows.Forms.Padding(2);
            this.Moves.Size = new System.Drawing.Size(101, 78);
            this.Moves.TabIndex = 6;
            this.Moves.TabStop = false;
            this.Moves.Text = "Moves";
            // 
            // Play
            // 
            this.Play.AutoSize = true;
            this.Play.Location = new System.Drawing.Point(23, 44);
            this.Play.Margin = new System.Windows.Forms.Padding(2);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(67, 23);
            this.Play.TabIndex = 2;
            this.Play.Text = "Play Move";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Pass
            // 
            this.Pass.AutoSize = true;
            this.Pass.Location = new System.Drawing.Point(23, 17);
            this.Pass.Margin = new System.Windows.Forms.Padding(2);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(60, 23);
            this.Pass.TabIndex = 1;
            this.Pass.Text = "Pass";
            this.Pass.UseVisualStyleBackColor = true;
            this.Pass.Click += new System.EventHandler(this.Pass_Click);
            // 
            // PrevH
            // 
            this.PrevH.AutoSize = true;
            this.PrevH.Location = new System.Drawing.Point(16, 8);
            this.PrevH.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PrevH.Name = "PrevH";
            this.PrevH.Size = new System.Drawing.Size(91, 13);
            this.PrevH.TabIndex = 40;
            this.PrevH.Text = "Last Hand Played";
            // 
            // P1
            // 
            this.P1.BackColor = System.Drawing.SystemColors.Control;
            this.P1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.P1.Location = new System.Drawing.Point(17, 23);
            this.P1.Margin = new System.Windows.Forms.Padding(2);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(40, 63);
            this.P1.TabIndex = 1;
            this.P1.Visible = false;
            // 
            // P2
            // 
            this.P2.Location = new System.Drawing.Point(61, 23);
            this.P2.Margin = new System.Windows.Forms.Padding(2);
            this.P2.Name = "P2";
            this.P2.Size = new System.Drawing.Size(40, 63);
            this.P2.TabIndex = 2;
            this.P2.Visible = false;
            // 
            // P3
            // 
            this.P3.Location = new System.Drawing.Point(104, 23);
            this.P3.Margin = new System.Windows.Forms.Padding(2);
            this.P3.Name = "P3";
            this.P3.Size = new System.Drawing.Size(40, 63);
            this.P3.TabIndex = 3;
            this.P3.Visible = false;
            // 
            // P4
            // 
            this.P4.Location = new System.Drawing.Point(147, 23);
            this.P4.Margin = new System.Windows.Forms.Padding(2);
            this.P4.Name = "P4";
            this.P4.Size = new System.Drawing.Size(40, 63);
            this.P4.TabIndex = 4;
            this.P4.Visible = false;
            // 
            // P5
            // 
            this.P5.Location = new System.Drawing.Point(190, 23);
            this.P5.Margin = new System.Windows.Forms.Padding(2);
            this.P5.Name = "P5";
            this.P5.Size = new System.Drawing.Size(40, 63);
            this.P5.TabIndex = 5;
            this.P5.Visible = false;
            // 
            // C26
            // 
            this.C26.active = false;
            this.C26.Location = new System.Drawing.Point(544, 229);
            this.C26.Margin = new System.Windows.Forms.Padding(2);
            this.C26.Name = "C26";
            this.C26.Size = new System.Drawing.Size(40, 63);
            this.C26.TabIndex = 32;
            this.C26.Click += new System.EventHandler(this.C26_Click);
            // 
            // C13
            // 
            this.C13.active = false;
            this.C13.Location = new System.Drawing.Point(544, 151);
            this.C13.Margin = new System.Windows.Forms.Padding(2);
            this.C13.Name = "C13";
            this.C13.Size = new System.Drawing.Size(40, 63);
            this.C13.TabIndex = 19;
            this.C13.Click += new System.EventHandler(this.C13_Click);
            // 
            // C25
            // 
            this.C25.active = false;
            this.C25.Location = new System.Drawing.Point(501, 229);
            this.C25.Margin = new System.Windows.Forms.Padding(2);
            this.C25.Name = "C25";
            this.C25.Size = new System.Drawing.Size(40, 63);
            this.C25.TabIndex = 31;
            this.C25.Click += new System.EventHandler(this.C25_Click);
            // 
            // C12
            // 
            this.C12.active = false;
            this.C12.Location = new System.Drawing.Point(501, 151);
            this.C12.Margin = new System.Windows.Forms.Padding(2);
            this.C12.Name = "C12";
            this.C12.Size = new System.Drawing.Size(40, 63);
            this.C12.TabIndex = 18;
            this.C12.Click += new System.EventHandler(this.C12_Click);
            // 
            // C24
            // 
            this.C24.active = false;
            this.C24.Location = new System.Drawing.Point(458, 229);
            this.C24.Margin = new System.Windows.Forms.Padding(2);
            this.C24.Name = "C24";
            this.C24.Size = new System.Drawing.Size(40, 63);
            this.C24.TabIndex = 30;
            this.C24.Click += new System.EventHandler(this.C24_Click);
            // 
            // C11
            // 
            this.C11.active = false;
            this.C11.Location = new System.Drawing.Point(458, 151);
            this.C11.Margin = new System.Windows.Forms.Padding(2);
            this.C11.Name = "C11";
            this.C11.Size = new System.Drawing.Size(40, 63);
            this.C11.TabIndex = 17;
            this.C11.Click += new System.EventHandler(this.C11_Click);
            // 
            // C23
            // 
            this.C23.active = false;
            this.C23.Location = new System.Drawing.Point(414, 229);
            this.C23.Margin = new System.Windows.Forms.Padding(2);
            this.C23.Name = "C23";
            this.C23.Size = new System.Drawing.Size(40, 63);
            this.C23.TabIndex = 29;
            this.C23.Click += new System.EventHandler(this.C23_Click);
            // 
            // C10
            // 
            this.C10.active = false;
            this.C10.Location = new System.Drawing.Point(414, 151);
            this.C10.Margin = new System.Windows.Forms.Padding(2);
            this.C10.Name = "C10";
            this.C10.Size = new System.Drawing.Size(40, 63);
            this.C10.TabIndex = 16;
            this.C10.Click += new System.EventHandler(this.C10_Click);
            // 
            // C22
            // 
            this.C22.active = false;
            this.C22.Location = new System.Drawing.Point(370, 229);
            this.C22.Margin = new System.Windows.Forms.Padding(2);
            this.C22.Name = "C22";
            this.C22.Size = new System.Drawing.Size(40, 63);
            this.C22.TabIndex = 28;
            this.C22.Click += new System.EventHandler(this.C22_Click);
            // 
            // C9
            // 
            this.C9.active = false;
            this.C9.Location = new System.Drawing.Point(370, 151);
            this.C9.Margin = new System.Windows.Forms.Padding(2);
            this.C9.Name = "C9";
            this.C9.Size = new System.Drawing.Size(40, 63);
            this.C9.TabIndex = 15;
            this.C9.Click += new System.EventHandler(this.C9_Click);
            // 
            // C21
            // 
            this.C21.active = false;
            this.C21.Location = new System.Drawing.Point(327, 229);
            this.C21.Margin = new System.Windows.Forms.Padding(2);
            this.C21.Name = "C21";
            this.C21.Size = new System.Drawing.Size(40, 63);
            this.C21.TabIndex = 27;
            this.C21.Click += new System.EventHandler(this.C21_Click);
            // 
            // C8
            // 
            this.C8.active = false;
            this.C8.Location = new System.Drawing.Point(327, 151);
            this.C8.Margin = new System.Windows.Forms.Padding(2);
            this.C8.Name = "C8";
            this.C8.Size = new System.Drawing.Size(40, 63);
            this.C8.TabIndex = 14;
            this.C8.Click += new System.EventHandler(this.C8_Click);
            // 
            // C20
            // 
            this.C20.active = false;
            this.C20.Location = new System.Drawing.Point(284, 229);
            this.C20.Margin = new System.Windows.Forms.Padding(2);
            this.C20.Name = "C20";
            this.C20.Size = new System.Drawing.Size(40, 63);
            this.C20.TabIndex = 26;
            this.C20.Click += new System.EventHandler(this.C20_Click);
            // 
            // C7
            // 
            this.C7.active = false;
            this.C7.Location = new System.Drawing.Point(284, 151);
            this.C7.Margin = new System.Windows.Forms.Padding(2);
            this.C7.Name = "C7";
            this.C7.Size = new System.Drawing.Size(40, 63);
            this.C7.TabIndex = 13;
            this.C7.Click += new System.EventHandler(this.C7_Click);
            // 
            // C19
            // 
            this.C19.active = false;
            this.C19.Location = new System.Drawing.Point(241, 229);
            this.C19.Margin = new System.Windows.Forms.Padding(2);
            this.C19.Name = "C19";
            this.C19.Size = new System.Drawing.Size(40, 63);
            this.C19.TabIndex = 25;
            this.C19.Click += new System.EventHandler(this.C19_Click);
            // 
            // C6
            // 
            this.C6.active = false;
            this.C6.Location = new System.Drawing.Point(241, 151);
            this.C6.Margin = new System.Windows.Forms.Padding(2);
            this.C6.Name = "C6";
            this.C6.Size = new System.Drawing.Size(40, 63);
            this.C6.TabIndex = 12;
            this.C6.Click += new System.EventHandler(this.C6_Click);
            // 
            // C18
            // 
            this.C18.active = false;
            this.C18.Location = new System.Drawing.Point(197, 229);
            this.C18.Margin = new System.Windows.Forms.Padding(2);
            this.C18.Name = "C18";
            this.C18.Size = new System.Drawing.Size(40, 63);
            this.C18.TabIndex = 24;
            this.C18.Click += new System.EventHandler(this.C18_Click);
            // 
            // C5
            // 
            this.C5.active = false;
            this.C5.Location = new System.Drawing.Point(197, 151);
            this.C5.Margin = new System.Windows.Forms.Padding(2);
            this.C5.Name = "C5";
            this.C5.Size = new System.Drawing.Size(40, 63);
            this.C5.TabIndex = 11;
            this.C5.Click += new System.EventHandler(this.C5_Click);
            // 
            // C17
            // 
            this.C17.active = false;
            this.C17.Location = new System.Drawing.Point(154, 229);
            this.C17.Margin = new System.Windows.Forms.Padding(2);
            this.C17.Name = "C17";
            this.C17.Size = new System.Drawing.Size(40, 63);
            this.C17.TabIndex = 23;
            this.C17.Click += new System.EventHandler(this.C17_Click);
            // 
            // C4
            // 
            this.C4.active = false;
            this.C4.Location = new System.Drawing.Point(154, 151);
            this.C4.Margin = new System.Windows.Forms.Padding(2);
            this.C4.Name = "C4";
            this.C4.Size = new System.Drawing.Size(40, 63);
            this.C4.TabIndex = 10;
            this.C4.Click += new System.EventHandler(this.C4_Click);
            // 
            // C16
            // 
            this.C16.active = false;
            this.C16.Location = new System.Drawing.Point(111, 229);
            this.C16.Margin = new System.Windows.Forms.Padding(2);
            this.C16.Name = "C16";
            this.C16.Size = new System.Drawing.Size(40, 63);
            this.C16.TabIndex = 22;
            this.C16.Click += new System.EventHandler(this.C16_Click);
            // 
            // C3
            // 
            this.C3.active = false;
            this.C3.Location = new System.Drawing.Point(111, 151);
            this.C3.Margin = new System.Windows.Forms.Padding(2);
            this.C3.Name = "C3";
            this.C3.Size = new System.Drawing.Size(40, 63);
            this.C3.TabIndex = 9;
            this.C3.Click += new System.EventHandler(this.C3_Click);
            // 
            // C15
            // 
            this.C15.active = false;
            this.C15.Location = new System.Drawing.Point(68, 229);
            this.C15.Margin = new System.Windows.Forms.Padding(2);
            this.C15.Name = "C15";
            this.C15.Size = new System.Drawing.Size(40, 63);
            this.C15.TabIndex = 21;
            this.C15.Click += new System.EventHandler(this.C15_Click);
            // 
            // C2
            // 
            this.C2.active = false;
            this.C2.Location = new System.Drawing.Point(68, 151);
            this.C2.Margin = new System.Windows.Forms.Padding(2);
            this.C2.Name = "C2";
            this.C2.Size = new System.Drawing.Size(40, 63);
            this.C2.TabIndex = 8;
            this.C2.Click += new System.EventHandler(this.C2_Click);
            // 
            // C14
            // 
            this.C14.active = false;
            this.C14.Location = new System.Drawing.Point(24, 229);
            this.C14.Margin = new System.Windows.Forms.Padding(2);
            this.C14.Name = "C14";
            this.C14.Size = new System.Drawing.Size(40, 63);
            this.C14.TabIndex = 20;
            this.C14.Click += new System.EventHandler(this.C14_Click);
            // 
            // C1
            // 
            this.C1.active = false;
            this.C1.Location = new System.Drawing.Point(24, 151);
            this.C1.Margin = new System.Windows.Forms.Padding(2);
            this.C1.Name = "C1";
            this.C1.Size = new System.Drawing.Size(40, 63);
            this.C1.TabIndex = 7;
            this.C1.Click += new System.EventHandler(this.C1_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(610, 305);
            this.Controls.Add(this.C26);
            this.Controls.Add(this.C13);
            this.Controls.Add(this.C25);
            this.Controls.Add(this.C12);
            this.Controls.Add(this.C24);
            this.Controls.Add(this.C11);
            this.Controls.Add(this.C23);
            this.Controls.Add(this.C10);
            this.Controls.Add(this.C22);
            this.Controls.Add(this.C9);
            this.Controls.Add(this.C21);
            this.Controls.Add(this.C8);
            this.Controls.Add(this.C20);
            this.Controls.Add(this.C7);
            this.Controls.Add(this.C19);
            this.Controls.Add(this.C6);
            this.Controls.Add(this.C18);
            this.Controls.Add(this.C5);
            this.Controls.Add(this.C17);
            this.Controls.Add(this.C4);
            this.Controls.Add(this.C16);
            this.Controls.Add(this.C3);
            this.Controls.Add(this.C15);
            this.Controls.Add(this.C2);
            this.Controls.Add(this.C14);
            this.Controls.Add(this.C1);
            this.Controls.Add(this.P5);
            this.Controls.Add(this.P4);
            this.Controls.Add(this.P3);
            this.Controls.Add(this.P2);
            this.Controls.Add(this.P1);
            this.Controls.Add(this.PrevH);
            this.Controls.Add(this.Moves);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(626, 343);
            this.MinimumSize = new System.Drawing.Size(626, 343);
            this.Name = "ClientForm";
            this.Text = "Presidents";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientForm_FormClosed);
            this.Load += new System.EventHandler(this.SoloForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SoloForm_Paint);
            this.Moves.ResumeLayout(false);
            this.Moves.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Moves;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Pass;
        private System.Windows.Forms.Label PrevH;
        private System.Windows.Forms.Panel P1;
        private System.Windows.Forms.Panel P2;
        private System.Windows.Forms.Panel P3;
        private System.Windows.Forms.Panel P4;
        private System.Windows.Forms.Panel P5;
        private TransparentMessagePanel C1;
        private TransparentMessagePanel C2;
        private TransparentMessagePanel C3;
        private TransparentMessagePanel C4;
        private TransparentMessagePanel C5;
        private TransparentMessagePanel C6;
        private TransparentMessagePanel C7;
        private TransparentMessagePanel C8;
        private TransparentMessagePanel C9;
        private TransparentMessagePanel C10;
        private TransparentMessagePanel C11;
        private TransparentMessagePanel C12;
        private TransparentMessagePanel C13;
        private TransparentMessagePanel C26;
        private TransparentMessagePanel C25;
        private TransparentMessagePanel C24;
        private TransparentMessagePanel C23;
        private TransparentMessagePanel C22;
        private TransparentMessagePanel C21;
        private TransparentMessagePanel C20;
        private TransparentMessagePanel C19;
        private TransparentMessagePanel C18;
        private TransparentMessagePanel C17;
        private TransparentMessagePanel C16;
        private TransparentMessagePanel C15;
        private TransparentMessagePanel C14;
    }
}