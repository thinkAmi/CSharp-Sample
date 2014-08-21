using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;            // INotifyPropertyChanged
using System.Runtime.CompilerServices;  // CallerMemberName属性

namespace DataGridMVVM
{
    public class Order : INotifyPropertyChanged
    {
        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged();
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged();

                TotalPrice = UnitPrice * Quantity;
            }
        }

        private int _unitPrice;
        public int UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                RaisePropertyChanged();

                TotalPrice = UnitPrice * Quantity;
            }
        }

        private int _totalPrice;
        public int TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                RaisePropertyChanged();
            }
        }


        //  「数量加算」ボタンで呼ばれる実際のメソッド
        public void AddQuantity()
        {
            Quantity++;
        }



        //  INotifyPropertyChanged関係
        //  INotifyPropertyChangedインタフェースに必要なメンバ
        public event PropertyChangedEventHandler PropertyChanged;

        //  その他、INotifyPropertyChanged向けのメソッドなど
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public override string ToString()
        {
            var a = new[] { ItemName, Quantity.ToString(), UnitPrice.ToString(), TotalPrice.ToString() };
            return string.Join(":", a);
        }
    }
}
