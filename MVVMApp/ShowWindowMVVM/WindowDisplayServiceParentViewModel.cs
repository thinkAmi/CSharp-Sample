using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input; // ICommand用

namespace ShowWindowMVVM
{
    public class WindowDisplayServiceParentViewModel : Microsoft.TeamFoundation.MVVM.ViewModelBase
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


        private ICommand _showErrorDialogCommand;
        public ICommand ShowErrorDialogCommand
        {
            get
            {
                if (_showErrorDialogCommand == null)
                {
                    _showErrorDialogCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShowErrorDialogCommand);
                }
                return _showErrorDialogCommand;
            }
        }


        private void ExecuteShowDialogCommand(object x)
        {
            //  XAMLで指定した子ViewのリソースのKeyと、子ViewModelのオブジェクトを渡す
            //  ちなみに、関係するXAMLの部分：
            //  <Window.Resources>
            //        <mvvm:RegisterWindow x:Key="ChildWindowKey" Type="local:WindowDisplayServiceChildView" />
            //  </Window.Resources>
            WindowDisplayService.ShowDialog("ChildWindowKey", new WindowDisplayServiceChildViewModel());
        }


        private void ExecuteShowErrorDialogCommand(object x)
        {
            //  この方法だとエラー発生
            WindowDisplayServiceErrorViewModel.ShowDialog();
        }
    }
}
