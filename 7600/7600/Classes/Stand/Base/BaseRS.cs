using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public class BaseRS : iBase
    {
        string name;

        public string Name
        {
            get { return name; }
        }

        uint port;

        public uint Port
        {
            get { return port; }
        }

        ushort setaddress, bufaddress;

        public ushort BufAddress
        {
            get { return bufaddress; }
        }

        public ushort SetAddress
        {
            get { return setaddress; }
        }

        GroupSignals group;

        public GroupSignals Group
        {
            set
            {

                if (value.name == groupName)
                {
                    group = value;
                }

            }

            get { return group; }
        }
        string groupName;

        public string GroupName
        {
            get { return groupName; }
        }
 
        public BaseRS(string _name, uint _port, ushort _setAddr, ushort _bufAddr, GroupSignals _group)
        {
            name = _name;
            group = _group;
            groupName = group.name;
            port = _port;
            setaddress = _setAddr;
            bufaddress = _bufAddr;
        }

        public BaseRS(string _name, uint _port, ushort _setAddr, ushort _bufAddr, string _groupName)
        {
            name = _name;
            group = null;
            groupName = _groupName;
            port = _port;
            setaddress = _setAddr;
            bufaddress = _bufAddr;
        }

        public bool isRS()
        {
            return true;
        }
    }
}
