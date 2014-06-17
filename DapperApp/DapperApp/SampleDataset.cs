namespace DapperApp {
    
    
    public partial class SampleDataset {
    }
}

namespace DapperApp.SampleDatasetTableAdapters {


    /// <summary>
    /// ItemTableのID列(オートナンバー型)をINSERT時に取得するためのPartialClass
    /// </summary>
    public partial class ItemTableAdapter {

        /// <summary>
        /// イベントで呼ばれる実際の処理：@@IDENTITYでオートナンバー型の列を取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Adapter_RowUpdated(object sender, System.Data.OleDb.OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == System.Data.StatementType.Insert
                && e.Status == System.Data.UpdateStatus.Continue)
            {
                var cmd = new System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY", null);
                cmd.Connection = e.Command.Connection;
                cmd.Transaction = e.Command.Transaction;

                var result = cmd.ExecuteScalar();

                if (result != null
                    && result.GetType() != typeof(System.DBNull))
                {
                    e.Row["ID"] = (int)result;
                    e.Row.AcceptChanges();
                }
            }
        }


        //  本当はSampleDatasetTableAdaptersのコンストラクタで設定したいが、
        //  デザイナが自動生成しているため設定できない
        //  -> ItemTableAdapterを生成した後にこのメソッドを呼んでイベントを紐付ける
        public void AddRowUpdatedEvent()
        {
            this.Adapter.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(Adapter_RowUpdated);

            //  上の代わりに、多くの例のように、'_adapter.RowUpdated'だと、NullReferenceExceptionが発生するので注意
            //this._adapter.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(Adapter_RowUpdated);

        }
    }
}
