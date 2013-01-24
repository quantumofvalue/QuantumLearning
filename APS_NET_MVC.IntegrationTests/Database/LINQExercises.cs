using System;
using System.IO;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APS_NET_MVC.IntegrationTests
{
    [TestClass]
    public class LINQExercises
    {
        [TestMethod]
        public void AddingItemsToDatabase()
        {
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);

            TestItemDBContext db = new TestItemDBContext();

            TestItem item = new TestItem { Text = "Example Text" };
            db.TestItems.Add(item);
            db.SaveChanges();
        }
    }
}
