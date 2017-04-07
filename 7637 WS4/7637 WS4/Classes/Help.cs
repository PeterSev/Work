using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7637_WS4
{
    public class Help
    {
        string imagelink;

        public string Imagelink
        {
            get { return imagelink; }
            set { imagelink = value; }
        }
        string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public Help(string _imagelink, string _comment)
        {
            imagelink = _imagelink;
            comment = _comment;
        }
    }
}
