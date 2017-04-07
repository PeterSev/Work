using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class GroupSignals
    {
        //public const ushort MAXBUFSIZE;
        public List<iBase> baseSignals;
        public string name;
        public byte[] valueRX, valueTX;

        public GroupSignals(string _name, ushort _bufRX_TX_Size )
        {
            baseSignals = new List<iBase>();
            name = _name;
            valueRX = new byte[_bufRX_TX_Size];
            valueTX = new byte[_bufRX_TX_Size];
        }
    }
}
