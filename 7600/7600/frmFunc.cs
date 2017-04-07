using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _7600
{
    public partial class frmFunc : Form
    {
        public frmBoardSignals _frmBoardSignal;
        BoardSignal signal;
        private bool bNeedReload = true;
        public frmFunc()
        {
            InitializeComponent();

            
        }

        void Init()
        {
            bNeedReload = false;
            //signal = _frmBoardSignal.signal;
            this.Text = _frmBoardSignal.signal.Name;
            numCur.Value = _frmBoardSignal.dataFunc.curVal;
            numMin.Value = _frmBoardSignal.dataFunc.minVal;
            numMax.Value = _frmBoardSignal.dataFunc.maxVal;
            numDiscr.Value = _frmBoardSignal.dataFunc.step;
            chkOn.Checked = _frmBoardSignal.dataFunc.bWork;
            chkNoDeset.Checked = _frmBoardSignal.dataFunc.bNoDeset;
        }

        private void frmFunc_Activated(object sender, EventArgs e)
        {
            if (bNeedReload)
                Init();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //_frmBoardSignal.dataFunc.curVal = (int)numCur.Value;
            _frmBoardSignal.dataFunc.minVal = (uint)numMin.Value;
            _frmBoardSignal.dataFunc.maxVal = (int)numMax.Value;
            _frmBoardSignal.dataFunc.step = (uint)numDiscr.Value;
            _frmBoardSignal.dataFunc.bWork = chkOn.Checked;
            _frmBoardSignal.dataFunc.bNoDeset = chkNoDeset.Checked;
            Close();
        }

        private void btnResetValue_Click(object sender, EventArgs e)
        {
            _frmBoardSignal.dataFunc.ResetValue();
        }
    }
}
