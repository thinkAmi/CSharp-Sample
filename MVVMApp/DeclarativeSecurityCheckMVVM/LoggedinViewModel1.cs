
using System.Windows.Input;				// ICommand
using Microsoft.TeamFoundation.MVVM;    // ViewModelBase

namespace DeclarativeSecurityCheckMVVM
{
    public partial class LoggedinViewModel : ViewModelBase
    {
        private ICommand _ShowCommand;
        public ICommand ShowCommand
        {
            get
            {
                if (_ShowCommand == null)
                {
                    _ShowCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShowCommand);
                }
                return _ShowCommand;
            }
        }
        private ICommand _ExceptionCommand;
        public ICommand ExceptionCommand
        {
            get
            {
                if (_ExceptionCommand == null)
                {
                    _ExceptionCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteExceptionCommand);
                }
                return _ExceptionCommand;
            }
        }
    }
}