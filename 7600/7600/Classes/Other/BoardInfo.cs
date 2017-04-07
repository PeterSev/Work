using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class BoardInfo
    {
        string imagelink;

        public string Imagelink
        {
            get { return imagelink; }
        }
        string comment;

        public string Comment
        {
            get { return comment; }
        }

        string comment_help;

        public string Comment_help
        {
            get { return comment_help; }
        }
        string name;

        public string Name
        {
            get { return name; }
        }
        string filename;

        public string Filename
        {
            get { return filename; }
        }

        List<string> listImageLink_Help;

        public List<string> ListImageLink_Help
        {
            get { return listImageLink_Help; }
        }

        double i27Max, i15Max, i12Max, i5Max, i3Max;

        public double I3Max
        {
            get { return i3Max; }
        }

        public double I5Max
        {
            get { return i5Max; }
        }

        public double I12Max
        {
            get { return i12Max; }
        }

        public double I15Max
        {
            get { return i15Max; }
        }

        public double I27Max
        {
            get { return i27Max; }
        }

        /// <summary>
        /// Информация о тестовой плате.
        /// </summary>
        /// <param name="_name">Название тестовой платы</param>
        /// <param name="_filename">Название файла списка сигналов данной платы</param>
        /// <param name="_imagelink">Ссылка на картинку с общим изображением платы</param>
        /// <param name="_comment">Комментарий под общим изображением платы</param>
        /// <param name="_listimagelink_help">Список ссылок на картинки с изображением подключения данной платы</param>
        /// <param name="_comment_help">Комментарий под изображением с подключением платы</param>
        public BoardInfo(string _name, string _filename, string _imagelink, string _comment, List<string> _listimagelink_help, string _comment_help)
        {
            name = _name;
            filename = _filename;
            imagelink = _imagelink;
            comment = _comment;
            listImageLink_Help = _listimagelink_help;
            comment_help = _comment_help;
        }

        public BoardInfo(string _name, string _filename, string _imagelink, string _comment, List<string> _listimagelink_help, string _comment_help, double _i27Max, double _i15Max, double _i12Max, double _i5Max, double _i3Max)
        {
            name = _name;
            filename = _filename;
            imagelink = _imagelink;
            comment = _comment;
            listImageLink_Help = _listimagelink_help;
            comment_help = _comment_help;
            i27Max = _i27Max;
            i15Max = _i15Max;
            i12Max = _i12Max;
            i5Max = _i5Max;
            i3Max = _i3Max;
        }
    }
}
