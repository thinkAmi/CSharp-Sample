using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowWindowMVVM
{
    public class DataContextChangedChildViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
    {
        public string Text { get { return "[D]ChildWindow"; } }

        public Func<bool?> ShowDialogBox { get; set; }

        public static bool? ShowDialog()
        {
            var vm = new DataContextChangedChildViewModel();
            var v = new DataContextChangedChildView();
            v.DataContext = vm;
            return vm.ShowDialogBox();
        }
    }
}
