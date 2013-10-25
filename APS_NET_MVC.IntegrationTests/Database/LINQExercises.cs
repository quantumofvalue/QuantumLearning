using System;
using System.IO;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ASP_NET_MVC.Models;

namespace APS_NET_MVC.IntegrationTests
{
    [TestClass]
    public class LINQExercises
    {
        [TestMethod]
        public void AddingItemsToTestDatabase()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(new string[] {
                System.Environment.GetEnvironmentVariable("QUANTUM"),
                ConfigurationManager.AppSettings["DataDirectory"] }));          

            TestItemDBContext db = new TestItemDBContext();

            TestItem item = new TestItem { Text = "Example Text" };
            db.TestItems.Add(item);
            db.SaveChanges();
        }

        [TestMethod]
        public void AddingItemsToDatabase()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(new string[] {
                System.Environment.GetEnvironmentVariable("QUANTUM"),
                ConfigurationManager.AppSettings["DataDirectory"] }));

            ItemDBContext db = new ItemDBContext();

            Item item = new Item { ItemText = "Example Item Text" };
            db.Items.Add(item);
            db.SaveChanges();
        }
    }
}
