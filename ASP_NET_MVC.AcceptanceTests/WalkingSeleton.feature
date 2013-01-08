Feature: Walking Skeleton
	As an eXtreme Programming Developer
	I want testing infrastructure setup and intial architecture
	In order to deliver at a sustainable pace

@mytag
Scenario: Walking Skeleton
	When I visit "~/"
     And I enter "item" into the "item" field
     And I click "Add"
     Then I should be redirected to "~/items"
     And I should see "item" on the page
