namespace _7600
{
    partial class frmChooseCheckDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChooseCheckDevice));
            this.btnProgramm1 = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnProgramm2 = new System.Windows.Forms.Button();
            this.btnProgramm3 = new System.Windows.Forms.Button();
            this.pict = new System.Windows.Forms.PictureBox();
            this.panelListFiles = new System.Windows.Forms.Panel();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProgramm1
            // 
            this.btnProgramm1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProgramm1.Location = new System.Drawing.Point(54, 432);
            this.btnProgramm1.Name = "btnProgramm1";
            this.btnProgramm1.Size = new System.Drawing.Size(190, 42);
            this.btnProgramm1.TabIndex = 0;
            this.btnProgramm1.Text = "frmMain";
            this.btnProgramm1.UseVisualStyleBackColor = true;
            this.btnProgramm1.Click += new System.EventHandler(this.btnProgramm1_Click);
            this.btnProgramm1.MouseEnter += new System.EventHandler(this.btnProgramm1_MouseEnter);
            this.btnProgramm1.MouseLeave += new System.EventHandler(this.btnProgramm1_MouseLeave);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(338, 498);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(640, 90);
            this.txtComment.TabIndex = 1;
            // 
            // btnProgramm2
            // 
            this.btnProgramm2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProgramm2.Location = new System.Drawing.Point(53, 480);
            this.btnProgramm2.Name = "btnProgramm2";
            this.btnProgramm2.Size = new System.Drawing.Size(190, 42);
            this.btnProgramm2.TabIndex = 0;
            this.btnProgramm2.Text = "Calculator";
            this.btnProgramm2.UseVisualStyleBackColor = true;
            this.btnProgramm2.Click += new System.EventHandler(this.btnProgramm2_Click);
            this.btnProgramm2.MouseEnter += new System.EventHandler(this.btnProgramm2_MouseEnter);
            this.btnProgramm2.MouseLeave += new System.EventHandler(this.btnProgramm1_MouseLeave);
            // 
            // btnProgramm3
            // 
            this.btnProgramm3.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProgramm3.Location = new System.Drawing.Point(54, 528);
            this.btnProgramm3.Name = "btnProgramm3";
            this.btnProgramm3.Size = new System.Drawing.Size(190, 42);
            this.btnProgramm3.TabIndex = 0;
            this.btnProgramm3.Text = "Notepad";
            this.btnProgramm3.UseVisualStyleBackColor = true;
            this.btnProgramm3.Click += new System.EventHandler(this.btnProgramm3_Click);
            this.btnProgramm3.MouseEnter += new System.EventHandler(this.btnProgramm3_MouseEnter);
            this.btnProgramm3.MouseLeave += new System.EventHandler(this.btnProgramm1_MouseLeave);
            // 
            // pict
            // 
            this.pict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pict.InitialImage = null;
            this.pict.Location = new System.Drawing.Point(338, 12);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(640, 480);
            this.pict.TabIndex = 2;
            this.pict.TabStop = false;
            // 
            // panelListFiles
            // 
            this.panelListFiles.Location = new System.Drawing.Point(12, 12);
            this.panelListFiles.Name = "panelListFiles";
            this.panelListFiles.Size = new System.Drawing.Size(320, 414);
            this.panelListFiles.TabIndex = 3;
            // 
            // tip
            // 
            this.tip.AutoPopDelay = 5000;
            this.tip.InitialDelay = 300;
            this.tip.ReshowDelay = 100;
            // 
            // frmChooseCheckDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 601);
            this.Controls.Add(this.panelListFiles);
            this.Controls.Add(this.pict);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnProgramm3);
            this.Controls.Add(this.btnProgramm2);
            this.Controls.Add(this.btnProgramm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmChooseCheckDevice";
            this.Text = "Выберите изделие";
            this.Activated += new System.EventHandler(this.frmChooseCheckDevice_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChooseCheckDevice_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProgramm1;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.Button btnProgramm2;
        private System.Windows.Forms.Button btnProgramm3;
        private System.Windows.Forms.Panel panelListFiles;
        private System.Windows.Forms.ToolTip tip;
    }
}