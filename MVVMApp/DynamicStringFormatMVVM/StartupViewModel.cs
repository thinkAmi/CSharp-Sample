using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;

namespace DynamicStringFormatMVVM
{
    class StartupViewModel : ViewModelBase
    {
        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get {
                if (_openCommand == null)
                {
                    _openCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteOpenCommand);
                }
                return _openCommand;
            }
        }
        

        private void ExecuteOpenCommand(object x)
        {
            WindowDisplayService.ShowDialog("NextView", new DynamicStringFormatViewModel());
        }
    }
}
