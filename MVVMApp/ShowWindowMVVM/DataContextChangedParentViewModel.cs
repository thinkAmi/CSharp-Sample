using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input; // ICommand用

namespace ShowWindowMVVM
{
    public class DataContextChangedParentViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
    {
        private ICommand _showDialogCommand;
        public ICommand ShowDialogCommand
        {
            get
            {
                if (_showDialogCommand == null)
                {
                    _showDialogCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShowDialogCommand);
                }
                return _showDialogCommand;
            }
        }


        private void ExecuteShowDialogCommand(object x)
        {
            var r = DataContextChangedChildViewModel.ShowDialog();
        }
    }
}
