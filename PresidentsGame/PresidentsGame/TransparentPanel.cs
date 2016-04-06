using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PresidentsGame
{
    class TransparentMessagePanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        delegate void nDelegate(); 

        public void ToggleActive()
        {
            if (this.InvokeRequired == false)
            {
                Graphics g = this.CreateGraphics();

                Pen myPen;

                if (this.active == true)
                {
                    myPen = new Pen(SystemColors.Control, 5);
                    this.active = false;
                }
                else
                {
                    myPen = new Pen(SystemColors.ActiveCaptionText, 5);
                    this.active = true;
                }

                g.DrawRectangle(myPen, g.VisibleClipBounds.X, g.VisibleClipBounds.Y, g.VisibleClipBounds.Width, g.VisibleClipBounds.Height);
            }
            else
            {
                nDelegate tactive =
                 new nDelegate(ToggleActive);
                this.Invoke(tactive);
            }
        }

        delegate void peDelegate(PaintEventArgs e);

        protected override void OnPaint(PaintEventArgs e)
        {

            if (this.InvokeRequired == false)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;

                Pen myPen;

                myPen = new Pen(SystemColors.Control, 5);
                this.active = false;

                g.DrawRectangle(myPen, g.VisibleClipBounds.X, g.VisibleClipBounds.Y, g.VisibleClipBounds.Width, g.VisibleClipBounds.Height);
            }
            else
            {
                peDelegate panelp =
                 new peDelegate(OnPaint);
                this.Invoke(panelp, new object[] { e });
            }
        }

        public bool active { get; set; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TransparentMessagePanel
            // 
            this.AutoSize = true;
            this.ResumeLayout(false);

        }

    }

}
