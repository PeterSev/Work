using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7637_WS4
{
    public class Board
    {
        string name;
        public string Name
        {
            get { return name; }
        }

        string catalog;
        public string Catalog
        {
            get { return catalog; }
        }

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

        public Board(string _name, string _catalog, string _imagelink, string _comment)
        {
            name = _name;
            catalog = _catalog;
            imagelink = _imagelink;
            comment = _comment;
        }
    }
}
