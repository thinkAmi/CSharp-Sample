using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;            //  INotifyDataErrorInfo, INotifyPropertyChanged用
using System.Runtime.CompilerServices;  //  CallerMemberName属性用

namespace MVVMApp
{
    class Order : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        //  INotifyDataErrorInfo向けのエラー情報
        private readonly Dictionary<string, HashSet<string>> _errors = new Dictionary<string, HashSet<string>>();


        //ViewModel用のプロパティ
        public int ID { get; set; }
        public string ItemName { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;

                if (_quantity == 0)
                {
                    UpdateErrors(errorMessage: "数量はゼロ以外です");
                }
                else
                {
                    TotalPrice = Quantity * UnitPrice;
                    RaisePropertyChanged("TotalPrice");
                    UpdateErrors();
                }

                RaisePropertyChanged();
            }
        }

        private int _unitPrice;
        public int UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;

                if (_unitPrice == 0)
                {
                    UpdateErrors(errorMessage: "単価はゼロ以外です");
                }
                else
                {
                    TotalPrice = Quantity * UnitPrice;
                    RaisePropertyChanged("TotalPrice");
                    UpdateErrors();
                }

                RaisePropertyChanged();
            }
        }

        public int TotalPrice { get; set; }



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


        //  INotifyDataErrorInfo関係
        //  INotifyDataErrorInfoインタフェースに必要なメンバ
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                return _errors.Count != 0;
            }
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || !_errors.ContainsKey(propertyName))
            {
                return null;
            }
            return _errors[propertyName];
        }


        //  その他、INotifyDataErrorInfoまわりで使われるメソッド
        protected void UpdateErrors([CallerMemberName]string propertyName = "", string errorMessage = "")
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                _errors.Remove(propertyName);
            }
            else
            {
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors[propertyName] = new HashSet<string>();
                }

                _errors[propertyName].Add(errorMessage);
            }
            RaiseErrorsChanged(propertyName);
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
