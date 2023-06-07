using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using static Io.Cucumber.Messages.Meta.Types;

namespace OneAutomationFramework.Pages
{
    public class ProductPage : BasePage
    {

        public ProductPage(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region Locators
        public string lnkAllProductsCart => "//ul[@class ='products columns-3']/li//a[contains(@class,'add_to_cart')]";
        public string lnkCart => "//a[contains(text(),'Cart')]";
        #endregion

        public void AddRandomProducts(int ProductCount = 0)
        {
            var lstAllProducts = driver.FindElements(By.XPath(lnkAllProductsCart)).ToList();
            var lstProducts = lstAllProducts.Take(ProductCount).ToList();
            foreach (var product in lstProducts)
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(product).Build().Perform();
                product.Click();
            }
        }

        public void GotoCart()
        {
            var Cart = driver.FindElement(By.XPath(lnkCart));
            if (Cart.Enabled)
                Cart.Click();
            else
                Console.WriteLine("Cart Link is not working");
        }
    }
}
