using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;           //  忘れずに追加しておく

namespace DapperApp
{
    class Program
    {
        const string SQLSelect = @"SELECT * FROM Item WHERE ItemName = @ItemName";
        const string SelectParameter = "りんご";

        static void Main(string[] args)
        {
            using (var cn = new System.Data.OleDb.OleDbConnection(new Connection().ConnectionString))
            {
                cn.Open();

                //  手抜き：第一引数に以下の文字列を渡すことで処理分岐
                switch (args.FirstOrDefault().ToLower())
                {
                    case "select":
                        SelectAll(cn);
                        SelectWithoutClass(cn);
                        SelectWithClass(cn);
                        break;

                    case "insert":
                        Insert(cn);
                        SelectAll(cn);
                        break;

                    case "update":
                        Update(cn);
                        SelectAll(cn);
                        break;

                    case "commit":
                        RunTransaction(cn, hasError: false);
                        SelectAll(cn);
                        break;

                    case "rollback":
                        RunTransaction(cn, hasError: true);
                        SelectAll(cn);
                        break;

                    default:
                        break;
                }

                cn.Close();
            }
        }


        static void SelectAll(System.Data.OleDb.OleDbConnection cn)
        {
            var sql = @"SELECT * FROM Item";
            var messages = cn.Query<Item>(sql).Select(a => a.ID.ToString() + ":" + a.ItemName);
            Console.WriteLine("------------------------------------");
            Console.WriteLine(string.Join(Environment.NewLine, messages));
            Console.WriteLine("------------------------------------");
        }


        static void SelectWithoutClass(System.Data.OleDb.OleDbConnection cn)
        {
            var results = cn.Query(SQLSelect, new { ItemName = SelectParameter });

            var messages = results.Select(a => a.ID.ToString() + ":" + a.ItemName);
            Console.WriteLine(string.Join(Environment.NewLine, messages));
        }

        static void SelectWithClass(System.Data.OleDb.OleDbConnection cn)
        {
            //  この第二引数にはインテリセンスはきかない
            var results = cn.Query<Item>(SQLSelect, new { ItemName = SelectParameter });

            //  ここでインテリセンスがきく
            var messages = results.Select(a => a.ID.ToString() + ":" + a.ItemName);
            Console.WriteLine(string.Join(Environment.NewLine, messages));
        }


        static void Insert(System.Data.OleDb.OleDbConnection cn, System.Data.IDbTransaction tr = null)
        {
            var sql = @"INSERT INTO Item(ItemName) VALUES(@ItemName)";
            var count = cn.Execute(sql, new { ItemName = DateTime.Now.Millisecond.ToString() }, transaction: tr);

            Console.WriteLine(count.ToString());
        }


        static void Update(System.Data.OleDb.OleDbConnection cn, System.Data.IDbTransaction tr = null)
        {
            var sql = @"UPDATE Item SET ItemName = 'シナノゴールド' WHERE ID = @Id";
            var count = cn.Execute(sql, new { Id = 1 }, transaction: tr);

            Console.WriteLine(count.ToString());
        }


        static void RunTransaction(System.Data.OleDb.OleDbConnection cn, bool hasError)
        {
            using (var tr = cn.BeginTransaction())
            {
                try
                {
                    Insert(cn, tr);

                    if (hasError)
                    {
                        throw new Exception();
                    }

                    Update(cn, tr);

                    tr.Commit();
                    Console.WriteLine("Commit");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    tr.Rollback();
                    Console.WriteLine("Rollback");
                }

                Console.WriteLine("End Transaction");
            }
        }
    }


    public class Connection
    {
        public string ConnectionString { get; private set; }
        public Connection()
        {
            //  接続文字列はSettings.settingsに設定したものを使える(Properties.Settings.Default)が、今回は自前で文字列を渡してみた
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "sample.accdb");
            ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
        }
    }

    public class Item
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
    }
}
