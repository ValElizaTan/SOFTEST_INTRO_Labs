@Availability
Feature: UsingCalculatorAvailability
  In order to calculate MTBF and Availability
  As someone who struggles with maths
  I want to be able to use my calculator to do this

  # MTBF = operating time / number of failures. Here: 1000 hours of
  # operating time over 10 observed failures gives an MTBF of 100 hours.
  Scenario: Calculating MTBF
    Given I have a calculator
    When I have entered 1000 and 10 into the calculator and press MTBF
    Then the result should be 100

  Scenario: Rejecting a non-positive operating time or failure count for MTBF
    Given I have a calculator
    When I have entered 0 and 10 into the calculator and press MTBF
    Then the calculation should be rejected

  # Availability = MTBF / (MTBF + MTTR). Here: MTBF of 90 hours and MTTR of
  # 10 hours gives a 0.9 (90%) steady-state availability.
  Scenario: Calculating Availability
    Given I have a calculator
    When I have entered 90 and 10 into the calculator and press Availability
    Then the result should be 0.9

  Scenario: Rejecting a negative MTBF or MTTR for Availability
    Given I have a calculator
    When I have entered -1 and 10 into the calculator and press Availability
    Then the calculation should be rejected

  # Same 90 / 10 example as above, but supplied as named values through a
  # step data table rather than two positional numbers, and routed through
  # ReliabilityContext instead of CalculatorContext.
  Scenario: Calculating Availability from named reliability values
    Given I have a calculator
    And the reliability values are
      | MTBF | MTTR |
      | 90   | 10   |
    When I calculate Availability from these values
    Then the result should be 0.9
