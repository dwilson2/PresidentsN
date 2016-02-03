using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresidentsN
{
    public partial class EntryScreen : Form
    {
        public EntryScreen()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            PlayerForm C1 = new PlayerForm();
            this.Hide();
            C1.ShowDialog();
        }
    }
}
