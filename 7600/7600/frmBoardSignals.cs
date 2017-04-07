using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace _7600
{
    public partial class frmBoardSignals : Form
    {
        //ссылка на главную основную форму
        public frmMainWindow _frmMainWindow;

        public frmFunc _frmFunc;

        private BoardInfo board;    //информация о выбранной плате
        public BoardSignal signal; //аналоговый сигнал, по лейбле значения которого был даблклик для вызова окошка функции
        private TestBoard testBoard;    //объект, хранящий сигналы платы
        private listGroupSignals listPU, listPK, listPTVC;  //списки групп ручных, периодических и тестируемых сигналов для каждого порта
        private bool bNeedReload = true;    //признак необходимости переинициализации. 
        private SignalParameters lastSignalParamPU, lastSignalParamPK, lastSignalParamPTVC;

        public uint countSuccessPU = 0, countSuccessPK = 0, countSuccessPTVC = 0;
        public uint countTimeoutPU = 0, countTimeoutPK = 0, countTimeoutPTVC = 0;
        private Int16 ListCount;
        private int curGroupIndex1 = 0, curGroupIndex2 = 0, curGroupIndex3 = 0;  //текущие индексы списков групп для параллельного тестирования
        //private Multimedia.Timer tmrPeriodic;
        private Timer tmrPeriodic;
        private uint iCurUpdateCounter = 0;  //счетчик отображения токов
        private uint iCurUpdateFreq = 0;    //частота отображения токов

        uint cntError = 0;
        List<string> listProblemSignals;

        private int curPeriodicIndex = 0;
        bIsRequestClass bIsRequest = new bIsRequestClass();


        private volatile bool bAutoMode = false;

        public volatile SignalStatus signalStatus;

        private volatile bool bPeriodicCycleDone = true;
        private volatile bool bCycleRequest = false, bShowTestSignalCurrent = false;

        double i27_last = 0, i15_last = 0, i12_last = 0, i5_last = 0, i3_last = 0;   //пременные токов, обновляющиеся при прохождении всего периодика
        double i27_saved = 0, i15_saved = 0, i12_saved = 0, i5_saved = 0, i3_saved = 0;

        int iTestSignalIndex = 0;

        //bool bExiting = false;

        TypingText t;
        public DataFunc dataFunc = new DataFunc();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams result = base.CreateParams;
                result.ExStyle |= 0x02000000;
                return result;
            }
        }

        public frmBoardSignals()
        {
            InitializeComponent();
            pict.SizeMode = PictureBoxSizeMode.StretchImage;




            lstLog.GetType().InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, lstLog, new object[] { true });
                            /*tmrPeriodic = new Multimedia.Timer();
                            tmrPeriodic.Stop();
                            tmrPeriodic.Tick += tmrPeriodic_Tick;
                            tmrPeriodic.Period = 1000;
                            tmrPeriodic.Resolution = 0;
                            tmrPeriodic.Mode = Multimedia.TimerMode.Periodic;*/

            tmrPeriodic = new Timer();
            tmrPeriodic.Tick+=tmrPeriodic_Tick;
            tmrPeriodic.Interval = 50;
            tmrPeriodic.Stop();
            
        }

        void tmrPeriodic_Tick(object sender, EventArgs e)
        {
            int num = testBoard.Scenario.periodic.listRun.Count;
            int ind;
            CheckBox chk;
            int i = curPeriodicIndex;
            bool bTagNull = true;
            if (bIsRequest.test)
            {
                for (ind = 0; ind < testBoard.Scenario.test.listRun.Count; ind++)
                {
                    chk = (CheckBox)panelTestSignals.Controls.Find("Chk" + ind.ToString(), false).FirstOrDefault();
                    if (chk.Tag != null)
                    {
                        switch ((SET_OR_GET)chk.Tag)
                        {
                            case SET_OR_GET.SET:
                                {
                                    if (testBoard.Scenario.test.listRun[ind].set != null)
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.test.listRun[ind].set.source.port,
                                            testBoard.Scenario.test.listRun[ind],
                                            SIGNAL_SECTION.Test,
                                            testBoard.Scenario.test.listRun[ind].Delay), ind, SET_OR_GET.SET))
                                        {
                                            bTagNull = false;
                                            break;
                                        }
                                        else
                                        {
                                            //bIsRequest.test = false;
                                        }
                                    }
                                    else
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.test.listRun[ind].get.source.port,
                                            testBoard.Scenario.test.listRun[ind],
                                            SIGNAL_SECTION.Test,
                                            testBoard.Scenario.test.listRun[ind].Delay), ind, SET_OR_GET.GET))
                                        {
                                            bTagNull = false;
                                            break;
                                        }
                                        else
                                        {
                                            //bIsRequest.test = false;
                                        }
                                    }
                                    chk.Tag = null;
                                }
                                break;
                            case SET_OR_GET.DESET:
                                {
                                    if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.test.listRun[ind].set.source.port,
                                            testBoard.Scenario.test.listRun[ind],
                                            SIGNAL_SECTION.Test,
                                            testBoard.Scenario.test.listRun[ind].Delay), ind, SET_OR_GET.DESET))
                                    {
                                        bTagNull = false;
                                        break;
                                    }
                                    else
                                    {
                                        //bIsRequest.test = false;
                                    }
                                    chk.Tag = null;
                                }
                                break;
                        }
                    }
                    if (!bTagNull) break;
                }
                if (ind >= testBoard.Scenario.test.listRun.Count) bIsRequest.test = false; ;
            }

            if (bIsRequest.manual)
            {
                for (ind = 0; ind < testBoard.Scenario.manual.listRun.Count; ind++)
                {
                    chk = (CheckBox)panelManualSignals.Controls.Find("Chk" + ind.ToString(), false).FirstOrDefault();
                    if (chk.Tag != null)
                    {
                        switch ((SET_OR_GET)chk.Tag)
                        {
                            case SET_OR_GET.SET:
                                {
                                    if (testBoard.Scenario.manual.listRun[ind].set != null)
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[ind].set.source.port,
                                            testBoard.Scenario.manual.listRun[ind],
                                            SIGNAL_SECTION.Manual,
                                            testBoard.Scenario.manual.listRun[ind].Delay), ind, SET_OR_GET.SET))
                                        {
                                            bTagNull = false;
                                            break;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[ind].get.source.port,
                                            testBoard.Scenario.manual.listRun[ind],
                                            SIGNAL_SECTION.Manual,
                                            testBoard.Scenario.manual.listRun[ind].Delay), ind, SET_OR_GET.GET))
                                        {
                                            bTagNull = false;
                                            break;
                                        }
                                        else
                                        {
                                            //bIsRequest.test = false;
                                        }
                                    }
                                    chk.Tag = null;
                                }
                                break;
                            case SET_OR_GET.DESET:
                                {
                                    if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[ind].set.source.port,
                                            testBoard.Scenario.manual.listRun[ind],
                                            SIGNAL_SECTION.Manual,
                                            testBoard.Scenario.manual.listRun[ind].Delay), ind, SET_OR_GET.DESET))
                                    {
                                        bTagNull = false;
                                        break;
                                    }
                                    else
                                    {
                                        //bIsRequest.test = false;
                                    }
                                    chk.Tag = null;
                                }
                                break;
                        }
                    }
                    if (!bTagNull) break;
                }
                if (ind >= testBoard.Scenario.manual.listRun.Count) bIsRequest.manual = false; ;
            }

            if (i == 0) 
            {
                if (bCycleRequest)
                {
                    bPeriodicCycleDone = false;
                    bCycleRequest = false;
                }
                else
                {
                    if (bAutoMode && bPeriodicCycleDone) return;
                    if (!bPeriodicCycleDone)
                    {
                        bPeriodicCycleDone = true;

                        if (!bAutoMode)
                        {
                            Label lbl27, lbl15, lbl12, lbl5, lbl3;
                            double i27, i15, i12, i5, i3;
                            BoardSignal signal = testBoard.Scenario.test.listRun[iTestSignalIndex];
                            lbl27 = (Label)panelTestSignals.Controls.Find("lbl27V" + iTestSignalIndex.ToString(), false).FirstOrDefault();
                            lbl15 = (Label)panelTestSignals.Controls.Find("lbl15V" + iTestSignalIndex.ToString(), false).FirstOrDefault();
                            lbl12 = (Label)panelTestSignals.Controls.Find("lbl12V" + iTestSignalIndex.ToString(), false).FirstOrDefault();
                            lbl5 = (Label)panelTestSignals.Controls.Find("lbl5V" + iTestSignalIndex.ToString(), false).FirstOrDefault();
                            lbl3 = (Label)panelTestSignals.Controls.Find("lbl3V" + iTestSignalIndex.ToString(), false).FirstOrDefault();

                            i27 =  double.Parse(_frmMainWindow._frmCurrents.lbl27_I.Text) - i27_last;
                            i15 = double.Parse(_frmMainWindow._frmCurrents.lbl15_I.Text) - i15_last;
                            i12 = double.Parse(_frmMainWindow._frmCurrents.lbl12_I.Text) - i12_last;
                            i5 = double.Parse(_frmMainWindow._frmCurrents.lbl5_I.Text) - i5_last;
                            i3 = double.Parse(_frmMainWindow._frmCurrents.lbl3_I.Text) - i3_last;

                            i27_last = double.Parse(_frmMainWindow._frmCurrents.lbl27_I.Text);
                            i15_last = double.Parse(_frmMainWindow._frmCurrents.lbl15_I.Text);
                            i12_last = double.Parse(_frmMainWindow._frmCurrents.lbl12_I.Text);
                            i5_last = double.Parse(_frmMainWindow._frmCurrents.lbl5_I.Text);
                            i3_last = double.Parse(_frmMainWindow._frmCurrents.lbl3_I.Text);

                            if (bShowTestSignalCurrent)
                            {
                                if (i27 >= signal.D27Max) lbl27.ForeColor = Color.Red; else lbl27.ForeColor = Color.Black;
                                if (i15 >= signal.D15Max) lbl15.ForeColor = Color.Red; else lbl15.ForeColor = Color.Black;
                                if (i12 >= signal.D12Max) lbl12.ForeColor = Color.Red; else lbl12.ForeColor = Color.Black;
                                if (i5 >= signal.D5Max) lbl5.ForeColor = Color.Red; else lbl5.ForeColor = Color.Black;
                                if (i3 >= signal.D3Max) lbl3.ForeColor = Color.Red; else lbl3.ForeColor = Color.Black;

                            
                                lbl27.Text = Math.Round(i27, 2).ToString();
                                lbl15.Text = Math.Round(i15, 2).ToString();
                                lbl12.Text = Math.Round(i12, 2).ToString();
                                lbl5.Text = Math.Round(i5, 2).ToString();
                                lbl3.Text = Math.Round(i3, 2).ToString();
                            }
                        }
                        return;
                    }
                }
            }
            
            for (int j = 0; j < 2; j++)
            {
                for (; i < num; i++)
                {
                    chk = (CheckBox)panelPeriodicSignals.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();
                    if (chk == null) continue;

                    if (chk.Tag != null)
                    {
                        switch ((SET_OR_GET)chk.Tag)
                        {
                            case SET_OR_GET.SET:
                                {
                                    if (testBoard.Scenario.periodic.listRun[i].set != null)
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[i].set.source.port,
                                            testBoard.Scenario.periodic.listRun[i],
                                            SIGNAL_SECTION.Periodic,
                                            testBoard.Scenario.periodic.listRun[i].Delay), i, SET_OR_GET.SET)) return;
                                    }
                                    else
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[i].get.source.port,
                                            testBoard.Scenario.periodic.listRun[i],
                                            SIGNAL_SECTION.Periodic,
                                            testBoard.Scenario.periodic.listRun[i].Delay), i, SET_OR_GET.GET)) return;
                                    }
                                    chk.Tag = null;
                                }
                                break;
                            case SET_OR_GET.DESET:
                                {
                                    if (testBoard.Scenario.periodic.listRun[i].set != null)
                                    {
                                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[i].set.source.port,
                                                testBoard.Scenario.periodic.listRun[i],
                                                SIGNAL_SECTION.Periodic,
                                                testBoard.Scenario.periodic.listRun[i].Delay), i, SET_OR_GET.DESET)) return;
                                    }
                                    else
                                    {

                                    }
                                    chk.Tag = null;
                                }
                                break;
                        }
                        break;
                    }
                    if (chk.Checked)
                    {
                        if (signalStatus == SignalStatus.SETSTART || signalStatus == SignalStatus.DESETSTART) return;
                        if (testBoard.Scenario.periodic.listRun[i].get != null)
                        {
                            if (testBoard.Scenario.periodic.listRun[i].get.source != null)
                            {
                                if(!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[i].get.source.port,
                                                testBoard.Scenario.periodic.listRun[i],
                                                SIGNAL_SECTION.Periodic,
                                                testBoard.Scenario.periodic.listRun[i].Delay), i, SET_OR_GET.GET)) return;
                            }
                            else
                            {
                                if(!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[i].set.source.port,
                                            testBoard.Scenario.periodic.listRun[i],
                                            SIGNAL_SECTION.Periodic,
                                            testBoard.Scenario.periodic.listRun[i].Delay), i, SET_OR_GET.SET)) return;
                            }
                        }
                        Label lblI, lblU;
                        Button indIIN_W;
                        double dIMax;
                        lblI = (Label)panelPeriodicSignals.Controls.Find("lblI" + i.ToString(), false).FirstOrDefault();
                        lblU = (Label)panelPeriodicSignals.Controls.Find("lblU" + i.ToString(), false).FirstOrDefault();
                        indIIN_W = (Button)panelPeriodicSignals.Controls.Find("indIIN_W" + i.ToString(), false).FirstOrDefault();

                        if (chk.Text.Contains("ON_27V_POWER"))
                        {
                            dIMax = double.Parse(_frmMainWindow._frmCurrents.lbl27_I.Text);
                            
                            lblI.Text = dIMax.ToString();
                            if (dIMax >= board.I27Max) lblI.ForeColor = Color.Red;
                            else lblI.ForeColor = Color.Black;

                            lblU.Text = _frmMainWindow._frmCurrents.lbl27_Uout.Text;
                            indIIN_W.BackColor = _frmMainWindow._frmCurrents.ind27_IIN_W.BackColor;
                        }
                        if (chk.Text.Contains("ON_15V_POWER"))
                        {
                            dIMax = double.Parse(_frmMainWindow._frmCurrents.lbl15_I.Text);

                            lblI.Text = dIMax.ToString();
                            if (dIMax >= board.I15Max) lblI.ForeColor = Color.Red;
                            else lblI.ForeColor = Color.Black;

                            lblI.Text = _frmMainWindow._frmCurrents.lbl15_I.Text;
                            lblU.Text = _frmMainWindow._frmCurrents.lbl15_Uout.Text;
                            indIIN_W.BackColor = _frmMainWindow._frmCurrents.ind15_IIN_W.BackColor;
                        }
                        if (chk.Text.Contains("ON_12V_POWER"))
                        {
                            dIMax = double.Parse(_frmMainWindow._frmCurrents.lbl12_I.Text);

                            lblI.Text = dIMax.ToString();
                            if (dIMax >= board.I12Max) lblI.ForeColor = Color.Red;
                            else lblI.ForeColor = Color.Black;

                            lblI.Text = _frmMainWindow._frmCurrents.lbl12_I.Text;
                            lblU.Text =  _frmMainWindow._frmCurrents.lbl12_Uout.Text;
                            indIIN_W.BackColor = _frmMainWindow._frmCurrents.ind12_IIN_W.BackColor;
                        }
                        if (chk.Text.Contains("ON_5V_POWER"))
                        {
                            dIMax = double.Parse(_frmMainWindow._frmCurrents.lbl5_I.Text);

                            lblI.Text = dIMax.ToString();
                            if (dIMax >= board.I5Max) lblI.ForeColor = Color.Red;
                            else lblI.ForeColor = Color.Black;

                            lblI.Text = _frmMainWindow._frmCurrents.lbl5_I.Text;
                            lblU.Text = _frmMainWindow._frmCurrents.lbl5_Uout.Text;
                            indIIN_W.BackColor = _frmMainWindow._frmCurrents.ind5_IIN_W.BackColor;
                        }
                        if (chk.Text.Contains("ON_3.3V_POWER"))
                        {
                            dIMax = double.Parse(_frmMainWindow._frmCurrents.lbl3_I.Text);

                            lblI.Text = dIMax.ToString();
                            if (dIMax >= board.I3Max) lblI.ForeColor = Color.Red;
                            else lblI.ForeColor = Color.Black;

                            lblI.Text =  _frmMainWindow._frmCurrents.lbl3_I.Text;
                            lblU.Text = _frmMainWindow._frmCurrents.lbl3_Uout.Text;
                            indIIN_W.BackColor = _frmMainWindow._frmCurrents.ind3_IIN_W.BackColor;
                        }
                        

                        break;
                    }
                }

                if (i < num) break;
                else 
                { 
                    i = 0;
                    curPeriodicIndex = 0;
                    if (j == 1)
                        return;
                }

                //i = 0;
                //curPeriodicIndex = 0;
            }

            curPeriodicIndex = i+1;
            if (curPeriodicIndex >= num) curPeriodicIndex = 0;
        }

        private void AutoMode()
        {
            //return;
            int num = testBoard.Scenario.test.listRun.Count;
            CheckBox chk;
            Label lbl27, lbl15, lbl12, lbl5, lbl3;
            cntError = 0;
            double d = 0;

            double i27=0, i15=0, i12=0, i5=0, i3 = 0;

            listProblemSignals.Clear();
            
            Invoke((MethodInvoker)delegate
            {
                panelTestSignals.VerticalScroll.Value = 0;
                panelTestSignals.VerticalScroll.Value = 0;

                progProcessor.Visible = true;
                progProcessor.Visible = true;
                progProcessor.Value = (int)d;
                lblProgPercent.Text = "Выполнено: " + progProcessor.Value.ToString() + "%";
            });
            for (int i = 0; i < num; i++)
            {
                if (!bAutoMode)
                {
                    System.Threading.Thread.CurrentThread.Abort();
                    progProcessor.Visible = false;
                    return;
                }

                chk = (CheckBox)panelTestSignals.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();

                bCycleRequest = true;
                while (!bPeriodicCycleDone || bCycleRequest) { System.Threading.Thread.Sleep(10); }

                Invoke((MethodInvoker)delegate
                {
                    i27 = double.Parse(_frmMainWindow._frmCurrents.lbl27_I.Text);
                    i15 = double.Parse(_frmMainWindow._frmCurrents.lbl15_I.Text);
                    i12 = double.Parse(_frmMainWindow._frmCurrents.lbl12_I.Text);
                    i5 = double.Parse(_frmMainWindow._frmCurrents.lbl5_I.Text);
                    i3 = double.Parse(_frmMainWindow._frmCurrents.lbl3_I.Text);

                    chk.Checked = true;
                    ChkMouseEnter(chk, testBoard.Scenario.test.listRun);

                    PerformChkTestClick(chk);

                    if (i >= 5)
                    {
                        if (panelTestSignals.VerticalScroll.Value < panelTestSignals.VerticalScroll.Maximum - 20)
                        {
                            panelTestSignals.VerticalScroll.Value += 20;
                            panelTestSignals.VerticalScroll.Value += 20;
                        }
                    } 
                });
                while (chk.Tag != null) System.Threading.Thread.Sleep(10);
                while (signalStatus == SignalStatus.SETSTART) { System.Threading.Thread.Sleep(10); }
                while (_frmMainWindow.bCommandBusyPK || _frmMainWindow.bCommandBusyPU || _frmMainWindow.bCommandBusyPTVC) { System.Threading.Thread.Sleep(10); }

                System.Threading.Thread.Sleep(50);

                {//дожидаемся выполнения посылки периодических сигналов для проверки потребления токов и выводим их на экран
                    bCycleRequest = true;
                    while (!bPeriodicCycleDone || bCycleRequest) { System.Threading.Thread.Sleep(10); }

                    lbl27 = (Label)panelTestSignals.Controls.Find("lbl27V" + i.ToString(), false).FirstOrDefault();
                    lbl15 = (Label)panelTestSignals.Controls.Find("lbl15V" + i.ToString(), false).FirstOrDefault();
                    lbl12 = (Label)panelTestSignals.Controls.Find("lbl12V" + i.ToString(), false).FirstOrDefault();
                    lbl5 = (Label)panelTestSignals.Controls.Find("lbl5V" + i.ToString(), false).FirstOrDefault();
                    lbl3 = (Label)panelTestSignals.Controls.Find("lbl3V" + i.ToString(), false).FirstOrDefault();

                    Invoke((MethodInvoker)delegate
                    {
                        int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
                        BoardSignal signal = testBoard.Scenario.test.listRun[index];

                        i27 = double.Parse(_frmMainWindow._frmCurrents.lbl27_I.Text) - i27;
                        i15 = double.Parse(_frmMainWindow._frmCurrents.lbl15_I.Text) - i15;
                        i12 = double.Parse(_frmMainWindow._frmCurrents.lbl12_I.Text) - i12;
                        i5 = double.Parse(_frmMainWindow._frmCurrents.lbl5_I.Text) - i5;
                        i3 = double.Parse(_frmMainWindow._frmCurrents.lbl3_I.Text) - i3;

                        if (i27 >= signal.D27Max) lbl27.ForeColor = Color.Red; else lbl27.ForeColor = Color.Black;
                        if (i15 >= signal.D15Max) lbl15.ForeColor = Color.Red; else lbl15.ForeColor = Color.Black;
                        if (i12 >= signal.D12Max) lbl12.ForeColor = Color.Red; else lbl12.ForeColor = Color.Black;
                        if (i5 >= signal.D5Max) lbl5.ForeColor = Color.Red; else lbl5.ForeColor = Color.Black;
                        if (i3 >= signal.D3Max) lbl3.ForeColor = Color.Red; else lbl3.ForeColor = Color.Black;

                        lbl27.Text = Math.Round(i27,2).ToString();
                        lbl15.Text = Math.Round(i15, 2).ToString();
                        lbl12.Text = Math.Round(i12, 2).ToString();
                        lbl5.Text = Math.Round(i5, 2).ToString();
                        lbl3.Text = Math.Round(i3, 2).ToString();

                        
                    });
                }



                Invoke((MethodInvoker)delegate
                {
                    chk.Checked = false;

                    //выполняем второй "клик" по ЧекБоксу для отработки DESET'аs
                    PerformChkTestClick(chk);
                });

                while (chk.Tag != null) System.Threading.Thread.Sleep(10);
                while (signalStatus == SignalStatus.DESETSTART) { System.Threading.Thread.Sleep(10); }
                while (_frmMainWindow.bCommandBusyPK || _frmMainWindow.bCommandBusyPU || _frmMainWindow.bCommandBusyPTVC) { System.Threading.Thread.Sleep(10); }

                System.Threading.Thread.Sleep(50);

                Invoke((MethodInvoker)delegate
                {
                    d = (double)((i + 1) / (double)num);
                    progProcessor.Value = (int)(d * 100);
                    lblProgPercent.Text = "Выполнено: " + progProcessor.Value.ToString() + "%";
                });
            }

            
            Invoke((MethodInvoker)delegate
            {
                btnStartAuto.PerformClick();
                string str = String.Format("Автотестирование завершено. Количество найденных несоответствий: " + cntError.ToString()) + "\nПроблемные сигналы:";
                listProblemSignals = listProblemSignals.Distinct().ToList();
                if (listProblemSignals.Count == 0) listProblemSignals.Add("Не обнаружено");
                //var list = listProblemSignals.Select(x => x.First());
                foreach (string s in listProblemSignals)
                    str += "\n\t" + s;
                MessageBox.Show(str, "Результат тестирования");
                progProcessor.Visible = false;
                lblProgPercent.Visible = false;
            });        

            System.Threading.Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// Происходит при повторном открытии формы
        /// </summary>
        void Init()
        {
            //bExiting = false;
            bNeedReload = false;
            //получаем информацию о выбранной плате
            board = _frmMainWindow._frmListBoard.chooseBoard;
            btnCurrents.Image = Properties.Resources.Current;
            btnOpenPDF.Image = Properties.Resources.PDF_file;
            btnStartAuto.Image = Properties.Resources.Auto;
            btnProgFinish.Image = Properties.Resources.Finish;
            btnProgTest.Image = Properties.Resources.Test;
            chkPowerON.Image = Properties.Resources.Power;
            
            ListCount = 0;
            listProblemSignals = new List<string>();

            //_frmFunc = new frmFunc();

            //Изменяем заголовок окна
            
            //i++;
            if (_frmMainWindow.bSelfDiag)
            {
                btnProgTest.Enabled = false;
                btnProgFinish.Enabled = false;
                //btnStartAuto.Enabled = false;
                this.Text = "Режим самодиагностики";
            }
            else
            {
                btnProgTest.Enabled = true;
                btnProgFinish.Enabled = true;
                //btnStartAuto.Enabled = true;
                this.Text = "Тестируемые сигналы платы " + board.Name;
            }
            //считываем файл списка сигналов для выбранной платы
            if (Utils.isFileExist(board.Filename))
            {
                testBoard = _frmMainWindow.xml.openBoardXML(_frmMainWindow.standSignals, board.Filename);

                lastSignalParamPU = new SignalParameters(0, null, SIGNAL_SECTION.Test, 0);// null;
                lastSignalParamPK = new SignalParameters(0, null, SIGNAL_SECTION.Test, 0); //null;
                lastSignalParamPTVC = new SignalParameters(0, null, SIGNAL_SECTION.Test, 0); //null;

                listPK = new listGroupSignals();
                listPU = new listGroupSignals();
                listPTVC = new listGroupSignals();

                countSuccessPU = countSuccessPK = countSuccessPTVC = countTimeoutPU = countTimeoutPK = countTimeoutPTVC = 0;

                CreateListGroupSignals(testBoard);

                this.SuspendLayout();
                ShowSignals(testBoard);
                this.ResumeLayout();

                t = new TypingText(Properties.Resources.comment_BoardSignals_Initial, txtComment);
                t.StartTyping();

                signalStatus = SignalStatus.READY;

                tmrPeriodic.Start();
            }
            else
            {
                MessageBox.Show("Ошибка загрузки файла списка сигналов платы " + board.Name, "Не могу прочитать файл");
                this.Close();
            }

            
        }

        private void CreateListGroupSignals(TestBoard testBoard)
        {
            listPK.listManualSignals.Clear();
            listPK.listPeriodicSignals.Clear();
            listPK.listTestSignals.Clear();

            listPU.listManualSignals.Clear();
            listPU.listPeriodicSignals.Clear();
            listPU.listTestSignals.Clear();

            listPTVC.listManualSignals.Clear();
            listPTVC.listPeriodicSignals.Clear();
            listPTVC.listTestSignals.Clear();


            for (int i = 0; i < testBoard.Scenario.test.listRun.Count; i++)
            {
                if (testBoard.Scenario.test.listRun[i].get == null || testBoard.Scenario.test.listRun[i].get.source == null) continue;
                switch (testBoard.Scenario.test.listRun[i].get.source.port)
                {
                    case (uint)PORT.PU:
                        {
                            if (listPU.listTestSignals.Count == 0)
                                listPU.listTestSignals.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPU.listTestSignals.Count; j++)
                                {
                                    if (listPU.listTestSignals[j].Group == testBoard.Scenario.test.listRun[i].get.source.group) break;
                                }
                                if (j == listPU.listTestSignals.Count)
                                    listPU.listTestSignals.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PK:
                        {
                            if (listPK.listTestSignals.Count == 0)
                                listPK.listTestSignals.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPK.listTestSignals.Count; j++)
                                {
                                    if (listPK.listTestSignals[j].Group == testBoard.Scenario.test.listRun[i].get.source.group) break;
                                }
                                if (j == listPK.listTestSignals.Count)
                                    listPK.listTestSignals.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PTVC:
                        {
                            if (listPTVC.listTestSignals.Count == 0)
                                listPTVC.listTestSignals.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPTVC.listTestSignals.Count; j++)
                                {
                                    if (listPTVC.listTestSignals[j].Group == testBoard.Scenario.test.listRun[i].get.source.group) break;
                                }
                                if (j == listPTVC.listTestSignals.Count)
                                    listPTVC.listTestSignals.Add(new GroupTestSignal(testBoard.Scenario.test.listRun[i], testBoard.Scenario.test.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                }
            }

            for (int i = 0; i < testBoard.Scenario.periodic.listRun.Count; i++)
            {
                if (testBoard.Scenario.periodic.listRun[i].get == null || testBoard.Scenario.periodic.listRun[i].get.source == null) continue;
                switch (testBoard.Scenario.periodic.listRun[i].get.source.port)
                {
                    case (uint)PORT.PU:
                        {
                            if (listPU.listPeriodicSignals.Count == 0)
                                listPU.listPeriodicSignals.Add(new GroupTestSignal(testBoard.Scenario.periodic.listRun[i], testBoard.Scenario.periodic.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPU.listPeriodicSignals.Count; j++)
                                {
                                    if (listPU.listPeriodicSignals[j].Group == testBoard.Scenario.periodic.listRun[i].get.source.group) break;
                                }
                                if (j == listPU.listPeriodicSignals.Count)
                                    listPU.listPeriodicSignals.Add(new GroupTestSignal(testBoard.Scenario.periodic.listRun[i], testBoard.Scenario.periodic.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PK:
                        {
                            if (listPK.listPeriodicSignals.Count == 0)
                                listPK.listPeriodicSignals.Add(new GroupTestSignal(testBoard.Scenario.periodic.listRun[i], testBoard.Scenario.periodic.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPK.listPeriodicSignals.Count; j++)
                                {
                                    if (listPK.listPeriodicSignals[j].Group == testBoard.Scenario.periodic.listRun[i].get.source.group) break;
                                }
                                if (j == listPK.listPeriodicSignals.Count)
                                    listPK.listPeriodicSignals.Add(new GroupTestSignal(testBoard.Scenario.periodic.listRun[i], testBoard.Scenario.periodic.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PTVC:
                        {
                            if (listPTVC.listPeriodicSignals.Count == 0)
                                listPTVC.listPeriodicSignals.Add(new GroupTestSignal(testBoard.Scenario.periodic.listRun[i], testBoard.Scenario.periodic.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPTVC.listPeriodicSignals.Count; j++)
                                {
                                    if (listPTVC.listPeriodicSignals[j].Group == testBoard.Scenario.periodic.listRun[i].get.source.group) break;
                                }
                                if (j == listPTVC.listPeriodicSignals.Count)
                                    listPTVC.listPeriodicSignals.Add(new GroupTestSignal(testBoard.Scenario.periodic.listRun[i], testBoard.Scenario.periodic.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                }
            }

            for (int i = 0; i < testBoard.Scenario.manual.listRun.Count; i++)
            {
                if (testBoard.Scenario.manual.listRun[i].get == null || testBoard.Scenario.manual.listRun[i].get.source == null) continue;
                switch (testBoard.Scenario.manual.listRun[i].get.source.port)
                {
                    case (uint)PORT.PU:
                        {
                            if (listPU.listManualSignals.Count == 0)
                                listPU.listManualSignals.Add(new GroupTestSignal(testBoard.Scenario.manual.listRun[i], testBoard.Scenario.manual.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPU.listManualSignals.Count; j++)
                                {
                                    if (listPU.listManualSignals[j].Group == testBoard.Scenario.manual.listRun[i].get.source.group) break;
                                }
                                if (j == listPU.listManualSignals.Count)
                                    listPU.listManualSignals.Add(new GroupTestSignal(testBoard.Scenario.manual.listRun[i], testBoard.Scenario.manual.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PK:
                        {
                            if (listPK.listManualSignals.Count == 0)
                                listPK.listManualSignals.Add(new GroupTestSignal(testBoard.Scenario.manual.listRun[i], testBoard.Scenario.manual.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPK.listManualSignals.Count; j++)
                                {
                                    if (listPK.listManualSignals[j].Group == testBoard.Scenario.manual.listRun[i].get.source.group) break;
                                }
                                if (j == listPK.listManualSignals.Count)
                                    listPK.listManualSignals.Add(new GroupTestSignal(testBoard.Scenario.manual.listRun[i], testBoard.Scenario.manual.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                    case (uint)PORT.PTVC:
                        {
                            if (listPTVC.listManualSignals.Count == 0)
                                listPTVC.listManualSignals.Add(new GroupTestSignal(testBoard.Scenario.manual.listRun[i], testBoard.Scenario.manual.listRun[i].get.source.group, i));
                            else
                            {
                                int j;
                                for (j = 0; j < listPTVC.listManualSignals.Count; j++)
                                {
                                    if (listPTVC.listManualSignals[j].Group == testBoard.Scenario.manual.listRun[i].get.source.group) break;
                                }
                                if (j == listPTVC.listManualSignals.Count)
                                    listPTVC.listManualSignals.Add(new GroupTestSignal(testBoard.Scenario.manual.listRun[i], testBoard.Scenario.manual.listRun[i].get.source.group, i));
                            }
                        }
                        break;
                }
            }
        }

        private void FillButtons()
        {
            panelButtons.Controls.Clear();
            panelButtons.SuspendLayout();

            //курсор ставим занятый
            this.Cursor = Cursors.WaitCursor;
            List<SysButton> list = testBoard.Scenario.buttons.listButtons;
            //int count = (list.Count <= 8) ? list.Count : 8;
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                CheckBox chk = new CheckBox();
                chk.Name = "Chk" + i.ToString();
                string sName = list[i].Name;
                if (sName.Length > 14) sName = sName.Substring(0, 14) + "..";
                chk.Text = sName;
                chk.Font = new System.Drawing.Font("Verdana", 8);
                chk.Appearance = Appearance.Button;
                chk.FlatStyle = FlatStyle.Flat;
                chk.FlatAppearance.BorderSize = 0;
                chk.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 128, 0);
                chk.TextAlign = ContentAlignment.MiddleCenter;
                chk.Left = 10;
                chk.Width = 220;
                chk.Top = i * 20 + 0;
                chk.Click += chk_ButtonClick;

                panelButtons.Controls.Add(chk);

            }
            panelButtons.ResumeLayout();
        }

        void chk_ButtonClick(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            PerformButtonClick(chk);
        }

        void PerformButtonClick(CheckBox chk)
        {
            int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
            List<SysButton> list = testBoard.Scenario.buttons.listButtons;
            BoardSignal sig;

            if (chk.Checked)
            {
                chk.ForeColor = Color.White;
            }
            else
            {
                chk.ForeColor = Color.Black;
            }

            for (int i = 0; i < list[index].listRun.Count; i++)
            {
                sig = list[index].listRun[i];
                if (!FindSignalAndPerformClick(sig, panelManualSignals, chk.Checked))
                    if (!FindSignalAndPerformClick(sig, panelTestSignals, chk.Checked))
                        FindSignalAndPerformClick(sig, panelPeriodicSignals, chk.Checked);
                while (chk.Tag != null) System.Threading.Thread.Sleep(10);
            }
        }

        private void chkPowerON_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk;
            if (chkPowerON.Checked)
            {
                chkPowerON.Image = Properties.Resources.PowerPressed;
                for (int i = 0; i < panelButtons.Controls.Count; i++)
                {
                    chk = (CheckBox)panelButtons.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();
                    if (chk.Text == "Power ON")
                    {
                        if (!chk.Checked)
                        {
                            chk.Checked = true;
                            PerformButtonClick(chk);
                            return;
                        }
                    }
                }
            }
            else
            {
                chkPowerON.Image = Properties.Resources.Power;
                for (int i = 0; i < panelButtons.Controls.Count; i++)
                {
                    chk = (CheckBox)panelButtons.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();
                    if (chk.Text == "Power ON")
                    {
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            PerformButtonClick(chk);
                            return;
                        }
                    }
                }
            }
        }

        bool FindSignalAndPerformClick(BoardSignal sig, Panel panel, bool isChecked)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                CheckBox chk = (CheckBox)panel.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();
                if (chk == null) continue;
                string sName = chk.Text;
                if(sig.Name == sName)
                {
                    if (chk.Checked != isChecked)
                        chk.Checked = isChecked;
                    else
                        return true;

                    switch (panel.Name)
                    {
                        case "panelManualSignals":
                            PerformChkManualClick(chk);
                            break;
                        case "panelTestSignals":
                            PerformChkTestClick(chk);
                            break;
                        case "panelPeriodicSignals":
                            PerformChkPeriodicClick(chk);
                            break;
                    }
                    return true;
                }
            }
            return false;
        }

        private void FillSignals(Panel panel, List<BoardSignal> list)
        {
            int iLeftSet = 130;
            int iLeftGet = 170;
            int iLeftIP = 200;
            panel.SuspendLayout();


            /*Label lbl = new Label();
            lbl.Text = "    СИГНАЛЫ";
            lbl.Name = "lblTitleSignals";
            lbl.Left = 10;
            lbl.Top = 0;

            panel.Controls.Add(lbl);*/

            for (int i = 0; i < list.Count; i++)
            {
                BoardSignal signal = list[i];

                CheckBox chk = new CheckBox();
                chk.Name = "Chk" + i.ToString();//testBoard.Scenario.test.listRun[i].Name;
                string name = signal.Name;
                if (name.Length > 20) name = name.Substring(0, 20) + "..";
                chk.Text = name;
                if (panel == panelTestSignals)
                {
                    chk.Click += chk_ClickTest;
                    chk.MouseEnter += chkTest_MouseEnter;
                }
                else if (panel == panelPeriodicSignals)
                {
                    chk.Click += chk_ClickPeriodic;
                    chk.MouseEnter += chkPeriodic_MouseEnter;
                }
                else if (panel == panelManualSignals)
                {
                    chk.Click += chk_ClickManual;
                    chk.MouseEnter += chkManual_MouseEnter;
                }
                //chk.AutoSize = true;
                chk.MouseHover += chk_MouseHover;
                chk.Font = new System.Drawing.Font("Verdana", 8);
                chk.Appearance = Appearance.Button;
                chk.FlatStyle = FlatStyle.Flat;
                chk.FlatAppearance.BorderSize = 0;
                chk.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 128, 0);
                chk.Left = 10;
                chk.Width = 120;
                chk.Top = i * 20 + 0;

                panel.Controls.Add(chk);

                if (signal.set != null)
                {
                    switch (signal.set.signalType)
                    {
                        case SIGNALTYPE.LOGIC:
                            {
                                Button indOut = new Button();
                                indOut.Left = iLeftSet;
                                indOut.Top = i * 20 + 5;
                                indOut.Name = "IndOut" + i.ToString();
                                indOut.Enabled = false;
                                indOut.Height = chk.Height - 5;
                                indOut.Width = chk.Height - 5 ;
                                indOut.FlatStyle = FlatStyle.Flat;
                                indOut.BackColor = Color.White;

                                panel.Controls.Add(indOut);
                            }
                            break;
                        case SIGNALTYPE.ARRAY:
                            {
                                Label lblValOn = new Label();
                                lblValOn.Text = "ARR";
                                lblValOn.Left = iLeftSet;
                                lblValOn.Top = i * 20 + 5;
                                lblValOn.Name = "lblValOn" + i.ToString();
                                lblValOn.AutoSize = true;

                                panel.Controls.Add(lblValOn);
                            }
                            break;
                        case SIGNALTYPE.ANALOG:
                            {
                                Label lblValOn = new Label();
                                lblValOn.MouseDoubleClick += lblValOn_MouseDoubleClick;
                                lblValOn.Text = signal.set.valueon.ToString();
                                lblValOn.Left = iLeftSet;
                                lblValOn.Top = i * 20 + 5;
                                lblValOn.Name = "lblValOn" + i.ToString();
                                lblValOn.AutoSize = true;

                                panel.Controls.Add(lblValOn);
                            }
                            break;
                    }
                }
                else
                {
                    Label lblValOn = new Label();
                    lblValOn.Text = "NULL";
                    lblValOn.Left = iLeftSet;
                    lblValOn.Top = i * 20 + 5;
                    lblValOn.Name = "lblValOn" + i.ToString();
                    lblValOn.AutoSize = true;

                    panel.Controls.Add(lblValOn);
                }

                if (signal.get != null)
                {
                    switch (signal.get.signalType)
                    { 
                        case SIGNALTYPE.LOGIC:
                            {
                                Button indIn = new Button();
                                indIn.Left = iLeftGet;
                                indIn.Top = i * 20 + 5;
                                indIn.Name = "IndIn" + i.ToString();
                                indIn.Enabled = false;
                                indIn.Height = chk.Height - 5;
                                indIn.Width = chk.Height - 5;
                                indIn.FlatStyle = FlatStyle.Flat;
                                indIn.BackColor = Color.White;

                                panel.Controls.Add(indIn);
                            }
                            break;
                        case SIGNALTYPE.ANALOG:
                            {
                                Label lblValMin = new Label();
                                lblValMin.Text = signal.get.min.ToString() + "-" + signal.get.max.ToString();
                                lblValMin.Left = iLeftGet;
                                lblValMin.Top = i * 20 + 5;
                                lblValMin.Name = "lblValMin" + i.ToString();
                                lblValMin.AutoSize = true;

                                panel.Controls.Add(lblValMin);
                            }
                            break;
                        default:
                            break;
                    }
                }

                if (panel == panelTestSignals) //для панели тестовых сигналов добавляем лейблы потребления токов
                {
                    Label lbl27V = new Label();
                    lbl27V.Name = "lbl27V" + i.ToString();
                    lbl27V.Text = "0";
                    lbl27V.Left = iLeftIP;
                    lbl27V.Top = i * 20 + 5;
                    lbl27V.AutoSize = true;
                    if (signal.D27Max == 999) lbl27V.Visible = false;

                    panel.Controls.Add(lbl27V);

                    Label lbl15V = new Label();
                    lbl15V.Name = "lbl15V" + i.ToString();
                    lbl15V.Text = "0";
                    lbl15V.Left = lbl27V.Width + lbl27V.Location.X + 20;
                    lbl15V.Top = i * 20 + 5;
                    lbl15V.AutoSize = true;
                    if (signal.D15Max == 999) lbl15V.Visible = false;

                    panel.Controls.Add(lbl15V);

                    Label lbl12V = new Label();
                    lbl12V.Name = "lbl12V" + i.ToString();
                    lbl12V.Text = "0";
                    lbl12V.Left = lbl15V.Width + lbl15V.Location.X + 20;
                    lbl12V.Top = i * 20 + 5;
                    lbl12V.AutoSize = true;
                    if (signal.D12Max == 999) lbl12V.Visible = false;

                    panel.Controls.Add(lbl12V);

                    Label lbl5V = new Label();
                    lbl5V.Name = "lbl5V" + i.ToString();
                    lbl5V.Text = "0";
                    lbl5V.Left = lbl12V.Location.X + lbl12V.Width + 20;
                    lbl5V.Top = i * 20 + 5;
                    lbl5V.AutoSize = true;
                    if (signal.D5Max == 999) lbl5V.Visible = false;

                    panel.Controls.Add(lbl5V);

                    Label lbl3V = new Label();
                    lbl3V.Name = "lbl3V" + i.ToString();
                    lbl3V.Text = "0";
                    lbl3V.Left = lbl5V.Width + lbl5V.Location.X + 20;
                    lbl3V.Top = i * 20 + 5;
                    lbl3V.AutoSize = true;
                    if (signal.D3Max == 999) lbl3V.Visible = false;

                    panel.Controls.Add(lbl3V);
                }
                if (panel == panelPeriodicSignals)
                {
                    if (signal.Name.Contains("27V_POWER") || signal.Name.Contains("15V_POWER") || signal.Name.Contains("12V_POWER") || signal.Name.Contains("5V_POWER") || signal.Name.Contains("3V_POWER"))
                    {
                        Label lblI = new Label();
                        lblI.Name = "lblI" + i.ToString();
                        lblI.Text = "I: 0";
                        lblI.Left = iLeftIP;
                        lblI.Top = i * 20 + 5;
                        lblI.AutoSize = true;

                        panel.Controls.Add(lblI);

                        Label lblU = new Label();
                        lblU.Name = "lblU" + i.ToString();
                        lblU.Text = "U: 0";
                        lblU.Left = lblI.Width + lblI.Location.X + 20;
                        lblU.Top = i * 20 + 5;
                        lblU.AutoSize = true;

                        panel.Controls.Add(lblU);

                        Button ind = new Button();
                        ind.Left = lblU.Width + lblU.Location.X + 30;
                        ind.Top = i * 20 + 0;
                        ind.Name = "IndIIN_W" + i.ToString();
                        ind.Enabled = false;
                        ind.Height = chk.Height - 5;
                        ind.Width = chk.Height - 5;
                        ind.FlatStyle = FlatStyle.Flat;
                        ind.BackColor = Color.White;

                        panel.Controls.Add(ind);
                    }
                }
            }
            panel.ResumeLayout();
        }

        

        void chk_MouseHover(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            tipMessage.SetToolTip(chk, chk.Text);

        }

        private void ShowSignals(TestBoard board)
        {
            //очистка панелей от кпрошлых контролов
            panelManualSignals.Controls.Clear();
            panelPeriodicSignals.Controls.Clear();
            panelTestSignals.Controls.Clear();

            SizeAndLocateGroupBoxes(board);

            //курсор ставим занятый
            this.Cursor = Cursors.WaitCursor;

            //заполнение секции тестируемых сигналов
            FillSignals(panelTestSignals, board.Scenario.test.listRun);
            //заполнение секции периодических сигналов
            FillSignals(panelPeriodicSignals, board.Scenario.periodic.listRun);
            //заполнение секции ручных сигналов
            FillSignals(panelManualSignals, board.Scenario.manual.listRun);

            //заполнение панели кнопок
            FillButtons();

            this.Cursor = Cursors.Default; 
        }

        void SizeAndLocateGroupBoxes(TestBoard board)
        {
            if (board.Scenario.buttons.listButtons.Count > 8)
                grpButtons.Height = 230;
            else
            {
                if (board.Scenario.buttons.listButtons.Count <= 0)
                    grpButtons.Height = 24;
                else
                    grpButtons.Height = board.Scenario.buttons.listButtons.Count * 24 + 20;
            }

            if (board.Scenario.manual.listRun.Count > 8)
                grpManual.Height = 230;
            else
            {
                if (board.Scenario.manual.listRun.Count <= 0)
                    grpManual.Height = 24;
                else
                    grpManual.Height = board.Scenario.manual.listRun.Count * 24 + 20;
            }

            grpTest.Height = grpPeriodic.Location.Y - grpTest.Location.Y;
        }

        void chkTest_MouseLeave(object sender, EventArgs e)
        {
            t.StopTyping();
            pict.Image = (Image)Properties.Resources.boardSignalsInitImage;
            t = new TypingText(Properties.Resources.comment_BoardSignals_Initial, txtComment);
            t.StartTyping();
        }

        void chkTest_MouseEnter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            ChkMouseEnter(chk, testBoard.Scenario.test.listRun);
        }

        void chkPeriodic_MouseEnter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            ChkMouseEnter(chk, testBoard.Scenario.periodic.listRun);   
        }

        void chkManual_MouseEnter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            ChkMouseEnter(chk, testBoard.Scenario.manual.listRun);
        }

        void ChkMouseEnter(CheckBox chk, List<BoardSignal> list)
        {
            int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));

            t.StopTyping();
            if (list[index].set != null)
            {
                t = new TypingText(list[index].set.comment, txtComment);
                if (Utils.isFileExist(list[index].set.imagelink))
                    pict.Image = Image.FromFile(list[index].set.imagelink);
                else
                    pict.Image = Properties.Resources.PictLoadError;
            }
            else
            {
                t = new TypingText("NO COMMENT", txtComment);
                pict.Image = Properties.Resources.PictLoadError;
            }

            t.StartTyping();
        }

        void chk_ClickPeriodic(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            PerformChkPeriodicClick(chk);
        }

        private void PerformChkPeriodicClick(CheckBox chk)
        {
            int num = panelPeriodicSignals.Controls.Count;
            int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
            Button ind = (Button)panelPeriodicSignals.Controls.Find("IndOut" + index.ToString(), false).FirstOrDefault();

            //testBoard.Scenario.test.listRun[index].signalStatus = SignalStatus.NOTREADY;

            if (chk.Checked)
            {
                chk.ForeColor = Color.White;
                if (ind != null) ind.BackColor = Color.Green;
                chk.Tag = SET_OR_GET.SET;
                /*CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[index].set.source.port,
                    testBoard.Scenario.periodic.listRun[index],
                     SIGNAL_SECTION.Periodic, testBoard.Scenario.periodic.listRun[index].Delay), index, SET_OR_GET.SET);*/
            }
            else
            {
                if (chk.ForeColor != Color.Red) chk.ForeColor = Color.Black;
                chk.Tag = SET_OR_GET.DESET;
                if (ind != null) ind.BackColor = Color.White;
                /*CreateAndSendSignal(new SignalParameters(testBoard.Scenario.periodic.listRun[index].set.source.port,
                    testBoard.Scenario.periodic.listRun[index],
                     SIGNAL_SECTION.Periodic, testBoard.Scenario.periodic.listRun[index].Delay), index, SET_OR_GET.DESET);*/
            }
        }

        void chk_ClickTest(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            bShowTestSignalCurrent = false;
            bCycleRequest = true;
            iTestSignalIndex = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
            while (!bPeriodicCycleDone || bCycleRequest) { Application.DoEvents(); }

            /*i27_saved = i27_last;
            i15_saved = i15_last;
            i12_saved = i12_last;
            i5_saved = i5_last;
            i3_saved = i3_last;*/

            PerformChkTestClick(chk);
        }

        private void PerformChkTestClick(CheckBox chk)
        {
            int num = panelTestSignals.Controls.Count;
            int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
            Button ind = (Button)panelTestSignals.Controls.Find("IndOut" + index.ToString(), false).FirstOrDefault();

            while (chk.Tag != null) { System.Threading.Thread.Sleep(10); Application.DoEvents(); }
            //while (_frmMainWindow.bCommandBusyPK || _frmMainWindow.bCommandBusyPU || _frmMainWindow.bCommandBusyPTVC) { System.Threading.Thread.Sleep(10); }


            if (chk.Checked)
            {
                signalStatus = SignalStatus.SETSTART;

                chk.ForeColor = Color.White;
                if (ind != null) ind.BackColor = Color.Green;

                lastSignalParamPK.Signal_type = SIGNAL_SECTION.Test;
                lastSignalParamPTVC.Signal_type = SIGNAL_SECTION.Test;
                lastSignalParamPU.Signal_type = SIGNAL_SECTION.Test;

                if (testBoard.Scenario.test.listRun[index].set != null)
                    if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.test.listRun[index].set.source.port,
                        testBoard.Scenario.test.listRun[index],
                         SIGNAL_SECTION.Test, testBoard.Scenario.test.listRun[index].Delay), index, SET_OR_GET.SET))
                    {
                        chk.Tag = SET_OR_GET.SET;
                        bIsRequest.test = true;
                    }
            }
            else
            {
                signalStatus = SignalStatus.DESETSTART;

                if (chk.ForeColor != Color.Red) chk.ForeColor = Color.Black;
                if (ind != null) ind.BackColor = Color.White;

                lastSignalParamPK.Signal_type = SIGNAL_SECTION.Test;
                lastSignalParamPTVC.Signal_type = SIGNAL_SECTION.Test;
                lastSignalParamPU.Signal_type = SIGNAL_SECTION.Test;

                if (testBoard.Scenario.test.listRun[index].set != null)
                    if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.test.listRun[index].set.source.port,
                        testBoard.Scenario.test.listRun[index],
                         SIGNAL_SECTION.Test, testBoard.Scenario.test.listRun[index].Delay), index, SET_OR_GET.DESET))
                    {
                        chk.Tag = SET_OR_GET.DESET;
                        bIsRequest.test = true;
                    }
            }
        }

        void lblValOn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //_frmFunc._frmBoardSignal = this;
            _frmFunc = new frmFunc();
            Label lbl = sender as Label;

            int index = int.Parse(lbl.Name.Substring(8, lbl.Name.Length - 8));
            signal = testBoard.Scenario.manual.listRun[index];

            _frmFunc._frmBoardSignal = _frmMainWindow._frmBoardSignals;
            
            _frmFunc.ShowDialog();
        }
        void PerformMouseDoubleClick(int index)
        {

        }

        void chk_ClickManual(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            PerformChkManualClick(chk);
        }

        private void PerformChkManualClick(CheckBox chk) 
        {
            int num = panelManualSignals.Controls.Count;
            int index = int.Parse(chk.Name.Substring(3, chk.Name.Length - 3));
            Button ind = (Button)panelManualSignals.Controls.Find("IndOut" + index.ToString(), false).FirstOrDefault();

            //testBoard.Scenario.test.listRun[index].signalStatus = SignalStatus.NOTREADY;
            while (chk.Tag != null) { System.Threading.Thread.Sleep(10); Application.DoEvents(); }

            if (chk.Checked)
            {
                signalStatus = SignalStatus.SETSTART;

                chk.ForeColor = Color.White;
                if (ind != null) ind.BackColor = Color.Green;
                if (testBoard.Scenario.manual.listRun[index].set != null)
                {
                    if (dataFunc.bWork)//новая проверка для изменения значения данных сигнала RS
                    {
                        if (testBoard.Scenario.manual.listRun[index].set.source.isRS)
                        {
                            testBoard.Scenario.manual.listRun[index].set.valueon = dataFunc.CalcAndGetValue();
                            Label lblVal = (Label)panelManualSignals.Controls.Find("lblValOn" + index.ToString(), false).FirstOrDefault();
                            lblVal.Text = dataFunc.curVal.ToString();
                        }
                    }
                    if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[index].set.source.port,
                        testBoard.Scenario.manual.listRun[index],
                         SIGNAL_SECTION.Manual, testBoard.Scenario.manual.listRun[index].Delay), index, SET_OR_GET.SET))
                    {
                        chk.Tag = SET_OR_GET.SET;
                        bIsRequest.manual = true;
                    }
                }
                else
                {
                    if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[index].get.source.port,
                        testBoard.Scenario.manual.listRun[index],
                         SIGNAL_SECTION.Manual, testBoard.Scenario.manual.listRun[index].Delay), index, SET_OR_GET.GET))
                    {
                        chk.Tag = SET_OR_GET.SET;
                        bIsRequest.manual = true;
                    }
                }
            }
            else
            {
                if (dataFunc.bNoDeset)//новая проверка для игнорирования десета - всегда будет слаться сет сигнала при любом состоянии чекбокса. Весь код в этой секции скопирован из условия выше chk.Checked
                {
                    signalStatus = SignalStatus.SETSTART;

                    chk.ForeColor = Color.White;
                    if (ind != null) ind.BackColor = Color.Green;
                    if (testBoard.Scenario.manual.listRun[index].set != null)
                    {
                        if (dataFunc.bWork)//новая проверка для изменения значения данных сигнала RS
                        {
                            if (testBoard.Scenario.manual.listRun[index].set.source.isRS)
                            {
                                testBoard.Scenario.manual.listRun[index].set.valueon = dataFunc.CalcAndGetValue();
                                Label lblVal = (Label)panelManualSignals.Controls.Find("lblValOn" + index.ToString(), false).FirstOrDefault();
                                lblVal.Text = dataFunc.curVal.ToString();
                            }
                        }
                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[index].set.source.port,
                            testBoard.Scenario.manual.listRun[index],
                             SIGNAL_SECTION.Manual, testBoard.Scenario.manual.listRun[index].Delay), index, SET_OR_GET.SET))
                        {
                            chk.Tag = SET_OR_GET.SET;
                            bIsRequest.manual = true;
                        }
                    }
                    else
                    {
                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[index].get.source.port,
                            testBoard.Scenario.manual.listRun[index],
                             SIGNAL_SECTION.Manual, testBoard.Scenario.manual.listRun[index].Delay), index, SET_OR_GET.GET))
                        {
                            chk.Tag = SET_OR_GET.SET;
                            bIsRequest.manual = true;
                        }
                    }
                }
                else
                {
                    signalStatus = SignalStatus.DESETSTART;

                    if (chk.ForeColor != Color.Red) chk.ForeColor = Color.Black;
                    if (ind != null) ind.BackColor = Color.White;
                    if (testBoard.Scenario.manual.listRun[index].set != null)
                    {
                        if (!CreateAndSendSignal(new SignalParameters(testBoard.Scenario.manual.listRun[index].set.source.port,
                            testBoard.Scenario.manual.listRun[index],
                             SIGNAL_SECTION.Manual, testBoard.Scenario.manual.listRun[index].Delay), index, SET_OR_GET.DESET))
                        {
                            chk.Tag = SET_OR_GET.DESET;
                            bIsRequest.manual = true;
                        }
                    }
                    else { }
                }
            }
        }


        public void ShowBuf(byte[] buf, BUF_TO_SHOW tx_rx)
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

        private void frmBoardSignals_FormClosing(object sender, FormClosingEventArgs e)
        {
            bNeedReload = true;
            e.Cancel = true;
            //bExiting = true;
            tmrPeriodic.Stop();
            _frmMainWindow._frmBoardSignals.Hide();

            if (_frmMainWindow.bSelfDiag)
            {
                _frmMainWindow.bSelfDiag = false;
                _frmMainWindow.Show();
            }
            else
                _frmMainWindow._frmListBoard.Show();

            
            //this.Hide();
            

        }

        private void runBat(string filename)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.StartInfo.FileName = filename;

            process.Start();

            process.BeginOutputReadLine();
            process.OutputDataReceived += process_OutputDataReceived;
        }

        private delegate void CheckStatusCallBack(string text);

        void process_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                SetText(e.Data);
            }
        }

        private void SetText(string text)
        {
            if (lstLog.InvokeRequired)
            {
                CheckStatusCallBack d = new CheckStatusCallBack(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                AddToList(text);
            }
        }

        private void frmBoardSignals_Activated(object sender, EventArgs e)
        {
            if(bNeedReload)
                Init();
        }

        private void FreePortAndChangeSignalStatus(SignalParameters sigPar, bool b1, bool b2)
        {
            sigPar.bPortBusy = false;

            if (!sigPar.bPortBusy && !b1 && !b2)
            {
                if (signalStatus == SignalStatus.SETSTART)
                    signalStatus = SignalStatus.SETREADY;
                if (signalStatus == SignalStatus.DESETSTART)
                    signalStatus = SignalStatus.READY;

                if (!bAutoMode)
                {
                    if (sigPar.Signal_type == SIGNAL_SECTION.Test)
                    {
                        bCycleRequest = true;
                        bShowTestSignalCurrent = true;
                    }
                    //while (!bPeriodicCycleDone || bCycleRequest) { Application.DoEvents(); }
                }
            }

        }

        private void ShowCurrents(Command com)
        {
            byte channel = (byte)(com.address >> 8);
            int Diag = (byte)com.count + (com.data[0] << 8);
            int I = com.data[1] + (com.data[2] << 8);
            int Uout = com.data[3] + (com.data[4] << 8);
            int Uin = com.data[5] + (com.data[6] << 8);
            int P = com.data[7] + (com.data[8] << 8);
            int T = com.data[9] + (com.data[10] << 8);

            //if (lastSignalParamPU.Signal.Name.Contains("27V"))
            if(channel == 1)
            {
                
                _frmMainWindow._frmCurrents.lbl27_D.Text = Diag.ToString();
                _frmMainWindow._frmCurrents.lbl27_I.Text = String.Format("{0}", Math.Round(I * 0.00248, 3));
                _frmMainWindow._frmCurrents.lbl27_Uout.Text = String.Format("{0}", Math.Round(Uout * 0.022, 3));
                _frmMainWindow._frmCurrents.lbl27_Uin.Text = String.Format("{0}", Math.Round(Uin * 0.0218, 3));
                _frmMainWindow._frmCurrents.lbl27_P.Text = String.Format("{0}", Math.Round(P * 0.2227, 3));
                _frmMainWindow._frmCurrents.lbl27_T.Text = String.Format("{0}", Math.Round(T * 0.0625, 3));

                if ((Diag & 0x01) != 0) _frmMainWindow._frmCurrents.ind27_Circ.BackColor = Color.LightGreen;         else _frmMainWindow._frmCurrents.ind27_Circ.BackColor = Color.White;
                if ((Diag & 0x02) != 0) _frmMainWindow._frmCurrents.ind27_CML.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_CML.BackColor = Color.White;
                if ((Diag & 0x04) != 0) _frmMainWindow._frmCurrents.ind27_Temp_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Temp_F.BackColor = Color.White;
                if ((Diag & 0x08) != 0) _frmMainWindow._frmCurrents.ind27_IIN_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_IIN_F.BackColor = Color.White;
                if ((Diag & 0x10) != 0) _frmMainWindow._frmCurrents.ind27_VINOVER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_VINOVER_F.BackColor = Color.White;
                if ((Diag & 0x20) != 0) _frmMainWindow._frmCurrents.ind27_VINUNDER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_VINUNDER_F.BackColor = Color.White;
                if ((Diag & 0x40) != 0) _frmMainWindow._frmCurrents.ind27_Dev.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Dev.BackColor = Color.White;
                if ((Diag & 0x80) != 0) _frmMainWindow._frmCurrents.ind27_Conf.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Conf.BackColor = Color.White;
                if ((Diag & 0x100) != 0) _frmMainWindow._frmCurrents.ind27_Ext.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Ext.BackColor = Color.White;
                if ((Diag & 0x200) != 0) _frmMainWindow._frmCurrents.ind27_Timer.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Timer.BackColor = Color.White;
                if ((Diag & 0x400) != 0) _frmMainWindow._frmCurrents.ind27_Temp_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Temp_W.BackColor = Color.White;
                if ((Diag & 0x800) != 0) _frmMainWindow._frmCurrents.ind27_Good.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_Good.BackColor = Color.White;
                if ((Diag & 0x1000) != 0) _frmMainWindow._frmCurrents.ind27_VINOVER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_VINOVER_W.BackColor = Color.White;
                if ((Diag & 0x2000) != 0) _frmMainWindow._frmCurrents.ind27_VINUNDER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_VINUNDER_W.BackColor = Color.White;
                if ((Diag & 0x4000) != 0) _frmMainWindow._frmCurrents.ind27_IIN_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_IIN_W.BackColor = Color.White;
                if ((Diag & 0x8000) != 0) _frmMainWindow._frmCurrents.ind27_VOUT_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind27_VOUT_W.BackColor = Color.White;
            }
            //else if (lastSignalParamPU.Signal.Name.Contains("15V"))
            else if(channel == 2)
            {
                _frmMainWindow._frmCurrents.lbl15_D.Text = Diag.ToString();
                _frmMainWindow._frmCurrents.lbl15_I.Text = String.Format("{0}", Math.Round(I * 0.000813, 3));
                _frmMainWindow._frmCurrents.lbl15_Uout.Text = String.Format("{0}", Math.Round(Uout * 0.0221, 3));
                _frmMainWindow._frmCurrents.lbl15_Uin.Text = String.Format("{0}", Math.Round(Uin * 0.0218, 3));
                _frmMainWindow._frmCurrents.lbl15_P.Text = String.Format("{0}", Math.Round(P * 0.075, 3));
                _frmMainWindow._frmCurrents.lbl15_T.Text = String.Format("{0}", Math.Round(T * 0.0625, 3));

                if ((Diag & 0x01) != 0) _frmMainWindow._frmCurrents.ind15_Circ.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Circ.BackColor = Color.White;
                if ((Diag & 0x02) != 0) _frmMainWindow._frmCurrents.ind15_CML.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_CML.BackColor = Color.White;
                if ((Diag & 0x04) != 0) _frmMainWindow._frmCurrents.ind15_Temp_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Temp_F.BackColor = Color.White;
                if ((Diag & 0x08) != 0) _frmMainWindow._frmCurrents.ind15_IIN_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_IIN_F.BackColor = Color.White;
                if ((Diag & 0x10) != 0) _frmMainWindow._frmCurrents.ind15_VINOVER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_VINOVER_F.BackColor = Color.White;
                if ((Diag & 0x20) != 0) _frmMainWindow._frmCurrents.ind15_VINUNDER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_VINUNDER_F.BackColor = Color.White;
                if ((Diag & 0x40) != 0) _frmMainWindow._frmCurrents.ind15_Dev.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Dev.BackColor = Color.White;
                if ((Diag & 0x80) != 0) _frmMainWindow._frmCurrents.ind15_Conf.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Conf.BackColor = Color.White;
                if ((Diag & 0x100) != 0) _frmMainWindow._frmCurrents.ind15_Ext.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Ext.BackColor = Color.White;
                if ((Diag & 0x200) != 0) _frmMainWindow._frmCurrents.ind15_Timer.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Timer.BackColor = Color.White;
                if ((Diag & 0x400) != 0) _frmMainWindow._frmCurrents.ind15_Temp_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Temp_W.BackColor = Color.White;
                if ((Diag & 0x800) != 0) _frmMainWindow._frmCurrents.ind15_Good.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_Good.BackColor = Color.White;
                if ((Diag & 0x1000) != 0) _frmMainWindow._frmCurrents.ind15_VINOVER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_VINOVER_W.BackColor = Color.White;
                if ((Diag & 0x2000) != 0) _frmMainWindow._frmCurrents.ind15_VINUNDER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_VINUNDER_W.BackColor = Color.White;
                if ((Diag & 0x4000) != 0) _frmMainWindow._frmCurrents.ind15_IIN_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_IIN_W.BackColor = Color.White;
                if ((Diag & 0x8000) != 0) _frmMainWindow._frmCurrents.ind15_VOUT_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind15_VOUT_W.BackColor = Color.White;
            
            }
            //else if (lastSignalParamPU.Signal.Name.Contains("12V"))
            else if(channel == 3)
            {
                _frmMainWindow._frmCurrents.lbl12_D.Text = Diag.ToString();
                _frmMainWindow._frmCurrents.lbl12_I.Text = String.Format("{0}", Math.Round(I * 0.0003513, 3));
                _frmMainWindow._frmCurrents.lbl12_Uout.Text = String.Format("{0}", Math.Round(Uout * 0.00454, 3));
                _frmMainWindow._frmCurrents.lbl12_Uin.Text = String.Format("{0}", Math.Round(Uin * 0.00454, 3));
                _frmMainWindow._frmCurrents.lbl12_P.Text = String.Format("{0}", Math.Round(P * 0.00653, 3));
                _frmMainWindow._frmCurrents.lbl12_T.Text = String.Format("{0}", Math.Round(T * 0.0625, 3));

                if ((Diag & 0x01) != 0) _frmMainWindow._frmCurrents.ind12_Circ.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Circ.BackColor = Color.White;
                if ((Diag & 0x02) != 0) _frmMainWindow._frmCurrents.ind12_CML.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_CML.BackColor = Color.White;
                if ((Diag & 0x04) != 0) _frmMainWindow._frmCurrents.ind12_Temp_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Temp_F.BackColor = Color.White;
                if ((Diag & 0x08) != 0) _frmMainWindow._frmCurrents.ind12_IIN_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_IIN_F.BackColor = Color.White;
                if ((Diag & 0x10) != 0) _frmMainWindow._frmCurrents.ind12_VINOVER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_VINOVER_F.BackColor = Color.White;
                if ((Diag & 0x20) != 0) _frmMainWindow._frmCurrents.ind12_VINUNDER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_VINUNDER_F.BackColor = Color.White;
                if ((Diag & 0x40) != 0) _frmMainWindow._frmCurrents.ind12_Dev.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Dev.BackColor = Color.White;
                if ((Diag & 0x80) != 0) _frmMainWindow._frmCurrents.ind12_Conf.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Conf.BackColor = Color.White;
                if ((Diag & 0x100) != 0) _frmMainWindow._frmCurrents.ind12_Ext.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Ext.BackColor = Color.White;
                if ((Diag & 0x200) != 0) _frmMainWindow._frmCurrents.ind12_Timer.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Timer.BackColor = Color.White;
                if ((Diag & 0x400) != 0) _frmMainWindow._frmCurrents.ind12_Temp_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Temp_W.BackColor = Color.White;
                if ((Diag & 0x800) != 0) _frmMainWindow._frmCurrents.ind12_Good.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_Good.BackColor = Color.White;
                if ((Diag & 0x1000) != 0) _frmMainWindow._frmCurrents.ind12_VINOVER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_VINOVER_W.BackColor = Color.White;
                if ((Diag & 0x2000) != 0) _frmMainWindow._frmCurrents.ind12_VINUNDER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_VINUNDER_W.BackColor = Color.White;
                if ((Diag & 0x4000) != 0) _frmMainWindow._frmCurrents.ind12_IIN_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_IIN_W.BackColor = Color.White;
                if ((Diag & 0x8000) != 0) _frmMainWindow._frmCurrents.ind12_VOUT_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind12_VOUT_W.BackColor = Color.White;
            }
            //else if (lastSignalParamPU.Signal.Name.Contains("3V"))
            else if(channel == 5)
            {
                _frmMainWindow._frmCurrents.lbl3_D.Text = Diag.ToString();
                _frmMainWindow._frmCurrents.lbl3_I.Text = String.Format("{0}", Math.Round(I * 0.00077, 3));
                _frmMainWindow._frmCurrents.lbl3_Uout.Text = String.Format("{0}", Math.Round(Uout * 0.00456, 3));
                _frmMainWindow._frmCurrents.lbl3_Uin.Text = String.Format("{0}", Math.Round(Uin * 0.00456, 3));
                _frmMainWindow._frmCurrents.lbl3_P.Text = String.Format("{0}", Math.Round(P * 0.01515, 3));
                _frmMainWindow._frmCurrents.lbl3_T.Text = String.Format("{0}", Math.Round(T * 0.0625, 3));

                if ((Diag & 0x01) != 0) _frmMainWindow._frmCurrents.ind3_Circ.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Circ.BackColor = Color.White;
                if ((Diag & 0x02) != 0) _frmMainWindow._frmCurrents.ind3_CML.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_CML.BackColor = Color.White;
                if ((Diag & 0x04) != 0) _frmMainWindow._frmCurrents.ind3_Temp_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Temp_F.BackColor = Color.White;
                if ((Diag & 0x08) != 0) _frmMainWindow._frmCurrents.ind3_IIN_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_IIN_F.BackColor = Color.White;
                if ((Diag & 0x10) != 0) _frmMainWindow._frmCurrents.ind3_VINOVER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_VINOVER_F.BackColor = Color.White;
                if ((Diag & 0x20) != 0) _frmMainWindow._frmCurrents.ind3_VINUNDER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_VINUNDER_F.BackColor = Color.White;
                if ((Diag & 0x40) != 0) _frmMainWindow._frmCurrents.ind3_Dev.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Dev.BackColor = Color.White;
                if ((Diag & 0x80) != 0) _frmMainWindow._frmCurrents.ind3_Conf.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Conf.BackColor = Color.White;
                if ((Diag & 0x100) != 0) _frmMainWindow._frmCurrents.ind3_Ext.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Ext.BackColor = Color.White;
                if ((Diag & 0x200) != 0) _frmMainWindow._frmCurrents.ind3_Timer.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Timer.BackColor = Color.White;
                if ((Diag & 0x400) != 0) _frmMainWindow._frmCurrents.ind3_Temp_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Temp_W.BackColor = Color.White;
                if ((Diag & 0x800) != 0) _frmMainWindow._frmCurrents.ind3_Good.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_Good.BackColor = Color.White;
                if ((Diag & 0x1000) != 0) _frmMainWindow._frmCurrents.ind3_VINOVER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_VINOVER_W.BackColor = Color.White;
                if ((Diag & 0x2000) != 0) _frmMainWindow._frmCurrents.ind3_VINUNDER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_VINUNDER_W.BackColor = Color.White;
                if ((Diag & 0x4000) != 0) _frmMainWindow._frmCurrents.ind3_IIN_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_IIN_W.BackColor = Color.White;
                if ((Diag & 0x8000) != 0) _frmMainWindow._frmCurrents.ind3_VOUT_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind3_VOUT_W.BackColor = Color.White;
            }
            //else if (lastSignalParamPU.Signal.Name.Contains("5V"))
            else if (channel == 4)
            {
                _frmMainWindow._frmCurrents.lbl5_D.Text = Diag.ToString();
                _frmMainWindow._frmCurrents.lbl5_I.Text = String.Format("{0}", Math.Round(I * 0.000793, 3));
                _frmMainWindow._frmCurrents.lbl5_Uout.Text = String.Format("{0}", Math.Round(Uout * 0.00456, 3));
                _frmMainWindow._frmCurrents.lbl5_Uin.Text = String.Format("{0}", Math.Round(Uin * 0.00456, 3));
                _frmMainWindow._frmCurrents.lbl5_P.Text = String.Format("{0}", Math.Round(P * 0.0146, 3));
                _frmMainWindow._frmCurrents.lbl5_T.Text = String.Format("{0}", Math.Round(T * 0.0625, 3));

                if ((Diag & 0x01) != 0) _frmMainWindow._frmCurrents.ind5_Circ.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Circ.BackColor = Color.White;
                if ((Diag & 0x02) != 0) _frmMainWindow._frmCurrents.ind5_CML.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_CML.BackColor = Color.White;
                if ((Diag & 0x04) != 0) _frmMainWindow._frmCurrents.ind5_Temp_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Temp_F.BackColor = Color.White;
                if ((Diag & 0x08) != 0) _frmMainWindow._frmCurrents.ind5_IIN_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_IIN_F.BackColor = Color.White;
                if ((Diag & 0x10) != 0) _frmMainWindow._frmCurrents.ind5_VINOVER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_VINOVER_F.BackColor = Color.White;
                if ((Diag & 0x20) != 0) _frmMainWindow._frmCurrents.ind5_VINUNDER_F.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_VINUNDER_F.BackColor = Color.White;
                if ((Diag & 0x40) != 0) _frmMainWindow._frmCurrents.ind5_Dev.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Dev.BackColor = Color.White;
                if ((Diag & 0x80) != 0) _frmMainWindow._frmCurrents.ind5_Conf.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Conf.BackColor = Color.White;
                if ((Diag & 0x100) != 0) _frmMainWindow._frmCurrents.ind5_Ext.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Ext.BackColor = Color.White;
                if ((Diag & 0x200) != 0) _frmMainWindow._frmCurrents.ind5_Timer.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Timer.BackColor = Color.White;
                if ((Diag & 0x400) != 0) _frmMainWindow._frmCurrents.ind5_Temp_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Temp_W.BackColor = Color.White;
                if ((Diag & 0x800) != 0) _frmMainWindow._frmCurrents.ind5_Good.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_Good.BackColor = Color.White;
                if ((Diag & 0x1000) != 0) _frmMainWindow._frmCurrents.ind5_VINOVER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_VINOVER_W.BackColor = Color.White;
                if ((Diag & 0x2000) != 0) _frmMainWindow._frmCurrents.ind5_VINUNDER_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_VINUNDER_W.BackColor = Color.White;
                if ((Diag & 0x4000) != 0) _frmMainWindow._frmCurrents.ind5_IIN_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_IIN_W.BackColor = Color.White;
                if ((Diag & 0x8000) != 0) _frmMainWindow._frmCurrents.ind5_VOUT_W.BackColor = Color.LightGreen; else _frmMainWindow._frmCurrents.ind5_VOUT_W.BackColor = Color.White;
            }
        }

        #region Посылки, Обработчики и таймеры входящих ответных посылок
        public void udp_receivedPU(Command com)
        {
            Invoke((MethodInvoker)delegate
            {
                //отображаем буфер пришедшей команды
                ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.RX);

                //AddToList("TX: " + sPort.PadRight(10) + " >>> " + BitConverter.ToString(com.GetCommandByteBuf()).PadRight(100));
                AddToList("RX PU:".PadRight(8) + " <<< " + BitConverter.ToString(com.GetCommandByteBuf()).PadRight(60));

                if (com.result == CommandResult.Success) countSuccessPU++;
                else countTimeoutPU++;

                lblStatus.Text = String.Format("Статус обмена - {0}", com.result.ToString());
                lblSuccessPU.Text = String.Format("Успешных посылок ПУ - {0}", countSuccessPU.ToString());
                lblTimeOutsPU.Text = String.Format("Таймауты ПУ - {0}", countTimeoutPU.ToString());

                //анализируем характер ответа на последнюю отправленную команду
                switch (com.set_or_get)
                {
                    case SET_OR_GET.SET:    //если ответ пришел на СЕТ
                        {
                            //стартуем таймер, который оттикает DELAY и стартанет запрос GET
                            if (lastSignalParamPU.Signal.get == null) 
                            { 
                                _frmMainWindow.bCommandBusyPU = false;
                                FreePortAndChangeSignalStatus(lastSignalParamPU, lastSignalParamPK.bPortBusy, lastSignalParamPTVC.bPortBusy);
                                return; 
                            }
                            if (lastSignalParamPU.Signal.get.source == null)
                            {
                                if (com.data.Length >= 11)
                                {
                                    if (iCurUpdateCounter >= iCurUpdateFreq) //прореживаем вывод данных токов
                                    {
                                        ShowCurrents(com);
                                        iCurUpdateCounter = 0;
                                    }
                                    else
                                        iCurUpdateCounter++;
                                }
                            }
                            else
                            {
                                switch (lastSignalParamPU.Signal_type)
                                {
                                    case SIGNAL_SECTION.Manual:
                                    case SIGNAL_SECTION.Periodic:
                                        {
                                            StartTimerPU(com);
                                        }
                                        break;
                                    case SIGNAL_SECTION.Test:
                                        {
                                            lastSignalParamPK.Delay = lastSignalParamPU.Delay;
                                            lastSignalParamPTVC.Delay = lastSignalParamPU.Delay;

                                            StartTimerPK(com);
                                            StartTimerPU(com);
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case SET_OR_GET.GET:    //если ответ пришел на ГЕТ
                        {
                            //индекс сигнала-элемента среди списка сигналов списка
                            int indx;// = com.index;

                            //стендовый базовый сигнал, выдираемый из группы последнего выбранного сигнала-параметра
                            BaseSignal baseSign;

                            //значение группы
                            uint groupVal = 0;

                            //индикатор, к которому будем обращаться при анализе ответной посылки
                            Button ind;
                            CheckBox chk;
                            Label lbl;
                            BoardSignal signal = lastSignalParamPU.Signal;

                            List<BoardSignal> list = null;
                            Panel panel = null;

                            switch (lastSignalParamPU.Signal_type)
                            {
                                case SIGNAL_SECTION.Test:
                                    {
                                        list = testBoard.Scenario.test.listRun;
                                        panel = panelTestSignals;
                                    }
                                    break;
                                case SIGNAL_SECTION.Periodic:
                                    {
                                        list = testBoard.Scenario.periodic.listRun;
                                        panel = panelPeriodicSignals;
                                    }
                                    break;
                                case SIGNAL_SECTION.Manual:
                                    {
                                        list = testBoard.Scenario.manual.listRun;
                                        panel = panelManualSignals;
                                    }
                                    break;
                            }

                            //копируем буфер пришедшей команды в групповой буфер секции GET
                            int len = 0;
                            len = (com.data.Length < signal.get.source.group.valueRX.Length) ? com.data.Length : signal.get.source.group.valueRX.Length;
                            Array.Copy(com.data, signal.get.source.group.valueRX, len);

                            switch (signal.get.signalType)
                            {
                                case SIGNALTYPE.LOGIC:
                                    {
                                        if (!signal.get.source.isRS)
                                        {
                                            //Собираем из буфера число - значение группы
                                            for (int i = 0; i < signal.get.source.group.valueRX.Length; i++)
                                            {
                                                groupVal += (uint)(signal.get.source.group.valueRX[i] << (8 * i));
                                            }

                                            //пробегаемся по всем базовым сигналам текущей группы
                                            for (int i = 0; i < signal.get.source.group.baseSignals.Count; i++)
                                            {
                                                //выдернули сигнал из списка группы
                                                baseSign = (BaseSignal)signal.get.source.group.baseSignals[i];

                                                //проверяем наличие текущего базового сигнала в загруженном списке и получаем его индекс в этом списке.
                                                indx = isListSignal(baseSign, list);
                                                if (indx != -1)
                                                {
                                                    //получаем ссылку на индикатор-лампочку по индексу
                                                    ind = (Button)panel.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();
                                                    chk = (CheckBox)panel.Controls.Find("Chk" + indx.ToString(), false).FirstOrDefault();

                                                    if (chk.Checked == ((groupVal & baseSign.iValue) != 0))
                                                    {
                                                        //проверяем наличие сигнала в групповом значении
                                                        if ((groupVal & baseSign.iValue) != 0)
                                                        {
                                                            ind.BackColor = Color.Green;
                                                        }
                                                        else
                                                        {
                                                            ind.BackColor = Color.White;
                                                        }
                                                        //chk.ForeColor = Color.Black;
                                                    }
                                                    else
                                                    {
                                                        if (list[indx].set != null)
                                                        {
                                                            ind.BackColor = Color.Red;
                                                            chk.ForeColor = Color.Red;

                                                            if (panel == panelTestSignals)
                                                            {
                                                                cntError++;
                                                                listProblemSignals.Add(chk.Text);
                                                            }
                                                        }
                                                    }
                                                    
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //провести сравнение
                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                if ((list[i].get != null) && (list[i].get.source != null) && (signal.get.source.group == list[i].get.source.group))
                                                {
                                                    if (list[i].get.signalType != SIGNALTYPE.LOGIC) continue;
                                                    ind = (Button)panel.Controls.Find("IndIn" + i.ToString(), false).FirstOrDefault();
                                                    chk = (CheckBox)panel.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();

                                                    uint mask = 0;
                                                    uint uRes = 0;
                                                    if (list[i].get.source.bValue)
                                                    {
                                                        mask = list[i].get.valueon;
                                                    }
                                                    else
                                                    {
                                                        mask = ~list[i].get.valueon;
                                                        uRes = mask;
                                                    }
                                                    if (chk.Checked == ((list[i].get.source.group.valueRX[list[i].get.indexbyte] & mask) != uRes))
                                                    {
                                                        if ((list[i].get.source.group.valueRX[list[i].get.indexbyte] & mask) != uRes)
                                                        {
                                                            ind.BackColor = Color.Green;
                                                        }
                                                        else
                                                        {
                                                            ind.BackColor = Color.White;
                                                        }
                                                        //chk.ForeColor = Color.Black;
                                                    }
                                                    else
                                                    {
                                                        if (list[i].set != null)
                                                        {
                                                            ind.BackColor = Color.Red;
                                                            chk.ForeColor = Color.Red;
                                                            if (panel == panelTestSignals)
                                                            {
                                                                cntError++;
                                                                listProblemSignals.Add(chk.Text);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case SIGNALTYPE.ANALOG:
                                    {
                                        if (!signal.get.source.isRS)
                                        {

                                        }
                                        else
                                        {
                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                if ((list[i].get != null) && (list[i].get.source != null) && (signal.get.source.group == list[i].get.source.group))
                                                {
                                                    if (list[i].get.signalType != SIGNALTYPE.ANALOG) continue;
                                                    int num = 0;
                                                    lbl = (Label)panel.Controls.Find("LblValMin" + i.ToString(), false).FirstOrDefault();
                                                    for (int j = 0; j < signal.get.sizebyte; j++)
                                                    {
                                                        //num += (int)(signal.get.source.group.valueRX[signal.get.indexbyte + j] << (8 * j));
                                                        num += (int)(list[i].get.source.group.valueRX[list[i].get.indexbyte + j] << (8 * j));
                                                    }
                                                    lbl.Text = num.ToString();
                                                    if ((num >= list[i].get.min) && (num <= list[i].get.max))
                                                        lbl.ForeColor = Color.Green;
                                                    else
                                                        lbl.ForeColor = Color.Red;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }  
                        }
                        break;
                    case SET_OR_GET.DESET:  //Если ответ пришел на ДЕСЕТ
                        {
                            int indx = com.index;
                            uint groupVal = 0;

                            BoardSignal signal = lastSignalParamPU.Signal;
                            Panel panel = null;

                            switch (lastSignalParamPU.Signal_type)
                            {
                                case SIGNAL_SECTION.Test:
                                    panel = panelTestSignals;
                                    break;
                                case SIGNAL_SECTION.Periodic:
                                    panel = panelPeriodicSignals;
                                    break;
                                case SIGNAL_SECTION.Manual:
                                    panel = panelManualSignals;
                                    break;
                            }

                            Button ind = (Button)panel.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            int len = 0;
                            len = (com.data.Length < signal.set.source.group.valueRX.Length) ? com.data.Length : signal.set.source.group.valueRX.Length;
                            Array.Copy(com.data, signal.set.source.group.valueRX, len);

                            switch (signal.set.signalType)
                            {
                                case SIGNALTYPE.LOGIC:
                                    {
                                        if (!signal.set.source.isRS)
                                        {
                                            for (int i = 0; i < signal.set.source.group.valueRX.Length; i++)
                                            {
                                                groupVal += (uint)(signal.set.source.group.valueRX[i] << (8 * i));
                                            }

                                            if ((groupVal & signal.set.valueon) != 0)
                                                if (ind != null) ind.BackColor = Color.Green;
                                                else
                                                    if (ind != null) ind.BackColor = Color.White;
                                        }
                                        else
                                        {
                                            if ((signal.set.source.group.valueRX[signal.set.indexbyte] & signal.set.valueon) != 0)
                                                if (ind != null) ind.BackColor = Color.Green;
                                                else
                                                    if (ind != null) ind.BackColor = Color.White;
                                        }
                                    }
                                    break;
                                case SIGNALTYPE.ARRAY:
                                    {

                                    }
                                    break;
                                case SIGNALTYPE.ANALOG:
                                    {

                                    }
                                    break;
                            }


                            //Инициируем запуск еще одного GET для считывания выключенного сигнала 
                            if (lastSignalParamPU.Signal.get == null) 
                            { 
                                _frmMainWindow.bCommandBusyPU = false;
                                FreePortAndChangeSignalStatus(lastSignalParamPU, lastSignalParamPK.bPortBusy, lastSignalParamPTVC.bPortBusy);

                                return; 
                            }
                            if (lastSignalParamPU.Signal.get.source == null)
                            {

                            }
                            else
                            {
                                switch (lastSignalParamPU.Signal_type)
                                {
                                    case SIGNAL_SECTION.Manual:
                                    case SIGNAL_SECTION.Periodic:
                                        {
                                            StartTimerPU(com);
                                        }
                                        break;
                                    case SIGNAL_SECTION.Test:
                                        {
                                            lastSignalParamPK.Delay = lastSignalParamPU.Delay;
                                            lastSignalParamPTVC.Delay = lastSignalParamPU.Delay;

                                            StartTimerPK(com);
                                            StartTimerPU(com);
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case SET_OR_GET.OTHER:
                        break;
                }
                _frmMainWindow.bCommandBusyPU = false;
            });

            
        }
        void StartTimerPU(Command com)
        {
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += tmr_TickPU;
            tmr.Interval = lastSignalParamPU.Delay;
            lastSignalParamPU.bPortBusy = true;
            tmr.Tag = com.index;
            curGroupIndex1 = 0;
            tmr.Start();
        }
        void tmr_TickPU(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = (System.Windows.Forms.Timer)sender;
            int ind = (int)tmr.Tag;
            tmr.Stop();

            switch (lastSignalParamPU.Signal_type)
            {
                case SIGNAL_SECTION.Test:
                    {
                        if (curGroupIndex1 >= listPU.listTestSignals.Count)
                        {
                            tmr.Stop();
                            curGroupIndex1 = 0;
                            FreePortAndChangeSignalStatus(lastSignalParamPU, lastSignalParamPK.bPortBusy, lastSignalParamPTVC.bPortBusy);
                        }
                        else
                        {
                            if(!CreateAndSendSignal(new SignalParameters(listPU.listTestSignals[curGroupIndex1].Signal.get.source.port,
                            listPU.listTestSignals[curGroupIndex1].Signal,
                             SIGNAL_SECTION.Test,
                             listPU.listTestSignals[curGroupIndex1].Signal.Delay),
                             listPU.listTestSignals[curGroupIndex1].Index,
                             SET_OR_GET.GET))
                            {
                                curGroupIndex1--;
                            }

                            curGroupIndex1++;

                            tmr.Interval = 20;
                            tmr.Start();
                        } 
                    }
                    break;
                case SIGNAL_SECTION.Periodic:
                    {
                        if (lastSignalParamPU.Signal.get != null && lastSignalParamPU.Signal.get.source!=null)
                            CreateAndSendSignal(lastSignalParamPU,
                                0,
                                SET_OR_GET.GET);

                        FreePortAndChangeSignalStatus(lastSignalParamPU, lastSignalParamPK.bPortBusy, lastSignalParamPTVC.bPortBusy);
                    }
                    break;
                case SIGNAL_SECTION.Manual:
                    {
                        if (lastSignalParamPU.Signal.get != null && lastSignalParamPU.Signal.get.source != null)
                            CreateAndSendSignal(lastSignalParamPU,
                                0,
                                SET_OR_GET.GET);

                        FreePortAndChangeSignalStatus(lastSignalParamPU, lastSignalParamPK.bPortBusy, lastSignalParamPTVC.bPortBusy);
                    }
                    break;
            }
        }
        public void udp_receivedPK(Command com)
        {
            Invoke((MethodInvoker)delegate
            {
                ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.RX);

                AddToList("RX PK:".PadRight(8) + " <<< " + BitConverter.ToString(com.GetCommandByteBuf()).PadRight(60));
                //AddToList("RX PK:     <<< " + BitConverter.ToString(com.GetCommandByteBuf()));

                if (com.result == CommandResult.Success) countSuccessPK++;
                else countTimeoutPK++;

                lblStatus.Text = String.Format("Статус обмена - {0}", com.result.ToString());
                lblSuccessPK.Text = String.Format("Успешных посылок ПК - {0}", countSuccessPK.ToString());
                lblTimeOutsPK.Text = String.Format("Таймауты ПК - {0}", countTimeoutPK.ToString());


                switch (com.set_or_get)
                {
                    case SET_OR_GET.SET:
                        {
                            //стартуем таймер, который оттикает DELAY и стартанет запрос GET

                            if (lastSignalParamPK.Signal.get == null) 
                            { 
                                _frmMainWindow.bCommandBusyPK = false;
                                FreePortAndChangeSignalStatus(lastSignalParamPK, lastSignalParamPU.bPortBusy, lastSignalParamPTVC.bPortBusy);
                                return; 
                            }
                            if (lastSignalParamPK.Signal.get.source == null)
                            {

                            }
                            else
                            {
                                switch (lastSignalParamPK.Signal_type)
                                {
                                    case SIGNAL_SECTION.Manual:
                                    case SIGNAL_SECTION.Periodic:
                                        {
                                            StartTimerPK(com);
                                        }
                                        break;
                                    case SIGNAL_SECTION.Test:
                                        {
                                            lastSignalParamPU.Delay = lastSignalParamPK.Delay;
                                            lastSignalParamPTVC.Delay = lastSignalParamPK.Delay;

                                            StartTimerPK(com);
                                            StartTimerPU(com);
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                }
                            }

                        }
                        break;
                    case SET_OR_GET.GET:
                        {
                            //индекс сигнала-элемента среди списка сигналов списка
                            int indx;// = com.index;

                            //стендовый базовый сигнал, выдираемый из группы последнего выбранного сигнала-параметра
                            BaseSignal baseSign;

                            //значение группы
                            uint groupVal = 0;

                            //индикатор, к которому будем обращаться при анализе ответной посылки
                            Button ind;
                            CheckBox chk;
                            Label lbl;
                            BoardSignal signal = lastSignalParamPK.Signal;

                            List<BoardSignal> list = null;
                            Panel panel = null;

                            switch (lastSignalParamPK.Signal_type)
                            {
                                case SIGNAL_SECTION.Test:
                                    {
                                        list = testBoard.Scenario.test.listRun;
                                        panel = panelTestSignals;
                                    }
                                    break;
                                case SIGNAL_SECTION.Periodic:
                                    {
                                        list = testBoard.Scenario.periodic.listRun;
                                        panel = panelPeriodicSignals;
                                    }
                                    break;
                                case SIGNAL_SECTION.Manual:
                                    {
                                        list = testBoard.Scenario.manual.listRun;
                                        panel = panelManualSignals;
                                    }
                                    break;
                            }

                            //копируем буфер пришедшей команды в групповой буфер секции GET

                            int len = 0;
                            len = (com.data.Length < signal.get.source.group.valueRX.Length) ? com.data.Length : signal.get.source.group.valueRX.Length;
                            Array.Copy(com.data, signal.get.source.group.valueRX, len);

                            switch (signal.get.signalType)
                            {
                                case SIGNALTYPE.LOGIC:
                                    {
                                        if (!signal.get.source.isRS)
                                        {
                                            //Собираем из буфера число - значение группы
                                            for (int i = 0; i < signal.get.source.group.valueRX.Length; i++)
                                            {
                                                groupVal += (uint)(signal.get.source.group.valueRX[i] << (8 * i));
                                            }

                                            //пробегаемся по всем базовым сигналам текущей группы
                                            for (int i = 0; i < signal.get.source.group.baseSignals.Count; i++)
                                            {
                                                //выдернули сигнал из списка группы
                                                baseSign = (BaseSignal)signal.get.source.group.baseSignals[i];

                                                //проверяем наличие текущего базового сигнала в загруженном списке и получаем его индекс в этом списке.
                                                indx = isListSignal(baseSign, list);
                                                if (indx != -1)
                                                {
                                                    //получаем ссылку на индикатор-лампочку по индексу
                                                    ind = (Button)panel.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();
                                                    chk = (CheckBox)panel.Controls.Find("Chk" + indx.ToString(), false).FirstOrDefault();
                                                    if (chk.Checked == ((groupVal & baseSign.iValue) != 0))
                                                    {
                                                        //проверяем наличие сигнала в групповом значении
                                                        if ((groupVal & baseSign.iValue) != 0)
                                                        {
                                                            ind.BackColor = Color.Green;
                                                        }
                                                        else
                                                        { 
                                                            ind.BackColor = Color.White; 
                                                        }
                                                        //chk.ForeColor = Color.Black;
                                                    }
                                                    else
                                                    {
                                                        if (list[indx].set != null)
                                                        {
                                                            ind.BackColor = Color.Red;
                                                            chk.ForeColor = Color.Red;
                                                            if (panel == panelTestSignals)
                                                            {
                                                                cntError++;
                                                                listProblemSignals.Add(chk.Text);
                                                            }
                                                        }
                                                    }
                                                    
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //провести сравнение
                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                if (((list[i].get != null) && (list[i].get.source != null)) && (signal.get.source.group == list[i].get.source.group))
                                                {
                                                    if (list[i].get.signalType != SIGNALTYPE.LOGIC) continue;

                                                    ind = (Button)panel.Controls.Find("IndIn" + i.ToString(), false).FirstOrDefault();
                                                    chk = (CheckBox)panel.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();

                                                    uint mask = 0;
                                                    uint uRes = 0;
                                                    if (list[i].get.source.bValue)
                                                    {
                                                        mask = list[i].get.valueon;
                                                    }
                                                    else
                                                    {
                                                        mask = ~list[i].get.valueon;
                                                        uRes = mask;
                                                    }
                                                    if (chk.Checked == ((list[i].get.source.group.valueRX[list[i].get.indexbyte] & mask) != uRes))
                                                    {
                                                        if ((list[i].get.source.group.valueRX[list[i].get.indexbyte] & mask) != uRes)
                                                        {
                                                            ind.BackColor = Color.Green;
                                                        }
                                                        else
                                                        {
                                                            ind.BackColor = Color.White;
                                                        }
                                                        //chk.ForeColor = Color.Black;
                                                    }
                                                    else
                                                    {
                                                        if (list[i].set != null)
                                                        {
                                                            ind.BackColor = Color.Red;
                                                            chk.ForeColor = Color.Red;
                                                            if (panel == panelTestSignals)
                                                            {
                                                                cntError++;
                                                                listProblemSignals.Add(chk.Text);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case SIGNALTYPE.ANALOG:
                                    {
                                        if (!signal.get.source.isRS)
                                        {

                                        }
                                        else
                                        {
                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                if ((list[i].get != null) && (list[i].get.source != null) && (signal.get.source.group == list[i].get.source.group))
                                                {
                                                    if (list[i].get.signalType != SIGNALTYPE.ANALOG) continue;

                                                    int num = 0;
                                                    lbl = (Label)panel.Controls.Find("LblValMin" + i.ToString(), false).FirstOrDefault();
                                                    for (int j = 0; j < signal.get.sizebyte; j++)
                                                    {
                                                        //num += (int)(signal.get.source.group.valueRX[signal.get.indexbyte + j] << (8 * j));
                                                        num += (int)(list[i].get.source.group.valueRX[list[i].get.indexbyte + j] << (8 * j));
                                                    }
                                                    lbl.Text = num.ToString();
                                                    if ((num >= list[i].get.min) && (num <= list[i].get.max))
                                                        lbl.ForeColor = Color.Green;
                                                    else
                                                        lbl.ForeColor = Color.Red;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case SET_OR_GET.DESET:
                        {
                            int indx = com.index;
                            uint groupVal = 0;

                            BoardSignal signal = lastSignalParamPK.Signal;
                            Panel panel = null;

                            switch (lastSignalParamPK.Signal_type)
                            {
                                case SIGNAL_SECTION.Test:
                                    panel = panelTestSignals;
                                    break;
                                case SIGNAL_SECTION.Periodic:
                                    panel = panelPeriodicSignals;
                                    break;
                                case SIGNAL_SECTION.Manual:
                                    panel = panelManualSignals;
                                    break;
                            }

                            Button ind = (Button)panel.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            int len = 0;
                            len = (com.data.Length < signal.set.source.group.valueRX.Length) ? com.data.Length : signal.set.source.group.valueRX.Length;
                            Array.Copy(com.data, signal.set.source.group.valueRX, len);

                            switch (signal.set.signalType)
                            {
                                case SIGNALTYPE.LOGIC:
                                    {
                                        if (!signal.set.source.isRS)
                                        {
                                            for (int i = 0; i < signal.set.source.group.valueRX.Length; i++)
                                            {
                                                groupVal += (uint)(signal.set.source.group.valueRX[i] << (8 * i));
                                            }

                                            if ((groupVal & signal.set.valueon) != 0)
                                                if(ind != null) ind.BackColor = Color.Green;
                                            else
                                                if(ind != null) ind.BackColor = Color.White;
                                        }
                                        else
                                        {
                                            if ((signal.set.source.group.valueRX[signal.set.indexbyte] & signal.set.valueon) != 0)
                                                if (ind != null) ind.BackColor = Color.Green;
                                            else
                                                if (ind != null) ind.BackColor = Color.White;
                                        }
                                    }
                                    break;
                                case SIGNALTYPE.ARRAY:
                                    {

                                    }
                                    break;
                                case SIGNALTYPE.ANALOG:
                                    {

                                    }
                                    break;
                            }


                            //Инициируем запуск еще одного GET для считывания выключенного сигнала 
                            if (lastSignalParamPK.Signal.get == null) 
                            { 
                                _frmMainWindow.bCommandBusyPK = false;
                                FreePortAndChangeSignalStatus(lastSignalParamPK, lastSignalParamPU.bPortBusy, lastSignalParamPTVC.bPortBusy);
                                return; 
                            }
                            if (lastSignalParamPK.Signal.get.source == null)
                            {

                            }
                            else
                            {
                                switch (lastSignalParamPK.Signal_type)
                                {
                                    case SIGNAL_SECTION.Manual:
                                    case SIGNAL_SECTION.Periodic:
                                        {
                                            StartTimerPK(com);
                                        }
                                        break;
                                    case SIGNAL_SECTION.Test:
                                        {
                                            lastSignalParamPU.Delay = lastSignalParamPK.Delay;
                                            lastSignalParamPTVC.Delay = lastSignalParamPK.Delay;

                                            StartTimerPK(com);
                                            StartTimerPU(com);
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case SET_OR_GET.OTHER:
                        break;
                }
                _frmMainWindow.bCommandBusyPK = false;
            });

            
        }
        void StartTimerPK(Command com)
        {
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += tmr_TickPK;
            tmr.Interval = lastSignalParamPK.Delay;
            lastSignalParamPK.bPortBusy = true;
            tmr.Tag = com.index;
            curGroupIndex2 = 0;
            tmr.Start();
        }
        void tmr_TickPK(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = (System.Windows.Forms.Timer)sender;
            int ind = (int)tmr.Tag;
            tmr.Stop();

            switch (lastSignalParamPK.Signal_type)
            {
                case SIGNAL_SECTION.Test:
                    {
                        if (curGroupIndex2 >= listPK.listTestSignals.Count)
                        {
                            tmr.Stop();
                            curGroupIndex2 = 0;

                            FreePortAndChangeSignalStatus(lastSignalParamPK, lastSignalParamPU.bPortBusy, lastSignalParamPTVC.bPortBusy);
                        }
                        else
                        {
                            if(!CreateAndSendSignal(new SignalParameters(listPK.listTestSignals[curGroupIndex2].Signal.get.source.port,
                            listPK.listTestSignals[curGroupIndex2].Signal,
                             SIGNAL_SECTION.Test,
                             listPK.listTestSignals[curGroupIndex2].Signal.Delay),
                             listPK.listTestSignals[curGroupIndex2].Index,
                             SET_OR_GET.GET))
                            {
                                curGroupIndex2--;
                            }

                            curGroupIndex2++;

                            tmr.Interval = 20;
                            tmr.Start();
                        }
                    }
                    break;
                case SIGNAL_SECTION.Periodic:
                    {
                        if (lastSignalParamPK.Signal.get != null && lastSignalParamPK.Signal.get.source!=null)
                            CreateAndSendSignal(lastSignalParamPK,
                                0,
                                SET_OR_GET.GET);

                        FreePortAndChangeSignalStatus(lastSignalParamPK, lastSignalParamPU.bPortBusy, lastSignalParamPTVC.bPortBusy);
                    }
                    break;
                case SIGNAL_SECTION.Manual:
                    {
                        if (lastSignalParamPK.Signal.get != null && lastSignalParamPK.Signal.get.source != null)
                            CreateAndSendSignal(lastSignalParamPK,
                                0,
                                SET_OR_GET.GET);

                        FreePortAndChangeSignalStatus(lastSignalParamPK, lastSignalParamPU.bPortBusy, lastSignalParamPTVC.bPortBusy);
                    }
                    break;
            }
        }
        public void udp_receivedPTVC(Command com)
        {

            Invoke((MethodInvoker)delegate
            {
                ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.RX);

                AddToList("RX PTVC:".PadRight(8) + " >>> " + BitConverter.ToString(com.GetCommandByteBuf()).PadRight(60));
                //AddToList("RX PTVC:   <<< " + BitConverter.ToString(com.GetCommandByteBuf()));
                if (com.result == CommandResult.Success) countSuccessPTVC++;
                else countTimeoutPTVC++;

                lblStatus.Text = String.Format("Статус обмена - {0}", com.result.ToString());
                lblSuccessPTVC.Text = String.Format("Успешных посылок ПТВЦ - {0}", countSuccessPTVC.ToString());
                lblTimeOutsPTVC.Text = String.Format("Таймауты ПТВЦ - {0}", countTimeoutPTVC.ToString());

                switch (com.set_or_get)
                {
                    case SET_OR_GET.SET:
                        {
                            //стартуем таймер, который оттикает DELAY и стартанет запрос GET
                            //StartTimerPTVC(com);
                            /*lastSignalParamPK.Delay = lastSignalParamPTVC.Delay;
                            lastSignalParamPU.Delay = lastSignalParamPTVC.Delay;*/
                            if (lastSignalParamPTVC.Signal.get == null) 
                            { 
                                _frmMainWindow.bCommandBusyPTVC = false;
                                FreePortAndChangeSignalStatus(lastSignalParamPTVC, lastSignalParamPU.bPortBusy, lastSignalParamPK.bPortBusy);
                                return; 
                            }
                            if (lastSignalParamPTVC.Signal.get.source == null)
                            {

                            }
                            else
                            {
                                switch (lastSignalParamPTVC.Signal_type)
                                {
                                    case SIGNAL_SECTION.Manual:
                                    case SIGNAL_SECTION.Periodic:
                                        {
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                    case SIGNAL_SECTION.Test:
                                        {
                                            lastSignalParamPK.Delay = lastSignalParamPTVC.Delay;
                                            lastSignalParamPU.Delay = lastSignalParamPTVC.Delay;

                                            StartTimerPK(com);
                                            StartTimerPU(com);
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case SET_OR_GET.GET:
                        {
                            //индекс сигнала-элемента среди списка сигналов списка
                            int indx;// = com.index;

                            //стендовый базовый сигнал, выдираемый из группы последнего выбранного сигнала-параметра
                            BaseSignal baseSign;

                            //значение группы
                            uint groupVal = 0;

                            //индикатор, к которому будем обращаться при анализе ответной посылки
                            Button ind;
                            CheckBox chk;
                            Label lbl;
                            BoardSignal signal = lastSignalParamPTVC.Signal;

                            List<BoardSignal> list = null;
                            Panel panel = null;

                            switch (lastSignalParamPTVC.Signal_type)
                            {
                                case SIGNAL_SECTION.Test:
                                    {
                                        list = testBoard.Scenario.test.listRun;
                                        panel = panelTestSignals;
                                    }
                                    break;
                                case SIGNAL_SECTION.Periodic:
                                    {
                                        list = testBoard.Scenario.periodic.listRun;
                                        panel = panelPeriodicSignals;
                                    }
                                    break;
                                case SIGNAL_SECTION.Manual:
                                    {
                                        list = testBoard.Scenario.manual.listRun;
                                        panel = panelManualSignals;
                                    }
                                    break;
                            }

                            //копируем буфер пришедшей команды в групповой буфер секции GET
                            int len = 0;
                            len = (com.data.Length < signal.get.source.group.valueRX.Length) ? com.data.Length : signal.get.source.group.valueRX.Length;
                            Array.Copy(com.data, signal.get.source.group.valueRX, len);


                            switch (signal.get.signalType)
                            {
                                case SIGNALTYPE.LOGIC:
                                    {
                                        if (!signal.get.source.isRS)
                                        {
                                            //Собираем из буфера число - значение группы
                                            for (int i = 0; i < signal.get.source.group.valueRX.Length; i++)
                                            {
                                                groupVal += (uint)(signal.get.source.group.valueRX[i] << (8 * i));
                                            }

                                            //пробегаемся по всем базовым сигналам текущей группы
                                            for (int i = 0; i < signal.get.source.group.baseSignals.Count; i++)
                                            {
                                                //выдернули сигнал из списка группы
                                                baseSign = (BaseSignal)signal.get.source.group.baseSignals[i];

                                                //проверяем наличие текущего базового сигнала в загруженном списке и получаем его индекс в этом списке.
                                                indx = isListSignal(baseSign, list);
                                                if (indx != -1)
                                                {
                                                    //получаем ссылку на индикатор-лампочку по индексу
                                                    ind = (Button)panel.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();
                                                    chk = (CheckBox)panel.Controls.Find("Chk" + indx.ToString(), false).FirstOrDefault();
                                                    if (chk.Checked == ((groupVal & baseSign.iValue) != 0))
                                                    {
                                                        //проверяем наличие сигнала в групповом значении
                                                        if ((groupVal & baseSign.iValue) != 0)
                                                        {
                                                            ind.BackColor = Color.Green;
                                                        }
                                                        else
                                                        {
                                                            ind.BackColor = Color.White;
                                                        }
                                                        //chk.ForeColor = Color.Black;
                                                    }
                                                    else
                                                    {
                                                        if (list[indx].set != null)
                                                        {
                                                            ind.BackColor = Color.Red;
                                                            chk.ForeColor = Color.Red;
                                                            if (panel == panelTestSignals)
                                                            {
                                                                cntError++;
                                                                listProblemSignals.Add(chk.Text);
                                                            }
                                                        }
                                                    }
                                                    
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //провести сравнение
                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                if ((list[i].get != null) && (list[i].get.source != null) && (signal.get.source.group == list[i].get.source.group))
                                                {
                                                    if (list[i].get.signalType != SIGNALTYPE.LOGIC) continue;
                                                    ind = (Button)panel.Controls.Find("IndIn" + i.ToString(), false).FirstOrDefault();
                                                    chk = (CheckBox)panel.Controls.Find("Chk" + i.ToString(), false).FirstOrDefault();

                                                    uint mask = 0;
                                                    uint uRes = 0;
                                                    if (list[i].get.source.bValue)
                                                    {
                                                        mask = list[i].get.valueon;
                                                    }
                                                    else
                                                    {
                                                        mask = ~list[i].get.valueon;
                                                        uRes = mask;
                                                    }
                                                    if (chk.Checked == ((list[i].get.source.group.valueRX[list[i].get.indexbyte] & mask) != uRes))
                                                    {
                                                        if ((list[i].get.source.group.valueRX[list[i].get.indexbyte] & mask) != uRes)
                                                        {
                                                            ind.BackColor = Color.Green;
                                                        }
                                                        else
                                                        {
                                                            ind.BackColor = Color.White;
                                                        }
                                                        //chk.ForeColor = Color.Black;
                                                    }
                                                    else
                                                    {
                                                        if (list[i].set != null)
                                                        {
                                                            ind.BackColor = Color.Red;
                                                            chk.ForeColor = Color.Red;
                                                            if (panel == panelTestSignals)
                                                            {
                                                                cntError++;
                                                                listProblemSignals.Add(chk.Text);
                                                            }
                                                        }
                                                    }
                                                    
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case SIGNALTYPE.ANALOG:
                                    {
                                        if (!signal.get.source.isRS)
                                        {

                                        }
                                        else
                                        {
                                            for (int i = 0; i < list.Count; i++)
                                            {
                                                if ((list[i].get != null) && (list[i].get.source != null) && (signal.get.source.group == list[i].get.source.group))
                                                {
                                                    if (list[i].get.signalType != SIGNALTYPE.ANALOG) continue;
                                                    int num = 0;
                                                    lbl = (Label)panel.Controls.Find("LblValMin" + i.ToString(), false).FirstOrDefault();
                                                    for (int j = 0; j < signal.get.sizebyte; j++)
                                                    {
                                                        //num += (int)(signal.get.source.group.valueRX[signal.get.indexbyte + j] << (8 * j));
                                                        num += (int)(list[i].get.source.group.valueRX[list[i].get.indexbyte + j] << (8 * j));
                                                    }
                                                    lbl.Text = num.ToString();
                                                    if ((num >= list[i].get.min) && (num <= list[i].get.max))
                                                        lbl.ForeColor = Color.Green;
                                                    else
                                                        lbl.ForeColor = Color.Red;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case SET_OR_GET.DESET:
                        {
                            int indx = com.index;
                            uint groupVal = 0;

                            BoardSignal signal = lastSignalParamPTVC.Signal;
                            Panel panel = null;

                            switch (lastSignalParamPTVC.Signal_type)
                            {
                                case SIGNAL_SECTION.Test:
                                    panel = panelTestSignals;
                                    break;
                                case SIGNAL_SECTION.Periodic:
                                    panel = panelPeriodicSignals;
                                    break;
                                case SIGNAL_SECTION.Manual:
                                    panel = panelManualSignals;
                                    break;
                            }

                            Button ind = (Button)panel.Controls.Find("IndIn" + indx.ToString(), false).FirstOrDefault();

                            int len = 0;
                            len = (com.data.Length < signal.set.source.group.valueRX.Length) ? com.data.Length : signal.set.source.group.valueRX.Length;
                            Array.Copy(com.data, signal.set.source.group.valueRX, len);

                            switch (signal.set.signalType)
                            {
                                case SIGNALTYPE.LOGIC:
                                    {
                                        if (!signal.set.source.isRS)
                                        {
                                            for (int i = 0; i < signal.set.source.group.valueRX.Length; i++)
                                            {
                                                groupVal += (uint)(signal.set.source.group.valueRX[i] << (8 * i));
                                            }

                                            if ((groupVal & signal.set.valueon) != 0)
                                                if (ind != null) ind.BackColor = Color.Green;
                                            else
                                                if (ind != null) ind.BackColor = Color.White;
                                        }
                                        else
                                        {
                                            if ((signal.set.source.group.valueRX[signal.set.indexbyte] & signal.set.valueon) != 0)
                                                if (ind != null) ind.BackColor = Color.Green;
                                            else
                                                if (ind != null) ind.BackColor = Color.White;
                                        }
                                    }
                                    break;
                                case SIGNALTYPE.ARRAY:
                                    {

                                    }
                                    break;
                                case SIGNALTYPE.ANALOG:
                                    {

                                    }
                                    break;
                            }
                            
                            //Инициируем запуск еще одного GET для считывания выключенного сигнала 
                            if (lastSignalParamPTVC.Signal.get == null) 
                            { 
                                _frmMainWindow.bCommandBusyPTVC = false;
                                FreePortAndChangeSignalStatus(lastSignalParamPTVC, lastSignalParamPU.bPortBusy, lastSignalParamPK.bPortBusy);
                                return; 
                            }
                            if (lastSignalParamPTVC.Signal.get.source == null)
                            {

                            }
                            else
                            {
                                switch (lastSignalParamPTVC.Signal_type)
                                {
                                    case SIGNAL_SECTION.Manual:
                                    case SIGNAL_SECTION.Periodic:
                                        {
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                    case SIGNAL_SECTION.Test:
                                        {
                                            lastSignalParamPK.Delay = lastSignalParamPTVC.Delay;
                                            lastSignalParamPU.Delay = lastSignalParamPTVC.Delay;

                                            StartTimerPK(com);
                                            StartTimerPU(com);
                                            StartTimerPTVC(com);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case SET_OR_GET.OTHER:
                        break;
                }
                _frmMainWindow.bCommandBusyPTVC = false;
            });

            
        }
        void StartTimerPTVC(Command com)
        {
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += tmr_TickPTVC;
            tmr.Interval = lastSignalParamPTVC.Delay;
            lastSignalParamPTVC.bPortBusy = true;
            tmr.Tag = com.index;
            curGroupIndex3 = 0;
            tmr.Start();
        }
        void tmr_TickPTVC(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = (System.Windows.Forms.Timer)sender;
            int ind = (int)tmr.Tag;
            tmr.Stop();

            switch (lastSignalParamPTVC.Signal_type)
            {
                case SIGNAL_SECTION.Test:
                    {
                        if (curGroupIndex3 >= listPTVC.listTestSignals.Count)
                        {
                            tmr.Stop();
                            curGroupIndex3 = 0;

                            FreePortAndChangeSignalStatus(lastSignalParamPTVC, lastSignalParamPU.bPortBusy, lastSignalParamPK.bPortBusy);

                        }
                        else
                        {
                            if(!CreateAndSendSignal(new SignalParameters(listPTVC.listTestSignals[curGroupIndex3].Signal.get.source.port,
                            listPTVC.listTestSignals[curGroupIndex3].Signal,
                            SIGNAL_SECTION.Test,
                            listPTVC.listTestSignals[curGroupIndex3].Signal.Delay),
                            listPTVC.listTestSignals[curGroupIndex3].Index,
                            SET_OR_GET.GET))
                            {
                                curGroupIndex3--;
                            }

                            curGroupIndex3++;

                            tmr.Interval = 20;
                            tmr.Start();
                        }
                    }
                    break;
                case SIGNAL_SECTION.Periodic:
                    {
                        if (lastSignalParamPTVC.Signal.get != null && lastSignalParamPTVC.Signal.get.source != null)
                            CreateAndSendSignal(lastSignalParamPTVC,
                                0,
                                SET_OR_GET.GET);

                        FreePortAndChangeSignalStatus(lastSignalParamPTVC, lastSignalParamPU.bPortBusy, lastSignalParamPK.bPortBusy);
                    }
                    break;
                case SIGNAL_SECTION.Manual:
                    {
                        if (lastSignalParamPTVC.Signal.get != null && lastSignalParamPTVC.Signal.get.source != null)
                            CreateAndSendSignal(lastSignalParamPTVC,
                                0,
                                SET_OR_GET.GET);

                        FreePortAndChangeSignalStatus(lastSignalParamPTVC, lastSignalParamPU.bPortBusy, lastSignalParamPK.bPortBusy);
                    }
                    break;
            }
        }


        private void AddToList(string str)
        {
            if (chkLogOn.Checked)
            {
                ListCount++;

                if (ListCount > 9999)
                {
                    lstLog.Items.Clear();
                    ListCount = 1;
                }
                string date = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                string outstr = string.Format("{0:0000}: {1} {2} {3} {4} {5} {6} {7} {8}", ListCount, date, str, signalStatus.ToString().PadRight(12), bPeriodicCycleDone.ToString().PadRight(5), bCycleRequest.ToString().PadRight(5), _frmMainWindow.bCommandBusyPU.ToString().PadRight(5), _frmMainWindow.bCommandBusyPK.ToString().PadRight(5), _frmMainWindow.bCommandBusyPTVC.ToString().PadRight(5));
                //lstLog.Items.Add(ListCount.ToString().PadRight(5) + ":  " + date.PadRight(12) + "   " + str + " " + signalStatus.ToString());

                lstLog.BeginUpdate();
                lstLog.Items.Add(outstr);
                lstLog.ClearSelected();
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
                lstLog.EndUpdate();
            }
        }

        public bool CreateAndSendSignal(SignalParameters signParam, int index, SET_OR_GET set_or_get)
        {
            //if (bExiting) return false;

            bool b1 = _frmMainWindow.bCommandBusyPK;
            bool b2 = _frmMainWindow.bCommandBusyPU;
            bool b3 = _frmMainWindow.bCommandBusyPTVC;
            switch (signParam.Port)
            {
                case (uint)PORT.PK:
                    if (_frmMainWindow.bCommandBusyPK) {  return false; }
                    break;
                case (uint)PORT.PU:
                    if (_frmMainWindow.bCommandBusyPU) {  return false; }
                    break;
                case (uint)PORT.PTVC:
                    if (_frmMainWindow.bCommandBusyPTVC) {  return false; }
                    break;
            }
            ushort addr = 0;
            ushort cnt = 0;
            uint port = 0;
            uint groupVal = 0;
            byte[] buf = null;
            byte[] tmp = null;
            Udp udp = null;
            CommandType comType = CommandType.TYPICAL; //признак отправки полноценной команды или же только буфера для сигнала ARRAY

            switch (set_or_get)
            {
                case SET_OR_GET.SET:
                    {
                        switch (signParam.Signal.set.signalType)
                        {
                            case SIGNALTYPE.LOGIC:
                                {
                                    cnt = signParam.Signal.set.sizebyte;
                                    port = signParam.Signal.set.source.port;
                                    buf = signParam.Signal.set.source.group.valueTX;
                                    if (!signParam.Signal.set.source.isRS)
                                    {
                                        addr = (ushort)signParam.Signal.set.source.address;
                                        //собираем из буфера группы число
                                        for (int i = 0; i < buf.Length; i++)
                                            groupVal += (uint)(buf[i] << (8 * i));

                                        if (signParam.Signal.set.source.bValue)
                                        //делаем ИЛИ с отправляемым сигналом
                                            groupVal |= signParam.Signal.set.valueon;
                                        else
                                            groupVal &= signParam.Signal.set.valueon;

                                        for (int i = 0; i < buf.Length; i++)
                                            buf[i] = (byte)((groupVal >> (8 * i)) & 0xFF);
                                    }
                                    else
                                    {
                                        addr = (ushort)signParam.Signal.set.source.bufAddr;

                                        if(signParam.Signal.set.source.bValue)
                                            buf[signParam.Signal.set.indexbyte] |= (byte)signParam.Signal.set.valueon;
                                        else
                                            buf[signParam.Signal.set.indexbyte] &= (byte)signParam.Signal.set.valueon;

                                        buf[buf.Length - 1] = CheckSum(buf, buf.Length - 1);
                                    }
                                    comType = CommandType.TYPICAL;
                                }
                                break;
                            case SIGNALTYPE.ARRAY:
                                {
                                    port = signParam.Signal.set.source.port;
                                    buf = signParam.Signal.set.source.group.valueTX;
                                    comType = CommandType.JUST_ARRAY;
                                }
                                break;
                            case SIGNALTYPE.ANALOG:
                                {
                                    comType = CommandType.TYPICAL;
                                    if (!signParam.Signal.set.source.isRS)
                                    {
                                        cnt = signParam.Signal.set.sizebyte;
                                        port = signParam.Signal.set.source.port;
                                        addr = (ushort)signParam.Signal.set.source.address;
                                        buf = signParam.Signal.set.source.group.valueTX;
                                    }
                                    else
                                    {
                                        addr = (ushort)signParam.Signal.set.source.bufAddr;
                                        port = signParam.Signal.set.source.port;
                                        uint valOn = signParam.Signal.set.valueon;
                                        buf = signParam.Signal.set.source.group.valueTX;
                                        //cnt = signParam.Signal.set.sizebyte;
                                        cnt = (ushort)signParam.Signal.set.source.group.valueTX.Length;

                                        for (int i = 0; i < signParam.Signal.set.sizebyte; i++)
                                        {
                                            buf[i + signParam.Signal.set.indexbyte] = (byte)((valOn >> (8 * i)) & 0xFF);
                                        }
                                        if(cnt > 1)
                                            buf[buf.Length - 1] = CheckSum(buf, buf.Length - 1);
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case SET_OR_GET.GET:
                    {
                        switch (signParam.Signal.get.signalType)
                        {
                            case SIGNALTYPE.LOGIC:
                                {
                                    cnt = signParam.Signal.get.sizebyte;
                                    port = signParam.Signal.get.source.port;
                                    buf = signParam.Signal.get.source.group.valueTX;

                                    if (!signParam.Signal.get.source.isRS)
                                    {
                                        addr = (ushort)signParam.Signal.get.source.address;
                                        //собираем из буфера группы число
                                        for (int i = 0; i < buf.Length; i++)
                                            groupVal += (uint)(buf[i] << (8 * i));

                                        //делаем ИЛИ с отправляемым сигналом
                                        //groupVal |= signal.get.valueon;

                                        for (int i = 0; i < buf.Length; i++)
                                            buf[i] = (byte)((groupVal >> (8 * i)) & 0xFF);
                                    }
                                    else
                                    {
                                        addr = (ushort)signParam.Signal.get.source.bufAddr;

                                        buf[buf.Length - 1] = CheckSum(buf, buf.Length - 1);
                                    }
                                    comType = CommandType.TYPICAL;
                                }
                                break;
                            case SIGNALTYPE.ARRAY:
                                {
                                    port = signParam.Signal.get.source.port;
                                    buf = signParam.Signal.get.source.group.valueTX;
                                    comType = CommandType.JUST_ARRAY;
                                }
                                break;
                            case SIGNALTYPE.ANALOG:
                                {
                                    comType = CommandType.TYPICAL;
                                    //cnt = signParam.Signal.get.sizebyte;
                                    cnt = (ushort)signParam.Signal.get.source.group.valueTX.Length;
                                    port = signParam.Signal.get.source.port;
                                    buf = signParam.Signal.get.source.group.valueTX;

                                    if (!signParam.Signal.get.source.isRS)
                                    {
                                        addr = (ushort)signParam.Signal.get.source.address;
                                    }
                                    else
                                    {
                                        addr = (ushort)signParam.Signal.get.source.bufAddr;
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case SET_OR_GET.DESET:
                    {
                        switch (signParam.Signal.set.signalType)
                        {
                            case SIGNALTYPE.LOGIC:
                                {
                                    cnt = signParam.Signal.set.sizebyte;
                                    port = signParam.Signal.set.source.port;
                                    buf = signParam.Signal.set.source.group.valueTX;

                                    if (!signParam.Signal.set.source.isRS)
                                    {
                                        addr = (ushort)signParam.Signal.set.source.address;
                                        //собираем из буфера группы число
                                        for (int i = 0; i < buf.Length; i++)
                                            groupVal += (uint)(buf[i] << (8 * i));

                                        if (signParam.Signal.set.source.bValue)
                                        //делаем И с отправляемым сигналом на выключение
                                            groupVal &= signParam.Signal.set.valueoff;
                                        else
                                            groupVal |= signParam.Signal.set.valueoff;

                                        for (int i = 0; i < buf.Length; i++)
                                            buf[i] = (byte)((groupVal >> (8 * i)) & 0xFF);
                                    }
                                    else
                                    {
                                        addr = (ushort)signParam.Signal.set.source.bufAddr;
                                        if (signParam.Signal.set.source.bValue)
                                            buf[signParam.Signal.set.indexbyte] &= (byte)signParam.Signal.set.valueoff;
                                        else
                                            buf[signParam.Signal.set.indexbyte] |= (byte)signParam.Signal.set.valueoff;
                                        buf[buf.Length - 1] = CheckSum(buf, buf.Length - 1);
                                    }
                                    comType = CommandType.TYPICAL;
                                }
                                break;
                            case SIGNALTYPE.ARRAY:
                                {
                                    port = signParam.Signal.set.source.port;
                                    //buf = signParam.Signal.set.source.group.valueTX;
                                    comType = CommandType.JUST_ARRAY;

                                    buf = new byte[signParam.Signal.set.source.group.valueTX.Length];
                                    Array.Copy(signParam.Signal.set.source.group.valueTX, buf, signParam.Signal.set.source.group.valueTX.Length);
                                    
                                    buf[1] &= 0xF7;

                                }
                                break;
                            case SIGNALTYPE.ANALOG:
                                {
                                    comType = CommandType.TYPICAL;
                                    //buf = signParam.Signal.set.source.group.valueTX;
                                    buf = new byte[signParam.Signal.set.source.group.valueTX.Length];
                                    Array.Copy(signParam.Signal.set.source.group.valueTX, buf, signParam.Signal.set.source.group.valueTX.Length);
                                    //cnt = signParam.Signal.set.sizebyte;
                                    //cnt = (ushort)signParam.Signal.set.source.group.valueTX.Length;
                                    port = signParam.Signal.set.source.port;
                                    uint valOff = signParam.Signal.set.valueoff;

                                    for (int i = 0; i < signParam.Signal.set.sizebyte; i++)
                                    {
                                        buf[i + signParam.Signal.set.indexbyte] = (byte)((valOff >> (8 * i)) & 0xFF);
                                    }

                                    if (!signParam.Signal.set.source.isRS)
                                    {
                                        addr = (ushort)signParam.Signal.set.source.address;
                                        cnt = signParam.Signal.set.sizebyte;//
                                    }
                                    else
                                    {
                                        addr = (ushort)signParam.Signal.set.source.bufAddr;
                                        cnt = (ushort)signParam.Signal.set.source.group.valueTX.Length;//

                                        if (cnt > 1)
                                            buf[buf.Length - 1] = CheckSum(buf, buf.Length - 1);
                                    }

                                    
                                    

                                    
                                    

                                }
                                break;
                        }
                    }
                    break;
            }

            //создаем команду с определенным адресом и количеством байт данных
            Command com = new Command(addr, cnt, set_or_get, index, comType);
            //заполняем буфер данных значением группы
            com.data = buf;

            //отображаем уходящий пакет на форме
            ShowBuf(com.GetCommandByteBuf(), BUF_TO_SHOW.TX);


            //выбираем порт для отправки
            if (port == (uint)PORT.PU) { udp = _frmMainWindow.udpPU;        signParam.bPortBusy = lastSignalParamPU.bPortBusy;      lastSignalParamPU = signParam;      _frmMainWindow.bCommandBusyPU = true; }
            if (port == (uint)PORT.PK) { udp = _frmMainWindow.udpPK;        signParam.bPortBusy = lastSignalParamPK.bPortBusy;      lastSignalParamPK = signParam;      _frmMainWindow.bCommandBusyPK = true; }
            if (port == (uint)PORT.PTVC) { udp = _frmMainWindow.udpPTVC;    signParam.bPortBusy = lastSignalParamPTVC.bPortBusy;    lastSignalParamPTVC = signParam;        _frmMainWindow.bCommandBusyPTVC = true; }

            //команда уходит
            if (udp != null)
            {
                //_frmMainWindow.bCommandBusyPK = true;
                udp.SendCommand(com);
            }
            else { MessageBox.Show("Проверьте соединение!", "7600", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }

            string sPort = String.Empty;
            switch (port)
            {
                case (uint)PORT.PU: sPort = "PU"; break;
                case (uint)PORT.PK: sPort = "PK"; break;
                case (uint)PORT.PTVC: sPort = "PTVC"; break;
            }
            //string s1 = 
            AddToList(string.Format("TX {0}:", sPort).PadRight(8) + " >>> " + BitConverter.ToString(com.GetCommandByteBuf()).PadRight(60));

            return true;
        }

        byte CheckSum(byte[] buf, int cnt)
        {
            byte chk = 0;
            for (int i = 0; i < cnt; i++) { chk ^= buf[i]; }
            return chk;
        }

        /// <summary>
        /// Проверяет наличие указанного базового сигнала среди указанного списка сигнал-параметров. Возвращает его индекс в списке при наличии или -1 при отсутствии.
        /// </summary>
        /// <param name="signal">Базовый стендовый сигнал, который ищем в списке</param>
        /// <param name="list">Список сигнал-параметров, в котором ведется поиск базового сигнала</param>
        /// <returns>Индекс сигнала в списке. Или -1 при отсутствии.</returns>
        private int isListSignal(BaseSignal signal, List<BoardSignal> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].get == null || list[i].get.source == null) continue;
                string str = list[i].get.source.name;
                if (signal.name == str) return i;
            }
            return -1;
        }

        #endregion

        private void btnOpenPDF_Click(object sender, EventArgs e)
        {
            _frmMainWindow._frmBoardShowPDF.Show();
            _frmMainWindow._frmBoardSignals.Hide();
        }

        private void btnStartAuto_Click(object sender, EventArgs e)
        {
            bAutoMode = !bAutoMode;
            if (bAutoMode)
            {
                //btnStartAuto.Text = "Стоп";

                //new System.Threading.Thread(() => AutoMode()).Start();
                //(new System.Threading.Thread(AutoMode)).Start();
                //t.Start();
                btnStartAuto.Image = (Image)Properties.Resources.Stop;
                Task.Factory.StartNew(AutoMode);
            }
            else
            {
                btnStartAuto.Image = (Image)Properties.Resources.Auto;
                progProcessor.Visible = false;
                //btnStartAuto.Text = "Старт";
            }
        }

        private void Cycle()
        {
            var processList = new[] { "calc", "notepad", "iexplorer" };
            while (true)
            {
                foreach (var process in System.Diagnostics.Process.GetProcesses())
                {
                    if (processList.Contains(process.ProcessName))
                    {
                        process.Kill();
                        SetText("Процесс " + process.ProcessName + " убит!");
                        //AddToList();
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void очиститьПолеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
            ListCount = 0;
        }

        private void radManual_CheckedChanged(object sender, EventArgs e)
        {
            /*if (radManual.Checked)
            {
                //grpManual.Enabled = true;
                grpPeriodic.Enabled = true;
                grpProgramm.Enabled = true;
                grpTest.Enabled = true;
                btnStartAuto.Enabled = false;
            }
            else
            {
                //grpManual.Enabled = false;
                grpPeriodic.Enabled = false;
                grpProgramm.Enabled = false;
                grpTest.Enabled = false;
                btnStartAuto.Enabled = true;
            }*/
        }

        private void btnProgTest_Click(object sender, EventArgs e)
        {
            string file;

            file = board.Name + "/RUN_XTST_Test.cmd";

            if (Utils.isFileExist(file))
                runBat(file);
            else
                AddToList("Файл " + file + " не найден!");
        }

        private void btnProgFinish_Click(object sender, EventArgs e)
        {
            string file;

            file = board.Name + "/RUN_XTST_Finish.cmd";

            if (Utils.isFileExist(file))
                runBat(file);
            else
                AddToList("Файл " + file + " не найден!");
        }

        private void btnOpenPDF_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string msg = "";
            switch (btn.Name)
            {
                case "btnOpenPDF": msg = "Открыть файл PDF";
                    break;
                case "btnStartAuto": msg = "Автоматические тестирование";
                    break;
                case "btnProgTest": msg = "Программирование тестовой прошивкой";
                    break;
                case "btnProgFinish": msg = "Запись рабочей программы";
                    break;
                case "btnPowerON": msg = "Включение/выключение питания платы";
                    break;

            }
            tipMessage.SetToolTip(btn, msg);
        }

        private void btnCurrents_Click(object sender, EventArgs e)
        {
            /*if (_frmMainWindow._frmCurrents.WindowState == FormWindowState.Minimized)
            {
                _frmMainWindow._frmCurrents.WindowState = FormWindowState.Normal;
                //_frmMainWindow._frmCurrents.Width = 800;
                //_frmMainWindow._frmCurrents.Height = 533;
            }*/
            _frmMainWindow._frmCurrents.Activate();
            _frmMainWindow._frmCurrents.Show();
        }

        private void скопироватьВБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string s = "";
            foreach (object o in lstLog.SelectedItems) s += o.ToString() + "\r\n";
            Clipboard.SetText(s);
        }

        private void chkLogDebug_CheckedChanged(object sender, EventArgs e)
        {
            int oldH = lstLog.Height;
            if (chkLogDebug.Checked)
            {
                lstLog.Top = lstLog.Location.Y - lstLog.ItemHeight * 55;
                lstLog.Height = oldH + lstLog.ItemHeight * 55;
            }
            else
            {
                lstLog.Top = lstLog.Location.Y + lstLog.ItemHeight * 55;
                lstLog.Height = oldH - lstLog.ItemHeight * 55;
            }
        }
    }

    public class listGroupSignals
    {
        public List<GroupTestSignal> listTestSignals;
        public List<GroupTestSignal> listPeriodicSignals;
        public List<GroupTestSignal> listManualSignals;

        public listGroupSignals()
        {
            listTestSignals = new List<GroupTestSignal>();
            listPeriodicSignals = new List<GroupTestSignal>();
            listManualSignals = new List<GroupTestSignal>();
        }

    }

    public class DataFunc
    {
        public bool bWork;
        public bool bNoDeset;

        uint _minVal;

        public uint minVal
        {
            get { return _minVal; }
            set { _minVal = value; }
        }
        uint _curVal;

        public uint curVal
        {
            get 
            { 
                return _curVal; 
            }
            /*set 
            {
                if (value > _maxVal)
                    _curVal = _minVal;
                else
                    _curVal = value; 
            }*/
        }
        int _maxVal;

        public int maxVal
        {
            get { return _maxVal; }
            set { _maxVal = value; }
        }
        uint _step;

        public uint step
        {
            get { return _step; }
            set { _step = value; }
        }

        public void ResetValue()
        {
            _curVal = _minVal;
        }

        public uint CalcAndGetValue()
        {
            _curVal += _step;
            if (_curVal > _maxVal)
                _curVal = _minVal;
            return _curVal;
        }

        public DataFunc()
        {
            _minVal = 0;
            _maxVal = 0;
            _curVal = 0;
            _step = 0;
            bWork = false;
            bNoDeset = false;
        }
    }

    public class bIsRequestClass
    {
        public bool periodic;
        public bool manual;
        public bool test;

        public bIsRequestClass()
        {
            periodic = false;
            manual = false;
            test = false;
        }
    }
}
