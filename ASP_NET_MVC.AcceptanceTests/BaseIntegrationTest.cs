using System;
using System.Configuration;
using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


public class BaseIntegrationTest : Steps
{
    public static string BaseUrl = GetBaseURL();

    // specify if our web-app uses a virtual path   
    private const string VirtualPath = "";

    private const int TimeOut = 30;
    private static int _testClassesRunning;

    private static IWebDriver StaticDriver = CreateDriverInstance();

    private static readonly string _environment = GetCurrentEnvironment();
    protected static readonly string _connectionString = GetConnectionString();

    private static string GetCurrentEnvironment()
    {
        string environment = "DEVELOPMENT";
        if (null != System.Environment.GetEnvironmentVariable("EFM_ENV"))
        {
            environment = "TEST";
        }
        return environment;
    }

    private static string GetConnectionString()
    {
        if ("DEVELOPMENT" == GetCurrentEnvironment())
        {
            return ConfigurationManager.AppSettings["DevelopmentConnectionString"];
        }
        else
        {
            return ConfigurationManager.AppSettings["TestConnectionString"];
        }
    }

    public String ConnectionString
    {
        get { return _connectionString; }
    }

    private static string GetBaseURL()
    {
        if ("DEVELOPMENT" == GetCurrentEnvironment())
        {
            return ConfigurationManager.AppSettings["DevelopmentBaseUrl"];
        }
        else
        {
            return ConfigurationManager.AppSettings["TestBaseUrl"];
        }
    }

    static BaseIntegrationTest()
    {
        StaticDriver.Manage().Timeouts().ImplicitlyWait(
           TimeSpan.FromSeconds(TimeOut));
    }

    // Pass in null if want to run your test-case without logging in.
    public static void BrowserDriverInitialize()
    {
        _testClassesRunning++;
    }

    public static void BrowserDriverCleanup()
    {
        try
        {
            _testClassesRunning--;
            if (_testClassesRunning == 0)
            {
                StaticDriver.Quit();
                StaticDriver = null;
            }
        }
        catch (Exception)
        {
            // Ignore errors if unable to close the browser
        }
    }

    public IWebDriver Driver
    {
        get
        {
            if (null == StaticDriver)
            {
                StaticDriver = CreateDriverInstance();
            }
            return StaticDriver;
        }
    }

    public void Open(string url)
    {
        Driver.Navigate().GoToUrl(BaseUrl + VirtualPath + url.Trim('~'));
    }

    public void OpenAbsolute(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public void Click(string id)
    {
        Click(By.Id(id));
    }

    public void Click(By locator)
    {
        Driver.FindElement(locator).Click();
    }

    public void ClickAndWait(string id, string newUrl)
    {
        ClickAndWait(By.Id(id), newUrl);
    }

    /// <summary>
    /// Use when you are navigating via a hyper-link and need for the page to fully load before 
    /// moving further.  
    /// </summary>
    public void ClickAndWait(By locator, string newUrl)
    {
        Driver.FindElement(locator).Click();
        WebDriverWait wait = new WebDriverWait(Driver,
              TimeSpan.FromSeconds(TimeOut));
        wait.Until(d => d.Url.Contains(newUrl.Trim('~')));
    }

    public void WaitForUrl(string url)
    {
        WebDriverWait wait = new WebDriverWait(Driver,
              TimeSpan.FromSeconds(TimeOut));
        wait.Until(d => d.Url.Contains(url.Trim('~')));
    }

    public void AssertCurrentPage(string pageUrl)
    {
        var absoluteUrl = new Uri(new Uri(BaseUrl), VirtualPath +
               pageUrl.Trim('~')).ToString();
        Assert.AreEqual(absoluteUrl, Driver.Url);
    }

    public void AssertTextContains(string id, string text)
    {
        AssertTextContains(By.Id(id), text);
    }

    public void AssertTextContains(By locator, string text)
    {
        Assert.IsTrue(Driver.FindElement(locator).Text.Contains(text));
    }

    public void AssertTextEquals(string id, string text)
    {
        AssertTextEquals(By.Id(id), text);
    }

    public void AssertTextEquals(By locator, string text)
    {
        Assert.AreEqual(text, Driver.FindElement(locator).Text);
    }

    public void AssertValueContains(string id, string text)
    {
        AssertValueContains(By.Id(id), text);
    }

    public void AssertValueContains(By locator, string text)
    {
        Assert.IsTrue(GetValue(locator).Contains(text));
    }

    public void AssertValueEquals(string id, string text)
    {
        AssertValueEquals(By.Id(id), text);
    }

    public void AssertValueEquals(By locator, string text)
    {
        Assert.AreEqual(text, GetValue(locator));
    }

    public IWebElement GetElement(string id)
    {
        return Driver.FindElement(By.Id(id));
    }

    public string GetValue(By locator)
    {
        return Driver.FindElement(locator).GetAttribute("value");
    }

    public string GetText(By locator)
    {
        return Driver.FindElement(locator).Text;
    }

    public void Type(string id, string text)
    {
        var element = GetElement(id);
        element.Clear();
        element.SendKeys(text);
    }

    public void Uncheck(string id)
    {
        Uncheck(By.Id(id));
    }

    public void Uncheck(By locator)
    {
        var element = Driver.FindElement(locator);
        if (element.Selected)
            element.Click();
    }

    // Selects an element from a drop-down list.
    public void Select(string id, string valueToBeSelected)
    {
        var options = GetElement(id).FindElements(By.TagName("option"));
        foreach (var option in options)
        {
            if (valueToBeSelected == option.Text)
                option.Click();
        }
    }

    private static IWebDriver CreateDriverInstance()
    {
        //return new InternetExplorerDriver();
        return new FirefoxDriver();
    }
}
