using Microsoft.TeamFoundation.MVVM;
using System.Windows.Input;

namespace LocalNuGetUser
{
    public class MenuViewModel : ViewModelBase
    {
        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand(ExecuteOpenCommand);
                }
                return _openCommand;
            }
        }

        private void ExecuteOpenCommand()
        {
            // WindowsFormウィンドウを開く
            // WindowsFormを直接Show()できないので、
            // WPFのView(WindowsFormView)のWindowsFormsHostの中にWindowsFormを貼付
            //var f = new WindowsFormView();
            //f.Show();

            WindowDisplayService.Show("WindowsForm", new WindowsFormViewModel());


            // WPFウィンドウを開く
            WindowDisplayService.Show("WPF", new WPFProject.MainViewModel());


            // クラスライブラリのメソッドを使ってメッセージボックスを表示
            MessageBoxService.ShowInformation(DllProject.Class1.Say());

        }
    }
}
