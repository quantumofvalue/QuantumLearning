using System;
using System.Threading;
using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;

using ASP_NET_MVC.Models;
using System.Configuration;
using System.IO;

namespace ASP_NET_MVC.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class WalkingSkeletonSteps : BaseIntegrationTest
    {
        [BeforeFeature()]
        public static void BeforeFeature()
        {
            BrowserDriverInitialize();
            
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);

            string PATH = Environment.GetEnvironmentVariable("JYOTHI");

            System.Console.WriteLine("PATH:"+PATH);

            if (PATH!=null && 0 == PATH.Length)
            {
                System.Console.WriteLine("PATH is an empty string");
            }
            else if (PATH == null)
            {
                System.Console.WriteLine("PATH is NOT an empty string - it is null!");
            }

            


            ItemDBContext db = new ItemDBContext(_connectionString);
            //Item item = db.Items.Find(2);
            //db.Items.SqlQuery("DELETE * FROM Items");
            //db.Database.SqlQuery("DELETE FROM Items;");
            //db.Database.ExecuteSqlCommand("DELETE FROM Items;");
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE Items;");
            //Item item = new Item { ItemText = "Super Test Item Text" };

            //db.Items.Add(item);
            //foreach (Item item in db.Items)
            //{
            //    db.Items.Remove(item);
            //}
            //db.Items.Remove(item);
            db.SaveChanges();
        }

        [AfterFeature()]
        public static void AfterFeature()
        {
            BrowserDriverCleanup();
        }

        [When(@"I visit ""(.*)""")]
        public void WhenIVisit(string url)
        {
            //OpenAbsolute("http://www.google.com");
            //Thread.Sleep(5000);
            Open(url);
        }
        
        [When(@"I enter ""(.*)"" into the ""(.*)"" field")]
        public void WhenIEnterIntoTheField(string fieldText, string fieldId)
        {
            Type(fieldId, fieldText);
        }
        
        [When(@"I click ""(.*)""")]
        public void WhenIClick(string buttonId)
        {
            Click(buttonId);
        }
        
        [Then(@"I should be redirected to ""(.*)""")]
        public void ThenIShouldBeRedirectedTo(string url)
        {
            WaitForUrl(url);
        }
        
        [Then(@"I should see ""(.*)"" on the page")]
        public void ThenIShouldSeeOnThePage(string textToBeFound)
        {
            AssertTextContains(By.TagName("p"), textToBeFound);
        }
    }
}
