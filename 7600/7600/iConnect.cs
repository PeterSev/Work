using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public interface iConnect
    {
        void SendCommand(Command com_out);
        bool Open();
        bool Close();
    }
}
