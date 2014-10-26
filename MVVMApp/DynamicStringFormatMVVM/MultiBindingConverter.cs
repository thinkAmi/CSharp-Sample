using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicStringFormatMVVM
{
    public class MultiBindingConverter : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // MultiBindingを使うと、第1引数で配列として設定した順に渡される
            var isReverse = values[0] == null ? false : (bool)values[0];
            var displayValue = values[1] == null ? 0 : (int)values[1];

            var format = isReverse ? "-##,#;##,#" : "##,#";
            return string.Format("{0:" + format + "}", displayValue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
