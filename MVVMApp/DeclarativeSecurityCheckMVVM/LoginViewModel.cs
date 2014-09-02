using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Windows.Input;             // ICommand
using System.Security.Principal;        // GenericIdentity, GenericPrincipal
using System.Threading;                 // Thread
using System.Runtime.CompilerServices;  // CallerMemberName属性

namespace DeclarativeSecurityCheckMVVM
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
            //  ComboBoxの確認のためのコードなので、このへんは本来不要
            var cb = x as System.Windows.Controls.ComboBox;
            var v1 = cb.SelectedValue;  //  直接入力するとSelectedValue/SelectedItemには値が入ってこない
            var v2 = cb.SelectedItem;   //  同上
            var v3 = cb.Text;           //  Textには入ってくる
            var v4 = UserName;          //  データバインディングしているプロパティにも同じ値が入ってくる

            var identity = new GenericIdentity(UserName);
            var principal = new GenericPrincipal(identity, GetRoles(UserName));
            Thread.CurrentPrincipal = principal;

            WindowDisplayService.ShowDialog("LoggedinViewKey", new LoggedinViewModel());
        }


        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private List<string> _comboBoxSource;
        public List<string> ComboBoxSource
        {
            get
            {
                if (_comboBoxSource == null)
                {
                    _comboBoxSource = new List<string>() { "Admin", "Guest" };
                }
                return _comboBoxSource;
            }
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


        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }
    }
}
