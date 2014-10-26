using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;

namespace DynamicStringFormatMVVM
{
    public class DynamicStringFormatViewModel : ViewModelBase
    {
        public int Quantity { get { return 1000000; } }
        public int MinusQuantity { get { return -1000000; } }


        public bool Reverse { get { return true; } }
        public bool Normal { get { return false; } }
    }
}
