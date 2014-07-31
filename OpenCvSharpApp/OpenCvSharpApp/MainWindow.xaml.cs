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

using System.ComponentModel;    //  BackgroundWorkerで使う
using OpenCvSharp;              //  Cv...で使う
using OpenCvSharp.Extensions;   //  WriteableBitmapConverterで使う

namespace OpenCvSharpApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();

            worker = new BackgroundWorker();

            //  ProgressChangedイベントを発生させるようにする
            worker.WorkerReportsProgress = true;

            //  RunWorkerAsyncメソッドで呼ばれるDoWorkに、
            //  別スレッドでUSBカメラの画像を取得し続けるイベントハンドラを追加
            worker.DoWork += (sender, e) =>
            {
                using (var capture = Cv.CreateCameraCapture(0))
                {
                    IplImage frame;
                    while (true)
                    {
                        frame = Cv.QueryFrame(capture);

                        //  新しい画像を取得したので、
                        //  ReportProgressメソッドを使って、ProgressChangedイベントを発生させる
                        worker.ReportProgress(0, frame);
                    }
                }
            };

            //  ReportProgressメソッドで呼ばれるProgressChangedのイベントハンドラを追加
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
        }


        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //  frameがe.UserStateプロパティにセットされて渡されてくる
            var image = (IplImage)e.UserState;


            //  Sourceプロパティにセットするため、frameをWriteableBitmapへと変換(Bitmapだと型変換エラー)
            //  WriteableBitmapConverterを使うには、
            //  usingディレクティブにOpenCvSharp.Extensionsを追加
            //  (OpenCvSharp.UserInterface.dll内)
            Monitor.Source = WriteableBitmapConverter.ToWriteableBitmap(image);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  DoWorkイベントハンドラの実行を開始
            worker.RunWorkerAsync();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  WriteableBitmapを渡しているので、その型へと戻す
            var image = (WriteableBitmap)Monitor.Source;

            //  Bitmap以外にも出力できるけれど、今回はBitmapにしておく
            //  また、ファイルは上書きで保存する
            using (var fs = new System.IO.FileStream("hoge.bmp", System.IO.FileMode.Create))
            {
                //  BmpBitmapEncoderの他に、PngBitmapEncoderとかもある
                var enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(image));
                enc.Save(fs);

                MessageBox.Show("保存しました");
            }
        }
    }
}
