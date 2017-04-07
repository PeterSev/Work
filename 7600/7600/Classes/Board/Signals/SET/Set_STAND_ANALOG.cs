using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class Set_STAND_ANALOG : iSet
    {
        BaseSignal source;

        public BaseSignal Source
        {
            get { return source; }
        }

        uint valueon, valueoff;

        public uint ValueOff
        {
            get { return valueoff; }
        }

        public uint ValueOn
        {
            get { return valueon; }
        }

        string comment, imagelink;

        public string ImageLink
        {
            get { return imagelink; }
        }

        public string Comment
        {
            get { return comment; }
        }

        /// <summary>
        /// Конструктор класса SET для стендового аналогового сигнала
        /// </summary>
        /// <param name="_source">Указываемый базовый сигнал стендовой платы (описан в XML стенда).</param>
        /// <param name="_valueon">Числовое значение аналогового сигнала для его включения.</param>
        /// <param name="_comment">Комментарий относительно установки данного сигнала.</param>
        /// <param name="_imagelink">Ссылка на картинку, описывающая сигнал.</param>
        /// <param name="_valueoff">Числовое значение аналогового сигнала для его выключения.</param>
        public Set_STAND_ANALOG(BaseSignal _source, uint _valueon, string _comment, string _imagelink, uint _valueoff)
        {
            source = _source;
            valueon = _valueon;
            comment = _comment;
            imagelink = _imagelink;
            valueoff = _valueoff;
        }

        public typeGetSet ReturnTypeSet()
        {
            return typeGetSet.STAND_ANALOG;
        }
    }
}
