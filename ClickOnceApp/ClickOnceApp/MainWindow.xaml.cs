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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClickOnceApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  アップデートの強制
            UpdateForcedly();

            //  データファイルにあるテキストファイルデータを読み込む
            var dataFile = ReadFile(System.Deployment.Application.ApplicationDeployment.CurrentDeployment.DataDirectory.ToString(), @"files\hoge.txt");

            //  ClickOnceキャッシュ領域にあるファイルデータを読み込む
            var cacheFile = ReadFile(System.AppDomain.CurrentDomain.BaseDirectory, @"files\fuga.txt");

            //  データバインド
            this.DataContext = new
            {
                VersionText = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(),
                DataFile = dataFile,
                CacheFile = cacheFile
            };
        }


        private string ReadFile(string dir, string filename)
        {
            var result = "";
            var path = System.IO.Path.Combine(dir, filename);

            using (var sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("Shift_JIS")))
            {
                result += sr.ReadToEnd();
            }

            return result;
        }


        private void UpdateForcedly()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                var deploy = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                if (deploy.CheckForUpdate())
                {
                    deploy.Update();

                    System.Windows.Forms.Application.Restart();
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }
    }
}
