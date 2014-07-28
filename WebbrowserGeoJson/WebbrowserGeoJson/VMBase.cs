using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    //  ViewModelBaseで使う
using System.ComponentModel;            //  INotifyDataErrorInfoやDataErrorsChangedEventArgsで使う
using System.Runtime.CompilerServices;  //  CallerMemberName属性で使う

namespace WebbrowserGeoJson
{
    class VMBase : ViewModelBase, INotifyDataErrorInfo
    {
        //  List<T>を使った上でエラーメッセージの重複をチェックするくらいなら、
        //  HashSet<T>を使えばエラーメッセージの重複は発生せず、パフォーマンス的にも良い
        //  See: http://theburningmonk.com/2011/03/hashset-vs-list-vs-dictionary/
        private readonly Dictionary<string, HashSet<string>> _errors = new Dictionary<string, HashSet<string>>();


        //  RaisePropertyChangedメソッドをオーバーライドして、
        //  CallerMemberName属性を使って呼び出し元のプロパティ名を取得すると、
        //  呼び出し元で対象のプロパティ名を文字列で設定する必要がない
        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }


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

            //  忘れずにエラー情報の変更を通知
            RaiseErrorsChanged(propertyName);
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }



        //  INotifyDataErrorInfoインタフェースの実装
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return _errors.Count != 0; }
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || !_errors.ContainsKey(propertyName))
            {
                return null;
            }

            return _errors[propertyName];
        }
    }
}
