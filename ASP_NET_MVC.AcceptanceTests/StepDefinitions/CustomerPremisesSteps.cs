using System;
using TechTalk.SpecFlow;

namespace ASP_NET_MVC.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CustomerPremisesSteps
    {
        [When(@"I eat (.*) cucumbers")]
        public void WhenIEatCucumbers(int p0)
        {
            System.Console.WriteLine(p0);
        }
        
        [Then(@"I should have (.*) cucumbers")]
        public void ThenIShouldHaveCucumbers(int p0)
        {
            System.Console.WriteLine(p0);
        }
    }
}
