using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7600
{
    public enum typeGetSet { UNDEFINED, STAND_BIT, STAND_ANALOG, RS_BIT, RS_ANALOG, ARRAY };
    public interface iGet
    {
        typeGetSet ReturnTypeGet();
        //iBase ReturnBase();
    }
}
