using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7637_WS4
{
    public class TestInfo
    {
        string name;

        public string Name
        {
            get { return name; }
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

        public TestInfo(string _name, string _imagelink, string _comment)
        {
            name = _name;
            imagelink = _imagelink;
            comment = _comment;
        }

    }
}
