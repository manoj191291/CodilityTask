Feature: Codility Task
Develop a Web test solution that automates below simple test scenario, composed as BDD scenarios. The target for the test is the dummy web site

@PriceItem @Cart
Scenario: Remove lowest price item
	Given I add '<NoOfItems>' random items to my cart
	When  I view my cart
	Then  I find total '<NoOfItems>' items listed in my cart
	When  I search for lowest price item
	And   I am able to remove the lowest price item from my cart
	Then  I am able to verify items in my cart
Examples:
	| NoOfItems |
	| 4         |

