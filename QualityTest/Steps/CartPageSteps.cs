using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAutomationFramework.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OneAutomationFramework.Steps
{
    [Binding]
    public sealed class CartPageSteps
    {
        public ScenarioContext? _scenarioContext;
        public CartPage cartPage;
        public CartPageSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            cartPage = new CartPage(scenarioContext);
        }

        [Then(@"I find total '([^']*)' items listed in my cart")]
        public void ThenIFindTotalItemsListedInMyCart(int totalItems)
        {
            Assert.AreEqual(cartPage.GetCartItems().Count, totalItems, $"Expected Item in Cart:{totalItems} Vs Actual Items in Cart:{cartPage.GetCartItems().Count}");
        }

        [When(@"I search for lowest price item")]
        public void WhenISearchForPriceItem()
        {
            var lowestPrice = cartPage.FindLowestPriceItem();
            _scenarioContext["lowPrice"] = lowestPrice;
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowestPriceItemFromMyCart()
        {
            var lowestPrice = (string)_scenarioContext["lowPrice"];
            cartPage.RemoveItemFromCart(lowestPrice);
        }

        [Then(@"I am able to verify items in my cart")]
        public void ThenIAmAbleToVerifyItemsInMyCart()
        {
            var cartItem = cartPage.GetCartItems().Count;
            Assert.AreEqual((int)_scenarioContext["noOfItems"], cartItem, $"Actual No Of Items:{cartItem} Vs Expected No Of Items:{(int)_scenarioContext["noOfItems"]}");
        }


    }
}
