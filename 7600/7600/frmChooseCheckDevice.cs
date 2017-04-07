using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace _7600
{
    public partial class frmChooseCheckDevice : Form
    {
        public frmMainWindow _frmMainWindow;

        private FilesInfo filesInfo;

        private bool bNeedReload = true;
        TypingText t;
        private string listFileName = "bin/ListFiles.xml";

        public frmChooseCheckDevice()
        {
            InitializeComponent();
            //Init();
        }

        private void Init()
        {
            bNeedReload = false;

            if (Utils.isFileExist(listFileName))
            {
                filesInfo = OpenListFiles(listFileName);
                ShowFilesInfo(filesInfo);

                pict.SizeMode = PictureBoxSizeMode.StretchImage;
                pict.Image = (Image)Properties.Resources.chooseDevInitImage;
                t = new TypingText(Properties.Resources.comment_chooseDevice_Initial, txtComment);
                t.StartTyping();
            }
            else { }
        }

        public FilesInfo OpenListFiles(string filename)
        {
             FilesInfo files = _frmMainWindow.xml.OpenListFiles(Application.StartupPath + "//" + filename);
             return files;
            //return new FilesInfo();
        }

        private void ShowFilesInfo(FilesInfo files)
        {
            panelListFiles.Controls.Clear();
            panelListFiles.SuspendLayout();
            for (int i = 0; i < files.listFileInfo.Count; i++)
            {
                Button btn = new Button();
                string name = files.listFileInfo[i].Name;
                if (name.Length > 20) name = name.Substring(0, 20) + "..";
                btn.Text = name;

                btn.Name = "btn" + i.ToString();
                btn.Click += btn_Click;
                btn.MouseHover += btn_MouseHover;
                btn.MouseEnter += btn_MouseEnter;
                btn.MouseLeave += btn_MouseLeave;
                btn.Font = new Font("Verdana", 16);
                btn.Left = 10;
                btn.Top = i * 50;
                btn.Height = 42;
                btn.Width = 300;

                panelListFiles.Controls.Add(btn);
            }
            panelListFiles.ResumeLayout();
        }

        void btn_MouseLeave(object sender, EventArgs e)
        {
            t.StopTyping();
            pict.Image = (Image)Properties.Resources.chooseDevInitImage;
            t = new TypingText(Properties.Resources.comment_chooseDevice_Initial, txtComment);
            t.StartTyping();
        }

        void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Name.Substring(3, btn.Name.Length - 3));
            t.StopTyping();
            t = new TypingText(filesInfo.listFileInfo[index].Comment, txtComment);

            if (Utils.isFileExist(filesInfo.listFileInfo[index].Imagelink))
                pict.Image = Image.FromFile(filesInfo.listFileInfo[index].Imagelink);
            else
                pict.Image = Properties.Resources.PictLoadError;


            t.StartTyping();
        }

        void btn_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Name.Substring(3, btn.Name.Length - 3));
            tip.SetToolTip(btn, filesInfo.listFileInfo[index].Name);
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Name.Substring(3, btn.Name.Length - 3));

            if (Utils.isFileExist(filesInfo.listFileInfo[index].Filename))
            {
                Process.Start(Application.StartupPath + "//" + filesInfo.listFileInfo[index].Filename);
            }
            else
                MessageBox.Show("Файл " + filesInfo.listFileInfo[index].Filename + " не найден!", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmChooseCheckDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            bNeedReload = true;
            _frmMainWindow._frmChooseCheckDevice.Hide();
            _frmMainWindow.Show();
        }

        private void btnProgramm1_Click(object sender, EventArgs e)
        {
            _frmMainWindow._frmChooseCheckDevice.Hide();
            _frmMainWindow._frmMain.Show();
        }

        private void btnProgramm1_MouseEnter(object sender, EventArgs e)
        {
            t.StopTyping();
            pict.Image = (Image)Properties.Resources.frmMainPicture;
            t = new TypingText(Properties.Resources.comment_frmMainMouseEnter, txtComment);
            t.StartTyping();
            //txtComment.Text = Properties.Resources.comment_frmMainMouseEnter; //"Сейчас вы наблюдаете комментарий относительно запуска формы frmMain!";
        }

        private void btnProgramm1_MouseLeave(object sender, EventArgs e)
        {
            //pict.Image = null;
            //txtComment.Text = string.Empty;
            t.StopTyping();

            pict.Image = (Image)Properties.Resources.chooseDevInitImage;
            t = new TypingText(Properties.Resources.comment_chooseDevice_Initial, txtComment);
            t.StartTyping();
        }

        private void btnProgramm2_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void btnProgramm3_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void btnProgramm2_MouseEnter(object sender, EventArgs e)
        {
            t.StopTyping();
            pict.Image = (Image)Properties.Resources.CalcPict;
            t = new TypingText(Properties.Resources.comment_Calc, txtComment);
            t.StartTyping();
            //txtComment.Text = Properties.Resources.comment_Calc; //"Сейчас вы наблюдаете комментарий относительно запуска формы frmMain!";
        }

        private void btnProgramm3_MouseEnter(object sender, EventArgs e)
        {
            t.StopTyping();
            pict.Image = (Image)Properties.Resources.NotepadPict;
            t = new TypingText(Properties.Resources.comment_Notepad, txtComment);
            t.StartTyping();
            //txtComment.Text = Properties.Resources.comment_Notepad; //"Сейчас вы наблюдаете комментарий относительно запуска формы frmMain!";
        }

        private void frmChooseCheckDevice_Activated(object sender, EventArgs e)
        {
            if (bNeedReload)
                Init();
        }
    }
}
