namespace DapperApp.Properties {
    
    
    // このクラスでは設定クラスでの特定のイベントを処理することができます:
    //  SettingChanging イベントは、設定値が変更される前に発生します。
    //  PropertyChanged イベントは、設定値が変更された後に発生します。
    //  SettingsLoaded イベントは、設定値が読み込まれた後に発生します。
    //  SettingsSaving イベントは、設定値が保存される前に発生します。
    internal sealed partial class Settings {
        
        public Settings() {
            // // 設定の保存と変更のイベント ハンドラーを追加するには、以下の行のコメントを解除します:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //

            //---------------------------------------------------------------------------
            //  【追加】自前のコネクションに切り替える
            //---------------------------------------------------------------------------
            //  　このサンプルでは、サーバーエクスプローラーでポトペタした時、
            //  Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\127.0.0.1\db\sample.accdb
            //  という、本来の接続文字列とは異なるようにしてある
            //  　TableAdapterは内部接続文字列(sampleConnectionString)を持っているため、
            //   使用時に接続文字列を切り替えるのは手間がかかる
            //  -> この場所で、本来の接続文字列へと切り替える
            this["sampleConnectionString"] = new Connection().ConnectionString;
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // SettingChangingEvent イベントを処理するコードをここに追加してください。
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // SettingsSaving イベントを処理するコードをここに追加してください。
        }
    }
}
