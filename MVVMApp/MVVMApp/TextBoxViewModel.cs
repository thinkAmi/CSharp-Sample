using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApp
{
    class TextBoxViewModel : VMBase
    {
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment == value) return;

                _comment = value;

                if (string.IsNullOrWhiteSpace(value))
                {
                    UpdateErrors(errorMessage: "入力必須です");
                }
                else
                {
                    UpdateErrors();
                }

                RaisePropertyChanged();
            }
        }
    }
}
