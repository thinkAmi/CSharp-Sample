
using System.Windows.Input;				// ICommand
using Microsoft.TeamFoundation.MVVM;    // ViewModelBase

namespace SerialPortReceiver
{
    public partial class SerialPortViewModel : ViewModelBase
    {
        private ICommand _StartCommand;
        public ICommand StartCommand
        {
            get
            {
                if (_StartCommand == null)
                {
                    _StartCommand = new RelayCommand(ExecuteStartCommand, CanExecuteStartCommand);
                }
                return _StartCommand;
            }
        }

        private ICommand _StopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_StopCommand == null)
                {
                    _StopCommand = new RelayCommand(ExecuteStopCommand, CanExecuteStopCommand);
                }
                return _StopCommand;
            }
        }

    }
}