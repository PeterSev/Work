using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7600
{
    public partial class frmCurrents : Form
    {
        public frmMainWindow _frmMainWindow;


        public frmCurrents()
        {
            InitializeComponent();
        }

        private void frmCurrents_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
