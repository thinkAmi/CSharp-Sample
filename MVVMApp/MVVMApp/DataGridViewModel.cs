using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;   // ObservableCollection用

namespace MVVMApp
{
    class DataGridViewModel : VMBase
    {
        private ObservableCollection<Order> _orders = new ObservableCollection<Order>
        {
            //  今回はデータベースを使わないので、適当にModelの初期値を設定
            new Order{ ID = 1, ItemName = "みかん", Quantity = 1, UnitPrice = 100 },
            new Order{ ID = 2, ItemName = "りんご", Quantity = 3, UnitPrice = 200 },
        };


        //  データバインド用
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
    }
}
