using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessageBoxMVVM
{
    /// <summary>
    /// DataContextChangedView.xaml の相互作用ロジック
    /// </summary>
    public partial class DataContextChangedView : Window
    {
        public DataContextChangedView()
        {
            DataContextChanged += (sender, e) =>
            {
                var x = DataContext as DataContextChangedViewModel;
                if (x == null) throw new InvalidCastException();

                x.ShowInformationMessageBox = (message) => MessageBox.Show(message, "info", MessageBoxButton.OK, MessageBoxImage.Information);
                x.ShowErrorMessageBox = (message) => MessageBox.Show(message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                x.ShowYesNoMessageBox = (message, title, button) => MessageBox.Show(message, title, button);
            };

            InitializeComponent();
        }
    }
}
