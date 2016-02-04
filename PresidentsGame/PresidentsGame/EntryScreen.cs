using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresidentsGame
{
    public partial class EntryScreen : Form
    {
        public EntryScreen()
        {
            InitializeComponent();
        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            ClientForm C1 = new ClientForm();
            C1.Show();
            this.Hide();
        }
    }
}
