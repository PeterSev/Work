using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7637_WS4
{
    public partial class frmTests : Form
    {
        public frmMain _frmMain;
        Board board = null;
        List<TestInfo> listTests = null;
        string listTestsFileName = "TestsInfo.xml";
        public string ListTestsFileName
        {
            get { return listTestsFileName; }
        }

        private bool bNeedReload = true;



        public frmTests()
        {
            InitializeComponent();
        }

        void Init()
        {
            bNeedReload = false;
            board = _frmMain.curBoard;

            this.Text = "Tests " + board.Name;
            this.BackColor = Color.RoyalBlue;
            txtComment.BackColor = Color.LightBlue;

            btnBU.Image = Properties.Resources.pict_BU;
            btnBZ.Image = Properties.Resources.pict_BZ;
            btnBPPP.Image = Properties.Resources.pict_BPPP;
            btnPP.Image = Properties.Resources.pict_PP;

            lblBU.ForeColor = lblBZ.ForeColor = lblBPPP.ForeColor = lblPP.ForeColor = Color.White;


            /*if (Utils.isFileExist(board.Catalog + "/" + ListTestsFileName))
            {
                listTests = XMLParser.OpenListTests(board.Catalog + "/" + ListTestsFileName);
                ShowTests(listTests);
            }
            else
            {
                MessageBox.Show("File " + board.Catalog + "/" + ListTestsFileName + " isn't found!", "Load error");
                this.Close();
            }*/
        }

        void ShowTests(List<TestInfo> list)
        {
           /* var bz = from item in listTests
                     where item.Name == "БЗ"
                     select item.
            btnBZ.Text = listTests.Select(n => n.Name).*/

            var s = from t in listTests
                    where t.Name == "БЗ"
                    select t.Imagelink;

            string fileName = "bin/" + s.ToList()[0];
            if(Utils.isFileExist(fileName))
                btnBZ.Image = Image.FromFile(fileName);
        }

        private void frmTests_Activated(object sender, EventArgs e)
        {
            if (bNeedReload)
                Init();
        }

        private void frmTests_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            bNeedReload = true;
            _frmMain._frmTests.Hide();
            _frmMain.Show();
        }

        private void btnBZ_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            _frmMain._frmBZ_Help.Show();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string font = "Microsoft Sans Serif";
            float font_size_large = 16;
            float font_size_small = 10;

            switch (btn.Name.Substring(3, btn.Name.Length - 3))
            {
                case "BZ":
                    lblBZ.Font = new Font(font, font_size_large, FontStyle.Bold);
                    lblBU.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblPP.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblBPPP.Font = new Font(font, font_size_small, FontStyle.Regular);
                    txtComment.Text = Properties.Resources.comment_BZ;

                    break;
                case "BU":
                    lblBZ.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblBU.Font = new Font(font, font_size_large, FontStyle.Bold);
                    lblPP.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblBPPP.Font = new Font(font, font_size_small, FontStyle.Regular);
                    txtComment.Text = Properties.Resources.comment_BU;

                    break; 
                case "BPPP":
                    lblBZ.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblBU.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblPP.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblBPPP.Font = new Font(font, font_size_large, FontStyle.Bold);
                    txtComment.Text = Properties.Resources.comment_BPPP;

                    break;
                case "PP":
                    lblBZ.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblBU.Font = new Font(font, font_size_small, FontStyle.Regular);
                    lblPP.Font = new Font(font, font_size_large, FontStyle.Bold);
                    lblBPPP.Font = new Font(font, font_size_small, FontStyle.Regular);
                    txtComment.Text = Properties.Resources.comment_PP;

                    /*lblBZ.ForeColor = Color.Black;
                    lblBU.ForeColor = Color.Black;
                    lblPP.ForeColor = Color.White;
                    lblBPPP.ForeColor = Color.Black;*/
                    break;
            }
        }
    }
}
