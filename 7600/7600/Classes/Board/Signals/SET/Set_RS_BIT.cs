using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class Set_RS_BIT : iSet
    {
        BaseRS source;

        public BaseRS Source
        {
            get { return source; }
        }

        bool valueon;

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

        string comment, imagelink;

        public string ImageLink
        {
            get { return imagelink; }
        }

        public string Comment
        {
            get { return comment; }
        }

        BoardRS boardRs;

        public BoardRS BoardRs
        {
            get { return boardRs; }
        }
        /// <summary>
        /// Конструктор класса SET для RS-го битового сигнала
        /// </summary>
        /// <param name="_source">Указываемый дефайн RS. Описан в XML платы.</param>
        /// <param name="_boardRs"></param>
        /// <param name="_valueon">Значение битового сигнала для его проверки. TRUE/FALSE</param>
        /// <param name="_indexbyte">Индекс байта, с которого начинают лежать данные.</param>
        /// <param name="_indexbit">Индекс бита в нужном байте, с которого лежат данные.</param>
        /// <param name="_comment">Комментарий относительно установки данного сигнала.</param>
        /// <param name="_imagelink">Ссылка на картинку, описывающая сигнал.</param>
        public Set_RS_BIT(BaseRS _source, BoardRS _boardRs, bool _valueon, byte _indexbyte, byte _indexbit, string _comment, string _imagelink)
        {
            source = _source;
            boardRs = _boardRs;
            valueon = _valueon;
            indexbyte = _indexbyte;
            indexbit = _indexbit;
            comment = _comment;
            imagelink = _imagelink;
        }


        public typeGetSet ReturnTypeSet()
        {
            return typeGetSet.RS_BIT;
        }
    }
}
