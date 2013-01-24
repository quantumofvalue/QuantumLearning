using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
        static ItemDBContext _db;

        [BeforeFeature()]
        public static void BeforeFeature()
        {
            BrowserDriverInitialize();
            
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);                       
        }

        [BeforeScenario()]
        public void BeforeScenario()
        {
            _db = new ItemDBContext(_connectionString); 
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE Items;");
            _db.SaveChanges();
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

        [Given(@"the database contains the following items")]
        public void GivenTheDatabaseContainsTheFollowingItems(Table table)
        {
            IEnumerable<Item> items = table.CreateSet<Item>();

            foreach (Item item in items)
            {
                _db.Items.Add(item);
            }
            _db.SaveChanges();
        }

        [Then(@"I should see the following items on the page")]
        public void ThenIShouldSeeTheFollowingItemsOnThePage(Table table)
        {
            IEnumerable<Item> items = table.CreateSet<Item>();
            
            var elements = Driver.FindElements(By.XPath("//p[@class='item']"));            

            List<string> expected = items.Select(i => i.ItemText).ToList();
            List<string> actual = elements.Select(e => e.Text.Substring(2)).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Given(@"I am on the page with a couple of items")]
        public void GivenIAmOnThePageWithACoupleOfItems()
        {
            string[] header = { "ItemText" };
            string[] row1 = { "Item1" };
            string[] row2 = { "Item2" };
            var t = new Table(header);
            t.AddRow(row1);
            t.AddRow(row2);

            Given("the database contains the following items", t);
            When("I visit \"~/\""); 
            Then("I should see the following items on the page", t);                      
        }

        [Then(@"I should see the details of item ""(.*)""")]
        public void ThenIShouldSeeTheDetailsOfItem(int id)
        {
            WaitForUrl("~/Items/" + id.ToString());

            Item item = _db.Items.Find(id);

            var elements = Driver.FindElements(By.XPath("//p"));

            Assert.AreEqual(String.Format("ID: {0}",id), elements[0].Text, "ID is wrong!");
            Assert.AreEqual(String.Format("ItemText: {0}",item.ItemText), elements[1].Text, "ItemText is wrong!");
        }


    }
}
