using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Windows.Input;             // ICommand
using System.Collections.ObjectModel;   // ObservableCollection

namespace DataGridMVVM
{
    public class DataGridButtonMainViewModel : ViewModelBase
    {
        //  コマンド関係

        //  現在の選択行をMessageBoxで表示する
        private ICommand _showMessageCommand;
        public ICommand ShowMessageCommand
        {
            get
            {
                if (_showMessageCommand == null)
                {
                    _showMessageCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShowMessageCommand);
                }
                return _showMessageCommand;
            }
        }

        private void ExecuteShowMessageCommand()
        {
            if (CurrentRowItem == null) return;

            //  CurrentRowItemはOrder型であり、Order型でToString()メソッドをオーバーライドしてある
            MessageBoxService.Show(CurrentRowItem.ToString());
        }


        //  現在の選択行をWindowで表示する
        private ICommand _showWindowCommand;
        public ICommand ShowWindowCommand
        {
            get
            {
                if (_showWindowCommand == null)
                {
                    _showWindowCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteShowWindowCommand);
                }
                return _showWindowCommand;
            }
        }

        private void ExecuteShowWindowCommand()
        {
            var vm = new DataGridButtonSubViewModel();
            vm.ItemName = CurrentRowItem.ItemName;
            vm.Quantity = CurrentRowItem.Quantity;
            WindowDisplayService.ShowDialog("SubWindowKey", vm);
        }


        //  現在の行の数量を加算する
        private ICommand _addQuantityCommand;
        public ICommand AddQuantityCommand
        {
            get
            {
                if (_addQuantityCommand == null)
                {
                    _addQuantityCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteAddQuantityCommand);
                }
                return _addQuantityCommand;
            }
        }

        private void ExecuteAddQuantityCommand()
        {
            if (CurrentRowItem == null) return;

            CurrentRowItem.AddQuantity();
        }



        //  データバインディング関係

        private ObservableCollection<Order> _orders = new ObservableCollection<Order>
        {
            //  今回はデータベースを使わないので、適当にModelの初期値を設定
            new Order{ ItemName = "みかん", Quantity = 1, UnitPrice = 100 },
            new Order{ ItemName = "りんご", Quantity = 3, UnitPrice = 200 },
        };

        //  DataGrid
        public ObservableCollection<Order> DataGrid
        {
            get
            {
                return _orders;
            }

            set
            {
                _orders = value;
            }
        }

        //  現在の選択行
        private Order _currentRowItem;
        public Order CurrentRowItem
        {
            get { return _currentRowItem; }
            set { _currentRowItem = value; }
        }
    }
}
