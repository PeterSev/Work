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
using System.Collections;
using System.Threading;

namespace _7600
{
    public enum BUF_TO_SHOW { TX, RX };
    public enum SET_OR_GET { SET, GET, OTHER, DESET};
    public enum COMTYPE {CONFIG_RS, FREE, SIGNAL };
    public enum PORT {PU = 55055, PK = 55056, PTVC = 55054};

    public partial class frmMain : Form
    {
        //public frmSettings frmSetts;
        //private Thread threadTyping;
        public frmMainWindow _frmMainWindow;
        //private SettingsXML sets;
        private Udp udpPU, udpPK, udpPTVC;
        private Command com_out;
        private bool bCommandBusy = false;
        private int curGroupIndex1 = 0, curGroupIndex2 = 0, curGroupIndex3 = 0;  //текущие индексы списков групп для параллельного тестирования

        //private XMLParser xml = new XMLParser();

        private TestBoard testBoard;
        //private StandSignals standSignals;
        private List<GroupTestSignal> listGroupTestSignals1, listGroupTestSignals2, listGroupTestSignals3;  //списки групп дял параллельного тестирования

        //последние отправленные сигналы по каждому порту
        BoardSignal lastSignal1, lastSignal2, lastSignal3;

        private uint cntTimeouts, cntSuccess; 

        public frmMain()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            //frmSetts = new frmSettings();
            //sets = new SettingsXML();
            com_out = null;
            //frmSetts.formMain = this;
            testBoard = null;
            //standSignals = null;
            listGroupTestSignals1 = new List<GroupTestSignal>();
            listGroupTestSignals2 = new List<GroupTestSignal>();
            listGroupTestSignals3 = new List<GroupTestSignal>();

            //threadTyping = new Thread(new ThreadStart(TypingText));

            cntSuccess = cntTimeouts = 0;

            cmbAddr.SelectedIndex = 0;
            cmbSpeedUart.SelectedIndex = 0;
            cmbParity.SelectedIndex = 0;
            cmbStopBits.SelectedIndex = 0;
        }

        void udp_receivedPU(Command com) 
        {
            Invoke((MethodInvoker)delegate
            {
                //отображаем буфер пришедшей команды
                ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.RX);

                //анализируем характер ответа на последнюю отправленную команду
                switch (com.set_or_get)
                {
                    case SET_OR_GET.SET:    //если ответ пришел на СЕТ
                        {
                            //стартуем таймер, который оттикает DELAY и стартанет запрос GET
                            StartTimerPU(com);
                        }
                        break;
                    case SET_OR_GET.GET:    //если ответ пришел на ГЕТ
                        {
                            //индекс сигнала-элемента среди списка сигналов списка SCENARIO.TEST
                            int indx;// = com.index;

                            //значение группы
                            uint groupVal = 0;

                            //индикатор, к которому будем обращаться при анализе ответной посылки
                            Button ind;// = (Button)grpSignals.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            //копируем буфер пришедшей команды в групповой буфер секции GET
                            lastSignal1.get.source.group.valueRX = com.data;

                            //Собираем из буфера число - значение группы
                            for (int i = 0; i < lastSignal1.get.source.group.valueRX.Length; i++)
                            {
                                groupVal += (uint)(lastSignal1.get.source.group.valueRX[i] << (8 * i));
                            }

                            //стендовый базовый сигнал, выдираемый из группы последнего выбранного сигнала-параметра
                            BaseSignal baseSign; 

                            //пробегаемся по всем базовым сигналам текущей группы
                            for (int i = 0; i < lastSignal1.get.source.group.baseSignals.Count; i++)
                            {
                                if (!lastSignal1.get.source.group.baseSignals[i].isRS())
                                {
                                    //выдернули сигнал из списка группы
                                    baseSign = (BaseSignal)lastSignal1.get.source.group.baseSignals[i];

                                    //проверяем наличие текущего базового сигнала в списке SCENARIO.TEST и получаем его индекс в этом списке.
                                    indx = isTestSignal(baseSign);
                                    if (indx != -1)
                                    {
                                        //получаем ссылку на индикатор-лампочку по индексу
                                        ind = (Button)panelTestSignals.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                                        //проверяем наличие сигнала в групповом значении
                                        if ((groupVal & baseSign.iValue) != 0)
                                            ind.BackColor = Color.Green;
                                        else
                                            ind.BackColor = Color.White;
                                    }
                                }
                            }
                        }
                        break;
                    case SET_OR_GET.DESET:  //Если ответ пришел на ДЕСЕТ
                        {
                            int indx = com.index;
                            uint groupVal = 0;
                            Button ind = (Button)panelTestSignals.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            lastSignal1.set.source.group.valueRX = com.data;
                            for (int i = 0; i < lastSignal1.set.source.group.valueRX.Length; i++)
                            {
                                groupVal += (uint)(lastSignal1.set.source.group.valueRX[i] << (8 * i));
                            }

                            if ((groupVal & lastSignal1.set.valueon) != 0)
                                ind.BackColor = Color.Green;
                            else
                                ind.BackColor = Color.White;
                            //Инициируем запуск еще одного GET для считывания выключенного сигнала 
                            StartTimerPU(com);
                        }
                        break;
                    case SET_OR_GET.OTHER:
                        break;
                }


                if (com.result == CommandResult.Success) cntSuccess++;
                if (com.result == CommandResult.Timeout) cntTimeouts++;

                lblStatus.Text = String.Format("Статус обмена - {0}", com.result.ToString());
                lblTimeOuts.Text = String.Format("Таймауты посылки - {0}", cntTimeouts.ToString());
                lblSuccess.Text = String.Format("Успешные посылки - {0}", cntSuccess.ToString());
            });

