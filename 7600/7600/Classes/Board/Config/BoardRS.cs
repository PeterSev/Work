using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class BoardRS
    {
        string name;

        public string Name
        {
            get { return name; }
        }
        ushort numbytes;

        public ushort NumBytes
        {
            get { return numbytes; }
        }
        byte marker;

        public byte Marker
        {
            get { return marker; }
        }

        public BoardRS(string _name, ushort _numbytes, byte _marker)
        {
            name = _name;
            numbytes = _numbytes;
            marker = _marker;
        }

    }
}
