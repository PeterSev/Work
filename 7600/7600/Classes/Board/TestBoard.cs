using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class TestBoard
    {
        private BoardConfig config;

        public BoardConfig Config
        {
            get { return config; }
        }

        private BoardSignals signals;

        public BoardSignals Signals
        {
            get { return signals; }
        }

        private BoardScenario scenario;

        public BoardScenario Scenario
        {
            get { return scenario; }
        }

        public TestBoard(BoardConfig boardConfigObj, BoardSignals boardSignalsObj, BoardScenario scenarioObj)
        {
            config = boardConfigObj;
            signals = boardSignalsObj;
            scenario = scenarioObj;
        }


        
    }
}
