using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Threading;

namespace LoginWindowClose
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            // XAMLに書いたEventTrigerを使用する方法(コードビハインド無し)
            Startup += (s, e) => Login(new LoginEventTrigerView());

            // コードビハインドのDataContextChangedイベントを使用する方法
            //Startup += (s, e) => Login(new LoginDataContextChangedView());
        }


        private void Login(System.Windows.Window loginView)
        {
            //  ログイン画面が閉じられた時にアプリケーションが終了しないよう、OnExplicitShutdownを設定しておく
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var isLoggedin = loginView.ShowDialog() ?? false;
            var isAuthenticated = System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated;

            if (isLoggedin && isAuthenticated)
            {
                //  MainWindowが閉じられた時にアプリケーションが終了するように変更
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                var vm = new LoggedinViewModel();
                vm.UserName = Thread.CurrentPrincipal.Identity.Name;

                var loggedinView = new LoggedinView();
                loggedinView.DataContext = vm;

                Current.MainWindow = loggedinView;
                loggedinView.ShowDialog();
            }
            else
            {
                //  OnExplicitShutdownの場合、明示的なShutdown()呼び出しが必要
                Current.Shutdown(-1);
            }
        }
    }
}
