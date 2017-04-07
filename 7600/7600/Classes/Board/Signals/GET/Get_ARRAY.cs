using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    class Get_ARRAY : iGet
    {
        private BaseSignal source;

        //public iBase Source
        public BaseSignal Source
        {
            get { return source; }
        }

        /// <summary>
        /// Конструктор класс GET для стендового битового сигнала
        /// </summary>
        /// <param name="_source">Указываемый базовый сигнал стендовой платы (описан в XML стенда)</param>
        public Get_ARRAY(BaseSignal _source)
        {
            source = _source;
        }

        public typeGetSet ReturnTypeGet()
        {
            return typeGetSet.ARRAY;
        }
    }
}
