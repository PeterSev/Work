using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    class Set_STAND_BIT : iSet
    {
        BaseSignal source;

        public BaseSignal Source
        {
            get { return source; }
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
        /// Конструктор класса SET для стендового битового сигнала
        /// </summary>
        /// <param name="_source">Указываемый базовый сигнал стендовой платы (описан в XML стенда)</param>
        /// <param name="_comment">Комментарий относительно установки данного сигнала.</param>
        /// <param name="_imagelink">Ссылка на картинку, описывающая сигнал.</param>
        public Set_STAND_BIT(BaseSignal _source, string _comment, string _imagelink)
        {
            source = _source;
            comment = _comment;
            imagelink = _imagelink;
        }


        public typeGetSet ReturnTypeSet()
        {
            return typeGetSet.STAND_BIT;
        }
    }
}
