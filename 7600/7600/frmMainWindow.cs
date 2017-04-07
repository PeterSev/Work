using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace _7600
{
    public partial class frmMainWindow : Form
    {
        //ссылки на дочерние формы
        public frmMainWindow _frmMainWindow;
        public frmMain _frmMain;
        public frmSettings _frmSettings;
        public frm7192 _frm7192;
        public frmChooseCheckDevice _frmChooseCheckDevice;
        public frmListBoard _frmListBoard;
        public frmHelpBoard _frmHelpBoard;
        public frmBoardSignals _frmBoardSignals;
        public frmBoardShowPDF _frmBoardShowPDF;
        public frmCurrents _frmCurrents;

        string sTitleMainForm = "7600.07.74.000-02";
        string sVersion = "2.3_Repo";
        string sFileStandSignals = "bin\\StandSignals.xml";
        string sFileSettings = "bin\\settings7600.xml";
        public string selfDiagBoardName = "Самодиагностика";

        public XMLParser xml;
        public StandSignals standSignals;


        private SettingsXML sets;

        public Udp udpPU, udpPK, udpPTVC;
        public volatile bool bCommandBusyPK = false;
        public volatile bool bCommandBusyPU = false;
        public volatile bool bCommandBusyPTVC = false;

        public bool bSelfDiag = false;

        //последние отправленные сигналы по каждому порту
        
        public frmMainWindow()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            _frmChooseCheckDevice = new frmChooseCheckDevice();
            _frmSettings = new frmSettings();
            _frmMain = new frmMain();
            _frmListBoard = new frmListBoard();
            _frmHelpBoard = new frmHelpBoard();
            _frmBoardSignals = new frmBoardSignals();
            _frmBoardShowPDF = new frmBoardShowPDF();
            _frmCurrents = new frmCurrents();
            _frmMainWindow = _frmChooseCheckDevice._frmMainWindow = _frmSettings._frmMainWindow = 
                _frmMain._frmMainWindow = _frmListBoard._frmMainWindow = _frmHelpBoard._frmMainWindow = 
                _frmBoardSignals._frmMainWindow = 
                _frmBoardShowPDF._frmMainWindow = 
                this;

            sets = new SettingsXML();
            xml = new XMLParser();


            this.Text = sTitleMainForm + sVersion;

            if (Utils.isFileExist(sFileStandSignals))
                standSignals = xml.openStandXML(Application.StartupPath + "//" + sFileStandSignals);
            else
            {
                MessageBox.Show("Файл стендовых сигналов " + sFileStandSignals + " не найден! Приложение не сможет функционировать и будет закрыто.", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Application.Exit();
                Environment.Exit(0);
            }
        }

        #region В секции все функции, относящиеся к Коннекту
        public void UDPConnect()
        {
            if (udpPU == null && udpPK == null && udpPTVC == null)
            {
                try
                {
                    udpPU = new Udp(
                        new System.Net.IPAddress(getIPAddr(1)),
                        int.Parse(_frmMainWindow._frmSettings.txtIPPort1.Text));
                    udpPK = new Udp(
                        new System.Net.IPAddress(getIPAddr(1)),
                        int.Parse(_frmMainWindow._frmSettings.txtIPPort2.Text));
                    udpPTVC = new Udp(
                        new System.Net.IPAddress(getIPAddr(1)),
                        int.Parse(_frmMainWindow._frmSettings.txtIPPort3.Text));

                    if (udpPU.Open() && udpPK.Open() && udpPTVC.Open())
                    {
                        _frmBoardSignals.countSuccessPU = _frmBoardSignals.countSuccessPK = _frmBoardSignals.countSuccessPTVC =
                            _frmBoardSignals.countTimeoutPU = _frmBoardSignals.countTimeoutPK = _frmBoardSignals.countTimeoutPTVC = 0;

                        udpPU.received += _frmBoardSignals.udp_receivedPU;
                        udpPK.received += _frmBoardSignals.udp_receivedPK;
                        udpPTVC.received += _frmBoardSignals.udp_receivedPTVC;

                        //btnConnect.Text = "Стоп обмен";
                        //btnConnect.BackColor = Color.LightGreen;

                        this.Text = sTitleMainForm + " | Подключение установлено. " + sVersion;
                        panel1.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        //MessageBox.Show("Ошибка открытия порта!", "DSP Firmware");
                        //btnConnect.Text = "Старт обмен";
                        //btnConnect.BackColor = Color.Transparent;
                        this.Text = sTitleMainForm + " | Ошибка открытия портов. " + sVersion;
                        panel1.BackColor = Color.Red;

                        udpPU = null;
                        udpPK = null;
                        udpPTVC = null;
                    }
                }
                catch 
                {
                    if (udpPU != null) { udpPU.Close(); udpPU = null; }
                    if (udpPK != null) { udpPK.Close(); udpPK = null; }
                    if (udpPTVC != null) { udpPTVC.Close(); udpPTVC = null; }
                    this.Text = sTitleMainForm + " | Ошибка соединения. " + sVersion; //MessageBox.Show(ex.Message + "\n\n\nНе удалось установить соединение.", "UDP Connect"); 
                    panel1.BackColor = Color.Red;
                }
            }
            else
            {
                try
                {
                    udpPU.Close();
                    udpPK.Close();
                    udpPTVC.Close();

                    this.Text = sTitleMainForm + " | Подключение не установлено. " + sVersion;
                    panel1.BackColor = Color.Transparent;

                    bCommandBusyPK = bCommandBusyPTVC = bCommandBusyPU = false;
                    
                    udpPU = null;
                    udpPK = null;
                    udpPTVC = null;
                }
                catch
                { 
                    udpPU = null; 
                    udpPK = null; 
                    udpPTVC = null;
                    this.Text = sTitleMainForm + " | Ошибка закрытия соединения. " + sVersion;
                    panel1.BackColor = Color.Red;
                    //MessageBox.Show(ex.Message, "UDPConnect"); 
                }
            }
        }

        private byte[] getIPAddr(int param)
        {
            byte[] buf = new byte[4];
            string[] text = new string[] { };
            buf.Max(a => (byte)a);
            switch (param)
            {
                case 1: text = _frmSettings.txtIPAddress1.Text.Split('.'); break;
                case 2: text = _frmSettings.txtIPAddress2.Text.Split('.'); break;
            }

            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = Convert.ToByte(text[i]);
            }
            return buf;
        }
        #endregion

        private void btnCheckDevices_Click(object sender, EventArgs e)
        {
            _frmChooseCheckDevice.Show();
            this.Hide();
        }

        private void frmMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.DoEvents();
            if (sets != null)
                SaveSettings();

            foreach (var process in System.Diagnostics.Process.GetProcesses())
            {
                // || process.ProcessName.Contains("armsvc")
                if (process.ProcessName.Contains("Acro"))
                {
                    process.Kill();
                }
            }

            Environment.Exit(0);
            /*_frmBoardShowPDF.Dispose();
            while (!_frmBoardShowPDF.IsDisposed) Application.DoEvents();

            this.Dispose();*/
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            _frmSettings.Show();
            this.Hide();
        }

        #region Сохранение настроек в ХМЛ

        private void LoadSettings()
        {
            try
            {
                sets = LoadList(Application.StartupPath + "\\" + sFileSettings);

                _frmSettings.txtIPAddress1.Text = sets.IpAddressTo;
                _frmSettings.txtIPPort1.Text = sets.IpPortTo1;
                _frmSettings.txtIPPort2.Text = sets.IpPortTo2;
                _frmSettings.txtIPPort3.Text = sets.IpPortTo3;

                /*frmSetts.txtIPPortBAD.Text = sets.IpPortFrom;
                frmSetts.chkIPFrom.Checked = sets.BIPAddrFromAny;
                if (frmSetts.chkIPFrom.Checked) { frmSetts.txtIPAddress2.Text = "IPAddress.Any"; }
                else frmSetts.txtIPAddress2.Text = sets.IpAddressFrom;*/
            }
            catch
            {
                MessageBox.Show("Файл настроек XML не может быть загружен. Использованы параметры по умолчанию.", "Ошибка открытия файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sets = new SettingsXML();
                _frmSettings.txtIPAddress1.Text = "192.168.0.1";
                _frmSettings.txtIPPort1.Text = "50000";
                _frmSettings.txtIPPort2.Text = "50001";
                _frmSettings.txtIPPort3.Text = "50002";
                /*frmSetts.txtIPAddress2.Text = "192.168.0.2";
                frmSetts.txtIPPortBAD.Text = "50000";
                frmSetts.chkIPFrom.Checked = false;*/
            }
        }

        private void SaveSettings()
        {
            sets.IpAddressTo = _frmSettings.txtIPAddress1.Text;
            sets.IpPortTo1 = _frmSettings.txtIPPort1.Text;
            sets.IpPortTo2 = _frmSettings.txtIPPort2.Text;
            sets.IpPortTo3 = _frmSettings.txtIPPort3.Text;

            /*sets.IpAddressFrom = frmSetts.txtIPAddress2.Text;
            sets.IpPortFrom = frmSetts.txtIPPortBAD.Text;
            sets.BIPAddrFromAny = frmSetts.chkIPFrom.Checked;*/
            SaveList(Application.StartupPath + "\\" + sFileSettings, sets);
        }

        private SettingsXML LoadList(string fileName)
        {
            XmlSerializer writer = new XmlSerializer(typeof(SettingsXML));
            using (TextReader tr = new StreamReader(fileName))
            {
                return (SettingsXML)writer.Deserialize(tr);
            }
        }

        private void SaveList(string fileName, SettingsXML obj)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(SettingsXML));
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    writer.Serialize(tw, obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured while saving XML-file");
            }
        }

        #endregion

        private void frmMainWindow_Load(object sender, EventArgs e)
        {
            LoadSettings();
            UDPConnect();
        }

        private void btnCheckBoards_Click(object sender, EventArgs e)
        {
            _frmListBoard.Show();
            this.Hide();
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UDPConnect();
        }

        private void btnSelfDiagnostic_Click(object sender, EventArgs e)
        {
            if (Utils.isFileExist(_frmListBoard.ListBoardFileName))
            {
                bSelfDiag = true;
                BoardsInfo boards = _frmListBoard.OpenListBoards(_frmListBoard.ListBoardFileName);
                for (int i = 0; i < boards.listBoardInfo.Count; i++)
                {
                    if (boards.listBoardInfo[i].Name == selfDiagBoardName)
                    {
                        _frmListBoard.chooseBoard = boards.listBoardInfo[i];
                        break;
                    }
                }

                if (_frmListBoard.chooseBoard!= null &&_frmListBoard.chooseBoard.Name == selfDiagBoardName)
                {
                    //this.Hide();
                    //_frmBoardSignals.Show();
                    this.Hide();
                    
                    _frmHelpBoard.Show();
                }
                else
                    MessageBox.Show("В списке плат не найден файл самодиагностики", "Ошибка загрузки платы", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Файл списка плат не найден!", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
