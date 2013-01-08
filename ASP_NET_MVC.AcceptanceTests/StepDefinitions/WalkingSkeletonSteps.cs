using System;
using System.Threading;
using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace ASP_NET_MVC.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class WalkingSkeletonSteps : BaseIntegrationTest
    {
        [BeforeFeature()]
        public static void BeforeFeature()
        {
            BrowserDriverInitialize();
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
