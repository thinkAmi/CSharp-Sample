using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Windows.Input;             // ICommand
using System.Security.Principal;        // GenericIdentity, GenericPrincipal
using System.Threading;                 // Thread

namespace LoginWindowCloseMVVM
{
    class LoginDataContextChangedViewModel : ViewModelBase
    {
        public Action<bool> CloseView { get; set; }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteLoginCommand);
                }
                return _loginCommand;
            }
        }

        private void ExecuteLoginCommand(object x)
        {
            var identity = new GenericIdentity("DataContextChanged");
            var principal = new GenericPrincipal(identity, new string[] { "fuga" });
            Thread.CurrentPrincipal = principal;

            CloseView(true);
        }
    }
}
