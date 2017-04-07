namespace _7637_WS4
{
    partial class frmBZ_Help
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
            this.txtComment = new System.Windows.Forms.TextBox();
            this.pict = new System.Windows.Forms.PictureBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.SystemColors.Control;
            this.txtComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtComment.Location = new System.Drawing.Point(12, 618);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(654, 91);
            this.txtComment.TabIndex = 3;
            this.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pict
            // 
            this.pict.Location = new System.Drawing.Point(12, 12);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(800, 600);
            this.pict.TabIndex = 2;
            this.pict.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.Controls.Add(this.btnOK);
            this.panelRight.Controls.Add(this.lblLeft);
            this.panelRight.Controls.Add(this.lblNum);
            this.panelRight.Controls.Add(this.lblRight);
            this.panelRight.Location = new System.Drawing.Point(672, 616);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(140, 94);
            this.panelRight.TabIndex = 12;
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
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLeft.ForeColor = System.Drawing.Color.White;
            this.lblLeft.Location = new System.Drawing.Point(12, 20);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(38, 26);
            this.lblLeft.TabIndex = 8;
            this.lblLeft.Text = "<<";
            this.lblLeft.Click += new System.EventHandler(this.lblLeft_Click);
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.ForeColor = System.Drawing.Color.White;
            this.lblNum.Location = new System.Drawing.Point(53, 27);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(37, 13);
            this.lblNum.TabIndex = 8;
            this.lblNum.Text = "1 из 2";
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRight.ForeColor = System.Drawing.Color.White;
            this.lblRight.Location = new System.Drawing.Point(94, 20);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(38, 26);
            this.lblRight.TabIndex = 8;
            this.lblRight.Text = ">>";
            this.lblRight.Click += new System.EventHandler(this.lblRight_Click);
            // 
            // frmBZ_Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 721);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.pict);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBZ_Help";
            this.Activated += new System.EventHandler(this.frmBZ_Help_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBZ_Help_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblRight;
    }
}