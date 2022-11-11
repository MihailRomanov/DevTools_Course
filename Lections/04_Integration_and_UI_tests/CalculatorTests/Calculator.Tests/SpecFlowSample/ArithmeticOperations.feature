Feature: Arithmetic Operations

Support a four base arithmetic operations

Scenario: Add two numbers
	Given Enter number 33
	And Press button +
	And Enter number 3
	When Press button =
	Then Display 36
