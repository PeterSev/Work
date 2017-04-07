using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public enum SIGNALTYPE { UNDEFINED, LOGIC, ANALOG, COMPOUND, ARRAY};
    public enum MODETYPE { UNDEFINED, WRITE, READ };
    public class BaseSignal : iBase
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


        public BaseSignal(string _name)
        {
            name = _name;
            groupName = "NONE";
            group = null;
            port = 55055;
            address = 0;
            type = SIGNALTYPE.UNDEFINED;
            sizeInBits = 0;
            indexBit = 0;
            mode = MODETYPE.UNDEFINED;
            bValue = true;
            iValue = 0;
        }

        /// <summary>
        /// Конструктор базовой сигнала стенда
        /// </summary>
        /// /// <param name="_name">Имя сигнала</param>
        /// <param name="_port">Номер порта</param>
        /// <param name="_addr">Базовый адрес</param>
        /// <param name="_type">Тип сигнала. LOGIC - битовой, ANALOG - аналоговый, COMPOUND - составной</param>
        /// <param name="_sizeInBits">Общий размер сигнала (группы) в битах</param>
        /// <param name="_indexBit">Индекс бита в составе всего сигнала (группы)</param>
        /// <param name="_mode">Режим доступа. WRITE, READ</param>
        /// <param name="_bValue">Логическое значение для сигнала. TRUE/FALSE. Для аналогового всегда FALSE</param>
        /// <param name="_iValue">Маска для битового сигнала и значение для аналогового. Если битовый сигнал FALSE, то устанавливается инверсная маска</param>
        /// <param name="groupName">Имя группы</param>
        /// <param name="group">Ссылка на группу</param>
        /// 
        public BaseSignal(string _name,uint _port, uint _addr, SIGNALTYPE _type, ushort _sizeInBits, ushort _indexBit, MODETYPE _mode, bool _bValue, uint _iValue, string _groupName, GroupSignals _group)
        {
            name = _name;
            port = _port;
            address = _addr;
            type = _type;
            sizeInBits = _sizeInBits;
            indexBit = _indexBit;
            mode = _mode;
            groupName = _groupName;
            group = _group;
            bValue = _bValue;
            iValue = _iValue;
        }

        public bool isRS()
        {
            return false;
        }

    }
}
