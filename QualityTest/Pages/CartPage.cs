using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OneAutomationFramework.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region Locators
        public string lblCartItems => "//tr[contains(@class,'cart-item ')]";
        #endregion


        public List<IWebElement> GetCartItems()
        {
            return driver.FindElements(By.XPath(lblCartItems)).ToList();
        }

        public string FindLowestPriceItem()
        {
            var cartItems = GetCartItems();
            return cartItems.Select(
                x => x.FindElement(By.XPath("//td[@class='product-price']")).Text
                ).ToList().Min();
        }

        public bool RemoveItemFromCart(string item)
        {
            var cartItems = GetCartItems();
            var lnkItemWithLowPrice = cartItems.Select(
                                        x => x.FindElement(By.XPath($"//td[@class='product-price']/span[contains(text(),'{item.Replace("$", "")}')]")).
                                               FindElement(By.XPath("//parent::td//preceding-sibling::td[@class='product-remove']"))).FirstOrDefault();
            if (lnkItemWithLowPrice.Enabled)
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(lnkItemWithLowPrice).Build().Perform();
                lnkItemWithLowPrice.Click();
                return true;
            }
            return false;

        }
    }
}
