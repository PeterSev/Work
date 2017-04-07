namespace _7600
{
    partial class frmListBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListBoard));
            this.pict = new System.Windows.Forms.PictureBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.panelListBoard = new System.Windows.Forms.Panel();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.SuspendLayout();
            // 
            // pict
            // 
            this.pict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pict.Location = new System.Drawing.Point(332, 12);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(640, 480);
            this.pict.TabIndex = 4;
            this.pict.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(332, 498);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(640, 90);
            this.txtComment.TabIndex = 3;
            // 
            // panelListBoard
            // 
            this.panelListBoard.AutoScroll = true;
            this.panelListBoard.Location = new System.Drawing.Point(6, 12);
            this.panelListBoard.Name = "panelListBoard";
            this.panelListBoard.Size = new System.Drawing.Size(320, 577);
            this.panelListBoard.TabIndex = 5;
            // 
            // tip
            // 
            this.tip.AutoPopDelay = 5000;
            this.tip.InitialDelay = 300;
            this.tip.ReshowDelay = 100;
            // 
            // frmListBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 601);
            this.Controls.Add(this.panelListBoard);
            this.Controls.Add(this.pict);
            this.Controls.Add(this.txtComment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmListBoard";
            this.Text = "Выберите плату";
            this.Activated += new System.EventHandler(this.frmListBoard_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmListBoard_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Panel panelListBoard;
        private System.Windows.Forms.ToolTip tip;
    }
}