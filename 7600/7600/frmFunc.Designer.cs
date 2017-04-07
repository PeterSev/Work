namespace _7600
{
    partial class frmFunc
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
            this.numCur = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numMax = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numDiscr = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkOn = new System.Windows.Forms.CheckBox();
            this.btnResetValue = new System.Windows.Forms.Button();
            this.chkNoDeset = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscr)).BeginInit();
            this.SuspendLayout();
            // 
            // numCur
            // 
            this.numCur.Location = new System.Drawing.Point(166, 12);
            this.numCur.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numCur.Name = "numCur";
            this.numCur.ReadOnly = true;
            this.numCur.Size = new System.Drawing.Size(72, 20);
            this.numCur.TabIndex = 0;
            this.numCur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Текущее значение сигнала";
            // 
            // numMin
            // 
            this.numMin.Location = new System.Drawing.Point(166, 38);
            this.numMin.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numMin.Name = "numMin";
            this.numMin.Size = new System.Drawing.Size(72, 20);
            this.numMin.TabIndex = 0;
            this.numMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Минимум";
            // 
            // numMax
            // 
            this.numMax.Location = new System.Drawing.Point(166, 64);
            this.numMax.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numMax.Name = "numMax";
            this.numMax.Size = new System.Drawing.Size(72, 20);
            this.numMax.TabIndex = 0;
            this.numMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMax.Value = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Максимум";
            // 
            // numDiscr
            // 
            this.numDiscr.Location = new System.Drawing.Point(166, 90);
            this.numDiscr.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numDiscr.Name = "numDiscr";
            this.numDiscr.Size = new System.Drawing.Size(72, 20);
            this.numDiscr.TabIndex = 0;
            this.numDiscr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numDiscr.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Дискрет";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(166, 116);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(71, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkOn
            // 
            this.chkOn.AutoSize = true;
            this.chkOn.Location = new System.Drawing.Point(12, 120);
            this.chkOn.Name = "chkOn";
            this.chkOn.Size = new System.Drawing.Size(45, 17);
            this.chkOn.TabIndex = 3;
            this.chkOn.Text = "Вкл";
            this.chkOn.UseVisualStyleBackColor = true;
            // 
            // btnResetValue
            // 
            this.btnResetValue.Location = new System.Drawing.Point(89, 116);
            this.btnResetValue.Name = "btnResetValue";
            this.btnResetValue.Size = new System.Drawing.Size(71, 23);
            this.btnResetValue.TabIndex = 2;
            this.btnResetValue.Text = "Сброс";
            this.btnResetValue.UseVisualStyleBackColor = true;
            this.btnResetValue.Click += new System.EventHandler(this.btnResetValue_Click);
            // 
            // chkNoDeset
            // 
            this.chkNoDeset.AutoSize = true;
            this.chkNoDeset.Location = new System.Drawing.Point(12, 97);
            this.chkNoDeset.Name = "chkNoDeset";
            this.chkNoDeset.Size = new System.Drawing.Size(68, 17);
            this.chkNoDeset.TabIndex = 4;
            this.chkNoDeset.Text = "NoDeset";
            this.chkNoDeset.UseVisualStyleBackColor = true;
            // 
            // frmFunc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 148);
            this.Controls.Add(this.chkNoDeset);
            this.Controls.Add(this.chkOn);
            this.Controls.Add(this.btnResetValue);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numDiscr);
            this.Controls.Add(this.numMax);
            this.Controls.Add(this.numMin);
            this.Controls.Add(this.numCur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFunc";
            this.Text = "Сигнал";
            this.Activated += new System.EventHandler(this.frmFunc_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.numCur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numCur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numDiscr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkOn;
        private System.Windows.Forms.Button btnResetValue;
        private System.Windows.Forms.CheckBox chkNoDeset;
    }
}