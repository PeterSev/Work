using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    /// <summary>
    /// Признак посылки всего пакета, включая адрес и размер согласно протоколу или же только данные
    /// </summary>
    public enum CommandType { TYPICAL, JUST_ARRAY};
    public class Command
    {
        public byte numCommand;
        public ushort address;
        public ushort count;
        public byte[] data;
        public SET_OR_GET set_or_get;
        public int index;
        public CommandType commandType; //признак отправки полноценной команды или же только буфера данных (для типа сигнала ARRAY)

        public CommandResult result;
        //public string name;
        public ulong timeOut;
        //public CommandType comType;

        /// <summary>
        /// Отправляемая команда
        /// </summary>
        /// <param name="_addr">Адрес данных</param>
        /// <param name="_cnt">Количество байт (размер)</param>
        /// <param name="_set_or_get">Команда для SET, GET или общая</param>
        /// <param name="_index">Индекс текущего сигнала среди общего списка сигналов (параметров) платы</param>
        public Command(ushort _addr, ushort _cnt, SET_OR_GET _set_or_get, int _index, CommandType _commandType) 
        {
            //comType = _comType;
            address = _addr;
            count = _cnt;
            set_or_get = _set_or_get;
            index = _index;
            timeOut = 0;
            commandType = _commandType;

            //data = new byte[_comType == CommandType.READ ? 0 : _cnt];
            data = new byte[_cnt];
        }

        /// <summary>
        /// Входящая команда
        /// </summary>
        /// <param name="_timeout">Время ожидания прихода команды, мс</param>
        public Command(ulong _timeout)
        {
            address = 0;
            count = 0;
            timeOut = _timeout;
            commandType = CommandType.TYPICAL;
            set_or_get = SET_OR_GET.OTHER;
            index = 0;
            data = new byte[0];
        }

        public void SetDataNewSize(ushort size)
        {
            data = new byte[size];
        }

        public byte[] GetCommandByteBuf()
        {
            byte[] buf;
            byte[] bufAddr = new byte[2];
            byte[] bufCnt = new byte[2];
            bufAddr[0] = (byte)address;
            bufAddr[1] = (byte)(address >> 8);
            bufCnt[0] = (byte)count;
            bufCnt[1] = (byte)(count >> 8);

            buf = bufAddr.Concat(bufCnt).ToArray().Concat(data).ToArray();

            if (commandType == CommandType.TYPICAL)
                return buf;
            else
                return data;
        }

        public void DiscardData()
        {
            Array.Clear(data, 0, data.Length);
        }
    }
}
