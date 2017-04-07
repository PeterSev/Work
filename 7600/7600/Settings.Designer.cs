namespace _7600
{
    partial class frmSettings
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
            this.grpUDPSets = new System.Windows.Forms.GroupBox();
            this.txtIPAddress1 = new System.Windows.Forms.TextBox();
            this.txtIPPort3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIPPort2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIPPort1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIPFrom = new System.Windows.Forms.CheckBox();
            this.txtIPAddress2 = new System.Windows.Forms.TextBox();
            this.txtIPPortBAD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpUDPSets.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUDPSets
            // 
            this.grpUDPSets.Controls.Add(this.txtIPAddress1);
            this.grpUDPSets.Controls.Add(this.txtIPPort3);
            this.grpUDPSets.Controls.Add(this.label4);
            this.grpUDPSets.Controls.Add(this.txtIPPort2);
            this.grpUDPSets.Controls.Add(this.label3);
            this.grpUDPSets.Controls.Add(this.txtIPPort1);
            this.grpUDPSets.Controls.Add(this.label7);
            this.grpUDPSets.Controls.Add(this.label8);
            this.grpUDPSets.Location = new System.Drawing.Point(12, 12);
            this.grpUDPSets.Name = "grpUDPSets";
            this.grpUDPSets.Size = new System.Drawing.Size(497, 50);
            this.grpUDPSets.TabIndex = 39;
            this.grpUDPSets.TabStop = false;
            this.grpUDPSets.Text = "Настройки конечной точки для отправки";
            // 
            // txtIPAddress1
            // 
            this.txtIPAddress1.Location = new System.Drawing.Point(62, 22);
            this.txtIPAddress1.Name = "txtIPAddress1";
            this.txtIPAddress1.Size = new System.Drawing.Size(90, 20);
            this.txtIPAddress1.TabIndex = 35;
            this.txtIPAddress1.Text = "192.168.0.1";
            this.txtIPAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIPPort3
            // 
            this.txtIPPort3.Location = new System.Drawing.Point(440, 22);
            this.txtIPPort3.Name = "txtIPPort3";
            this.txtIPPort3.Size = new System.Drawing.Size(43, 20);
            this.txtIPPort3.TabIndex = 36;
            this.txtIPPort3.Text = "55055";
            this.txtIPPort3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(369, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Порт ПТВЦ";
            // 
            // txtIPPort2
            // 
            this.txtIPPort2.Location = new System.Drawing.Point(320, 22);
            this.txtIPPort2.Name = "txtIPPort2";
            this.txtIPPort2.Size = new System.Drawing.Size(43, 20);
            this.txtIPPort2.TabIndex = 36;
            this.txtIPPort2.Text = "55055";
            this.txtIPPort2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Порт ПК";
            // 
            // txtIPPort1
            // 
            this.txtIPPort1.Location = new System.Drawing.Point(214, 22);
            this.txtIPPort1.Name = "txtIPPort1";
            this.txtIPPort1.Size = new System.Drawing.Size(43, 20);
            this.txtIPPort1.TabIndex = 36;
            this.txtIPPort1.Text = "55055";
            this.txtIPPort1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(158, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Порт ПУ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "IP-адрес";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIPFrom);
            this.groupBox1.Controls.Add(this.txtIPAddress2);
            this.groupBox1.Controls.Add(this.txtIPPortBAD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 50);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки конечной точки для приема";
            // 
            // chkIPFrom
            // 
            this.chkIPFrom.AutoSize = true;
            this.chkIPFrom.Location = new System.Drawing.Point(16, 22);
            this.chkIPFrom.Name = "chkIPFrom";
            this.chkIPFrom.Size = new System.Drawing.Size(58, 17);
            this.chkIPFrom.TabIndex = 37;
            this.chkIPFrom.Text = "любой";
            this.chkIPFrom.UseVisualStyleBackColor = true;
            this.chkIPFrom.CheckedChanged += new System.EventHandler(this.chkIPFrom_CheckedChanged);
            // 
            // txtIPAddress2
            // 
            this.txtIPAddress2.Location = new System.Drawing.Point(136, 20);
            this.txtIPAddress2.Name = "txtIPAddress2";
            this.txtIPAddress2.Size = new System.Drawing.Size(90, 20);
            this.txtIPAddress2.TabIndex = 35;
            this.txtIPAddress2.Text = "192.168.0.2";
            this.txtIPAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIPPortBAD
            // 
            this.txtIPPortBAD.Location = new System.Drawing.Point(270, 20);
            this.txtIPPortBAD.Name = "txtIPPortBAD";
            this.txtIPPortBAD.Size = new System.Drawing.Size(44, 20);
            this.txtIPPortBAD.TabIndex = 36;
            this.txtIPPortBAD.Text = "50000";
            this.txtIPPortBAD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Порт";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP-адрес";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 129);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpUDPSets);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Параметры";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.grpUDPSets.ResumeLayout(false);
            this.grpUDPSets.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox grpUDPSets;
        public System.Windows.Forms.TextBox txtIPAddress1;
        public System.Windows.Forms.TextBox txtIPPort1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtIPAddress2;
        public System.Windows.Forms.TextBox txtIPPortBAD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox chkIPFrom;
        public System.Windows.Forms.TextBox txtIPPort3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtIPPort2;
        private System.Windows.Forms.Label label3;
    }
}