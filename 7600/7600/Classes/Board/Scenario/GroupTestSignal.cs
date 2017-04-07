using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class GroupTestSignal
    {
        BoardSignal signal;

        public BoardSignal Signal
        {
            get { return signal; }
        }
        GroupSignals group;

        public GroupSignals Group
        {
            get { return group; }
        }
        int index;

        public int Index
        {
            get { return index; }
        }

        public GroupTestSignal(BoardSignal _signal, GroupSignals _group, int _index)
        {
            signal = _signal;
            group = _group;
            index = _index;
        }
    }
}
