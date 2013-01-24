Feature: Walking Skeleton
	As an eXtreme Programming Developer
	I want testing infrastructure setup and intial architecture
	In order to deliver at a sustainable pace

@mytag
Scenario: Walking Skeleton
	When I visit "~/"
    And I enter "item" into the "ItemText" field
    And I click "add_button"
    Then I should see "item" on the page

Scenario: Showing the items stored in the database
	Given the database contains the following items
	| ItemText |
	| Item1    |
	| Item2    |
	| Item3    |
	| Item4    |
	When I visit "~/"
	Then I should see the following items on the page
	| ItemText |
	| Item1    |
	| Item2    |
	| Item3    |
	| Item4    |

Scenario: Seeing the details of the elements
	Given I am on the page with a couple of items
	When I click "1"
	Then I should see the details of item "1"

