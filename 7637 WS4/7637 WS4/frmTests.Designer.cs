namespace _7637_WS4
{
    partial class frmTests
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
            this.panel = new System.Windows.Forms.Panel();
            this.lblPP = new System.Windows.Forms.Label();
            this.lblBU = new System.Windows.Forms.Label();
            this.lblBPPP = new System.Windows.Forms.Label();
            this.lblBZ = new System.Windows.Forms.Label();
            this.btnPP = new System.Windows.Forms.Button();
            this.btnBU = new System.Windows.Forms.Button();
            this.btnBPPP = new System.Windows.Forms.Button();
            this.btnBZ = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
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
            this.txtComment.Size = new System.Drawing.Size(800, 91);
            this.txtComment.TabIndex = 2;
            this.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lblPP);
            this.panel.Controls.Add(this.lblBU);
            this.panel.Controls.Add(this.lblBPPP);
            this.panel.Controls.Add(this.lblBZ);
            this.panel.Controls.Add(this.btnPP);
            this.panel.Controls.Add(this.btnBU);
            this.panel.Controls.Add(this.btnBPPP);
            this.panel.Controls.Add(this.btnBZ);
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 600);
            this.panel.TabIndex = 3;
            // 
            // lblPP
            // 
            this.lblPP.AutoSize = true;
            this.lblPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPP.Location = new System.Drawing.Point(397, 563);
            this.lblPP.Name = "lblPP";
            this.lblPP.Size = new System.Drawing.Size(28, 17);
            this.lblPP.TabIndex = 1;
            this.lblPP.Text = "ПП";
            this.lblPP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBU
            // 
            this.lblBU.AutoSize = true;
            this.lblBU.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBU.Location = new System.Drawing.Point(397, 263);
            this.lblBU.Name = "lblBU";
            this.lblBU.Size = new System.Drawing.Size(122, 17);
            this.lblBU.TabIndex = 1;
            this.lblBU.Text = "Блок управления";
            this.lblBU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBPPP
            // 
            this.lblBPPP.AutoSize = true;
            this.lblBPPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBPPP.Location = new System.Drawing.Point(3, 563);
            this.lblBPPP.Name = "lblBPPP";
            this.lblBPPP.Size = new System.Drawing.Size(47, 17);
            this.lblBPPP.TabIndex = 1;
            this.lblBPPP.Text = "БППП";
            this.lblBPPP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBZ
            // 
            this.lblBZ.AutoSize = true;
            this.lblBZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBZ.Location = new System.Drawing.Point(3, 263);
            this.lblBZ.Name = "lblBZ";
            this.lblBZ.Size = new System.Drawing.Size(98, 17);
            this.lblBZ.TabIndex = 1;
            this.lblBZ.Text = "Блок зеркала";
            this.lblBZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPP
            // 
            this.btnPP.Location = new System.Drawing.Point(400, 300);
            this.btnPP.Name = "btnPP";
            this.btnPP.Size = new System.Drawing.Size(400, 260);
            this.btnPP.TabIndex = 3;
            this.btnPP.UseVisualStyleBackColor = true;
            this.btnPP.Enter += new System.EventHandler(this.btn_MouseEnter);
            this.btnPP.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            // 
            // btnBU
            // 
            this.btnBU.Location = new System.Drawing.Point(400, 0);
            this.btnBU.Name = "btnBU";
            this.btnBU.Size = new System.Drawing.Size(400, 260);
            this.btnBU.TabIndex = 1;
            this.btnBU.UseVisualStyleBackColor = true;
            this.btnBU.Enter += new System.EventHandler(this.btn_MouseEnter);
            this.btnBU.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            // 
            // btnBPPP
            // 
            this.btnBPPP.Location = new System.Drawing.Point(0, 300);
            this.btnBPPP.Name = "btnBPPP";
            this.btnBPPP.Size = new System.Drawing.Size(400, 260);
            this.btnBPPP.TabIndex = 2;
            this.btnBPPP.UseVisualStyleBackColor = true;
            this.btnBPPP.Enter += new System.EventHandler(this.btn_MouseEnter);
            this.btnBPPP.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            // 
            // btnBZ
            // 
            this.btnBZ.Location = new System.Drawing.Point(0, 0);
            this.btnBZ.Name = "btnBZ";
            this.btnBZ.Size = new System.Drawing.Size(400, 260);
            this.btnBZ.TabIndex = 0;
            this.btnBZ.UseVisualStyleBackColor = true;
            this.btnBZ.Click += new System.EventHandler(this.btnBZ_Click);
            this.btnBZ.Enter += new System.EventHandler(this.btn_MouseEnter);
            this.btnBZ.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            // 
            // frmTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 719);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.txtComment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmTests";
            this.Text = "Tests";
            this.Activated += new System.EventHandler(this.frmTests_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTests_FormClosing);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnPP;
        private System.Windows.Forms.Button btnBU;
        private System.Windows.Forms.Button btnBPPP;
        private System.Windows.Forms.Button btnBZ;
        private System.Windows.Forms.Label lblPP;
        private System.Windows.Forms.Label lblBU;
        private System.Windows.Forms.Label lblBPPP;
        private System.Windows.Forms.Label lblBZ;
    }
}