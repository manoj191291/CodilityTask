using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAutomationFramework.Drivers;
using OneAutomationFramework.Drivers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OneAutomationFramework.Hooks
{
    [Binding]
    public sealed class SeleniumHooks
    {

        private static IWebDriver? _driver;
        private readonly SeleniumConfiguration? _configuration;
        ScenarioContext _scenarioContext;

        public SeleniumHooks(SeleniumDriver driver, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = driver.Current;
            _scenarioContext["driver"] = _driver;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver.Url = @"https://cms.demo.katalon.com/";
            Console.WriteLine("Navigated to " + _driver.Url);
        }

        //[AfterScenario]
        //public void AfterScenario()
        //{
        //    _driver.Dispose();
        //}
    }
}
