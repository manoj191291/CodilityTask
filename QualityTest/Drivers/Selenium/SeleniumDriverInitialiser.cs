
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;


namespace OneAutomationFramework.Drivers.Selenium
{
    public interface IDriverInitialiser
    {
        IWebDriver GetChromeDriver(bool? privateMode = false, string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
        IWebDriver GetEdgeDriver(bool? privateMode = false, string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
        IWebDriver GetFirefoxDriver(bool? privateMode = false, string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const float DEFAULT_TIMEOUT = 30f;

        public IWebDriver GetChromeDriver(bool? privateMode = false, string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            Logger.Info("Getting chrome driver");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AcceptInsecureCertificates = true;
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            chromeOptions.AddArguments(args ?? new string[] { });
            if (privateMode.Value)
                chromeOptions.AddArguments("--incognito");
            if (headless.Value)
            {
                chromeOptions.AddArgument("--headless=new");
            }
            try
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                var driver = new ChromeDriver(chromeOptions);

                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout ?? DEFAULT_TIMEOUT);
                Logger.Info("Chrome instance created successfully");
                return driver;
            }
            catch (Exception e)
            {
                Logger.Error("Creation of chrome instance failed due to " + e.Message);
                throw;
            }
        }

        public IWebDriver GetFirefoxDriver(bool? privateMode = false, string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AcceptInsecureCertificates = true;
            firefoxOptions.AddArgument("--start-maximized");
            if (privateMode.Value)
                firefoxOptions.AddArguments("-private");
            if (headless.Value)
            {
                firefoxOptions.AddArgument("--headless");
            }
            try
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                var driver = new FirefoxDriver(firefoxOptions);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout ?? DEFAULT_TIMEOUT);
                Logger.Info("Firefox instance created successfully");
                return driver;
            }
            catch (Exception e)
            {
                Logger.Error("Creation of firefox instance failed due to " + e.Message);
                throw;
            }
        }


        public IWebDriver GetEdgeDriver(bool? privateMode = false, string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var edgeOptions = new EdgeOptions();
            if (privateMode.Value)
            {
                // edgeOptions.UseInPrivateBrowsing = true;
            }
            if (headless.Value)
            {
                // edgeOptions.AddAdditionalCapability("--headless", true);
            }

            string file = new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Edge\\{new EdgeConfig().GetMatchingBrowserVersion()}\\X64\\MicrosoftWebDriver.exe"))
                File.Move($"{Directory.GetCurrentDirectory()}\\Edge\\{new EdgeConfig().GetMatchingBrowserVersion()}\\X64\\msedgedriver.exe",
                          $"{Directory.GetCurrentDirectory()}\\Edge\\{new EdgeConfig().GetMatchingBrowserVersion()}\\X64\\MicrosoftWebDriver.exe");
            try
            {
                var driver = new EdgeDriver($"{Directory.GetCurrentDirectory()}\\Edge\\{new EdgeConfig().GetMatchingBrowserVersion()}\\X64", edgeOptions);

                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout ?? DEFAULT_TIMEOUT);
                Logger.Info("Edge instance created successfully");
                return driver;
            }
            catch (Exception e)
            {
                Logger.Error("Creation of edge instance failed due to " + e.Message);
                throw;
            }
        }
        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }
    }
}
