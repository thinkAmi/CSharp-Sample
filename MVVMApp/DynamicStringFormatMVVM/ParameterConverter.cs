using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicStringFormatMVVM
{
    public class ParameterConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object isReverse, System.Globalization.CultureInfo info)
        {
            // XAMLのConverterParameterにて、符号を逆転して表示するかを指定すると、第3引数として渡される
            var format = (bool)isReverse ? "-##,#;##,#" : "##,#";
            return string.Format("{0:" + format + "}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
