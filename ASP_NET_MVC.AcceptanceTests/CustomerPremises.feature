Feature: CustomerPremises
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario Outline: eating
	When I eat <eat> cucumbers
	Then I should have <left> cucumbers

Examples:
    | start | eat | left |
    |  12   |  5  |  7   |
    |  20   |  5  |  15  |
