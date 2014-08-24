using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Windows.Input;             // ICommand
using System.Security.Principal;        // GenericIdentity, GenericPrincipal
using System.Threading;                 // Thread

namespace GenericPrincipalMVVM
{
    public class LoginViewModel : ViewModelBase
    {
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
            var passwordBox = x as System.Windows.Controls.PasswordBox;

            //  Passwordプロパティを推奨されていないけど、SecurePasswordプロパティはいろいろと手間なので、今回は使う
            var identity = new GenericIdentity(passwordBox.Password);
            var principal = new GenericPrincipal(identity, GetRoles(passwordBox.Password));
            Thread.CurrentPrincipal = principal;

            WindowDisplayService.ShowDialog("LoggedinViewKey", new LoggedinViewModel());
        }

        private string[] GetRoles(string name)
        {
            if (name == "Admin")
            {
                return new string[] { "Role1", "Role2" };
            }
            else
            {
                return new string[] { "Role1" };
            }
        }
    }
}
