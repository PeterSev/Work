using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7600
{
    public class TypingText
    {
        private string Message;
        private TextBox txtBox;
        private System.Windows.Forms.Timer tmr;
        private int index;

        public TypingText(string msg, TextBox txt)
        {
            Message = msg;
            txtBox = txt;
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 30;
            tmr.Tick += tmr_Tick;

            index = 0;
            //DoTyping();
            //tmr.Start();
        }

        public void StartTyping()
        {
            txtBox.Text = string.Empty;
            tmr.Start();
        }
        public void StopTyping()
        {
            tmr.Stop();
        }

        private void DoTyping(int ind)
        {
            string lastText = txtBox.Text;
            if (Message == null) Message = "NO COMMENT";
            if (ind >= Message.Length)
            {
                StopTyping();
                return;
            }
            txtBox.Text += Message[ind];
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            DoTyping(index);
            index++;
        }
    }
}
