using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input; // ICommand用

namespace MessageBoxMVVM
{
    class MessageBoxServiceViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
    {
        private ICommand _showCommand;
        public ICommand ShowCommand
        {
            get
            {
                if (_showCommand == null)
                {
                    _showCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShowCommand);
                }
                return _showCommand;
            }
        }


        private void ExecuteShowCommand(object x)
        {
            // Microsoft.TeamFoundation.MVVM.ViewModelBaseのMessageBoxServiceプロパティに、
            // MessageBoxに関するものが設定されている
            var r = MessageBoxService.Show("Yes or No", "title", System.Windows.MessageBoxButton.YesNo);

            if (r == System.Windows.MessageBoxResult.Yes)
            {
                MessageBoxService.ShowInformation("Yes", "info");
            }
            else
            {
                MessageBoxService.ShowError("No", "error");
            }
        }
    }
}
