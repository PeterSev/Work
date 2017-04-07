using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    class Get_RS_ANALOG : iGet
    {
        BaseRS source;

        public BaseRS Source
        {
            get { return source; }
        }

        uint min, max;

        public uint Max
        {
            get { return max; }
        }

        public uint Min
        {
            get { return min; }
        }

        ushort sizebyte;

        public ushort SizeByte
        {
            get { return sizebyte; }
        }

        ushort indexbyte;

        public ushort IndexByte
        {
            get { return indexbyte; }
        }

        /// <summary>
        /// Конструктор класс GET для RS-го аналогового сигнала
        /// </summary>
        /// <param name="_source">Указываемый дефайн RS. Описан в XML платы.</param>
        /// <param name="_min">Нижняя граница проверяемого диапазона.</param>
        /// <param name="_max">Верхняя граница проверяемого диапазона.</param>
        /// <param name="_sizebyte">Размер сигнала в байтах.</param>
        /// <param name="_indexbyte">Индекс байта с которого начинают лежать данные.</param>
        public Get_RS_ANALOG(BaseRS _source, uint _min, uint _max, ushort _sizebyte, ushort _indexbyte)
        {
            source = _source;
            min = _min;
            max = _max;
            sizebyte = _sizebyte;
            indexbyte = _indexbyte;
        }

        public typeGetSet ReturnTypeGet()
        {
            return typeGetSet.RS_ANALOG;
        }
    }
}
