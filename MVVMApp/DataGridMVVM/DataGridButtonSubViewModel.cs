using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Runtime.CompilerServices;  // CallerMemberName属性

namespace DataGridMVVM
{
    class DataGridButtonSubViewModel : ViewModelBase
    {
        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                CallPropertyChanged();
            }
        }


        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                CallPropertyChanged();
            }
        }


        private void CallPropertyChanged([CallerMemberName]string propertyName = "")
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
