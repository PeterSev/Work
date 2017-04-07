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
    public partial class frmHelpBoard : Form
    {
        public frmMainWindow _frmMainWindow;

        private BoardInfo board;
        private TypingText t;
        private int indexPic = 0;

        private bool bNeedReload = true;
        public frmHelpBoard()
        {
            InitializeComponent();
        }

        void Init()
        {
            bNeedReload = false;
            indexPic = 0;
            pict.SizeMode = PictureBoxSizeMode.StretchImage;
            board = _frmMainWindow._frmListBoard.chooseBoard;
            this.Text = "Подключение платы " + board.Name;

            /*if (Utils.isFileExist(board.Imagelink_help))
                pict.Image = Image.FromFile(board.Imagelink_help);
            else
                pict.Image = Properties.Resources.PictLoadError;*/

            ShowPicture(indexPic);

            t = new TypingText(board.Comment_help, txtComment);
            t.StartTyping();
        }

        private void frmHelpBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            t.StopTyping();
            bNeedReload = true;
            _frmMainWindow._frmHelpBoard.Hide();

            if (_frmMainWindow.bSelfDiag)
            {
                _frmMainWindow.bSelfDiag = false;
                _frmMainWindow.Show();
            }
            else
                _frmMainWindow._frmListBoard.Show();
        }

        private void frmHelpBoard_Activated(object sender, EventArgs e)
        {
            if (bNeedReload)
                Init();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bNeedReload = true;
            _frmMainWindow._frmHelpBoard.Hide();
            //_frmMainWindow._frmBoardSignals = new frmBoardSignals();
            _frmMainWindow._frmBoardSignals.Show();
        }

        private void lblRight_Click(object sender, EventArgs e)
        {
            indexPic++;
            if (indexPic > board.ListImageLink_Help.Count - 1) indexPic = 0;
            ShowPicture(indexPic);
        }

        private void ShowPicture(int index)
        {
            if (board.ListImageLink_Help.Count > 0 && Utils.isFileExist(board.ListImageLink_Help[index]))
                pict.Image = Image.FromFile(board.ListImageLink_Help[index]);
            else
                pict.Image = Properties.Resources.PictLoadError;

            lblNum.Text = (index+1).ToString() + " из " + board.ListImageLink_Help.Count;
        }

        private void lblLeft_Click(object sender, EventArgs e)
        {
            indexPic--;
            if (indexPic < 0) indexPic = board.ListImageLink_Help.Count - 1;
            ShowPicture(indexPic);
        }
    }
}
