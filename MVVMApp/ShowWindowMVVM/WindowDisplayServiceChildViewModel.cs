using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowWindowMVVM
{
    public class WindowDisplayServiceChildViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
    {
        public string Text { get{ return "[W]ChildWindow"; } }
    }
}
