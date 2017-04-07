namespace _7600
{
    partial class frmHelpBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelpBoard));
            this.pict = new System.Windows.Forms.PictureBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.panelControl = new System.Windows.Forms.Panel();
            this.panelPict = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.panelControl.SuspendLayout();
            this.panelPict.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pict
            // 
            this.pict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pict.Location = new System.Drawing.Point(0, 0);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(623, 506);
            this.pict.TabIndex = 6;
            this.pict.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(12, 13);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(461, 90);
            this.txtComment.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOK.Location = new System.Drawing.Point(12, 54);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 40);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLeft.Location = new System.Drawing.Point(12, 20);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(38, 26);
            this.lblLeft.TabIndex = 8;
            this.lblLeft.Text = "<<";
            this.lblLeft.Click += new System.EventHandler(this.lblLeft_Click);
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRight.Location = new System.Drawing.Point(94, 20);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(38, 26);
            this.lblRight.TabIndex = 8;
            this.lblRight.Text = ">>";
            this.lblRight.Click += new System.EventHandler(this.lblRight_Click);
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(53, 27);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(37, 13);
            this.lblNum.TabIndex = 8;
            this.lblNum.Text = "1 из 2";
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.panelRight);
            this.panelControl.Controls.Add(this.txtComment);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 506);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(623, 115);
            this.panelControl.TabIndex = 9;
            // 
            // panelPict
            // 
            this.panelPict.Controls.Add(this.pict);
            this.panelPict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPict.Location = new System.Drawing.Point(0, 0);
            this.panelPict.Name = "panelPict";
            this.panelPict.Size = new System.Drawing.Size(623, 506);
            this.panelPict.TabIndex = 10;
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.Controls.Add(this.btnOK);
            this.panelRight.Controls.Add(this.lblLeft);
            this.panelRight.Controls.Add(this.lblNum);
            this.panelRight.Controls.Add(this.lblRight);
            this.panelRight.Location = new System.Drawing.Point(479, 3);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(140, 109);
            this.panelRight.TabIndex = 11;
            // 
            // frmHelpBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 621);
            this.Controls.Add(this.panelPict);
            this.Controls.Add(this.panelControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHelpBoard";
            this.Text = "Подключение платы";
            this.Activated += new System.EventHandler(this.frmHelpBoard_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHelpBoard_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.panelPict.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Panel panelPict;
        private System.Windows.Forms.Panel panelRight;
    }
}