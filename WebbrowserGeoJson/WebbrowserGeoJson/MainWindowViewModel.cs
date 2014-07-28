using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbrowserGeoJson
{
    class MainWindowViewModel : VMBase
    {
        public MainWindowViewModel()
        {
            //  外部のGeoJsonデータを読み込み、loadGeoJson()を使って表示する場合
            //Uri = String.Format("file://{0}GeoJsonLoad.html", AppDomain.CurrentDomain.BaseDirectory);

            //  JavaScript内のGeoJsonデータを読み込み、addGeoJson()を使って表示する場合
            //Uri = String.Format("file://{0}GeoJsonAddByJs.html", AppDomain.CurrentDomain.BaseDirectory);

            //  C#でGeoJsonデータを作成し、addGeoJson()を使って表示する場合
            Uri = String.Format("file://{0}GeoJsonAddByCs.html", AppDomain.CurrentDomain.BaseDirectory);


            //  C#からデータを渡すために、初期化しておく
            GoogleMap = new Map();
        }

        private string _uri;
        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                RaisePropertyChanged();
            }
        }

        //  ObjectForScriptingにデータを渡すためのプロパティ
        public Map GoogleMap { get; private set; }
    }
}
