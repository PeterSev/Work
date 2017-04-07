using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class SysButton
    {
        public List<BoardSignal> listRun;
        private string name;

        public string Name
        {
            get { return name; }
        }
        public SysButton(string _name)
        {
            listRun = new List<BoardSignal>();
            name = _name;
        }
    }
}
