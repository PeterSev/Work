using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _7600
{
    
    public class XMLParser
  
    {
        private enum TYPESIGUSING {NULL,PERIODIC, TEST, MANUAL,BUTTON };
        private enum TYPERSSIGNAL { NULL, BIT, ANALOG };
        TYPESIGUSING useSignalType = TYPESIGUSING.NULL;
        SIGNALTYPE signalType = SIGNALTYPE.UNDEFINED;
        //private StandSignals standSignals;
        private TestBoard testBoard;

        public XMLParser()
        {
            //standSignals = new StandSignals();
            testBoard = null;
        }


        public TestBoard openBoardXML(StandSignals standSignals, string XMLname)
        {
            BoardSignal boardSignal = null;
            BoardSignals boardSignals = new BoardSignals();
            //BoardConfig boardConfig = null;
            BoardConfig boardConfig =new BoardConfig();
            BoardScenario scenario = null;
            Periodic periodic = new Periodic();
            Manual manual = new Manual();
            Test runTest = new Test();
            //GroupSignals groupSignal = null;

            string commentString = null;
            string imageLinkString = null;
            string str;

            bool bSectionSet = false;
            bool bSectionGet = false;
            bool bSectionConfig = false;

            //string name;
            string paramName = "";
            string setSourceName = "";
            string getSourceName = "";




            string boardSignalName = null;
            string boardRSName = null;

            UInt16 delay = 1000;
            ushort numBytes = 0;
            byte marker = 0x0;
            byte setIndexByte=0;
            byte setIndexBit = 0;
            byte getIndexByte = 0;
            byte getIndexBit = 0;

            bool bSetValueON = true;
            bool bGetValueON = true;
            UInt32 uiSetValueON = 0xffffffff;
            UInt32 uiGetValueON = 0xffffffff;
            UInt32 uiGetMAX = 0x0;
            UInt32 uiGetMIN = 0xffffffff;

            ushort setSizeByte=0;
            ushort getSizeByte=0;
            UInt32 uiSetValueOFF = 0xffffffff;
            TYPERSSIGNAL setRSignalType = TYPERSSIGNAL.NULL;
            TYPERSSIGNAL getRSignalType = TYPERSSIGNAL.NULL;
            //TYPERSSIGNAL rsSignalType = TYPERSSIGNAL.NULL;

            XmlTextReader reader = new XmlTextReader(XMLname);
            Buttons buttonsObj= new Buttons();
            SysButton sysButton=null;
            double dI27MAX = 999;
            double dI15MAX = 999;
            double dI12MAX = 999;
            double dI05MAX = 999;
            double dI03MAX = 999;
            string sysButtonName=string.Empty;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.

                        if (reader.Name == "RS")
                        {
                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                if (bSectionConfig)
                                    boardRSName = reader.Value;
                            }
                            setRSignalType = TYPERSSIGNAL.NULL;
                            getRSignalType = TYPERSSIGNAL.NULL;

                        }


                        paramName = reader.Name;
                        str = "<" + reader.Name + ">";
                        if (reader.Name == "SIGNAL")
                        {
                            getSourceName = null;
                            setSourceName = null;

                            commentString = string.Empty;
                            imageLinkString = string.Empty;

                            dI27MAX = 999;
                            dI15MAX = 999;
                            dI12MAX = 999;
                            dI05MAX = 999;
                            dI03MAX = 999;
                            reader.MoveToNextAttribute();
                            str += " " + reader.Name + "='" + reader.Value + "'" + ">";
                            if (reader.Name == "NAME")
                            {
                                //baseSignal = new BaseSignal(reader.Value);
                                boardSignalName = reader.Value;
                            }
                        }

                        //Console.Write("<" + reader.Name);
                        else
                        {
                           // while (reader.MoveToNextAttribute()) // Чтение атрибутов.
                           //     str += " " + reader.Name + "='" + reader.Value + "'" + ">";
                        }


                        if (reader.Name == "BUTTON")
                        {


                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                //baseSignal = new BaseSignal(reader.Value);
                                sysButtonName = reader.Value;
                                sysButton = new SysButton(sysButtonName) ;
                                useSignalType = TYPESIGUSING.BUTTON;
                            }
                        }

                        if (reader.Name == "SET")
                        {
                            bSectionSet = true;
                        }
                        if (reader.Name == "GET")
                        {
                            bSectionGet = true;
                            getSourceName = "";
                        }

                        if (reader.Name == "CONFIG")
                        {
                            bSectionConfig = true;
                        }

                        if (reader.Name == "PERIODIC")
                        {
                            useSignalType=TYPESIGUSING.PERIODIC;
                        }

                        if (reader.Name == "TEST")
                        {
                            useSignalType = TYPESIGUSING.TEST;
                        }

                        if (reader.Name == "MANUAL")
                        {
                            useSignalType=TYPESIGUSING.MANUAL;
                        }

                        str += ">";
                        //lstBxXML.Items.Add(str);
                        break;
                    case XmlNodeType.Text: // Вывести текст в каждом элементе.
                        str = reader.Value;

                        //lstBxXML.Items.Add(str);
                        //Console.WriteLine(reader.Value);

                        if (paramName == "SOURCE")
                        {
                            if (bSectionSet)
                                setSourceName = reader.Value;
                            if (bSectionGet)
                                getSourceName = reader.Value;

                        }


                        if (paramName == "VALUEOFF")
                        {
                            uint uValue = 0xffffffff;
                            if (str2uint(reader.Value,out uValue))
                            {
                                if (bSectionSet)
                                    uiSetValueOFF = uValue;
                             }
                        }

                        if (paramName == "MIN")
                        {
                          uint uValue = 0;
                          if (str2uint(reader.Value, out uValue))
                                {
                                    if (bSectionGet)
                                    {
                                        getRSignalType = TYPERSSIGNAL.ANALOG;
                                        uiGetMIN = uValue;
                                    }
                                }
                        }

                        if (paramName == "MAX")
                        {
                            uint uValue = 0;
                            if (str2uint(reader.Value, out uValue))
                            {
                                if (bSectionGet)
                                {
                                    getRSignalType = TYPERSSIGNAL.ANALOG;
                                    uiGetMAX = uValue;
                                }
                            }
                        }


                        
                        if (paramName == "VALUEON")
                        {
                            bool bValue;
                            if (bool.TryParse(reader.Value, out bValue))
                            {
                                if (bSectionSet)
                                {
                                    setRSignalType = TYPERSSIGNAL.BIT;
                                    bSetValueON = bValue;
                                }
                                if (bSectionGet)
                                {
                                    getRSignalType = TYPERSSIGNAL.BIT;
                                    bGetValueON = bValue;
                                }
                            }
                            else {
                                uint uValue=0;
                                if (str2uint(reader.Value, out uValue))
                                    {
                                        
                                        if (bSectionSet)
                                        {
                                            setRSignalType = TYPERSSIGNAL.ANALOG;
                                            uiSetValueON = uValue;
                                        }
                                        if (bSectionGet)
                                            uiGetValueON = uValue;
                                    }

                            }
                        }

                        if (paramName == "SIZEBYTE")
                        {
                            ushort val;
                            if (ushort.TryParse(reader.Value, out val))
                            {
                                if (bSectionSet)
                                    setSizeByte = val;
                                if (bSectionGet)
                                    getSizeByte = val;
                            }
                        }

                        if (paramName == "INDEXBYTE")
                        {
                            byte index;
                            if (byte.TryParse(reader.Value, out index))
                            {
                                if (bSectionSet)
                                    setIndexByte = index;
                                if (bSectionGet)
                                    getIndexByte = index;
                            }
                        }


                        if (paramName == "INDEXBIT")
                        {
                            byte index;
                            if (byte.TryParse(reader.Value, out index))
                            {
                                if (bSectionSet)
                                    setIndexBit = index;
                                if (bSectionGet)
                                    getIndexBit = index;
                            }
                        }


                        if (paramName == "COMMENT")
                        {
                            commentString = reader.Value;
                        }

                        if (paramName == "IMAGELINK")
                        {
                            imageLinkString = reader.Value;
                        }

                        if (paramName == "DELAY")
                        {
                            UInt16 _delay;
                            if (UInt16.TryParse(reader.Value, out _delay))
                            {
                                delay = _delay;
                            }
                        }


                        if (paramName == "RUN")
                        {
                            //runTestName = reader.Value;
                            //BoardSignal _boardSignal = getBoardSignal(runTestName, boardSignals);
                            BoardSignal _boardSignal = getBoardSignal(reader.Value, boardSignals);
                            if (_boardSignal != null)
                            {
                                switch (useSignalType)
                                {
                                    case TYPESIGUSING.PERIODIC:
                                        periodic.listRun.Add(_boardSignal);
                                        break;
                                    case TYPESIGUSING.TEST:
                                        runTest.listRun.Add(_boardSignal);
                                        break;
                                    case TYPESIGUSING.MANUAL:
                                        manual.listRun.Add(_boardSignal);
                                        break;
                                    case TYPESIGUSING.BUTTON:
                                        sysButton.listRun.Add(_boardSignal);
                                        break;
                                }
                            }
                        }

                        if (paramName == "D27MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                    dI27MAX = dValue;
                            }
                        }

                        if (paramName == "D15MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI15MAX = dValue;
                            }
                        }

                        if (paramName == "D12MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI12MAX = dValue;
                            }
                        }

                        if (paramName == "D5MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI05MAX = dValue;
                            }
                        }

                        if (paramName == "D3MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI03MAX = dValue;
                            }
                        }



                        /*
                        if (paramName == "VALUE")
                        {

                        }


                        paramName = "";
                        */
                        if (paramName == "NUMBYTES")
                        {
                            ushort _numBytes;
                            if (ushort.TryParse(reader.Value, out _numBytes))
                            {
                                if(bSectionConfig)
                                    numBytes = _numBytes;
                            }
                        }

                        if (paramName == "MARKER")
                        {
                            byte _marker;
                            if (byte.TryParse(reader.Value.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out _marker))
                            {
                                if (bSectionConfig)
                                    marker = _marker;
                            }
                        }

                        break;
                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        //Console.Write("</" + reader.Name);
                        if (reader.Name == "CONFIG")
                        {
                            bSectionConfig = false;
                        }

                        if (reader.Name == "RS")
                        {
                            if (bSectionConfig)
                            {
                                BaseRS baseRS;
                                if((baseRS= getBaseRS(standSignals, boardRSName)) != null) {
                                    GroupSignals groupRS = new GroupSignals(baseRS.Name, numBytes);
                                    if (groupRS.valueTX.Length > 0)
                                    {
                                        if(marker!=0xff)
                                            groupRS.valueTX[0] = marker;
                                    }
                                    groupRS.valueRX= new byte[64];
                                    baseRS.Group = groupRS;
                                    BoardRS boardRS = new BoardRS(boardRSName,numBytes,marker);
                                    boardConfig.listBoardRS.Add(boardRS);
                                }
                            }
                        }


                        if (reader.Name == "SIGNAL")
                        {
                            bool err = false;
                            iSet set=null;
                            iGet get = null;
                            if (setSourceName != null)
                            {
                                if (setSourceName.Substring(0, 2) != "RS")
                                {
                                    BaseSignal baseSignal = getBaseSignal(standSignals, setSourceName);
                                    if (baseSignal != null)
                                    {
                                        switch (baseSignal.type)
                                        {
                                            case SIGNALTYPE.LOGIC:
                                                set = new Set_STAND_BIT(baseSignal, commentString, imageLinkString);
                                                break;
                                            case SIGNALTYPE.ARRAY:
                                                set = new Set_ARRAY(baseSignal, commentString, imageLinkString);
                                                break;
                                            case SIGNALTYPE.ANALOG:
                                                set = new Set_STAND_ANALOG(baseSignal, uiSetValueON, commentString, imageLinkString, uiSetValueOFF);
                                         break;

                                        }
                                    }
                                    else
                                    {
                                        err = true;
                                    }
                                }
                                else
                                {
                                    //BaseRS baseRS=getBaseRS
                                    BaseRS baseRS = getBaseRS(standSignals, setSourceName);
                                    BoardRS boardRS = getBoardRS(boardConfig, setSourceName);
                                    if (setRSignalType == TYPERSSIGNAL.BIT)
                                        set = new Set_RS_BIT(baseRS, boardRS, bSetValueON, setIndexByte, setIndexBit, commentString, imageLinkString);
                                    else
                                    {
                                        if (setRSignalType == TYPERSSIGNAL.ANALOG)
                                            set = new Set_RS_ANALOG(baseRS, boardRS, uiSetValueON, setSizeByte, setIndexByte, commentString, imageLinkString, uiSetValueOFF);
                                        //baseRS, boardRS, bSetValueON, setIndexByte, setIndexBit, commentString, imageLinkString);
                                    }

                                }
                            }
                            if (getSourceName != null)
                            {
                                if (getSourceName.Length >= 2)
                                {
                                    if (getSourceName.Substring(0, 2) != "RS")
                                    {

                                        BaseSignal baseSignal = getBaseSignal(standSignals, getSourceName);
                                        if (baseSignal != null)
                                        {
                                            switch (baseSignal.type)
                                            {
                                                case SIGNALTYPE.LOGIC:
                                                    get = new Get_STAND_BIT(baseSignal);
                                                    break;
                                                case SIGNALTYPE.ARRAY:
                                                    get = new Get_ARRAY(baseSignal);
                                                    break;
                                                 case SIGNALTYPE.ANALOG:
                                                    get = new Get_STAND_ANALOG(baseSignal,uiGetMIN, uiGetMAX);
                                                    break;
                                                default:
                                                    get = new Get_ARRAY(null);
                                                    break;

                                            }
                                        }
                                    }
                                    else
                                    {
                                        BaseRS baseRS = getBaseRS(standSignals, getSourceName);
                                        BoardRS boardRS = getBoardRS(boardConfig, getSourceName);
                                        if (getRSignalType == TYPERSSIGNAL.BIT)
                                              get = new Get_RS_BIT(baseRS, boardRS, bGetValueON, getIndexByte, getIndexBit);
                                        else
                                        {
                                            if (getRSignalType == TYPERSSIGNAL.ANALOG)
                                            {
                                                get = new Get_RS_ANALOG(baseRS, uiGetMIN, uiGetMAX, getSizeByte, getIndexByte);
                                            }

                                        }
                                    }
                                }
                                if (get==null)
                                    get = new Get_ARRAY(null);
                            }
                            if(!err)
                                boardSignal = new BoardSignal(boardSignalName, set, get, delay, dI27MAX, dI15MAX, dI12MAX, dI05MAX, dI03MAX);
                            else
                                boardSignal = null;
                            boardSignals.listBoardSignals.Add(boardSignal);


                            //boardSignals = new BoardSignals();
                        }

                        if (reader.Name == "BUTTON")
                        {
                            if(sysButton!=null) {
                                buttonsObj.listButtons.Add(sysButton);
                                useSignalType = TYPESIGUSING.NULL;
                                sysButton=null;
                            }
                        }

                        if (reader.Name == "SET")
                        {
                            bSectionSet = false;
                        }
                        if (reader.Name == "GET")
                        {
                            bSectionGet = false;
                        }

                        str = "</" + reader.Name + ">";
                        //lstBxXML.Items.Add(str);

                        //Console.WriteLine(">");
                        //lstBxXML.Items.Add(">");
                        break;
                }
            }

            
            scenario = new BoardScenario(periodic, runTest, manual,buttonsObj);

            testBoard = new TestBoard(boardConfig, boardSignals, scenario);

            return testBoard;

        }

        private BoardSignal getBoardSignal(string signalName, BoardSignals boardSignals)
        {
            // bool groupExist = false;
            if (boardSignals.listBoardSignals.Count != 0)
            {
                foreach (BoardSignal boardSignal in boardSignals.listBoardSignals)
                {
                    if (boardSignal != null)
                    {
                        if (signalName == boardSignal.Name)
                        {
                            return boardSignal;
                        }
                    }

                }
            }
            return null;
        }
        private bool str2uint(string str,  out uint uiValue)
        {
            if (str.ToLower().Contains("0x"))
            {
                if (uint.TryParse(str.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out uiValue))
                    return true;

            }
            else
            {
                if (uint.TryParse(str, out uiValue))
                    return true;
            }


            return false;
        }

        private BaseRS getBaseRS(StandSignals standSignals, string rsName)
        {
            if (standSignals.listRS.Count != 0)
            {
                foreach(BaseRS baseRS in standSignals.listRS)
                {
                    if (rsName == baseRS.Name)
                            {
                                return baseRS;
                            }
                }
            }
            return null;

        }
        private BoardRS getBoardRS(BoardConfig boardConfig, string rsName)
        {
            if (boardConfig.listBoardRS.Count != 0)
            {
                foreach (BoardRS boardRS in boardConfig.listBoardRS)
                {
                    if (rsName == boardRS.Name)
                    {
                        return boardRS;
                    }
                }
            }
            return null;

        }

        private BaseSignal getBaseSignal(StandSignals standSignals, string signalName)
        {
            // bool groupExist = false;
            if (standSignals.listSignals.Count != 0)
            {
                foreach (GroupSignals grSignals in standSignals.listSignals)
                {
                    if (standSignals.listSignals.Count != 0)
                    {
                        foreach (BaseSignal baseSignal in grSignals.baseSignals)
                            if (signalName == baseSignal.name)
                            {
                                return baseSignal;
                            }
                    }
                }
            }
            return null;
        }

        public StandSignals openStandXML(string XMLname)
        {
            BaseSignal baseSignal = null;
            GroupSignals groupSignal = null;
            StandSignals standSignals = new StandSignals();
            string str;
            string paramName = "";
            standSignals.listSignals.Clear();
            //lstBxXML.Items.Clear();
            int errors = 0;
            BaseRS baseRS = null;
            string baseRSname = null;
            uint rsPort=0;
            ushort setRSaddress=0;
            ushort bufRSaddress=0;
            string baseSigValueStr = null;
            bool bRS = false;
            //XmlTextReader reader = new XmlTextReader("StandSignals.xml");
            XmlTextReader reader = new XmlTextReader(XMLname);

            //           XmlTextReader reader = new XmlTextReader("Books.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                        paramName = reader.Name;

                        if (reader.Name == "SIGNAL")
                        {
                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                baseSignal = new BaseSignal(reader.Value);
                                baseSigValueStr = null;
                            }
                        }

                        if (reader.Name == "RS")
                        {
                            bRS = true;
                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                                baseRSname = reader.Value;
                        }

                        break;

                    case XmlNodeType.Text: // Вывести текст в каждом элементе.
                        str = reader.Value;
                        //lstBxXML.Items.Add(str);
                        //Console.WriteLine(reader.Value);

                        if (paramName == "PORT")
                        {
                            UInt32 port;
                            if (UInt32.TryParse(reader.Value, out port))
                            {
                                if(!bRS) {
                                    if (baseSignal != null)
                                        baseSignal.port = port;
                                }
                                else {
                                    rsPort=port;
                                }
                            }
                        }

                        if (paramName == "ADDRESS")
                        {
                            UInt32 addr;
                            if (UInt32.TryParse(reader.Value, out addr))
                            {
                                if (baseSignal != null)
                                    baseSignal.address = addr;
                            }
                        }


                        if (paramName == "TYPE")
                        {
                            switch (reader.Value)
                            {
                                case "LOGIC":
                                    signalType = SIGNALTYPE.LOGIC;
                                break;
                                case "ANALOG":
                                    signalType = SIGNALTYPE.ANALOG;
                                break;
                                case "ARRAY":
                                    signalType = SIGNALTYPE.ARRAY;
                                break;
                                case "COMPOUND":
                                    signalType = SIGNALTYPE.COMPOUND;
                                    break;
                                default:
                                    signalType = SIGNALTYPE.UNDEFINED;
                                    break;
                            }
                                if (baseSignal != null)
                                    baseSignal.type = signalType;
                        }


                        if (paramName == "SETADDRESS")
                        {
                            UInt16 addr;
                            if (UInt16.TryParse(reader.Value, out addr))
                            {
                               if(bRS) {
                                   setRSaddress=addr;
                               }
                            }
                        }

                        if (paramName == "BUFADDRESS")
                        {
                            UInt16 addr;
                            if (UInt16.TryParse(reader.Value, out addr))
                            {
                               if(bRS) {
                                   bufRSaddress=addr;
                               }
                            }
                        }

                        if (paramName == "SIZEINBITS")
                        {
                            UInt16 sizebits;
                            if (UInt16.TryParse(reader.Value, out sizebits))
                            {
                                if (baseSignal != null)
                                    baseSignal.sizeInBits = sizebits;
                            }
                        }

                        if (paramName == "INDEXBIT")
                        {
                            UInt16 indexbit;
                            if (UInt16.TryParse(reader.Value, out indexbit))
                            {
                                if (baseSignal != null)
                                    baseSignal.indexBit = indexbit;
                            }
                        }

                        if (paramName == "GROUP")
                        {
                            //                            if (UInt16.TryParse(reader.Value, out indexbit))
                            {
                                if (baseSignal != null)
                                    baseSignal.groupName = reader.Value;
                            }
                        }


                        if (paramName == "VALUE")
                        {
                            UInt32 value;
                            if (baseSignal != null) {
                                baseSignal.iValue = 0;
                                switch (baseSignal.type)
                                {
                                    case SIGNALTYPE.LOGIC:
                                        baseSignal.bValue = false;
                                        if (reader.Value == "TRUE")
                                        {
                                            baseSignal.bValue = true;
                                            baseSignal.iValue = (uint)(1 << baseSignal.indexBit);
                                        }
                                        else
                                        {
                                            if (reader.Value != "FALSE")
                                                errors++;
                                            else
                                                baseSignal.iValue = (uint) (~(1 << baseSignal.indexBit));
                                        }
                                        break;
                                    case SIGNALTYPE.ANALOG:
                                        if (UInt32.TryParse(reader.Value, out value))
                                        {
                                            if (baseSignal != null)
                                                baseSignal.iValue = value;
                                        }
                                        break;
                                    case SIGNALTYPE.ARRAY:
                                       // baseSignal.bValue = true;
                                        baseSigValueStr = reader.Value;
                                        break;
                                }

                             }
                        }


                        paramName = "";
                        break;
                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        //Console.Write("</" + reader.Name);
                        if (reader.Name == "RS")
                        {
                            bRS = false;
                            //GroupSignals groupSignals=new GroupSignals
                            baseRS = new BaseRS(baseRSname, rsPort, setRSaddress, bufRSaddress,baseRSname); 
                            standSignals.listRS.Add(baseRS);

                        }
                        if (reader.Name == "SIGNAL")
                        {
                            if (baseSignal.groupName == null)
                                baseSignal.groupName = "NONE";

                            bool groupExist = false;
                            if (standSignals.listSignals.Count != 0)
                            {
                                foreach (GroupSignals grSignals in standSignals.listSignals)
                                {
                                    if (grSignals.name == baseSignal.groupName)
                                    {
                                        grSignals.baseSignals.Add(baseSignal);
                                        if (!baseSignal.bValue)
                                        {
                                            uint groupVal = 0;
                                            for (int i = 0; i < grSignals.valueTX.Length; i++)
                                                groupVal += (uint)(grSignals.valueTX[i] << (8 * i));

                                            groupVal |= ~baseSignal.iValue;

                                            for (int i = 0; i < grSignals.valueTX.Length; i++)
                                                grSignals.valueTX[i] = (byte)((groupVal >> (8 * i)) & 0xFF);
                                        }
                                        baseSignal.group = grSignals;
                                        groupExist = true;
                                        break;
                                    }

                                }
                            }
                            if (!groupExist)
                            {
                                groupSignal = new GroupSignals(baseSignal.groupName, (ushort)(baseSignal.sizeInBits / 8));
                                if(signalType == SIGNALTYPE.ARRAY) {
                                    if(baseSigValueStr!=null) {
                                        byte[] buf = baseSigValueStr.Split('-').Select(n => Convert.ToByte(n, 16)).ToArray();
                                        groupSignal.valueTX = buf;
                                        }
                                    }
                                groupSignal.baseSignals.Add(baseSignal);
                                if (!baseSignal.bValue)
                                {
                                    uint groupVal = 0;
                                    for (int i = 0; i < groupSignal.valueTX.Length; i++)
                                        groupVal += (uint)(groupSignal.valueTX[i] << (8 * i));

                                    groupVal |= ~baseSignal.iValue;

                                    for (int i = 0; i < groupSignal.valueTX.Length; i++)
                                        groupSignal.valueTX[i] = (byte)((groupVal >> (8 * i)) & 0xFF);
                                }
                                baseSignal.group = groupSignal;
                                standSignals.listSignals.Add(groupSignal);
                            }


                        }
                        str = "</" + reader.Name + ">";
                        //lstBxXML.Items.Add(str);

                        //Console.WriteLine(">");
                        //lstBxXML.Items.Add(">");
                        break;
                }
            }
            return standSignals;
        }
        private bool str2double(string strValue, out  double dValue)
        {
//            if (uint.TryParse(str.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out uiValue))
            if (double.TryParse(strValue.Replace('.',','), out dValue))
                    return true;
            else
            {
                if (double.TryParse(strValue, out dValue))
                    return true;

            }
            return false;
        }

        public BoardsInfo OpenListBoards(string listBoardName)
        {

            BoardsInfo boards = new BoardsInfo();
            XmlTextReader reader = new XmlTextReader(listBoardName);
            string boardName = null;

            double dI27MAX = 999;
            double dI15MAX = 999;
            double dI12MAX = 999;
            double dI05MAX = 999;
            double dI03MAX = 999;


            string commentString = string.Empty;
            string imageLinkString = string.Empty;
            string commentHelpString = string.Empty;
            string imageLinkHelpString = string.Empty;
            List<string> listImageLink_Help=null;
            //            string str;

            string paramName = string.Empty;
            string fileName = string.Empty;

            // UInt16 delay = 1000;


            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                        paramName = reader.Name;
                        if (reader.Name == "BOARD")
                        {
                            dI27MAX = 999;
                            dI15MAX = 999;
                            dI12MAX = 999;
                            dI05MAX = 999;
                            dI03MAX = 999;

                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                boardName = reader.Value;
                                listImageLink_Help=new List<string>();
                            }
                        }

                        break;

                    case XmlNodeType.Text: // Вывести текст в каждом элементе.

                        if (paramName == "FILENAME")
                        {
                            fileName = reader.Value;
                        }


                        if (paramName == "COMMENT")
                        {
                            commentString = reader.Value;
                        }

                        if (paramName == "IMAGELINK")
                        {
                            imageLinkString = reader.Value;
                        }

                        if (paramName == "COMMENT_HELP")
                        {
                            commentHelpString = reader.Value;
                        }

                        if (paramName == "IMAGELINK_HELP")
                        {
                            imageLinkHelpString = reader.Value;
                            listImageLink_Help.Add(imageLinkHelpString);
                            
                        }

                        if (paramName == "I27MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                    dI27MAX = dValue;
                            }
                        }

                        if (paramName == "I15MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI15MAX = dValue;
                            }
                        }

                        if (paramName == "I12MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI12MAX = dValue;
                            }
                        }

                        if (paramName == "I5MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI05MAX = dValue;
                            }
                        }

                        if (paramName == "I3MAX")
                        {
                            double dValue = 0;
                            if (str2double(reader.Value, out dValue))
                            {
                                dI03MAX = dValue;
                            }
                        }

                        break;

                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        if (reader.Name == "BOARD")
                        {
                           // BoardInfo boardInf = new BoardInfo(boardName, fileName, imageLinkString, commentString, imageLinkHelpString, commentHelpString);
                            //BoardInfo boardInf = new BoardInfo(boardName, fileName, imageLinkString, commentString, imageLinkHelpString, commentHelpString);
                            //BoardInfo boardInf = new BoardInfo(boardName, fileName, imageLinkString, commentString, listImageLink_Help, commentHelpString);
                            BoardInfo boardInf = new BoardInfo(boardName, fileName, imageLinkString, commentString, listImageLink_Help, commentHelpString,
                                dI27MAX, dI15MAX, dI12MAX, dI05MAX, dI03MAX);
             
                            commentString = string.Empty;
                            imageLinkString = string.Empty;
                            commentHelpString = string.Empty;
                            imageLinkHelpString = string.Empty;
                            fileName = string.Empty;
                            listImageLink_Help=null;

                            boards.listBoardInfo.Add(boardInf);
                        }

                        break;
                }
            }


            return boards;
        }
    
        public FilesInfo OpenListFiles(string listFileName)
        {
            FilesInfo files=new FilesInfo();
            XmlTextReader reader = new XmlTextReader(listFileName);
            string file_Name = null;




            string commentString = string.Empty;
            string imageLinkString = string.Empty;
            string paramName = string.Empty;
            string fileName = string.Empty;


            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                        paramName = reader.Name;
                        if (reader.Name == "FILE")
                        {
                          fileName = string.Empty;
                          commentString = string.Empty;
                          imageLinkString = string.Empty;

                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                file_Name = reader.Value;
                                //listImageLink_Help=new List<string>();
                            }
                        }

                        break;

                    case XmlNodeType.Text: // Вывести текст в каждом элементе.

                        if (paramName == "FILENAME")
                        {
                            fileName = reader.Value;
                        }


                        if (paramName == "COMMENT")
                        {
                            commentString = reader.Value;
                        }

                        if (paramName == "IMAGELINK")
                        {
                            imageLinkString = reader.Value;
                        }

 
                        break;
                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        if (reader.Name == "FILE")
                        {
                            FileInfo fileInf = new FileInfo(file_Name, fileName, imageLinkString, commentString);
                     
                            commentString = string.Empty;
                            imageLinkString = string.Empty;
                            fileName = string.Empty;

                            files.listFileInfo.Add(fileInf);
                        }

                        break;
                }
            }


            return files;
        }
    
    }
}




