using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;
using System.Windows.Input;

namespace SharedViewMVVM
{
    public class MenuViewModel : ViewModelBase
    {
        private ICommand _receivingCommand;
        public ICommand ReceivingCommand
        {
            get
            {
                if (_receivingCommand == null)
                {
                    _receivingCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteReceivingCommand);
                }
                return _receivingCommand;
            }
        }

        private ICommand _shipingCommand;
        public ICommand ShipingCommand
        {
            get
            {
                if (_shipingCommand == null)
                {
                    _shipingCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShippingCommand);
                }
                return _shipingCommand;
            }
        }

        private void ExecuteReceivingCommand(object x)
        {
            WindowDisplayService.ShowDialog("Receiving", new ReceivingViewModel());
        }

        private void ExecuteShippingCommand(object x)
        {
            WindowDisplayService.ShowDialog("Shiping", new ShipingViewModel());
        }
    }
}
