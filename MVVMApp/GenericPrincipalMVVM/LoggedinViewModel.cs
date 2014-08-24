using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Windows.Input;             // ICommand
using System.Threading;                 // Thread

namespace GenericPrincipalMVVM
{
    public class LoggedinViewModel : ViewModelBase
    {
        //  ログインしたユーザー名
        private string _loggedinUser;
        public string LoggedinUser
        {
            get
            {
                if (_loggedinUser == null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    //  認証されていたらログインしたユーザー名を表示
                    _loggedinUser = Thread.CurrentPrincipal.Identity.Name;
                }

                return _loggedinUser;
            }
        }



        private ICommand _allCommand;
        public ICommand AllCommand
        {
            get
            {
                if (_allCommand == null)
                {
                    _allCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteAllCommand, CanExecuteAllCommand);
                }
                return _allCommand;
            }
        }

        private void ExecuteAllCommand(object x) { }

        private bool CanExecuteAllCommand(object x)
        {
            //  認証されて`Role1`に属していたらtrue
            return Thread.CurrentPrincipal.Identity.IsAuthenticated && Thread.CurrentPrincipal.IsInRole("Role1");
        }



        private ICommand _limitCommand;
        public ICommand LimitCommand
        {
            get
            {
                if (_limitCommand == null)
                {
                    _limitCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteLimitCommand, CanExecuteLimitCommand);
                }
                return _limitCommand;
            }
        }

        private void ExecuteLimitCommand(object x) { }

        private bool CanExecuteLimitCommand(object x)
        {
            //  認証されて`Role2`に属していたらtrue
            return Thread.CurrentPrincipal.Identity.IsAuthenticated && Thread.CurrentPrincipal.IsInRole("Role2");
        }



        public System.Windows.Visibility VisibilityAll
        {
            get
            {
                //  認証されて`Role1`に属していたらVisible, そうでなければHidden
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated && Thread.CurrentPrincipal.IsInRole("Role1"))
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }

        public System.Windows.Visibility VisibilityLimit
        {
            get
            {
                //  認証されて`Role2`に属していたらVisible, そうでなければHidden
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated && Thread.CurrentPrincipal.IsInRole("Role2"))
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }
    }
}