            bCommandBusy = false;
        }
        void StartTimerPU(Command com)
        {
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += tmr_TickPU;
            tmr.Interval = lastSignal1.Delay;
            tmr.Tag = com.index;
            curGroupIndex1 = 0;
            tmr.Start();
        }
        void tmr_TickPU(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = (System.Windows.Forms.Timer)sender;
            int ind = (int)tmr.Tag;
            tmr.Stop();
            //CreateAndSendSignal(lastSignal1, ind, SET_OR_GET.GET);

            //CreateAndSendSignal(testBoard.Scenario.test.listRun[curGroupIndex1], curGroupIndex1, SET_OR_GET.GET);
            CreateAndSendSignal(listGroupTestSignals1[curGroupIndex1].Signal, listGroupTestSignals1[curGroupIndex1].Index, SET_OR_GET.GET);
            curGroupIndex1++;
            if (curGroupIndex1 >= listGroupTestSignals1.Count)
            {
                tmr.Stop();
                curGroupIndex1 = 0;
            }
            else
            {
                tmr.Interval = 50;
                tmr.Start();
            }
        }
        void udp_receivedPK(Command com)
        {
            Invoke((MethodInvoker)delegate
            {
                ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.RX);

                switch (com.set_or_get)
                {
                    case SET_OR_GET.SET:
                        {
                            //стартуем таймер, который оттикает DELAY и стартанет запрос GET
                            StartTimerPK(com);
                        }
                        break;
                    case SET_OR_GET.GET:
                        {
                            int indx = com.index;
                            uint groupVal = 0;
                            Button ind = (Button)panelTestSignals.Controls.Find("IndOut" + indx.ToString(), false).FirstOrDefault();

                            lastSignal2.get.source.group.valueRX = com.data;
                            for (int i = 0; i < lastSignal2.get.source.group.valueRX.Length; i++)
                            {
                                groupVal += (uint)(lastSignal2.get.source.group.valueRX[i] << (8 * i));
                            }

                            if ((groupVal & lastSignal2.get.valueon) != 0)
                                ind.BackColor = Color.Green;
                            else
                                ind.BackColor = Color.Red;

                        }
                        break;
                    case SET_OR_GET.DESET:
                        {
                            int indx = com.index;
                            uint groupVal = 0;
                            Button ind = (Button)panelTestSignals.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            lastSignal2.set.source.group.valueRX = com.data;
                            for (int i = 0; i < lastSignal2.set.source.group.valueRX.Length; i++)
                            {
                                groupVal += (uint)(lastSignal2.set.source.group.valueRX[i] << (8 * i));
                            }

                            if ((groupVal & lastSignal2.set.valueon) != 0)
                                ind.BackColor = Color.Green;
                            else
                                ind.BackColor = Color.White;

                            //Инициируем запуск еще одного GET для считывания выключенного сигнала 
                            StartTimerPK(com);
                        }
                        break;
                    case SET_OR_GET.OTHER:
                        break;
                }

                if (com.result == CommandResult.Success) cntSuccess++;
                if (com.result == CommandResult.Timeout) cntTimeouts++;

                lblStatus.Text = String.Format("Статус обмена - {0}", com.result.ToString());
                lblTimeOuts.Text = String.Format("Таймауты посылки - {0}", cntTimeouts.ToString());
                lblSuccess.Text = String.Format("Успешные посылки - {0}", cntSuccess.ToString());
            });

            bCommandBusy = false;
        }
        void StartTimerPK(Command com)
        {
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += tmr_TickPK;
            tmr.Interval = lastSignal2.Delay;
            tmr.Tag = com.index;
            curGroupIndex2 = 0;
            tmr.Start();
        }
        void tmr_TickPK(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = (System.Windows.Forms.Timer)sender;
            int ind = (int)tmr.Tag;
            tmr.Stop();
            //CreateAndSendSignal(lastSignal2, ind, SET_OR_GET.GET);

            //CreateAndSendSignal(testBoard.Scenario.test.listRun[curGroupIndex1], curGroupIndex1, SET_OR_GET.GET);
            CreateAndSendSignal(listGroupTestSignals2[curGroupIndex2].Signal, listGroupTestSignals2[curGroupIndex2].Index, SET_OR_GET.GET);
            curGroupIndex2++;
            if (curGroupIndex2 >= listGroupTestSignals2.Count)
            {
                tmr.Stop();
                curGroupIndex2 = 0;
            }
            else
            {
                tmr.Interval = 50;
                tmr.Start();
            }
        }
        void udp_receivedPTVC(Command com)
        {

            Invoke((MethodInvoker)delegate
            {
                ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.RX);

                switch (com.set_or_get)
                {
                    case SET_OR_GET.SET:
                        {
                            //стартуем таймер, который оттикает DELAY и стартанет запрос GET
                            StartTimerPTVC(com);
                        }
                        break;
                    case SET_OR_GET.GET:
                        {
                            int indx = com.index;
                            uint groupVal = 0;
                            Button ind = (Button)panelTestSignals.Controls.Find("IndOut" + indx.ToString(), false).FirstOrDefault();

                            lastSignal3.get.source.group.valueRX = com.data;
                            for (int i = 0; i < lastSignal3.get.source.group.valueRX.Length; i++)
                            {
                                groupVal += (uint)(lastSignal3.get.source.group.valueRX[i] << (8 * i));
                            }

                            if ((groupVal & lastSignal3.get.valueon) != 0)
                                ind.BackColor = Color.Green;
                            else
                                ind.BackColor = Color.Red;

                        }
                        break;
                    case SET_OR_GET.DESET:
                        {
                            int indx = com.index;
                            uint groupVal = 0;
                            Button ind = (Button)panelTestSignals.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            lastSignal3.set.source.group.valueRX = com.data;
                            for (int i = 0; i < lastSignal3.set.source.group.valueRX.Length; i++)
                            {
                                groupVal += (uint)(lastSignal3.set.source.group.valueRX[i] << (8 * i));
                            }

                            if ((groupVal & lastSignal3.set.valueon) != 0)
                                ind.BackColor = Color.Green;
                            else
                                ind.BackColor = Color.White;
                            //Инициируем запуск еще одного GET для считывания выключенного сигнала 
                            StartTimerPTVC(com);
                        }
                        break;
                    case SET_OR_GET.OTHER:
                        break;
                }


                if (com.result == CommandResult.Success) cntSuccess++;
                if (com.result == CommandResult.Timeout) cntTimeouts++;

                lblStatus.Text = String.Format("Статус обмена - {0}", com.result.ToString());
                lblTimeOuts.Text = String.Format("Таймауты посылки - {0}", cntTimeouts.ToString());
                lblSuccess.Text = String.Format("Успешные посылки - {0}", cntSuccess.ToString());
            });

            bCommandBusy = false;
        }
        void StartTimerPTVC(Command com)
        {
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += tmr_TickPTVC;
            tmr.Interval = lastSignal3.Delay;
            tmr.Tag = com.index;
            curGroupIndex3 = 0;
            tmr.Start();
        }
        void tmr_TickPTVC(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = (System.Windows.Forms.Timer)sender;
            int ind = (int)tmr.Tag;
            tmr.Stop();
            //CreateAndSendSignal(lastSignal1, ind, SET_OR_GET.GET);

            //CreateAndSendSignal(testBoard.Scenario.test.listRun[curGroupIndex1], curGroupIndex1, SET_OR_GET.GET);
            CreateAndSendSignal(listGroupTestSignals3[curGroupIndex3].Signal, listGroupTestSignals3[curGroupIndex3].Index, SET_OR_GET.GET);
            curGroupIndex3++;
            if (curGroupIndex3 >= listGroupTestSignals3.Count)
            {
                tmr.Stop();
                curGroupIndex3 = 0;
            }
            else
            {
                tmr.Interval = 50;
                tmr.Start();
            }    
        }

        void ShowBuf(byte[] buf, BUF_TO_SHOW tx_rx)
        {
            TextBox txt;
            if (tx_rx == BUF_TO_SHOW.RX) txt = txtRX;
            else txt = txtTX;
            try
            {
                txt.Text = BitConverter.ToString(buf); 
            }
            catch { txt.Text = "DataError"; }
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmSetts.ShowDialog();
        }
        
        /*#region Сохранение настроек в ХМЛ

        private void LoadSettings()
        {
            try
            {
                sets = LoadList(Application.StartupPath + "\\settings7600.xml");

                frmSetts.txtIPAddress1.Text = sets.IpAddressTo;
                frmSetts.txtIPPort1.Text = sets.IpPortTo1;
                frmSetts.txtIPPort2.Text = sets.IpPortTo2;
                frmSetts.txtIPPort3.Text = sets.IpPortTo3;
                

            }
            catch
            {
                MessageBox.Show("Файл настроек XML не может быть загружен. Использованы параметры по умолчанию.", "Ошибка открытия файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sets = new SettingsXML();
                frmSetts.txtIPAddress1.Text = "192.168.0.1";
                frmSetts.txtIPPort1.Text = "50000";
                frmSetts.txtIPPort2.Text = "50001";
                frmSetts.txtIPPort3.Text = "50002";

            }
        }

        private void SaveSettings()
        {
            sets.IpAddressTo = frmSetts.txtIPAddress1.Text;
            sets.IpPortTo1 = frmSetts.txtIPPort1.Text;
            sets.IpPortTo2 = frmSetts.txtIPPort2.Text;
            sets.IpPortTo3 = frmSetts.txtIPPort3.Text;

            SaveList(Application.StartupPath + "\\settings7600.xml", sets);
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

        #endregion*/

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            _frmMainWindow._frmMain.Hide();
            _frmMainWindow._frmChooseCheckDevice.Show();            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            UDPConnect();
        }

        private void UDPConnect()
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
                        cntTimeouts = 0; cntSuccess = 0;
                        udpPU.received += udp_receivedPU;
                        udpPK.received += udp_receivedPK;
                        udpPTVC.received += udp_receivedPTVC;

                        btnConnect.Text = "Стоп обмен";
                        btnConnect.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка открытия порта!", "DSP Firmware");
                        //AddToList("Ошибка подключения");
                        btnConnect.Text = "Старт обмен";
                        btnConnect.BackColor = Color.Transparent;

                        udpPU = null;
                        udpPK = null;
                        udpPTVC = null;
                    }
                }
                catch (Exception ex) { udpPU = null; udpPK = null; udpPTVC = null; MessageBox.Show(ex.Message + "\n\n\nНе удалось установить соединение.", "UDP Connect"); }
            }
            else
            {
                try
                {
                    udpPU.Close();
                    udpPK.Close();
                    udpPTVC.Close();
                    btnConnect.BackColor = Color.Transparent;
                    btnConnect.Text = "Старт обмен";
                    bCommandBusy = false;
                    udpPU = null;
                    udpPK = null;
                    udpPTVC = null;
                }
                catch (Exception ex) { udpPU = null; udpPK = null; udpPTVC = null; MessageBox.Show(ex.Message, "UDPConnect"); }
            }
        }
        
        public byte[] getIPAddr(int param)
        {
            byte[] buf = new byte[4];
            string[] text = new string[] { };
            buf.Max(a => (byte)a);
            switch (param)
            {
                case 1: text = _frmMainWindow._frmSettings.txtIPAddress1.Text.Split('.'); break;
                case 2: text = _frmMainWindow._frmSettings.txtIPAddress2.Text.Split('.'); break;
            }
            
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = Convert.ToByte(text[i]);
            }
            return buf;
        }

        private void btnSendCom_Click(object sender, EventArgs e)
        {
            CreateAndSendCommand(com_out, udpPK, COMTYPE.CONFIG_RS);
        }

        private void CreateAndSendCommand(Command com, Udp udp, COMTYPE comtype)
        {
            if (udp == null) return;
            if (bCommandBusy) return;

            ushort addr;
            if (!ushort.TryParse(txtAddr.Text, out addr)) addr = 0;
            switch (comtype)
            {
                case COMTYPE.CONFIG_RS:
                    {
                        int timeoutRazdel, periodCycle, timeoutWait;
                        if (!int.TryParse(txtTimeoutRazdel.Text, out timeoutRazdel)) timeoutRazdel = 0;
                        if (!int.TryParse(txtPeriodCycle.Text, out periodCycle)) periodCycle = 0;
                        if (!int.TryParse(txtTimeoutWait.Text, out timeoutWait)) timeoutWait = 0;

                        com = new Command(addr, 9, SET_OR_GET.OTHER,0, CommandType.TYPICAL);
                        //for (int i = 0; i < com_out.data.Length; i++) com_out.data[i] = (byte)i;

                        com.data[0] |= (byte)cmbSpeedUart.SelectedIndex;
                        com.data[1] |= (byte)(cmbParity.SelectedIndex + 1);
                        if (radMaster.Checked) com.data[1] |= (1 << 3);
                        com.data[1] |= (byte)((cmbStopBits.SelectedIndex) << 4);
                        if (radRS485.Checked) com.data[1] |= (1 << 6);
                        if (radReadWrite.Checked) com.data[1] |= (1 << 7);

                        com.data[3] = (byte)(timeoutRazdel / 100);
                        com.data[4] = (byte)(((timeoutRazdel / 100) >> 8) & 0xFF);
                        com.data[5] = (byte)(periodCycle);
                        com.data[6] = (byte)((periodCycle >> 8) & 0xFF);
                        com.data[7] = (byte)(timeoutWait / 100);
                        com.data[8] = (byte)(((timeoutWait / 100) >> 8) & 0xFF);
                        break;
                    }
                case COMTYPE.FREE:
                    {
                        byte[] buf;
                        string str = txtTVPOut.Text;
                        try
                        {
                            buf = str.Split(',').Select(n => Convert.ToByte(n, 16)).ToArray();
                            com = new Command(addr, (ushort)buf.Length, SET_OR_GET.OTHER,0, CommandType.TYPICAL);
                            Array.Copy(buf, com.data, buf.Length);
                        }
                        catch
                        {
                            com = new Command(addr, 0, SET_OR_GET.OTHER,0, CommandType.TYPICAL);
                        }
                        break;
                    }
                case COMTYPE.SIGNAL:
                    {

                    }
                    break;
            }
            

            ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.TX);
            bCommandBusy = true;
            udp.SendCommand(com);

            while (bCommandBusy) { this.Cursor = Cursors.AppStarting; Application.DoEvents(); }
            this.Cursor = Cursors.Default;
        }

        private void CreateAndSendSignal(BoardSignal signal, int index, SET_OR_GET set_or_get)
        {
            if (bCommandBusy) return;
            ushort addr = 0;
            ushort cnt = 0;
            uint port = 0;
            uint groupVal = 0;
            byte[] buf = null;
            Udp udp = null;

            //lastSignal1 = signal;

            switch (set_or_get)
            {
                case SET_OR_GET.SET:
                    {
                        //if (signal.set.source == null) return;
                        addr = (ushort)signal.set.source.address;
                        cnt = signal.set.sizebyte;
                        port = signal.set.source.port;
                        buf = signal.set.source.group.valueTX;

                        //собираем из буфера группы число
                        for (int i = 0; i < buf.Length; i++)
                            groupVal += (uint)(buf[i] << (8 * i));

                        //делаем ИЛИ с отправляемым сигналом
                        groupVal |= signal.set.valueon;

                        
                    }
                    break;
                case SET_OR_GET.GET:
                    {
                        addr = (ushort)signal.get.source.address;
                        cnt = signal.get.sizebyte;
                        port = signal.get.source.port;
                        buf = signal.get.source.group.valueTX;

                        //собираем из буфера группы число
                        for (int i = 0; i < buf.Length; i++)
                            groupVal += (uint)(buf[i] << (8 * i));

                        //делаем ИЛИ с отправляемым сигналом
                        //groupVal |= signal.get.valueon;
                    }
                    break;
                case SET_OR_GET.DESET:
                    {
                        addr = (ushort)signal.set.source.address;
                        cnt = signal.set.sizebyte;
                        port = signal.set.source.port;
                        buf = signal.set.source.group.valueTX;

                        //собираем из буфера группы число
                        for (int i = 0; i < buf.Length; i++)
                            groupVal += (uint)(buf[i] << (8 * i));

                        //делаем И с отправляемым сигналом на выключение
                        groupVal &= signal.set.valueoff;
                    }
                    break;
            }
            

            //выбираем порт для отправки
            if (port == uint.Parse(_frmMainWindow._frmSettings.txtIPPort1.Text)) { udp = udpPU; lastSignal1 = signal; }
            if (port == uint.Parse(_frmMainWindow._frmSettings.txtIPPort2.Text)) { udp = udpPK; lastSignal2 = signal; }
            if (port == uint.Parse(_frmMainWindow._frmSettings.txtIPPort3.Text)) { udp = udpPTVC; lastSignal3 = signal; }

            //собираем обратно получившееся значение в групповой буфер
            for (int i = 0; i < buf.Length; i++)
                buf[i] = (byte)((groupVal >> (8 * i)) & 0xFF);

            //создаем команду с определенным адресом и количеством байт данных
            Command com = new Command(addr, cnt, set_or_get, index, CommandType.TYPICAL);

            //заполняем буфер данных значением группы
            com.data = buf;

            //отображаем уходящий пакет на форме
            ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.TX);
            
            //команда уходит
            if (udp != null)
            {
                bCommandBusy = true;
                udp.SendCommand(com);
            }
            else { MessageBox.Show("Проверьте соединение!", "7600", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            while (bCommandBusy) { this.Cursor = Cursors.AppStarting; Application.DoEvents(); }
            this.Cursor = Cursors.Default;
        }

        private void cmbAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbAddr.SelectedIndex)
            {
                case 0: txtAddr.Text = "0"; break;
                case 1: txtAddr.Text = "16"; break;
                case 2: txtAddr.Text = "32"; break;
            }
        }

        private void txtTX_MouseHover(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.SelectionLength > 0)
            {
                tipMessage.SetToolTip(txt, HexStrToBinStr(txt.SelectedText));
            }
        }

        private string HexStrToBinStr(string str)
        {
            //string st = str.Replace("-","").Replace(" ","");
            string outstr = string.Empty;
            int i;
            foreach (char c in str)
            {
                string s = c.ToString();
                if (int.TryParse(s, System.Globalization.NumberStyles.HexNumber,null,out i))
                {
                    byte b = Convert.ToByte(s, 16);
                    outstr += Convert.ToString(b, 2).PadLeft(4, '0') + " ";
                }
                else
                    outstr += "- ";
            }
            return outstr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateAndSendCommand(com_out, udpPU, COMTYPE.FREE);
        }

        private void btnLoadSignals_Click(object sender, EventArgs e)
        {
            /*GroupSignals group1 = new GroupSignals("OUT_3V", 24/8);
            GroupSignals group2 = new GroupSignals("IN_3V", 24/8);
            BaseSignal baseSignal1 = new BaseSignal("OUT_3V_14", 55055, 1, SIGNALTYPE.LOGIC, 24, 13, MODETYPE.WRITE, true, (1 << 13), "OUT_3V", group1);
            BaseSignal baseSignal2 = new BaseSignal("IN_3V_14", 55055, 1, SIGNALTYPE.LOGIC, 24, 13, MODETYPE.READ, false, (1 << 13),"IN_3V", group2);
            Set_STAND_BIT set = new Set_STAND_BIT(baseSignal1, "This should be a comment!", "C:/Images/pict1.jpg");
            Get_STAND_BIT get = new Get_STAND_BIT(baseSignal2);

            BoardSignal signal = new BoardSignal("LINE_SOME", set, get, 1000);
            BoardSignals signals = new BoardSignals();
            signals.listBoardSignals.Add(signal);

            BoardRS boardRS = new BoardRS("1", 6, 0x55);
            BoardConfig boardConfig = new BoardConfig();
            boardConfig.listBoardRS.Add(boardRS);

            Manual manual = new Manual();
            Periodic periodic = new Periodic();
            Test test = new Test();
            test.listRun.Add(signal);
            test.listRun.Add(signal);
            test.listRun.Add(signal);

            BoardScenario scenario = new BoardScenario(periodic, test, manual);

            testBoard = new TestBoard(boardConfig, signals, scenario);*/

            //standSignals = xml.openStandXML("StandSignals_04.xml");
            //testBoard = xml.openBoardXML(xml.openStandXML("StandSignals_04.xml"), "TestBoard_04.xml");
            testBoard = _frmMainWindow.xml.openBoardXML(_frmMainWindow.standSignals, "TestBoard_04.xml");
            CreateListGroupSignals(testBoard);

            ShowSignals(testBoard);
        }

        private void CreateListGroupSignals(TestBoard testBoard)
        {
            listGroupTestSignals1.Clear();
            listGroupTestSignals2.Clear();
            listGroupTestSignals3.Clear();

            for (int i = 0; i < testBoard.Scenario.test.listRun.Count; i++)
            {
                switch (testBoard.Scenario.test.listRun[i].get.source.port)
                {
                    case (uint)PORT.PU:
                        {
                            if (listGroupTestSignals1.Count == 0)
                                listGroupTestSignals1.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listGroupTestSignals1.Count; j++)
                                {
                                    if (listGroupTestSignals1[j].Group == testBoard.Scenario.test.listRun[i].get.source.group) break;
                                }
                                if (j == listGroupTestSignals1.Count)
                                    listGroupTestSignals1.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PK:
                        {
                            if (listGroupTestSignals2.Count == 0)
                                listGroupTestSignals2.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listGroupTestSignals2.Count; j++)
                                {
                                    if (listGroupTestSignals2[j].Group == testBoard.Scenario.test.listRun[i].get.source.group) break;
                                }
                                if (j == listGroupTestSignals2.Count)
                                    listGroupTestSignals2.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PTVC:
                        {
                            if (listGroupTestSignals3.Count == 0)
                                listGroupTestSignals3.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listGroupTestSignals3.Count; j++)
                                {
                                    if (listGroupTestSignals3[j].Group == testBoard.Scenario.test.listRun[i].get.source.group) break;
                                }
                                if (j == listGroupTestSignals3.Count)
                                    listGroupTestSignals3.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                }
            }
        }

        private void ShowSignals(TestBoard board)
        {
            panelTestSignals.Controls.Clear();
            panelTestSignals.SuspendLayout();
            this.Cursor = Cursors.AppStarting;
            for (int i = 0; i < board.Scenario.test.listRun.Count; i++)
            {
                CheckBox chk = new CheckBox();
                chk.Name = "Chk" + i.ToString();//testBoard.Scenario.test.listRun[i].Name;
                string name = board.Scenario.test.listRun[i].Name;
                if (name.Length > 12) name = name.Substring(0, 12) + "..";
                chk.Text = name;
                chk.Click += chk_Click;
                chk.AutoSize = true;
                chk.Font = new System.Drawing.Font("Verdana", 8);
                chk.Appearance = Appearance.Button;
                chk.FlatStyle = FlatStyle.Flat;
                chk.FlatAppearance.BorderSize = 0;
                chk.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 128, 0);
                chk.Left = 10;
                chk.Width = 90;
                //chk.AutoSize = true;
                chk.Top = i * 20 + 0;

                //panelTestSignals.SuspendLayout();
                panelTestSignals.Controls.Add(chk);
                //panelTestSignals.ResumeLayout();


                /*grpSignals.SuspendLayout();
                grpSignals.Controls.Add(chk);
                grpSignals.ResumeLayout();*/


                Button indOut = new Button();
                indOut.Left = 120;
                indOut.Top = i * 20 + 0;
                indOut.Name = "IndOut" + i.ToString();//testBoard.Scenario.test.listRun[i].Name;
                indOut.Enabled = false;
                indOut.Height = chk.Height-5;
                indOut.Width = chk.Height-5;
                indOut.FlatStyle = FlatStyle.Flat;
                indOut.BackColor = Color.White;

                //panelTestSignals.SuspendLayout();
                panelTestSignals.Controls.Add(indOut);
                //panelTestSignals.ResumeLayout();

                /*grpSignals.SuspendLayout();
                grpSignals.Controls.Add(indOut);
                grpSignals.ResumeLayout();*/

                Button indIn = new Button();
                indIn.Left = 150;
                indIn.Top = i * 20 + 0;
                indIn.Name = "IndIn" + i.ToString();//testBoard.Scenario.test.listRun[i].Name;
                indIn.Enabled = false;
                indIn.Height = chk.Height-5;
                indIn.Width = chk.Height-5;
                indIn.FlatStyle = FlatStyle.Flat;
                indIn.BackColor = Color.White;

                //panelTestSignals.SuspendLayout();
                panelTestSignals.Controls.Add(indIn);
                //panelTestSignals.ResumeLayout();

                /*grpSignals.SuspendLayout();
                grpSignals.Controls.Add(indIn);
                grpSignals.ResumeLayout();*/

            }
            panelTestSignals.ResumeLayout();
            this.Cursor = Cursors.Default;
        }

        void chk_Click(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            int num = panelTestSignals.Controls.Count;
            int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
            Button ind = (Button)panelTestSignals.Controls.Find("IndOut" + index.ToString(), false).FirstOrDefault();
            if (chk.Checked)
            {
                //chk.BackColor = Color.Green;
                chk.ForeColor = Color.White;
                ind.BackColor = Color.Green;
                CreateAndSendSignal(testBoard.Scenario.test.listRun[index], index, SET_OR_GET.SET);
            }
            else
            {
                //chk.BackColor = Color.Transparent;
                chk.ForeColor = Color.Black;
                ind.BackColor = Color.White;
                CreateAndSendSignal(testBoard.Scenario.test.listRun[index], index, SET_OR_GET.DESET);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateAndSendCommand(com_out, udpPK, COMTYPE.FREE);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateAndSendCommand(com_out, udpPTVC, COMTYPE.FREE);
        }

        /// <summary>
        /// Проверяет наличие указанного базового сигнала среди списка сигнал-параметров SCENARIO.TEST. Возвращает его индекс в списке при наличии или -1 при отсутствии.
        /// </summary>
        /// <param name="signal">Базовый стендовый сигнал, который ищем в списке</param>
        /// <returns>Индекс сигнала в списке SCENARIO.TEST. Или -1 при отсутствии.</returns>
        private int isTestSignal(BaseSignal signal)
        {
            for (int i = 0; i < testBoard.Scenario.test.listRun.Count; i++)
            {
                string str = testBoard.Scenario.test.listRun[i].get.source.name;
                if (signal.name == str) return i;
            }

                /*foreach (BoardSignal boardSign in testBoard.Scenario.test.listRun)
                {
                    string str = boardSign.get.source.name;
                    if (signal.name == str) return true;
                }*/
            return -1;
        }

        private void btnConfigPTVC_Click(object sender, EventArgs e)
        {
            CreateAndSendCommand(com_out, udpPTVC, COMTYPE.CONFIG_RS);
        }
    }
}
