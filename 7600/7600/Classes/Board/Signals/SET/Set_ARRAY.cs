using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    class Set_ARRAY:iSet
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
        /// Конструктор класса SET для стендового сигнала с массивом данных
        /// </summary>
        /// <param name="_source">Указываемый базовый сигнал стендовой платы (описан в XML стенда)</param>
        /// <param name="_comment">Комментарий относительно установки данного сигнала.</param>
        /// <param name="_imagelink">Ссылка на картинку, описывающая сигнал.</param>
        public Set_ARRAY(BaseSignal _source, string _comment, string _imagelink)
        {
            source = _source;
            comment = _comment;
            imagelink = _imagelink;
        }

        public typeGetSet ReturnTypeSet()
        {
            return typeGetSet.ARRAY;
        }
    }
}
