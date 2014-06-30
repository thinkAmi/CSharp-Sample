using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerCompact4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ItemCatalog())
            {
                var member = new Member
                {
                    Name = "thinkAmi",
                    Address = "SQLServerCompact4"
                };
                db.Members.Add(member);


                var item = new Item
                {
                    Name = "シナノドルチェ",
                    Member = member
                };
                db.Items.Add(item);

                int recordAffected = db.SaveChanges();


                db.Items.Select(i => i).ToList().ForEach(row => Console.WriteLine("Id: {0}, Name: {1}, Member Name: {2}, Address: {3}", row.Id, row.Name, row.Member.Name, row.Member.Address));
                Console.ReadKey();
            }
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> SoldAt { get; set; }
        public virtual Member Member { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
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
