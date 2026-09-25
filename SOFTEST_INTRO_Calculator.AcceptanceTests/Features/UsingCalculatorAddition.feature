@Addition
Feature: UsingCalculatorAddition
  In order to avoid mistakes
  As a calculator user
  I want to be told the sum of two numbers

  Scenario: Add two numbers
    Given I have a calculator
    When I have entered 50 and 70 into the calculator and press add
    Then the result should be 120

  # KEYWORDS: deliberately inexplicable examples, straight from the lab
  # handout. Do NOT "fix" these to look like normal addition — the point of
  # this scenario is that the numbers do not obviously encode a sensible
  # rule. See the "Consider" note in the lab PDF: a passing test is only
  # useful once you understand what it's actually asserting. Investigate
  # before changing Add(), and be ready to explain (or challenge) the rule.
  Scenario Outline: Add zeros for special cases
    Given I have a calculator
    When I have entered <value1> and <value2> into the calculator and press add
    Then the result should be <value3>

    Examples:
      | value1 | value2 | value3 |
      | 1      | 11     | 7      |
      | 10     | 11     | 11     |
      | 11     | 11     | 15     |
