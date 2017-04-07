using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class FileInfo
    {
        string name, filename, imagelink, comment;

        public string Comment
        {
            get { return comment; }
        }

        public string Imagelink
        {
            get { return imagelink; }
        }

        public string Filename
        {
            get { return filename; }
        }

        public string Name
        {
            get { return name; }
        }

        public FileInfo(string _name, string _filename, string _imagelink, string _comment)
        {
            name = _name;
            filename = _filename;
            imagelink = _imagelink;
            comment = _comment;
        }
    }
}
