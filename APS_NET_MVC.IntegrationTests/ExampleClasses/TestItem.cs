using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace APS_NET_MVC.IntegrationTests
{
    public class TestItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }

    public class TestItemDBContext : DbContext
    {
        public DbSet<TestItem> TestItems { get; set; }

        public TestItemDBContext()
        {
        }

        public TestItemDBContext(string connectionString)
            : base(connectionString)
        {
        }
    } 
}
