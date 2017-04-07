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
    public partial class frmBoardShowPDF : Form
    {
        public frmMainWindow _frmMainWindow;
        private BoardInfo board;    //информация о выбранной плате
        private bool bNeedReload = true;    //признак необходимости переинициализации.
        private string sTitle = "Схема платы ";

        public frmBoardShowPDF()
        {
            InitializeComponent();
        }

        private void Init()
        {
            bNeedReload = false;
            board = _frmMainWindow._frmListBoard.chooseBoard;
            this.Text = sTitle + board.Name;

            string filename = board.Filename.Substring(0,board.Filename.Length - 3) + "pdf";

            if (Utils.isFileExist(filename))
            {
                filename = Application.StartupPath + "\\" + filename;

                System.Diagnostics.Process process = new System.Diagnostics.Process();


                process.StartInfo.FileName = filename;

                process.Start();


                /*axAcroPDF1.LoadFile(filename);
                axAcroPDF1.src = filename;
                axAcroPDF1.setShowToolbar(true);
                axAcroPDF1.setView("FitH");
                axAcroPDF1.setLayoutMode("SinglePage");

                axAcroPDF1.Show();*/
            }
            else
            {
                MessageBox.Show("Файл PDF не найден", "Ошибка загрузки");
                //this.Close();
            }

        }

        private void frmBoardShowPDF_FormClosing(object sender, FormClosingEventArgs e)
        {
            bNeedReload = true;
            e.Cancel = true;
            _frmMainWindow._frmBoardShowPDF.Hide();
            _frmMainWindow._frmBoardSignals.Show();
        }

        
        private void frmBoardShowPDF_Activated(object sender, EventArgs e)
        {
            if (bNeedReload)
                Init();
        }
    }
}
