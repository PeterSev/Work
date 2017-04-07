using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class Set_RS_ANALOG:iSet
    {
        BaseRS source;

        public BaseRS Source
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

        BoardRS boardRs;

        public BoardRS BoardRs
        {
            get { return boardRs; }
        }

        /// <summary>
        /// Конструктор класса SET для RS-го аналогового сигнала
        /// </summary>
        /// <param name="_source">Указываемый дефайн RS. Описан в XML платы.</param>
        /// <param name="_valueon">Числовое значение аналогового сигнала для его включения.</param>
        /// <param name="_sizebyte">Размер сигнала в байтах.</param>
        /// <param name="_indexbyte">Индекс байта с которого начинают лежать данные.</param>
        /// <param name="_comment">Комментарий относительно установки данного сигнала.</param>
        /// <param name="_imagelink">Ссылка на картинку, описывающая сигнал.</param>
        /// <param name="_valueoff">Числовое значение аналогового сигнала для его выключения.</param>
        public Set_RS_ANALOG(BaseRS _source, BoardRS _boardRs, uint _valueon, ushort _sizebyte, ushort _indexbyte, string _comment, string _imagelink, uint _valueoff)
        {
            source = _source;
            boardRs = _boardRs;
            valueon = _valueon;
            sizebyte = _sizebyte;
            indexbyte = _indexbyte;
            comment = _comment;
            imagelink = _imagelink;
            valueoff = _valueoff;
        }

        public typeGetSet ReturnTypeSet()
        {
            return typeGetSet.RS_ANALOG;
        }
    }
}
