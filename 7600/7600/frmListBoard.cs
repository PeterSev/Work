using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _7600
{
    public partial class frmListBoard : Form
    {
        public frmMainWindow _frmMainWindow;

        private BoardsInfo boardsInfo;
        public BoardInfo chooseBoard;

        private bool bNeedReload = true;
        private string listBoardFileName = "bin\\ListBoard.xml";
        public string ListBoardFileName
        {
            get { return listBoardFileName; }
        }
        TypingText t;
        public frmListBoard()
        {
            InitializeComponent();
        }

        private void Init()
        {
            bNeedReload = false;
            if (Utils.isFileExist(ListBoardFileName))
            {
                
                boardsInfo = OpenListBoards(ListBoardFileName);
                ShowBoardInfo(boardsInfo);

                pict.SizeMode = PictureBoxSizeMode.StretchImage;
                pict.Image = (Image)Properties.Resources.listBoardInitmage;
                t = new TypingText(Properties.Resources.comment_ListBoard_Initial, txtComment);
                t.StartTyping();
            }
            else
            {
                MessageBox.Show("Файл списка плат не найден!", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        public BoardsInfo OpenListBoards(string filename)
        {
            BoardsInfo boards = _frmMainWindow.xml.OpenListBoards(Application.StartupPath + "//" + filename);

            return boards;
        }

        private void ShowBoardInfo(BoardsInfo boards)
        {
            panelListBoard.Controls.Clear();
            panelListBoard.SuspendLayout();
            for (int i = 0; i < boards.listBoardInfo.Count; i++)
            {
                Button btn = new Button();

                string name = boards.listBoardInfo[i].Name;
                //if (name == _frmMainWindow.selfDiagBoardName) continue;

                if (name.Length > 20) name = name.Substring(0, 20) + "..";
                btn.Text = name;

                btn.Name = "btn" + i.ToString();
                btn.Click += btn_Click;
                btn.MouseEnter += btn_MouseEnter;
                btn.MouseLeave += btn_MouseLeave;
                btn.MouseHover += btn_MouseHover;
                btn.Font = new Font("Verdana", 16);
                btn.Left = 10;
                btn.Top = i * 50 + 0;
                btn.Height = 42;
                btn.Width = 300;

                

                
                panelListBoard.Controls.Add(btn);
                
            }
            panelListBoard.ResumeLayout();
        }

        void btn_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Name.Substring(3, btn.Name.Length - 3));
            tip.SetToolTip(btn, boardsInfo.listBoardInfo[index].Name);
        }

        void btn_MouseLeave(object sender, EventArgs e)
        {
            t.StopTyping();
            pict.Image = (Image)Properties.Resources.listBoardInitmage;
            t = new TypingText(Properties.Resources.comment_ListBoard_Initial, txtComment);
            t.StartTyping();
        }

        void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Name.Substring(3, btn.Name.Length - 3));
            t.StopTyping();
            t = new TypingText(boardsInfo.listBoardInfo[index].Comment, txtComment);

            if (Utils.isFileExist(boardsInfo.listBoardInfo[index].Imagelink))
                pict.Image = Image.FromFile(boardsInfo.listBoardInfo[index].Imagelink);
            else
                pict.Image = Properties.Resources.PictLoadError;


            t.StartTyping();
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Name.Substring(3, btn.Name.Length - 3));
            chooseBoard = boardsInfo.listBoardInfo[index];

            t.StopTyping();
            _frmMainWindow._frmListBoard.Hide();
            _frmMainWindow._frmHelpBoard.Show();
        }

        private void frmListBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            bNeedReload = true;
            _frmMainWindow._frmListBoard.Hide();
            _frmMainWindow.Show();
            
        }

        private void frmListBoard_Activated(object sender, EventArgs e)
        {
            if(bNeedReload)
                Init();
        }
    }
}
