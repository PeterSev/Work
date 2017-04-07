using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class BoardScenario
    {
        private Periodic _periodic;

        public Periodic periodic
        {
            get { return _periodic; }
        }

        private Test _test;

        public Test test
        {
            get { return _test; }
        }

        private Manual _manual;

        public Manual manual
        {
            get { return _manual; }
        }

        private Buttons _buttons;

        public Buttons buttons
        {
            get { return _buttons; }
        }

        public BoardScenario(Periodic periodicObj, Test testObj, Manual manualObj, Buttons buttonsObj)
        {
            _periodic = periodicObj;
            _test = testObj;
            _manual = manualObj;
            _buttons = buttonsObj;
        }
    }
}
