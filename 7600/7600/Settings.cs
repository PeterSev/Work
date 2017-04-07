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
    public partial class frmSettings : Form
    {
        public frmMain formMain;
        public frmMainWindow _frmMainWindow;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void chkIPFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIPFrom.Checked)
            {
                txtIPAddress2.Text = "IPAddress.Any";
                txtIPAddress2.Enabled = false;
            }
            else
            {
                txtIPAddress2.Text = "192.168.0.2";
                txtIPAddress2.Enabled = true;
            }

        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("Для применения изменений (если они были сделаны) требуется перезагрузить программу!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            e.Cancel = true;

            //_frmMainWindow.UDPConnect();
            _frmMainWindow._frmSettings.Hide();
            _frmMainWindow.Show();
        }
    }
}
