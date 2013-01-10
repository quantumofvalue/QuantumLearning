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
