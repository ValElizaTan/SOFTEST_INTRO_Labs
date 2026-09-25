@BasicMusa
Feature: UsingCalculatorBasicReliability
  In order to calculate the Basic Musa model's failures and intensities
  As a Software Quality Metric enthusiast
  I want to use my calculator to do this

  # lambda0 = 0.5 failures per execution-time unit, nu0 = 100 expected
  # total failures, tau = 10 execution-time units accumulated so far.
  # lambda(tau) = lambda0 * exp(-lambda0*tau/nu0) = 0.5 * exp(-0.05).
  Scenario: Current failure intensity for a positive execution time
    Given I have a calculator
    And the reliability growth parameters are
      | lambda0 | nu0 | tau |
      | 0.5     | 100 | 10  |
    When I calculate the current failure intensity
    Then the failure intensity should be 0.475614712250357

  Scenario: Current failure intensity at the start of execution
    Given I have a calculator
    And the reliability growth parameters are
      | lambda0 | nu0 | tau |
      | 0.5     | 100 | 0   |
    When I calculate the current failure intensity
    Then the failure intensity should be 0.5

  # mu(tau) = nu0 * [1 - exp(-lambda0*tau/nu0)] = 100 * (1 - exp(-0.05)).
  Scenario: Expected cumulative failures for a positive execution time
    Given I have a calculator
    And the reliability growth parameters are
      | lambda0 | nu0 | tau |
      | 0.5     | 100 | 10  |
    When I calculate the expected cumulative failures
    Then the cumulative failures should be 4.877057549928598

  Scenario: No failures are expected before any execution time has passed
    Given I have a calculator
    And the reliability growth parameters are
      | lambda0 | nu0 | tau |
      | 0.5     | 100 | 0   |
    When I calculate the expected cumulative failures
    Then the cumulative failures should be 0

  Scenario Outline: Reject invalid Basic Musa parameters
    Given I have a calculator
    And the reliability growth parameters are
      | lambda0    | nu0    | tau    |
      | <lambda0>  | <nu0>  | <tau>  |
    When I calculate the current failure intensity
    Then the calculation should be rejected

    Examples:
      | lambda0 | nu0 | tau |
      | 0       | 100 | 10  |
      | -0.5    | 100 | 10  |
      | 0.5     | 0   | 10  |
      | 0.5     | -100| 10  |
      | 0.5     | 100 | -1  |
