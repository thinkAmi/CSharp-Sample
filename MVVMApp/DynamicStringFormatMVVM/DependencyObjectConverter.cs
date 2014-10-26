using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;       // DependencyObjectまわり
using System.Windows.Data;  // IValueConverter

namespace DynamicStringFormatMVVM
{
    public class DependencyObjectConverter : DependencyObject, IValueConverter
    {
        public bool IsReverse
        {
            get
            {
                return (bool)GetValue(IsReverseProperty);
            }
            set
            {
                SetValue(IsReverseProperty, value);
            }
        }

        public static readonly DependencyProperty IsReverseProperty
            = DependencyProperty.Register("IsReverse", typeof(bool), typeof(DependencyObjectConverter));


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var format = IsReverse ? "-##,#;##,#" : "##,#";
            return string.Format("{0:" + format + "}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
