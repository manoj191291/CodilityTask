
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OneAutomationFramework.Drivers.Selenium
{

    
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class SeleniumDriver : IDisposable
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly IDriverInitialiser _driverInitialiser;
        protected readonly AsyncLazy<IWebDriver> _currentBrowserLazy;
        public ScenarioContext _scenarioContext;
        protected bool _isDisposed;

        public SeleniumDriver(ISeleniumConfiguration seleniumConfiguration, IDriverInitialiser driverInitialiser, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _seleniumConfiguration = seleniumConfiguration;
            _driverInitialiser = driverInitialiser;
            if (_currentBrowserLazy == null)
                _currentBrowserLazy = new AsyncLazy<IWebDriver>(CreateSeleniumDriver);
        }

        /// <summary>
        /// The current Selenium instance
        /// </summary>
        public IWebDriver Current => _currentBrowserLazy.Value.Result;

        /// <summary>
        /// Creates a new instance of Playwright (opens a browser)
        /// </summary>
        /// <returns></returns>
        public IWebDriver CreateSeleniumDriver()
        {       
            try
            {
                return _seleniumConfiguration.Browser switch
                {
                    Browser.Chrome => _driverInitialiser.GetChromeDriver(_seleniumConfiguration.privateMode, _seleniumConfiguration.Arguments, _seleniumConfiguration.DefaultTimeout, _seleniumConfiguration.Headless),
                    Browser.Firefox => _driverInitialiser.GetFirefoxDriver(_seleniumConfiguration.privateMode, _seleniumConfiguration.Arguments, _seleniumConfiguration.DefaultTimeout, _seleniumConfiguration.Headless),
                    Browser.Edge => _driverInitialiser.GetEdgeDriver(_seleniumConfiguration.privateMode, _seleniumConfiguration.Arguments, _seleniumConfiguration.DefaultTimeout, _seleniumConfiguration.Headless),
                    _ => throw new NotImplementedException($"Support for browser {_seleniumConfiguration.Browser} is not implemented yet"),
                };
            }
            catch (Exception e)
            {
                Logger.Error("Unable to return browser driver due to " + e.Message);
                throw;

            }
        }

        /// <summary>
        /// Disposes the Selenium instance (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentBrowserLazy.IsValueCreated)
            {
                Task.Run(async delegate
                {
                    Current.Close();
                    Current.Dispose();
                });
            }
            _isDisposed = true;
        }
    }
}
