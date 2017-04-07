using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using _7600.Stand;

namespace _7600
{
    public class StandSignals
    {
        public List<GroupSignals> listSignals;
        public List<BaseRS> listRS;

        public StandSignals()
        {
            listSignals = new List<GroupSignals>();
            listRS = new List<BaseRS>();
        }
    }
}
