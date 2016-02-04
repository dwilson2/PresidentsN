namespace PresidentsGame
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
            this.Deal = new System.Windows.Forms.Button();
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
            this.Moves.Controls.Add(this.Deal);
            this.Moves.Location = new System.Drawing.Point(711, 10);
            this.Moves.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Moves.Name = "Moves";
            this.Moves.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Moves.Size = new System.Drawing.Size(92, 103);
            this.Moves.TabIndex = 26;
            this.Moves.TabStop = false;
            this.Moves.Text = "Moves";
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(4, 65);
            this.Play.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(80, 19);
            this.Play.TabIndex = 2;
            this.Play.Text = "Play Move";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Pass
            // 
            this.Pass.Location = new System.Drawing.Point(5, 41);
            this.Pass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(80, 19);
            this.Pass.TabIndex = 1;
            this.Pass.Text = "Pass";
            this.Pass.UseVisualStyleBackColor = true;
            // 
            // Deal
            // 
            this.Deal.Location = new System.Drawing.Point(5, 18);
            this.Deal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Deal.Name = "Deal";
            this.Deal.Size = new System.Drawing.Size(80, 19);
            this.Deal.TabIndex = 0;
            this.Deal.Text = "Deal/ Restart";
            this.Deal.UseVisualStyleBackColor = true;
            this.Deal.Click += new System.EventHandler(this.Deal_Click);
            // 
            // PrevH
            // 
            this.PrevH.AutoSize = true;
            this.PrevH.Location = new System.Drawing.Point(21, 10);
            this.PrevH.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PrevH.Name = "PrevH";
            this.PrevH.Size = new System.Drawing.Size(91, 13);
            this.PrevH.TabIndex = 27;
            this.PrevH.Text = "Last Hand Played";
            this.PrevH.Visible = false;
            // 
            // P1
            // 
            this.P1.BackColor = System.Drawing.SystemColors.Control;
            this.P1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.P1.Location = new System.Drawing.Point(23, 28);
            this.P1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(53, 78);
            this.P1.TabIndex = 1;
            this.P1.Visible = false;
            // 
            // P2
            // 
            this.P2.Location = new System.Drawing.Point(81, 28);
            this.P2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.P2.Name = "P2";
            this.P2.Size = new System.Drawing.Size(53, 78);
            this.P2.TabIndex = 1;
            this.P2.Visible = false;
            // 
            // P3
            // 
            this.P3.Location = new System.Drawing.Point(139, 28);
            this.P3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.P3.Name = "P3";
            this.P3.Size = new System.Drawing.Size(53, 78);
            this.P3.TabIndex = 2;
            this.P3.Visible = false;
            // 
            // P4
            // 
            this.P4.Location = new System.Drawing.Point(196, 28);
            this.P4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.P4.Name = "P4";
            this.P4.Size = new System.Drawing.Size(53, 78);
            this.P4.TabIndex = 3;
            this.P4.Visible = false;
            // 
            // P5
            // 
            this.P5.Location = new System.Drawing.Point(254, 28);
            this.P5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.P5.Name = "P5";
            this.P5.Size = new System.Drawing.Size(53, 78);
            this.P5.TabIndex = 4;
            this.P5.Visible = false;
            // 
            // C26
            // 
            this.C26.active = true;
            this.C26.Location = new System.Drawing.Point(725, 282);
            this.C26.Margin = new System.Windows.Forms.Padding(2);
            this.C26.Name = "C26";
            this.C26.Size = new System.Drawing.Size(53, 78);
            this.C26.TabIndex = 35;
            this.C26.Click += new System.EventHandler(this.C26_Click);
            // 
            // C13
            // 
            this.C13.active = true;
            this.C13.Location = new System.Drawing.Point(725, 186);
            this.C13.Margin = new System.Windows.Forms.Padding(2);
            this.C13.Name = "C13";
            this.C13.Size = new System.Drawing.Size(53, 78);
            this.C13.TabIndex = 31;
            this.C13.Click += new System.EventHandler(this.C13_Click);
            // 
            // C25
            // 
            this.C25.active = true;
            this.C25.Location = new System.Drawing.Point(668, 282);
            this.C25.Margin = new System.Windows.Forms.Padding(2);
            this.C25.Name = "C25";
            this.C25.Size = new System.Drawing.Size(53, 78);
            this.C25.TabIndex = 36;
            this.C25.Click += new System.EventHandler(this.C25_Click);
            // 
            // C12
            // 
            this.C12.active = true;
            this.C12.Location = new System.Drawing.Point(668, 186);
            this.C12.Margin = new System.Windows.Forms.Padding(2);
            this.C12.Name = "C12";
            this.C12.Size = new System.Drawing.Size(53, 78);
            this.C12.TabIndex = 31;
            this.C12.Click += new System.EventHandler(this.C12_Click);
            // 
            // C24
            // 
            this.C24.active = true;
            this.C24.Location = new System.Drawing.Point(610, 282);
            this.C24.Margin = new System.Windows.Forms.Padding(2);
            this.C24.Name = "C24";
            this.C24.Size = new System.Drawing.Size(53, 78);
            this.C24.TabIndex = 37;
            this.C24.Click += new System.EventHandler(this.C24_Click);
            // 
            // C11
            // 
            this.C11.active = true;
            this.C11.Location = new System.Drawing.Point(610, 186);
            this.C11.Margin = new System.Windows.Forms.Padding(2);
            this.C11.Name = "C11";
            this.C11.Size = new System.Drawing.Size(53, 78);
            this.C11.TabIndex = 31;
            this.C11.Click += new System.EventHandler(this.C11_Click);
            // 
            // C23
            // 
            this.C23.active = true;
            this.C23.Location = new System.Drawing.Point(552, 282);
            this.C23.Margin = new System.Windows.Forms.Padding(2);
            this.C23.Name = "C23";
            this.C23.Size = new System.Drawing.Size(53, 78);
            this.C23.TabIndex = 38;
            this.C23.Click += new System.EventHandler(this.C23_Click);
            // 
            // C10
            // 
            this.C10.active = true;
            this.C10.Location = new System.Drawing.Point(552, 186);
            this.C10.Margin = new System.Windows.Forms.Padding(2);
            this.C10.Name = "C10";
            this.C10.Size = new System.Drawing.Size(53, 78);
            this.C10.TabIndex = 31;
            this.C10.Click += new System.EventHandler(this.C10_Click);
            // 
            // C22
            // 
            this.C22.active = true;
            this.C22.Location = new System.Drawing.Point(494, 282);
            this.C22.Margin = new System.Windows.Forms.Padding(2);
            this.C22.Name = "C22";
            this.C22.Size = new System.Drawing.Size(53, 78);
            this.C22.TabIndex = 39;
            this.C22.Click += new System.EventHandler(this.C22_Click);
            // 
            // C9
            // 
            this.C9.active = true;
            this.C9.Location = new System.Drawing.Point(494, 186);
            this.C9.Margin = new System.Windows.Forms.Padding(2);
            this.C9.Name = "C9";
            this.C9.Size = new System.Drawing.Size(53, 78);
            this.C9.TabIndex = 31;
            this.C9.Click += new System.EventHandler(this.C9_Click);
            // 
            // C21
            // 
            this.C21.active = true;
            this.C21.Location = new System.Drawing.Point(436, 282);
            this.C21.Margin = new System.Windows.Forms.Padding(2);
            this.C21.Name = "C21";
            this.C21.Size = new System.Drawing.Size(53, 78);
            this.C21.TabIndex = 40;
            this.C21.Click += new System.EventHandler(this.C21_Click);
            // 
            // C8
            // 
            this.C8.active = true;
            this.C8.Location = new System.Drawing.Point(436, 186);
            this.C8.Margin = new System.Windows.Forms.Padding(2);
            this.C8.Name = "C8";
            this.C8.Size = new System.Drawing.Size(53, 78);
            this.C8.TabIndex = 31;
            this.C8.Click += new System.EventHandler(this.C8_Click);
            // 
            // C20
            // 
            this.C20.active = true;
            this.C20.Location = new System.Drawing.Point(379, 282);
            this.C20.Margin = new System.Windows.Forms.Padding(2);
            this.C20.Name = "C20";
            this.C20.Size = new System.Drawing.Size(53, 78);
            this.C20.TabIndex = 41;
            this.C20.Click += new System.EventHandler(this.C20_Click);
            // 
            // C7
            // 
            this.C7.active = true;
            this.C7.Location = new System.Drawing.Point(379, 186);
            this.C7.Margin = new System.Windows.Forms.Padding(2);
            this.C7.Name = "C7";
            this.C7.Size = new System.Drawing.Size(53, 78);
            this.C7.TabIndex = 31;
            this.C7.Click += new System.EventHandler(this.C7_Click);
            // 
            // C19
            // 
            this.C19.active = true;
            this.C19.Location = new System.Drawing.Point(321, 282);
            this.C19.Margin = new System.Windows.Forms.Padding(2);
            this.C19.Name = "C19";
            this.C19.Size = new System.Drawing.Size(53, 78);
            this.C19.TabIndex = 42;
            this.C19.Click += new System.EventHandler(this.C19_Click);
            // 
            // C6
            // 
            this.C6.active = true;
            this.C6.Location = new System.Drawing.Point(321, 186);
            this.C6.Margin = new System.Windows.Forms.Padding(2);
            this.C6.Name = "C6";
            this.C6.Size = new System.Drawing.Size(53, 78);
            this.C6.TabIndex = 31;
            this.C6.Click += new System.EventHandler(this.C6_Click);
            // 
            // C18
            // 
            this.C18.active = true;
            this.C18.Location = new System.Drawing.Point(263, 282);
            this.C18.Margin = new System.Windows.Forms.Padding(2);
            this.C18.Name = "C18";
            this.C18.Size = new System.Drawing.Size(53, 78);
            this.C18.TabIndex = 43;
            this.C18.Click += new System.EventHandler(this.C18_Click);
            // 
            // C5
            // 
            this.C5.active = true;
            this.C5.Location = new System.Drawing.Point(263, 186);
            this.C5.Margin = new System.Windows.Forms.Padding(2);
            this.C5.Name = "C5";
            this.C5.Size = new System.Drawing.Size(53, 78);
            this.C5.TabIndex = 31;
            this.C5.Click += new System.EventHandler(this.C5_Click);
            // 
            // C17
            // 
            this.C17.active = true;
            this.C17.Location = new System.Drawing.Point(206, 282);
            this.C17.Margin = new System.Windows.Forms.Padding(2);
            this.C17.Name = "C17";
            this.C17.Size = new System.Drawing.Size(53, 78);
            this.C17.TabIndex = 44;
            this.C17.Click += new System.EventHandler(this.C17_Click);
            // 
            // C4
            // 
            this.C4.active = true;
            this.C4.Location = new System.Drawing.Point(206, 186);
            this.C4.Margin = new System.Windows.Forms.Padding(2);
            this.C4.Name = "C4";
            this.C4.Size = new System.Drawing.Size(53, 78);
            this.C4.TabIndex = 31;
            this.C4.Click += new System.EventHandler(this.C4_Click);
            // 
            // C16
            // 
            this.C16.active = true;
            this.C16.Location = new System.Drawing.Point(148, 282);
            this.C16.Margin = new System.Windows.Forms.Padding(2);
            this.C16.Name = "C16";
            this.C16.Size = new System.Drawing.Size(53, 78);
            this.C16.TabIndex = 34;
            this.C16.Click += new System.EventHandler(this.C16_Click);
            // 
            // C3
            // 
            this.C3.active = true;
            this.C3.Location = new System.Drawing.Point(148, 186);
            this.C3.Margin = new System.Windows.Forms.Padding(2);
            this.C3.Name = "C3";
            this.C3.Size = new System.Drawing.Size(53, 78);
            this.C3.TabIndex = 30;
            this.C3.Click += new System.EventHandler(this.C3_Click);
            // 
            // C15
            // 
            this.C15.active = true;
            this.C15.Location = new System.Drawing.Point(90, 282);
            this.C15.Margin = new System.Windows.Forms.Padding(2);
            this.C15.Name = "C15";
            this.C15.Size = new System.Drawing.Size(53, 78);
            this.C15.TabIndex = 33;
            this.C15.Click += new System.EventHandler(this.C15_Click);
            // 
            // C2
            // 
            this.C2.active = true;
            this.C2.Location = new System.Drawing.Point(90, 186);
            this.C2.Margin = new System.Windows.Forms.Padding(2);
            this.C2.Name = "C2";
            this.C2.Size = new System.Drawing.Size(53, 78);
            this.C2.TabIndex = 29;
            this.C2.Click += new System.EventHandler(this.C2_Click);
            // 
            // C14
            // 
            this.C14.active = true;
            this.C14.Location = new System.Drawing.Point(32, 282);
            this.C14.Margin = new System.Windows.Forms.Padding(2);
            this.C14.Name = "C14";
            this.C14.Size = new System.Drawing.Size(53, 78);
            this.C14.TabIndex = 32;
            this.C14.Click += new System.EventHandler(this.C14_Click);
            // 
            // C1
            // 
            this.C1.active = true;
            this.C1.Location = new System.Drawing.Point(32, 186);
            this.C1.Margin = new System.Windows.Forms.Padding(2);
            this.C1.Name = "C1";
            this.C1.Size = new System.Drawing.Size(53, 78);
            this.C1.TabIndex = 28;
            this.C1.Click += new System.EventHandler(this.C1_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(813, 375);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(829, 413);
            this.MinimumSize = new System.Drawing.Size(829, 413);
            this.Name = "ClientForm";
            this.Text = "Presidents";
            this.Load += new System.EventHandler(this.SoloForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SoloForm_Paint);
            this.Moves.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Moves;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Pass;
        private System.Windows.Forms.Button Deal;
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