namespace _7600
{
    partial class frmBoardSignals
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoardSignals));
            this.txtComment = new System.Windows.Forms.TextBox();
            this.grpPeriodic = new System.Windows.Forms.GroupBox();
            this.panelPeriodicSignals = new System.Windows.Forms.Panel();
            this.panelPeriodicTitle = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHelp = new System.Windows.Forms.TextBox();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.panelTestSignals = new System.Windows.Forms.Panel();
            this.panelTestTitle = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpManual = new System.Windows.Forms.GroupBox();
            this.panelManualSignals = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.txtTX = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtRX = new System.Windows.Forms.TextBox();
            this.statusStripUART = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSuccessPU = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimeOutsPU = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSuccessPK = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimeOutsPK = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSuccessPTVC = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimeOutsPTVC = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelPict = new System.Windows.Forms.Panel();
            this.pict = new System.Windows.Forms.PictureBox();
            this.btnStartAuto = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.contextMenuLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.очиститьПолеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скопироватьВБуферToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenPDF = new System.Windows.Forms.Button();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnProgTest = new System.Windows.Forms.Button();
            this.btnProgFinish = new System.Windows.Forms.Button();
            this.chkPowerON = new System.Windows.Forms.CheckBox();
            this.tipMessage = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProgPercent = new System.Windows.Forms.Label();
            this.progProcessor = new System.Windows.Forms.ProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelGroups = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCurrents = new System.Windows.Forms.Button();
            this.chkLogOn = new System.Windows.Forms.CheckBox();
            this.chkLogDebug = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuFunc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpPeriodic.SuspendLayout();
            this.panelPeriodicTitle.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.panelTestTitle.SuspendLayout();
            this.grpManual.SuspendLayout();
            this.panelPict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.contextMenuLog.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(402, 737);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(857, 99);
            this.txtComment.TabIndex = 5;
            // 
            // grpPeriodic
            // 
            this.grpPeriodic.Controls.Add(this.panelPeriodicSignals);
            this.grpPeriodic.Controls.Add(this.panelPeriodicTitle);
            this.grpPeriodic.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpPeriodic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpPeriodic.Location = new System.Drawing.Point(0, 695);
            this.grpPeriodic.Name = "grpPeriodic";
            this.grpPeriodic.Size = new System.Drawing.Size(396, 237);
            this.grpPeriodic.TabIndex = 7;
            this.grpPeriodic.TabStop = false;
            this.grpPeriodic.Text = "Периодические";
            // 
            // panelPeriodicSignals
            // 
            this.panelPeriodicSignals.AutoScroll = true;
            this.panelPeriodicSignals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPeriodicSignals.Location = new System.Drawing.Point(3, 41);
            this.panelPeriodicSignals.Name = "panelPeriodicSignals";
            this.panelPeriodicSignals.Size = new System.Drawing.Size(390, 193);
            this.panelPeriodicSignals.TabIndex = 0;
            // 
            // panelPeriodicTitle
            // 
            this.panelPeriodicTitle.Controls.Add(this.label8);
            this.panelPeriodicTitle.Controls.Add(this.label9);
            this.panelPeriodicTitle.Controls.Add(this.label10);
            this.panelPeriodicTitle.Controls.Add(this.label7);
            this.panelPeriodicTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPeriodicTitle.Location = new System.Drawing.Point(3, 16);
            this.panelPeriodicTitle.Name = "panelPeriodicTitle";
            this.panelPeriodicTitle.Size = new System.Drawing.Size(390, 25);
            this.panelPeriodicTitle.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(302, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "IIN_W";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(252, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Uout";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "I";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Сигналы";
            // 
            // txtHelp
            // 
            this.txtHelp.Location = new System.Drawing.Point(-12, 409);
            this.txtHelp.Multiline = true;
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.ReadOnly = true;
            this.txtHelp.Size = new System.Drawing.Size(1264, 22);
            this.txtHelp.TabIndex = 8;
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.panelTestSignals);
            this.grpTest.Controls.Add(this.panelTestTitle);
            this.grpTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpTest.Location = new System.Drawing.Point(0, 0);
            this.grpTest.Name = "grpTest";
            this.grpTest.Size = new System.Drawing.Size(396, 231);
            this.grpTest.TabIndex = 7;
            this.grpTest.TabStop = false;
            this.grpTest.Text = "Тестируемые";
            // 
            // panelTestSignals
            // 
            this.panelTestSignals.AutoScroll = true;
            this.panelTestSignals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTestSignals.Location = new System.Drawing.Point(3, 41);
            this.panelTestSignals.Name = "panelTestSignals";
            this.panelTestSignals.Size = new System.Drawing.Size(390, 187);
            this.panelTestSignals.TabIndex = 0;
            // 
            // panelTestTitle
            // 
            this.panelTestTitle.Controls.Add(this.label6);
            this.panelTestTitle.Controls.Add(this.label5);
            this.panelTestTitle.Controls.Add(this.label4);
            this.panelTestTitle.Controls.Add(this.label3);
            this.panelTestTitle.Controls.Add(this.label2);
            this.panelTestTitle.Controls.Add(this.label1);
            this.panelTestTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTestTitle.Location = new System.Drawing.Point(3, 16);
            this.panelTestTitle.Name = "panelTestTitle";
            this.panelTestTitle.Size = new System.Drawing.Size(390, 25);
            this.panelTestTitle.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Δ3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Δ5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Δ12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Δ15";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Δ27";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сигналы";
            // 
            // grpManual
            // 
            this.grpManual.Controls.Add(this.panelManualSignals);
            this.grpManual.Controls.Add(this.txtHelp);
            this.grpManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpManual.Location = new System.Drawing.Point(0, 0);
            this.grpManual.Name = "grpManual";
            this.grpManual.Size = new System.Drawing.Size(396, 230);
            this.grpManual.TabIndex = 9;
            this.grpManual.TabStop = false;
            this.grpManual.Text = "Ручные";
            // 
            // panelManualSignals
            // 
            this.panelManualSignals.AutoScroll = true;
            this.panelManualSignals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelManualSignals.Location = new System.Drawing.Point(3, 16);
            this.panelManualSignals.Name = "panelManualSignals";
            this.panelManualSignals.Size = new System.Drawing.Size(390, 211);
            this.panelManualSignals.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(412, 899);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(22, 13);
            this.label29.TabIndex = 57;
            this.label29.Text = "RX";
            this.label29.Visible = false;
            // 
            // txtTX
            // 
            this.txtTX.Location = new System.Drawing.Point(440, 870);
            this.txtTX.Name = "txtTX";
            this.txtTX.ReadOnly = true;
            this.txtTX.Size = new System.Drawing.Size(619, 20);
            this.txtTX.TabIndex = 56;
            this.txtTX.Text = "00";
            this.txtTX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTX.Visible = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(413, 873);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(21, 13);
            this.label28.TabIndex = 58;
            this.label28.Text = "TX";
            this.label28.Visible = false;
            // 
            // txtRX
            // 
            this.txtRX.Location = new System.Drawing.Point(440, 896);
            this.txtRX.Name = "txtRX";
            this.txtRX.ReadOnly = true;
            this.txtRX.Size = new System.Drawing.Size(619, 20);
            this.txtRX.TabIndex = 55;
            this.txtRX.Text = "00";
            this.txtRX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRX.Visible = false;
            // 
            // statusStripUART
            // 
            this.statusStripUART.Location = new System.Drawing.Point(0, 932);
            this.statusStripUART.Name = "statusStripUART";
            this.statusStripUART.Size = new System.Drawing.Size(1264, 22);
            this.statusStripUART.TabIndex = 59;
            this.statusStripUART.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(99, 19);
            this.lblStatus.Text = "Статус обмена - ";
            // 
            // lblSuccessPU
            // 
            this.lblSuccessPU.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblSuccessPU.Name = "lblSuccessPU";
            this.lblSuccessPU.Size = new System.Drawing.Size(152, 19);
            this.lblSuccessPU.Text = "Успешные посылки ПУ - ";
            // 
            // lblTimeOutsPU
            // 
            this.lblTimeOutsPU.Name = "lblTimeOutsPU";
            this.lblTimeOutsPU.Size = new System.Drawing.Size(89, 19);
            this.lblTimeOutsPU.Text = "Таймауты ПУ -";
            // 
            // lblSuccessPK
            // 
            this.lblSuccessPK.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblSuccessPK.Name = "lblSuccessPK";
            this.lblSuccessPK.Size = new System.Drawing.Size(152, 19);
            this.lblSuccessPK.Text = "Успешные посылки ПК - ";
            // 
            // lblTimeOutsPK
            // 
            this.lblTimeOutsPK.Name = "lblTimeOutsPK";
            this.lblTimeOutsPK.Size = new System.Drawing.Size(89, 19);
            this.lblTimeOutsPK.Text = "Таймауты ПК -";
            // 
            // lblSuccessPTVC
            // 
            this.lblSuccessPTVC.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblSuccessPTVC.Name = "lblSuccessPTVC";
            this.lblSuccessPTVC.Size = new System.Drawing.Size(168, 19);
            this.lblSuccessPTVC.Text = "Успешные посылки ПТВЦ - ";
            // 
            // lblTimeOutsPTVC
            // 
            this.lblTimeOutsPTVC.Name = "lblTimeOutsPTVC";
            this.lblTimeOutsPTVC.Size = new System.Drawing.Size(105, 19);
            this.lblTimeOutsPTVC.Text = "Таймауты ПТВЦ -";
            // 
            // panelPict
            // 
            this.panelPict.AutoScroll = true;
            this.panelPict.Controls.Add(this.pict);
            this.panelPict.Location = new System.Drawing.Point(402, 75);
            this.panelPict.Name = "panelPict";
            this.panelPict.Size = new System.Drawing.Size(857, 656);
            this.panelPict.TabIndex = 61;
            // 
            // pict
            // 
            this.pict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pict.Location = new System.Drawing.Point(0, 0);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(857, 656);
            this.pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict.TabIndex = 6;
            this.pict.TabStop = false;
            // 
            // btnStartAuto
            // 
            this.btnStartAuto.Location = new System.Drawing.Point(612, 5);
            this.btnStartAuto.Name = "btnStartAuto";
            this.btnStartAuto.Size = new System.Drawing.Size(64, 64);
            this.btnStartAuto.TabIndex = 67;
            this.btnStartAuto.UseVisualStyleBackColor = true;
            this.btnStartAuto.Click += new System.EventHandler(this.btnStartAuto_Click);
            this.btnStartAuto.MouseHover += new System.EventHandler(this.btnOpenPDF_MouseHover);
            // 
            // lstLog
            // 
            this.lstLog.ContextMenuStrip = this.contextMenuLog;
            this.lstLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ItemHeight = 14;
            this.lstLog.Location = new System.Drawing.Point(402, 842);
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstLog.Size = new System.Drawing.Size(857, 74);
            this.lstLog.TabIndex = 7;
            // 
            // contextMenuLog
            // 
            this.contextMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьПолеToolStripMenuItem,
            this.скопироватьВБуферToolStripMenuItem});
            this.contextMenuLog.Name = "contextMenuStrip1";
            this.contextMenuLog.Size = new System.Drawing.Size(194, 48);
            // 
            // очиститьПолеToolStripMenuItem
            // 
            this.очиститьПолеToolStripMenuItem.Name = "очиститьПолеToolStripMenuItem";
            this.очиститьПолеToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.очиститьПолеToolStripMenuItem.Text = "Очистить поле";
            this.очиститьПолеToolStripMenuItem.Click += new System.EventHandler(this.очиститьПолеToolStripMenuItem_Click);
            // 
            // скопироватьВБуферToolStripMenuItem
            // 
            this.скопироватьВБуферToolStripMenuItem.Name = "скопироватьВБуферToolStripMenuItem";
            this.скопироватьВБуферToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.скопироватьВБуферToolStripMenuItem.Text = "Скопировать в буфер";
            this.скопироватьВБуферToolStripMenuItem.Click += new System.EventHandler(this.скопироватьВБуферToolStripMenuItem_Click);
            // 
            // btnOpenPDF
            // 
            this.btnOpenPDF.Location = new System.Drawing.Point(682, 5);
            this.btnOpenPDF.Name = "btnOpenPDF";
            this.btnOpenPDF.Size = new System.Drawing.Size(64, 64);
            this.btnOpenPDF.TabIndex = 63;
            this.btnOpenPDF.UseVisualStyleBackColor = true;
            this.btnOpenPDF.Click += new System.EventHandler(this.btnOpenPDF_Click);
            this.btnOpenPDF.MouseHover += new System.EventHandler(this.btnOpenPDF_MouseHover);
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.panelButtons);
            this.grpButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpButtons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpButtons.Location = new System.Drawing.Point(0, 0);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(396, 230);
            this.grpButtons.TabIndex = 68;
            this.grpButtons.TabStop = false;
            this.grpButtons.Text = "Кнопки";
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(3, 16);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(390, 211);
            this.panelButtons.TabIndex = 0;
            // 
            // btnProgTest
            // 
            this.btnProgTest.Location = new System.Drawing.Point(472, 5);
            this.btnProgTest.Name = "btnProgTest";
            this.btnProgTest.Size = new System.Drawing.Size(64, 64);
            this.btnProgTest.TabIndex = 69;
            this.btnProgTest.UseVisualStyleBackColor = true;
            this.btnProgTest.Click += new System.EventHandler(this.btnProgTest_Click);
            this.btnProgTest.MouseHover += new System.EventHandler(this.btnOpenPDF_MouseHover);
            // 
            // btnProgFinish
            // 
            this.btnProgFinish.Location = new System.Drawing.Point(542, 5);
            this.btnProgFinish.Name = "btnProgFinish";
            this.btnProgFinish.Size = new System.Drawing.Size(64, 64);
            this.btnProgFinish.TabIndex = 69;
            this.btnProgFinish.UseVisualStyleBackColor = true;
            this.btnProgFinish.Click += new System.EventHandler(this.btnProgFinish_Click);
            this.btnProgFinish.MouseHover += new System.EventHandler(this.btnOpenPDF_MouseHover);
            // 
            // chkPowerON
            // 
            this.chkPowerON.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPowerON.Location = new System.Drawing.Point(402, 5);
            this.chkPowerON.Name = "chkPowerON";
            this.chkPowerON.Size = new System.Drawing.Size(64, 64);
            this.chkPowerON.TabIndex = 70;
            this.chkPowerON.UseVisualStyleBackColor = true;
            this.chkPowerON.CheckedChanged += new System.EventHandler(this.chkPowerON_CheckedChanged);
            // 
            // tipMessage
            // 
            this.tipMessage.AutoPopDelay = 5000;
            this.tipMessage.InitialDelay = 300;
            this.tipMessage.ReshowDelay = 100;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblProgPercent);
            this.groupBox2.Controls.Add(this.progProcessor);
            this.groupBox2.Location = new System.Drawing.Point(752, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 64);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Авторежим";
            // 
            // lblProgPercent
            // 
            this.lblProgPercent.AutoSize = true;
            this.lblProgPercent.Location = new System.Drawing.Point(45, 19);
            this.lblProgPercent.Name = "lblProgPercent";
            this.lblProgPercent.Size = new System.Drawing.Size(84, 13);
            this.lblProgPercent.TabIndex = 0;
            this.lblProgPercent.Text = "Выполнено: 0%";
            this.lblProgPercent.Visible = false;
            // 
            // progProcessor
            // 
            this.progProcessor.Location = new System.Drawing.Point(6, 35);
            this.progProcessor.Name = "progProcessor";
            this.progProcessor.Size = new System.Drawing.Size(420, 23);
            this.progProcessor.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progProcessor.TabIndex = 0;
            this.progProcessor.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panelGroups
            // 
            this.panelGroups.Controls.Add(this.splitContainer1);
            this.panelGroups.Controls.Add(this.grpPeriodic);
            this.panelGroups.Controls.Add(this.grpButtons);
            this.panelGroups.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGroups.Location = new System.Drawing.Point(0, 0);
            this.panelGroups.Name = "panelGroups";
            this.panelGroups.Size = new System.Drawing.Size(396, 932);
            this.panelGroups.TabIndex = 72;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 230);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpManual);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpTest);
            this.splitContainer1.Size = new System.Drawing.Size(396, 465);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 69;
            // 
            // btnCurrents
            // 
            this.btnCurrents.Location = new System.Drawing.Point(1190, 5);
            this.btnCurrents.Name = "btnCurrents";
            this.btnCurrents.Size = new System.Drawing.Size(64, 64);
            this.btnCurrents.TabIndex = 63;
            this.btnCurrents.UseVisualStyleBackColor = true;
            this.btnCurrents.Click += new System.EventHandler(this.btnCurrents_Click);
            this.btnCurrents.MouseHover += new System.EventHandler(this.btnOpenPDF_MouseHover);
            // 
            // chkLogOn
            // 
            this.chkLogOn.AutoSize = true;
            this.chkLogOn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLogOn.Location = new System.Drawing.Point(1190, 899);
            this.chkLogOn.Name = "chkLogOn";
            this.chkLogOn.Size = new System.Drawing.Size(42, 17);
            this.chkLogOn.TabIndex = 73;
            this.chkLogOn.Text = "ON";
            this.chkLogOn.UseVisualStyleBackColor = true;
            // 
            // chkLogDebug
            // 
            this.chkLogDebug.AutoSize = true;
            this.chkLogDebug.Location = new System.Drawing.Point(1126, 899);
            this.chkLogDebug.Name = "chkLogDebug";
            this.chkLogDebug.Size = new System.Drawing.Size(58, 17);
            this.chkLogDebug.TabIndex = 74;
            this.chkLogDebug.Text = "Debug";
            this.chkLogDebug.UseVisualStyleBackColor = true;
            this.chkLogDebug.CheckedChanged += new System.EventHandler(this.chkLogDebug_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuFunc
            // 
            this.contextMenuFunc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setToolStripMenuItem});
            this.contextMenuFunc.Name = "contextMenuFunc";
            this.contextMenuFunc.Size = new System.Drawing.Size(91, 26);
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(90, 22);
            this.setToolStripMenuItem.Text = "Set";
            // 
            // frmBoardSignals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 954);
            this.Controls.Add(this.chkLogDebug);
            this.Controls.Add(this.chkLogOn);
            this.Controls.Add(this.panelGroups);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkPowerON);
            this.Controls.Add(this.btnProgFinish);
            this.Controls.Add(this.btnProgTest);
            this.Controls.Add(this.btnCurrents);
            this.Controls.Add(this.btnOpenPDF);
            this.Controls.Add(this.btnStartAuto);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.panelPict);
            this.Controls.Add(this.statusStripUART);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.txtTX);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtRX);
            this.Controls.Add(this.txtComment);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBoardSignals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Тестируемые сигналы платы";
            this.Activated += new System.EventHandler(this.frmBoardSignals_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBoardSignals_FormClosing);
            this.grpPeriodic.ResumeLayout(false);
            this.panelPeriodicTitle.ResumeLayout(false);
            this.panelPeriodicTitle.PerformLayout();
            this.grpTest.ResumeLayout(false);
            this.panelTestTitle.ResumeLayout(false);
            this.panelTestTitle.PerformLayout();
            this.grpManual.ResumeLayout(false);
            this.grpManual.PerformLayout();
            this.panelPict.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.contextMenuLog.ResumeLayout(false);
            this.grpButtons.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelGroups.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuFunc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.GroupBox grpPeriodic;
        private System.Windows.Forms.TextBox txtHelp;
        private System.Windows.Forms.GroupBox grpTest;
        private System.Windows.Forms.GroupBox grpManual;
        private System.Windows.Forms.Panel panelPeriodicSignals;
        private System.Windows.Forms.Panel panelTestSignals;
        private System.Windows.Forms.Panel panelManualSignals;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox txtTX;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.TextBox txtRX;
        public System.Windows.Forms.StatusStrip statusStripUART;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblSuccessPU;
        private System.Windows.Forms.ToolStripStatusLabel lblTimeOutsPU;
        private System.Windows.Forms.ToolStripStatusLabel lblSuccessPK;
        private System.Windows.Forms.ToolStripStatusLabel lblTimeOutsPK;
        private System.Windows.Forms.ToolStripStatusLabel lblSuccessPTVC;
        private System.Windows.Forms.ToolStripStatusLabel lblTimeOutsPTVC;
        private System.Windows.Forms.Panel panelPict;
        private System.Windows.Forms.Button btnOpenPDF;
        public System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnStartAuto;
        private System.Windows.Forms.ContextMenuStrip contextMenuLog;
        private System.Windows.Forms.ToolStripMenuItem очиститьПолеToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnProgTest;
        private System.Windows.Forms.Button btnProgFinish;
        private System.Windows.Forms.CheckBox chkPowerON;
        private System.Windows.Forms.ToolTip tipMessage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblProgPercent;
        private System.Windows.Forms.ProgressBar progProcessor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panelGroups;
        private System.Windows.Forms.Button btnCurrents;
        private System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.ToolStripMenuItem скопироватьВБуферToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkLogOn;
        private System.Windows.Forms.CheckBox chkLogDebug;
        private System.Windows.Forms.Panel panelTestTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPeriodicTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuFunc;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
    }
}