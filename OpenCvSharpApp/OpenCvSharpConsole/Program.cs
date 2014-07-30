using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace OpenCvSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //  CreateCameraCaptureの引数はカメラのIndex(通常は0から始まる)
            using (var capture = Cv.CreateCameraCapture(0))
            {
                IplImage frame = new IplImage();

                //  W320 x H240のウィンドウを作る
                double w = 320, h = 240;
                Cv.SetCaptureProperty(capture, CaptureProperty.FrameWidth, w);
                Cv.SetCaptureProperty(capture, CaptureProperty.FrameHeight, h);

                //  何かキーを押すまでは、Webカメラの画像を表示し続ける
                while (Cv.WaitKey(1) == -1)
                {
                    //  カメラからフレームを取得
                    frame = Cv.QueryFrame(capture);

                    //  Window「Capture」を作って、Webカメラの画像を表示
                    Cv.ShowImage("Capture", frame);
                }

                //  bmp以外に、jpegやpngでの保存が可能
                frame.SaveImage("result.bmp");

                //  使い終わったWindow「Capture」を破棄
                Cv.DestroyWindow("Capture");
            }
        }
    }
}
