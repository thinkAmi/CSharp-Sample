using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;   //  ComVisible属性で使う
using System.ComponentModel;            //  INotifyPropertyChangedインタフェースで使う
using Codeplex.Data;                    //  DynamicJSONで使う
using System.Runtime.CompilerServices;  //  CallerMemberName属性で使う

namespace WebbrowserGeoJson
{
    [ComVisible(true)]
    public class Map : INotifyPropertyChanged
    {
        string _geoJson;
        public string GeoJson
        {
            get
            {
                if (!string.IsNullOrEmpty(_geoJson)) return _geoJson;

                var j = new
                {
                    type = "FeatureCollection",
                    features = new[] {
                        new {
                            type = "Feature", 
                            property = new {},
                            geometry = new {
                                type = "Point",
                                coordinates = new [] { 137.883, -28}
                            }
                        }
                    }
                };
                //  DynamicJSONでJSON文字列化
                return DynamicJson.Serialize(j);
            }
            set
            {
                this._geoJson = value;
                RaisePropertyChanged();
            }
        }


        //  INotifyPropertyChangedインタフェースの実装
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
