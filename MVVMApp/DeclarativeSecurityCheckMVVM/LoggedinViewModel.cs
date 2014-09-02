using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Security.Permissions;      // PrincipalPermission属性
using System.Threading;                 // Thread

namespace DeclarativeSecurityCheckMVVM
{
    public partial class LoggedinViewModel : ViewModelBase
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Role2")]
        private void ExecuteShowCommand(object x)
        {
            MessageBoxService.ShowInformation("権限があります");
        }

        private void ExecuteExceptionCommand(object x)
        {
            throw new System.NotImplementedException();
        }
    }
}
