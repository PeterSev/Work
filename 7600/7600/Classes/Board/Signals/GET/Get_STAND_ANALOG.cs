using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    class Get_STAND_ANALOG : iGet
    {
        BaseSignal source;

        public BaseSignal Source
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

        /// <summary>
        /// Конструктор класс GET для стендового аналогового сигнала
        /// </summary>
        /// <param name="_source">Указываемый базовый сигнал стендовой платы (описан в XML стенда)</param>
        /// <param name="_min">Нижняя граница проверяемого диапазона.</param>
        /// <param name="_max">Верхняя граница проверяемого диапазона.</param>
        public Get_STAND_ANALOG(BaseSignal _source, uint _min, uint _max)
        {
            source = _source;
            min = _min;
            max = _max;
        }

        public typeGetSet ReturnTypeGet()
        {
            return typeGetSet.STAND_ANALOG;
        }
    }
}
