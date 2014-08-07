using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input; // ICommand用

namespace MessageBox
{
    public class DataContextChangedViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
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


        public Action<string> ShowInformationMessageBox { get; set; }
        public Action<string> ShowErrorMessageBox { get; set; }
        public Func<string, string, System.Windows.MessageBoxButton, System.Windows.MessageBoxResult> ShowYesNoMessageBox { get; set; }


        private void ExecuteShowCommand(object x)
        {
            var r = ShowYesNoMessageBox("Yes or No", "title", System.Windows.MessageBoxButton.YesNo);
            if (r == System.Windows.MessageBoxResult.Yes)
            {
                ShowInformationMessageBox("Yes");
            }
            else
            {
                ShowErrorMessageBox("No");
            }
        }
    }
}
