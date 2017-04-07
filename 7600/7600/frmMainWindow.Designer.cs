namespace _7600
{
    partial class frmMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainWindow));
            this.btnCheckDevices = new System.Windows.Forms.Button();
            this.btnCheckBoards = new System.Windows.Forms.Button();
            this.btnSelfDiagnostic = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnCheckDevices
            // 
            this.btnCheckDevices.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckDevices.Location = new System.Drawing.Point(12, 12);
            this.btnCheckDevices.Name = "btnCheckDevices";
            this.btnCheckDevices.Size = new System.Drawing.Size(250, 250);
            this.btnCheckDevices.TabIndex = 0;
            this.btnCheckDevices.Text = "Проверка   изделий";
            this.btnCheckDevices.UseVisualStyleBackColor = true;
            this.btnCheckDevices.Click += new System.EventHandler(this.btnCheckDevices_Click);
            // 
            // btnCheckBoards
            // 
            this.btnCheckBoards.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckBoards.Location = new System.Drawing.Point(268, 12);
            this.btnCheckBoards.Name = "btnCheckBoards";
            this.btnCheckBoards.Size = new System.Drawing.Size(250, 250);
            this.btnCheckBoards.TabIndex = 0;
            this.btnCheckBoards.Text = "Проверка плат";
            this.btnCheckBoards.UseVisualStyleBackColor = true;
            this.btnCheckBoards.Click += new System.EventHandler(this.btnCheckBoards_Click);
            // 
            // btnSelfDiagnostic
            // 
            this.btnSelfDiagnostic.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelfDiagnostic.Location = new System.Drawing.Point(12, 268);
            this.btnSelfDiagnostic.Name = "btnSelfDiagnostic";
            this.btnSelfDiagnostic.Size = new System.Drawing.Size(250, 250);
            this.btnSelfDiagnostic.TabIndex = 0;
            this.btnSelfDiagnostic.Text = "Самодиагностика";
            this.btnSelfDiagnostic.UseVisualStyleBackColor = true;
            this.btnSelfDiagnostic.Click += new System.EventHandler(this.btnSelfDiagnostic_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Location = new System.Drawing.Point(268, 268);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(250, 250);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 530);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            // 
            // frmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 530);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnCheckBoards);
            this.Controls.Add(this.btnSelfDiagnostic);
            this.Controls.Add(this.btnCheckDevices);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainWindow_FormClosing);
            this.Load += new System.EventHandler(this.frmMainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckDevices;
        private System.Windows.Forms.Button btnCheckBoards;
        private System.Windows.Forms.Button btnSelfDiagnostic;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel panel1;
    }
}