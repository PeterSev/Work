using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace _7600
{
    public enum SIGNAL_SECTION { Periodic, Test, Manual};
    public static class Utils
    {
        public static bool isFileExist(string filename)
        {
            if (!File.Exists(Application.StartupPath + "\\" + filename))
                return false;
            return true;
        }
    }

    public class SignalParameters
    {
        uint port;

        public uint Port
        {
            get { return port; }
        }
        BoardSignal signal;

        public BoardSignal Signal
        {
            get { return signal; }
        }
        SIGNAL_SECTION signal_type;

        public SIGNAL_SECTION Signal_type
        {
            get { return signal_type; }
            set { signal_type = value; }
        }

        ushort delay;

        public ushort Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        bool bPortbusy;

        public bool bPortBusy
        {
            get { return bPortbusy; }
            set { bPortbusy = value; }
        }

        public SignalParameters(uint _port, BoardSignal _signal, SIGNAL_SECTION _signal_type, ushort _delay)
        {
            port = _port;
            delay = _delay;
            signal = _signal;
            signal_type = _signal_type;
            bPortbusy = false;

        }
    }
}
