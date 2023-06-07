using OneAutomationFramework.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OneAutomationFramework.Steps
{
    [Binding]
    public sealed class ProductPageSteps
    {
        public ScenarioContext? _scenarioContext;
        public ProductPage productPage;
        public ProductPageSteps(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;
            productPage = new ProductPage(scenarioContext);
        }


        [Given(@"I add '([^']*)' random items to my cart")]
        public void GivenIAddRandomItemsToMyCart(int noOfItems)
        {
            productPage.AddRandomProducts(noOfItems);
            _scenarioContext["noOfItems"] = noOfItems;
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            productPage.GotoCart();
        }

    }
}
