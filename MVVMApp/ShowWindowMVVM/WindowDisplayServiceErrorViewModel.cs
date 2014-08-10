using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowWindowMVVM
{
    public class WindowDisplayServiceErrorViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
    {
        public static bool? ShowDialog()
        {
            var vm = new WindowDisplayServiceErrorViewModel();

            //  ここでエラー(例外)が発生：ViewのXAMLには下記の記述は行っている
            //  ServiceNotFoundExceptionはハンドルされませんでした
            //  Service not found: Microsoft.TeamFoundation.MVVM.IWindowDisplayService.
            //  Make sure that 'mvvm:MVVMSupport.ViewModel="{Binding}"' is in your .xaml file.
            return vm.WindowDisplayService.ShowDialog("ErrorWindowKey", vm);
        }
    }
}
