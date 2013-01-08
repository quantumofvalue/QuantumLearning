using System;
using System.Threading;
using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace ASP_NET_MVC.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class TestFeatureSteps : BaseIntegrationTest
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

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            OpenAbsolute("http://www.google.com");
            Thread.Sleep(5000);
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
