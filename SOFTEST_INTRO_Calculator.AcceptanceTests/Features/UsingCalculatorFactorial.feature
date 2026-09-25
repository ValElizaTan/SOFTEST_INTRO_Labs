@Factorial
Feature: UsingCalculatorFactorial
  In order to count arrangements
  As a calculator user
  I want to calculate the factorial of a number

  Scenario: A normal factorial calculation
    Given I have a calculator
    When I have entered 5 into the calculator and press factorial
    Then the factorial result should be 120

  Scenario: The factorial identity case
    Given I have a calculator
    When I have entered 0 into the calculator and press factorial
    Then the factorial result should be 1

  Scenario: Reject an unsupported factorial value
    Given I have a calculator
    When I have entered 21 into the calculator and press factorial
    Then the calculation should be rejected
