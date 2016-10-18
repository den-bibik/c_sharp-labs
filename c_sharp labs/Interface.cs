using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    interface IDeepCopy
    { object DeepCopy(); }

    interface IDate
    { DateTime Date { get; set; } }

}
