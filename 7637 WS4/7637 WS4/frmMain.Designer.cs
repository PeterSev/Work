namespace _7637_WS4
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
            this.pict = new System.Windows.Forms.PictureBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.SuspendLayout();
            // 
            // pict
            // 
            this.pict.Location = new System.Drawing.Point(12, 113);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(800, 600);
            this.pict.TabIndex = 0;
            this.pict.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.SystemColors.Control;
            this.txtComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtComment.Location = new System.Drawing.Point(12, 719);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(800, 91);
            this.txtComment.TabIndex = 1;
            this.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(12, 7);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 100);
            this.panel.TabIndex = 2;
            // 
            // tip
            // 
            this.tip.AutoPopDelay = 5000;
            this.tip.InitialDelay = 300;
            this.tip.ReshowDelay = 100;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(824, 817);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.pict);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "7637 WorkStation 4";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolTip tip;
    }
}

