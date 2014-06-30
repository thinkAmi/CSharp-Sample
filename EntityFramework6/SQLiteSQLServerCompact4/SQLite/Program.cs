using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteDDL();

            using (var db = new ItemCatalog())
            {
                var member = new Member
                {
                    Name = "thinkAmi",
                    Address = "SQLite"
                };
                db.Members.Add(member);

                var item = new Item
                {
                    Name = "シナノゴールド",
                    Member = member
                };
                db.Items.Add(item);

                //  これを入れるとエラーになる
                //var item2 = new Item
                //{
                //    Name = "hoge",
                //    MemberId = 3
                //};
                //db.Items.Add(item2);


                int recordAffected = db.SaveChanges();


                db.Items.Select(i => i).ToList().ForEach(row => Console.WriteLine("Id: {0}, Name: {1}, Member Name: {2}, Address: {3}", row.Id, row.Name, row.Member.Name, row.Member.Address));
                Console.ReadKey();
            }
        }

        static void ExecuteDDL()
        {
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "sample.sqlite");
            System.Data.SQLite.SQLiteConnection.CreateFile(path);

            var cnStr = new System.Data.SQLite.SQLiteConnectionStringBuilder() { DataSource = path };

            using (var cn = new System.Data.SQLite.SQLiteConnection(cnStr.ToString()))
            {
                cn.Open();

                //  テーブル名は複数形で指定する(Memberではなく、Members)
                var sql = "CREATE TABLE Members (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Address TEXT, TelNo TEXT); ";
                sql += "CREATE TABLE Items (Id INTEGER PRIMARY KEY AUTOINCREMENT, Price INTEGER, MemberId INTEGER, Name TEXT, SoldAt datetime, FOREIGN KEY(MemberId) REFERENCES Members(Id))";

                var cmd = new System.Data.SQLite.SQLiteCommand(sql, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }
        }
    }

    public class Item
    {
        //  SQLiteの場合、INTEGER型のプライマリーキーは64bitになることから、C#でint(32bit)型にすると、
        //  「The type of the key field 'Id' is expected to be 'System.Int32', but the value provided is actually of type 'System.Int64'.」
        //  というエラーが出るため、long型を使う
        public long Id { get; set; }
        public int Price { get; set; }
        //  Member.Idはlong型であるため、外部キー制約があるMemberIdもlong型にしないとエラーとなる
        public long MemberId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> SoldAt { get; set; }
        public virtual Member Member { get; set; }
    }

    public class Member
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }

    public class ItemCatalog : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<Item> Items { get; set; }
        public System.Data.Entity.DbSet<Member> Members { get; set; }
    }
}
