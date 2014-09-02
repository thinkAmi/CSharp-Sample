using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DeclarativeSecurityCheckMVVM
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public App()
            : base()
        {
            //  集約エラーハンドラ
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                UnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
            };

            DispatcherUnhandledException += (s, e) =>
            {
                // e.Handledをtrueにしないとエラーハンドリングをしなかったと判断されてしまうので、trueにしとく
                e.Handled = true;

                UnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                UnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
            };
        }



        private void UnhandledException(Exception exception, string eventName, bool handled = false)
        {
            //  SecurityExceptionが飛んできたら権限エラーとみなす
            //  権限エラー以外でも発生しないか検討する必要はありそうだけど...
            if (exception is System.Security.SecurityException)
            {
                var e = (System.Security.SecurityException)exception;
                var state = e.PermissionState; // これでRoleの値がstringとして取れるけど使い道が...

                MessageBox.Show("権限がないため、使用できません");
            }
            else
            {
                MessageBox.Show("例外が発生したため、終了します");

                //  ログ取るとか

                //  続行できないと考えて、終了させる
                this.Shutdown();
            }
        }
    }
}
