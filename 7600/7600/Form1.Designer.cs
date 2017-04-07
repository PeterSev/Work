namespace _7600
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.txtTX = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtRX = new System.Windows.Forms.TextBox();
            this.statusStripUART = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSuccess = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimeOuts = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnConfigPK = new System.Windows.Forms.Button();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTimeoutWait = new System.Windows.Forms.TextBox();
            this.txtPeriodCycle = new System.Windows.Forms.TextBox();
            this.txtTimeoutRazdel = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radReadWrite = new System.Windows.Forms.RadioButton();
            this.radRead = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radRS485 = new System.Windows.Forms.RadioButton();
            this.radRS422 = new System.Windows.Forms.RadioButton();
            this.radMaster = new System.Windows.Forms.RadioButton();
            this.radSlave = new System.Windows.Forms.RadioButton();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSpeedUart = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAddr = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tipMessage = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTVPOut = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grpSignals = new System.Windows.Forms.GroupBox();
            this.panelTestSignals = new System.Windows.Forms.Panel();
            this.btnLoadSignals = new System.Windows.Forms.Button();
            this.btnConfigPTVC = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStripUART.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpSignals.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сервисToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 49;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // сервисToolStripMenuItem
            // 
            this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
            this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.сервисToolStripMenuItem.Text = "Сервис";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 27);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 25);
            this.btnConnect.TabIndex = 50;
            this.btnConnect.Text = "Старт обмен";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 658);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(22, 13);
            this.label29.TabIndex = 53;
            this.label29.Text = "RX";
            // 
            // txtTX
            // 
            this.txtTX.Location = new System.Drawing.Point(36, 629);
            this.txtTX.Name = "txtTX";
            this.txtTX.ReadOnly = true;
            this.txtTX.Size = new System.Drawing.Size(645, 20);
            this.txtTX.TabIndex = 52;
            this.txtTX.Text = "00";
            this.txtTX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTX.MouseHover += new System.EventHandler(this.txtTX_MouseHover);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(9, 632);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(21, 13);
            this.label28.TabIndex = 54;
            this.label28.Text = "TX";
            // 
            // txtRX
            // 
            this.txtRX.Location = new System.Drawing.Point(36, 655);
            this.txtRX.Name = "txtRX";
            this.txtRX.ReadOnly = true;
            this.txtRX.Size = new System.Drawing.Size(645, 20);
            this.txtRX.TabIndex = 51;
            this.txtRX.Text = "00";
            this.txtRX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRX.MouseHover += new System.EventHandler(this.txtTX_MouseHover);
            // 
            // statusStripUART
            // 
            this.statusStripUART.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblSuccess,
            this.lblTimeOuts});
            this.statusStripUART.Location = new System.Drawing.Point(0, 692);
            this.statusStripUART.Name = "statusStripUART";
            this.statusStripUART.Size = new System.Drawing.Size(933, 24);
            this.statusStripUART.TabIndex = 55;
            this.statusStripUART.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(99, 19);
            this.lblStatus.Text = "Статус обмена - ";
            // 
            // lblSuccess
            // 
            this.lblSuccess.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(133, 19);
            this.lblSuccess.Text = "Успешные посылки - ";
            // 
            // lblTimeOuts
            // 
            this.lblTimeOuts.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblTimeOuts.Name = "lblTimeOuts";
            this.lblTimeOuts.Size = new System.Drawing.Size(126, 19);
            this.lblTimeOuts.Text = "Таймауты посылки -";
            // 
            // btnConfigPK
            // 
            this.btnConfigPK.Location = new System.Drawing.Point(543, 177);
            this.btnConfigPK.Name = "btnConfigPK";
            this.btnConfigPK.Size = new System.Drawing.Size(120, 23);
            this.btnConfigPK.TabIndex = 56;
            this.btnConfigPK.Text = "Конфиг ПК";
            this.btnConfigPK.UseVisualStyleBackColor = true;
            this.btnConfigPK.Click += new System.EventHandler(this.btnSendCom_Click);
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(277, 30);
            this.txtAddr.MaxLength = 5;
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(37, 20);
            this.txtAddr.TabIndex = 57;
            this.txtAddr.Text = "0";
            this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Адрес данных";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTimeoutWait);
            this.groupBox1.Controls.Add(this.txtPeriodCycle);
            this.groupBox1.Controls.Add(this.txtTimeoutRazdel);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.radMaster);
            this.groupBox1.Controls.Add(this.radSlave);
            this.groupBox1.Controls.Add(this.cmbStopBits);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbSpeedUart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbAddr);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnConfigPTVC);
            this.groupBox1.Controls.Add(this.btnConfigPK);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 211);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Конфигурация UART";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(382, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(225, 13);
            this.label10.TabIndex = 65;
            this.label10.Text = "Таймаут ожидания приемного пакета, мкс";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(377, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 13);
            this.label9.TabIndex = 65;
            this.label9.Text = "Период циклической отправки посылки, мс";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(384, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(223, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "Таймаут разделения пакетов данных, мкс";
            // 
            // txtTimeoutWait
            // 
            this.txtTimeoutWait.Location = new System.Drawing.Point(613, 73);
            this.txtTimeoutWait.MaxLength = 4;
            this.txtTimeoutWait.Name = "txtTimeoutWait";
            this.txtTimeoutWait.Size = new System.Drawing.Size(50, 20);
            this.txtTimeoutWait.TabIndex = 64;
            this.txtTimeoutWait.Text = "1000000";
            this.txtTimeoutWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPeriodCycle
            // 
            this.txtPeriodCycle.Location = new System.Drawing.Point(613, 46);
            this.txtPeriodCycle.MaxLength = 5;
            this.txtPeriodCycle.Name = "txtPeriodCycle";
            this.txtPeriodCycle.Size = new System.Drawing.Size(50, 20);
            this.txtPeriodCycle.TabIndex = 64;
            this.txtPeriodCycle.Text = "1023";
            this.txtPeriodCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTimeoutRazdel
            // 
            this.txtTimeoutRazdel.Location = new System.Drawing.Point(613, 19);
            this.txtTimeoutRazdel.MaxLength = 6;
            this.txtTimeoutRazdel.Name = "txtTimeoutRazdel";
            this.txtTimeoutRazdel.Size = new System.Drawing.Size(50, 20);
            this.txtTimeoutRazdel.TabIndex = 64;
            this.txtTimeoutRazdel.Text = "102300";
            this.txtTimeoutRazdel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radReadWrite);
            this.panel2.Controls.Add(this.radRead);
            this.panel2.Location = new System.Drawing.Point(145, 181);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 18);
            this.panel2.TabIndex = 63;
            // 
            // radReadWrite
            // 
            this.radReadWrite.AutoSize = true;
            this.radReadWrite.Location = new System.Drawing.Point(88, -1);
            this.radReadWrite.Name = "radReadWrite";
            this.radReadWrite.Size = new System.Drawing.Size(49, 17);
            this.radReadWrite.TabIndex = 62;
            this.radReadWrite.Text = "R/W";
            this.radReadWrite.UseVisualStyleBackColor = true;
            // 
            // radRead
            // 
            this.radRead.AutoSize = true;
            this.radRead.Checked = true;
            this.radRead.Location = new System.Drawing.Point(11, -1);
            this.radRead.Name = "radRead";
            this.radRead.Size = new System.Drawing.Size(72, 17);
            this.radRead.TabIndex = 61;
            this.radRead.TabStop = true;
            this.radRead.Text = "ReadOnly";
            this.radRead.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radRS485);
            this.panel1.Controls.Add(this.radRS422);
            this.panel1.Location = new System.Drawing.Point(145, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 18);
            this.panel1.TabIndex = 63;
            // 
            // radRS485
            // 
            this.radRS485.AutoSize = true;
            this.radRS485.Location = new System.Drawing.Point(88, -1);
            this.radRS485.Name = "radRS485";
            this.radRS485.Size = new System.Drawing.Size(61, 17);
            this.radRS485.TabIndex = 62;
            this.radRS485.Text = "RS-485";
            this.radRS485.UseVisualStyleBackColor = true;
            // 
            // radRS422
            // 
            this.radRS422.AutoSize = true;
            this.radRS422.Checked = true;
            this.radRS422.Location = new System.Drawing.Point(11, -1);
            this.radRS422.Name = "radRS422";
            this.radRS422.Size = new System.Drawing.Size(61, 17);
            this.radRS422.TabIndex = 61;
            this.radRS422.TabStop = true;
            this.radRS422.Text = "RS-422";
            this.radRS422.UseVisualStyleBackColor = true;
            // 
            // radMaster
            // 
            this.radMaster.AutoSize = true;
            this.radMaster.Location = new System.Drawing.Point(233, 100);
            this.radMaster.Name = "radMaster";
            this.radMaster.Size = new System.Drawing.Size(69, 17);
            this.radMaster.TabIndex = 62;
            this.radMaster.Text = "ведущий";
            this.radMaster.UseVisualStyleBackColor = true;
            // 
            // radSlave
            // 
            this.radSlave.AutoSize = true;
            this.radSlave.Checked = true;
            this.radSlave.Location = new System.Drawing.Point(156, 100);
            this.radSlave.Name = "radSlave";
            this.radSlave.Size = new System.Drawing.Size(71, 17);
            this.radSlave.TabIndex = 61;
            this.radSlave.TabStop = true;
            this.radSlave.Text = "ведомый";
            this.radSlave.UseVisualStyleBackColor = true;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbStopBits.Location = new System.Drawing.Point(227, 127);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(75, 21);
            this.cmbStopBits.TabIndex = 60;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.Items.AddRange(new object[] {
            "Нечетный (odd)",
            "Четный (even)",
            "Всегда 0 (space)",
            "Всегда 1 (mark)",
            "Отсутствует"});
            this.cmbParity.Location = new System.Drawing.Point(227, 73);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(75, 21);
            this.cmbParity.TabIndex = 60;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Режим";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Разрешение передачи";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Интерфейс физики";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Кол-во стоповых бит";
            // 
            // cmbSpeedUart
            // 
            this.cmbSpeedUart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeedUart.FormattingEnabled = true;
            this.cmbSpeedUart.Items.AddRange(new object[] {
            "19200",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cmbSpeedUart.Location = new System.Drawing.Point(227, 46);
            this.cmbSpeedUart.Name = "cmbSpeedUart";
            this.cmbSpeedUart.Size = new System.Drawing.Size(75, 21);
            this.cmbSpeedUart.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Управление паритетом";
            // 
            // cmbAddr
            // 
            this.cmbAddr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAddr.FormattingEnabled = true;
            this.cmbAddr.Items.AddRange(new object[] {
            "RS-14",
            "RS-15",
            "RS-16"});
            this.cmbAddr.Location = new System.Drawing.Point(227, 19);
            this.cmbAddr.Name = "cmbAddr";
            this.cmbAddr.Size = new System.Drawing.Size(75, 21);
            this.cmbAddr.TabIndex = 59;
            this.cmbAddr.SelectedIndexChanged += new System.EventHandler(this.cmbAddr_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(81, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "Выбор RS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Скорость обмена";
            // 
            // tipMessage
            // 
            this.tipMessage.AutoPopDelay = 10000;
            this.tipMessage.InitialDelay = 300;
            this.tipMessage.ReshowDelay = 100;
            this.tipMessage.ToolTipTitle = "Бинарное отображение";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtTVPOut);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(12, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 82);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Свободная посылка";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 13);
            this.label12.TabIndex = 59;
            this.label12.Text = "Байты через запятую";
            // 
            // txtTVPOut
            // 
            this.txtTVPOut.Location = new System.Drawing.Point(145, 19);
            this.txtTVPOut.Name = "txtTVPOut";
            this.txtTVPOut.Size = new System.Drawing.Size(518, 20);
            this.txtTVPOut.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(343, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 23);
            this.button3.TabIndex = 56;
            this.button3.Text = "Послать в ПТВЦ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(162, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 56;
            this.button2.Text = "Послать в ПК";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 56;
            this.button1.Text = "Послать в ПУ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpSignals
            // 
            this.grpSignals.Controls.Add(this.panelTestSignals);
            this.grpSignals.Location = new System.Drawing.Point(687, 33);
            this.grpSignals.Name = "grpSignals";
            this.grpSignals.Size = new System.Drawing.Size(229, 642);
            this.grpSignals.TabIndex = 61;
            this.grpSignals.TabStop = false;
            this.grpSignals.Text = "Сигналы";
            // 
            // panelTestSignals
            // 
            this.panelTestSignals.AutoScroll = true;
            this.panelTestSignals.Location = new System.Drawing.Point(6, 19);
            this.panelTestSignals.Name = "panelTestSignals";
            this.panelTestSignals.Size = new System.Drawing.Size(207, 605);
            this.panelTestSignals.TabIndex = 62;
            // 
            // btnLoadSignals
            // 
            this.btnLoadSignals.Location = new System.Drawing.Point(510, 547);
            this.btnLoadSignals.Name = "btnLoadSignals";
            this.btnLoadSignals.Size = new System.Drawing.Size(165, 23);
            this.btnLoadSignals.TabIndex = 0;
            this.btnLoadSignals.Text = "Загрузить сигналы платы";
            this.btnLoadSignals.UseVisualStyleBackColor = true;
            this.btnLoadSignals.Click += new System.EventHandler(this.btnLoadSignals_Click);
            // 
            // btnConfigPTVC
            // 
            this.btnConfigPTVC.Location = new System.Drawing.Point(417, 176);
            this.btnConfigPTVC.Name = "btnConfigPTVC";
            this.btnConfigPTVC.Size = new System.Drawing.Size(120, 23);
            this.btnConfigPTVC.TabIndex = 56;
            this.btnConfigPTVC.Text = "Конфиг ПТВЦ";
            this.btnConfigPTVC.UseVisualStyleBackColor = true;
            this.btnConfigPTVC.Click += new System.EventHandler(this.btnConfigPTVC_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 716);
            this.Controls.Add(this.btnLoadSignals);
            this.Controls.Add(this.grpSignals);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStripUART);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.txtTX);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtRX);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.Text = "7600.07.74.000-02";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStripUART.ResumeLayout(false);
            this.statusStripUART.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpSignals.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox txtTX;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.TextBox txtRX;
        public System.Windows.Forms.StatusStrip statusStripUART;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblTimeOuts;
        private System.Windows.Forms.ToolStripStatusLabel lblSuccess;
        private System.Windows.Forms.Button btnConfigPK;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAddr;
        private System.Windows.Forms.ComboBox cmbSpeedUart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radMaster;
        private System.Windows.Forms.RadioButton radSlave;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radRS485;
        private System.Windows.Forms.RadioButton radRS422;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radReadWrite;
        private System.Windows.Forms.RadioButton radRead;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip tipMessage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTimeoutWait;
        private System.Windows.Forms.TextBox txtPeriodCycle;
        private System.Windows.Forms.TextBox txtTimeoutRazdel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTVPOut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpSignals;
        private System.Windows.Forms.Button btnLoadSignals;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelTestSignals;
        private System.Windows.Forms.Button btnConfigPTVC;
    }
}

