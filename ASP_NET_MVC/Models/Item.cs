using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ASP_NET_MVC.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string ItemText { get; set; }
    }

    public class ItemDBContext : DbContext
    {
        public ItemDBContext()
        {
        }

        public ItemDBContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Item> Items { get; set; }
    } 
}
