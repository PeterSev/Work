using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class SettingsXML
    {
        private string ipAddressTo;
        public string IpAddressTo
        {
            get { return ipAddressTo; }
            set { ipAddressTo = value; }
        }
        
        private string ipPortTo1;
        public string IpPortTo1
        {
            get { return ipPortTo1; }
            set { ipPortTo1 = value; }
        }

        private string ipPortTo2;
        public string IpPortTo2
        {
            get { return ipPortTo2; }
            set { ipPortTo2 = value; }
        }

        private string ipPortTo3;
        public string IpPortTo3
        {
            get { return ipPortTo3; }
            set { ipPortTo3 = value; }
        }
        
    }
}
