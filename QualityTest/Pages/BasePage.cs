using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OneAutomationFramework.Pages
{
    public abstract class BasePage
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected IWebDriver? driver;
        public ScenarioContext? _scenarioContext;

        protected BasePage(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            driver = (IWebDriver)scenarioContext["driver"];
        }
    }
}
