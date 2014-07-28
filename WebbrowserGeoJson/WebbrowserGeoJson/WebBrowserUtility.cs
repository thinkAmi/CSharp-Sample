using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;           //  Dependency...やUIPropertyMetadataで使う
using System.Windows.Controls;  //  WebBrowserで使う


namespace WebbrowserGeoJson
{
    public static class WebBrowserUtility
    {
        //  WebBrowser.Sourceプロパティへのデータバインディングを可能にするための依存プロパティ
        public static readonly DependencyProperty SourceProperty 
            = DependencyProperty.RegisterAttached("Source", 
                                                  typeof(string), 
                                                  typeof(WebBrowserUtility),
                                                  new UIPropertyMetadata(null, SourcePropertyChanged));

        public static string GetSource(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceProperty);
        }

        public static void SetSource(DependencyObject obj, string value)
        {
            obj.SetValue(SourceProperty, value);
        }

        public static void SourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            //  WebBrowser.Sourceプロパティへのデータバインディング内容を設定
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = string.IsNullOrEmpty(uri) ? null : new Uri(uri);
            }
        }


        //  WebBrowser.ObjectForScriptingプロパティへのデータバインディングを可能にするための依存プロパティ
        public static readonly DependencyProperty ObjectForScriptingProperty
            = DependencyProperty.RegisterAttached("ObjectForScripting", 
                                                  typeof(object), 
                                                  typeof(WebBrowserUtility), 
                                                  new UIPropertyMetadata(OnObjectForScriptingPropertyChanged));

        public static string GetObjectForScripting(DependencyObject obj)
        {
            return (string)obj.GetValue(ObjectForScriptingProperty);
        }

        public static void SetObjectForScripting(DependencyObject obj, string value)
        {
            obj.SetValue(ObjectForScriptingProperty, value);
        }

        public static void OnObjectForScriptingPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                browser.ObjectForScripting = e.NewValue;
            }
        }
    }
}
