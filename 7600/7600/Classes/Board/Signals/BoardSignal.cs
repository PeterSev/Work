using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    /// <summary>
    /// Состояния сигнала. SETSTART - сигнал начал выполнение после SETа, READY - сигнал готов к выполнению Сета и его Гетов. SETREADY - сигнал принял все Геты. DESETSTART - сигнал начал выполнение ДЕСЕТа
    /// </summary>
    public enum SignalStatus { READY, SETSTART, SETREADY, DESETSTART };

    public class CommonSet
    {
        public Source source;
        public uint valueon;
        public ushort indexbyte;
        public ushort indexbit;
        public uint valueoff;
        public ushort sizebyte;
        public string comment;
        public string imagelink;

        public SIGNALTYPE signalType;

        public CommonSet()
        {
            source = null;
            valueon = 0;
            valueoff = 0;
            indexbit = 0;
            indexbyte = 0;
            sizebyte = 0;
            comment = string.Empty;
            imagelink = string.Empty;

            signalType = SIGNALTYPE.UNDEFINED;
        }        
    }

    public class CommonGet
    {
        public Source source;
        public uint valueon;
        public ushort indexbyte;
        public ushort indexbit;
        public ushort sizebyte;
        public uint min;
        public uint max;

        public SIGNALTYPE signalType;

        public CommonGet()
        {
            source = null;
            valueon = 0;
            indexbit = 0;
            indexbyte = 0;
            sizebyte = 0;
            min = max = 0;

            signalType = SIGNALTYPE.UNDEFINED;
        }
    }

    public class Source
    {
        public string name;
        public string groupName;
        public GroupSignals group;
        public uint port;
        public uint address;
        public SIGNALTYPE type;
        public ushort sizeInBits;
        public ushort indexBit;
        public MODETYPE mode;
        public bool bValue;
        public uint iValue;

        public ushort setAddr;
        public ushort bufAddr;

        public bool isRS;
    }

    public class GROUP_RS
    {
        //string name;
        public byte[] valueRX, valueTX;
    }

    public class BoardSignal
    {
        string name;
        public CommonSet set = new CommonSet();
        public CommonGet get = new CommonGet();

        public string Name
        {
            get { return name; }
        }

        ushort delay;
        public ushort Delay
        {
            get { return delay; }
        }

        double d27Max = 0, d15Max = 0, d12Max = 0, d5Max = 0, d3Max = 0;

        public double D3Max
        {
            get { return d3Max; }
        }

        public double D5Max
        {
            get { return d5Max; }
        }

        public double D12Max
        {
            get { return d12Max; }
        }

        public double D15Max
        {
            get { return d15Max; }
        }

        public double D27Max
        {
            get { return d27Max; }
        }

        public BoardSignal(string _name, iSet _set, iGet _get, ushort _delay, double _d27Max, double _d15Max, double _d12Max, double _d5max, double _d3Max)
        {
            name = _name;
            delay = _delay;
            d27Max = _d27Max;
            d15Max = _d15Max;
            d12Max = _d12Max;
            d5Max = _d5max;
            d3Max = _d3Max;

            if (_set != null)
            {
                switch (_set.ReturnTypeSet())
                {
                    case typeGetSet.RS_ANALOG:
                        {
                            BaseRS tmp = ((Set_RS_ANALOG)_set).Source;
                            set.source = new Source();
                            set.source.name = tmp.Name;
                            set.source.groupName = tmp.GroupName;
                            set.source.group = tmp.Group;
                            set.source.port = tmp.Port;
                            set.source.address = 0;
                            set.source.type = SIGNALTYPE.ANALOG;
                            set.source.sizeInBits = (ushort)(((Set_RS_ANALOG)_set).SizeByte * 8);
                            set.source.indexBit = 0;
                            set.source.mode = MODETYPE.UNDEFINED;
                            set.source.bValue = false;
                            set.source.iValue = ((Set_RS_ANALOG)_set).ValueOn;

                            set.source.setAddr = tmp.SetAddress;
                            set.source.bufAddr = tmp.BufAddress;

                            set.valueon = ((Set_RS_ANALOG)_set).ValueOn;
                            set.indexbyte = ((Set_RS_ANALOG)_set).IndexByte;
                            set.indexbit = 0;
                            set.valueoff = ((Set_RS_ANALOG)_set).ValueOff;
                            set.sizebyte = ((Set_RS_ANALOG)_set).SizeByte;
                            set.comment = ((Set_RS_ANALOG)_set).Comment;
                            set.imagelink = ((Set_RS_ANALOG)_set).ImageLink;

                            //set.signalType = typeGetSet.RS_ANALOG;
                            set.signalType = set.source.type;
                            set.source.isRS = tmp.isRS();
                        }
                        break;
                    case typeGetSet.RS_BIT:
                        {
                            //BaseRS tmp = ((BaseRS)((Set_RS_BIT)_set).Source);
                            BaseRS tmp = ((Set_RS_BIT)_set).Source;
                            set.source = new Source();
                            set.source.name = tmp.Name;
                            set.source.groupName = tmp.GroupName;
                            set.source.group = tmp.Group;
                            set.source.port = tmp.Port;
                            set.source.address = 0;
                            set.source.type = SIGNALTYPE.LOGIC;
                            set.source.sizeInBits = (ushort)(8 * ((Set_RS_BIT)_set).BoardRs.NumBytes);
                            set.source.indexBit = ((Set_RS_BIT)_set).IndexBit;
                            set.source.mode = MODETYPE.WRITE;
                            set.source.bValue = ((Set_RS_BIT)_set).ValueOn;

                            if (set.source.bValue)
                                set.source.iValue = (uint)(1 << ((Set_RS_BIT)_set).IndexBit);
                            else
                                set.source.iValue = (uint)(~(1 << ((Set_RS_BIT)_set).IndexBit));

                            set.source.setAddr = tmp.SetAddress;
                            set.source.bufAddr = tmp.BufAddress;
                            set.source.isRS = tmp.isRS();


                            set.valueon = set.source.iValue;
                            set.indexbyte = ((Set_RS_BIT)_set).IndexByte;
                            set.indexbit = set.source.indexBit;
                            set.valueoff = ~set.valueon;

                            set.sizebyte = (ushort)(set.source.sizeInBits / 8);
                            set.comment = ((Set_RS_BIT)_set).Comment;
                            set.imagelink = ((Set_RS_BIT)_set).ImageLink;

                            set.signalType = set.source.type;
                        }
                        break;
                    case typeGetSet.STAND_ANALOG:
                        {
                            BaseSignal tmp = ((Set_STAND_ANALOG)_set).Source;
                            set.source = new Source();

                            set.source.name = tmp.name;
                            set.source.group = tmp.group;
                            set.source.groupName = tmp.groupName;
                            set.source.port = tmp.port;
                            set.source.address = tmp.address;
                            set.source.type = tmp.type;
                            set.source.sizeInBits = tmp.sizeInBits;
                            set.source.indexBit = tmp.indexBit;
                            set.source.mode = tmp.mode;
                            set.source.bValue = tmp.bValue;
                            set.source.iValue = tmp.iValue;
                            set.source.setAddr = 0;
                            set.source.bufAddr = 0;
                            set.source.isRS = tmp.isRS();

                            set.valueon = ((Set_STAND_ANALOG)_set).ValueOn;
                            set.indexbyte = 0;
                            set.indexbit = set.source.indexBit;
                            set.valueoff = ((Set_STAND_ANALOG)_set).ValueOff;
                            set.sizebyte = (ushort)(set.source.sizeInBits / 8);
                            set.comment = ((Set_STAND_ANALOG)_set).Comment;
                            set.imagelink = ((Set_STAND_ANALOG)_set).ImageLink;
                            set.signalType = set.source.type;     

                            for(int i=0;i<set.sizebyte;i++)
                                set.source.group.valueTX[i] = (byte)((set.valueon >> (8 * i)) & 0xFF);
                        }
                        break;
                    case typeGetSet.STAND_BIT:
                        {
                            BaseSignal tmp = ((Set_STAND_BIT)_set).Source;
                            set.source = new Source();
                            set.source.name = tmp.name;
                            set.source.groupName = tmp.groupName;
                            set.source.group = tmp.group;
                            set.source.port = tmp.port;
                            set.source.address = tmp.address;
                            set.source.type = tmp.type;
                            set.source.sizeInBits = tmp.sizeInBits;
                            set.source.indexBit = tmp.indexBit;
                            set.source.mode = tmp.mode;
                            set.source.bValue = tmp.bValue;
                            set.source.iValue = tmp.iValue;
                            set.source.isRS = tmp.isRS();
                            set.source.setAddr = 0;
                            set.source.bufAddr = 0;

                            set.valueon = set.source.iValue;
                            set.indexbyte = 0;
                            set.indexbit = set.source.indexBit;
                            set.valueoff = ~set.valueon;
                            set.sizebyte = (ushort)(set.source.sizeInBits / 8);

                            set.comment = ((Set_STAND_BIT)_set).Comment;
                            set.imagelink = ((Set_STAND_BIT)_set).ImageLink;

                            set.signalType = set.source.type;
                        }
                        break;
                    case typeGetSet.ARRAY:
                        {
                            BaseSignal tmp = ((Set_ARRAY)_set).Source;
                            set.source = new Source();
                            set.source.name = tmp.name;
                            set.source.groupName = tmp.groupName;
                            set.source.group = tmp.group;
                            set.source.port = tmp.port;
                            set.source.address = tmp.address;
                            set.source.type = tmp.type;
                            set.source.sizeInBits = tmp.sizeInBits;
                            set.source.indexBit = tmp.indexBit;     // ((Set_STAND_BIT)_set).
                            set.source.mode = tmp.mode;
                            set.source.bValue = tmp.bValue;
                            set.source.iValue = tmp.iValue;
                            set.source.isRS = tmp.isRS();
                            set.source.setAddr = 0;
                            set.source.bufAddr = 0;

                            set.valueon = set.source.iValue;
                            set.indexbyte = 0;
                            set.indexbit = set.source.indexBit;
                            set.valueoff = ~set.valueon;
                            set.sizebyte = (ushort)(set.source.sizeInBits / 8);

                            set.comment = ((Set_ARRAY)_set).Comment;
                            set.imagelink = ((Set_ARRAY)_set).ImageLink;

                            //set.signalType = typeGetSet.ARRAY;
                            set.signalType = set.source.type;
                        }
                        break;
                }
            }
            else
            {
                set = null;
            }

            if (_get != null)
            {
                switch (_get.ReturnTypeGet())
                {
                    case typeGetSet.RS_ANALOG:
                        {
                            BaseRS tmp = ((Get_RS_ANALOG)_get).Source;
                            get.source = new Source();
                            get.source.name = tmp.Name;
                            get.source.groupName = tmp.GroupName;
                            get.source.group = tmp.Group;
                            get.source.port = tmp.Port;
                            get.source.address = 0;
                            get.source.type = SIGNALTYPE.ANALOG;
                            get.source.sizeInBits = (ushort)(((Get_RS_ANALOG)_get).SizeByte * 8);
                            get.source.indexBit = 0;
                            get.source.mode = MODETYPE.UNDEFINED;
                            get.source.bValue = false;
                            get.source.iValue = 0;
                            get.source.setAddr = tmp.SetAddress;
                            get.source.bufAddr = tmp.BufAddress;
                            get.source.isRS = tmp.isRS();

                            get.valueon = get.source.iValue;
                            get.indexbyte = ((Get_RS_ANALOG)_get).IndexByte;
                            get.indexbit = 0;
                            get.sizebyte = (ushort)(get.source.sizeInBits / 8);
                            get.min = ((Get_RS_ANALOG)_get).Min;
                            get.max = ((Get_RS_ANALOG)_get).Max;
                            get.signalType = get.source.type;
                        }
                        break;
                    case typeGetSet.RS_BIT:
                        {
                            BaseRS tmp = ((Get_RS_BIT)_get).Source;
                            get.source = new Source();
                            get.source.name = tmp.Name;
                            get.source.groupName = tmp.GroupName;
                            get.source.group = tmp.Group;
                            get.source.port = tmp.Port;
                            get.source.address = 0;
                            get.source.type = SIGNALTYPE.LOGIC;
                            get.source.sizeInBits = (ushort)(8 * ((Get_RS_BIT)_get).BoardRs.NumBytes);
                            get.source.indexBit = ((Get_RS_BIT)_get).IndexBit;
                            get.source.mode = MODETYPE.READ;
                            get.source.bValue = ((Get_RS_BIT)_get).ValueOn;
                            if (get.source.bValue)
                                get.source.iValue = (uint)(1 << ((Get_RS_BIT)_get).IndexBit);
                            else
                                get.source.iValue = (uint)(~(1 << ((Get_RS_BIT)_get).IndexBit));
                            get.source.setAddr = tmp.SetAddress;
                            get.source.bufAddr = tmp.BufAddress;
                            get.source.isRS = tmp.isRS();

                            get.valueon = get.source.iValue;
                            get.indexbyte = ((Get_RS_BIT)_get).IndexByte;
                            get.indexbit = get.source.indexBit;
                            get.sizebyte = (ushort)(get.source.sizeInBits / 8);
                            get.min = 0;
                            get.max = 0;

                            //get.signalType = typeGetSet.RS_BIT;
                            get.signalType = get.source.type;
                        }
                        break;
                    case typeGetSet.STAND_ANALOG:
                        {
                            BaseSignal tmp = ((Get_STAND_ANALOG)_get).Source;
                            get.source = new Source();

                            get.source.name = tmp.name;
                            get.source.groupName = tmp.groupName;
                            get.source.group = tmp.group;
                            get.source.port = tmp.port;
                            get.source.address = tmp.address;
                            get.source.type = tmp.type;
                            get.source.sizeInBits = tmp.sizeInBits;
                            get.source.indexBit = tmp.indexBit;
                            get.source.mode = tmp.mode;
                            get.source.bValue = tmp.bValue;
                            get.source.iValue = tmp.iValue;
                            get.source.setAddr = 0;
                            get.source.bufAddr = 0;
                            get.source.isRS = tmp.isRS();

                            get.valueon = get.source.iValue;
                            get.indexbyte = 0;
                            get.indexbit = tmp.indexBit;
                            get.sizebyte = (ushort)(get.source.sizeInBits / 8);
                            get.min = ((Get_STAND_ANALOG)_get).Min;
                            get.max = ((Get_STAND_ANALOG)_get).Max;
                        }
                        break;
                    case typeGetSet.STAND_BIT:
                        {
                            BaseSignal tmp = ((Get_STAND_BIT)_get).Source;
                            get.source = new Source();
                            get.source.name = tmp.name;
                            get.source.groupName = tmp.groupName;
                            get.source.group = tmp.group;
                            get.source.port = tmp.port;
                            get.source.address = tmp.address;
                            get.source.type = tmp.type;
                            get.source.sizeInBits = tmp.sizeInBits;
                            get.source.indexBit = tmp.indexBit;
                            get.source.mode = tmp.mode;
                            get.source.bValue = tmp.bValue;
                            get.source.iValue = tmp.iValue;
                            get.source.isRS = tmp.isRS();
                            get.source.setAddr = 0;
                            get.source.bufAddr = 0;

                            get.valueon = get.source.iValue;
                            get.indexbyte = 0;
                            get.indexbit = get.source.indexBit;
                            get.sizebyte = (ushort)(get.source.sizeInBits / 8);
                            get.min = 0;
                            get.max = 0;

                            //get.signalType = typeGetSet.STAND_BIT;
                            get.signalType = get.source.type;
                        }
                        break;
                    case typeGetSet.ARRAY:
                        {
                            BaseSignal tmp = ((Get_ARRAY)_get).Source;

                            //если Source сигнала Array равен нулю, ничего не делаем. Это признак для необходимости анализа ответного пакета
                            //проверка дабы не было исключения
                            if (tmp != null)
                            {
                                get.source = new Source();
                                get.source.name = tmp.name;
                                get.source.groupName = tmp.groupName;
                                get.source.group = tmp.group;
                                get.source.port = tmp.port;
                                get.source.address = tmp.address;
                                get.source.type = tmp.type;
                                get.source.sizeInBits = tmp.sizeInBits;
                                get.source.indexBit = tmp.indexBit;
                                get.source.mode = tmp.mode;
                                get.source.bValue = tmp.bValue;
                                get.source.iValue = tmp.iValue;
                                get.source.isRS = tmp.isRS();
                                get.source.setAddr = 0;
                                get.source.bufAddr = 0;

                                get.valueon = get.source.iValue;
                                get.indexbyte = 0;
                                get.indexbit = get.source.indexBit;
                                get.sizebyte = (ushort)(get.source.sizeInBits / 8);
                                get.min = 0;
                                get.max = 0;

                                //get.signalType = typeGetSet.ARRAY;
                                get.signalType = get.source.type;
                            }
                        }
                        break;
                }
            }
            else
            {
                get = null;
            }
        }
    }
}
