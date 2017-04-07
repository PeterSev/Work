using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    class Get_RS_BIT : iGet
    {
        private BaseRS source;

        public BaseRS Source
        {
            get { return source; }
        }
        private bool valueon;

        public bool ValueOn
        {
            get { return valueon; }
        }
        private byte indexbyte;

        public byte IndexByte
        {
            get { return indexbyte; }
        }
        private byte indexbit;

        public byte IndexBit
        {
            get { return indexbit; }
        }

        BoardRS boardRs;

        public BoardRS BoardRs
        {
            get { return boardRs; }
        }

        /// <summary>
        /// Конструктор класс GET для RS-го битового сигнала
        /// </summary>
        /// <param name="_source">Указываемый дефайн RS. Описан в XML платы.</param>
        /// <param name="_ValueOn">Значение битового сигнала для его проверки. TRUE/FALSE</param>
        /// <param name="_IndexByte">Индекс байта, с которого начинают лежать данные.</param>
        /// <param name="_IndexBit">Индекс бита в нужном байте, с которого лежат данные.</param>
        public Get_RS_BIT(BaseRS _source, BoardRS _boardRs, bool _valueon, byte _indexbyte, byte _indexbit)
        {
            source = _source;
            boardRs = _boardRs;
            valueon = _valueon;
            indexbyte = _indexbyte;
            indexbit = _indexbit;
        }

        public typeGetSet ReturnTypeGet()
        {
            return typeGetSet.RS_BIT;
        }
    }
}
