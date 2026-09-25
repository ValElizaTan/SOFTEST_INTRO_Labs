using NUnit.Framework;
using Reqnroll;
using SOFTEST_INTRO_Calculator;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

// KEYWORDS: shared bindings — the one "Given I have a calculator" step and
// the result/rejection assertions reused by every feature in this project.
// Kept in a single class so Reqnroll never sees two methods matching the
// same step text (an ambiguous binding).
[Binding]
public sealed class CommonCalculatorSteps
{
    private readonly CalculatorContext _context;

    public CommonCalculatorSteps(CalculatorContext context)
    {
        _context = context;
    }

    [Given("I have a calculator")]
    public void GivenIHaveACalculator()
    {
        _context.Calculator = new Calculator();
        _context.Result = null;
        _context.IntegerResult = null;
        _context.Error = null;
    }

    [Then("the result should be {double}")]
    public void ThenTheResultShouldBe(double expected)
    {
        Assert.That(_context.Result, Is.Not.Null, "No result was recorded — did the When step run and complete?");
        Assert.That(_context.Result!.Value, Is.EqualTo(expected).Within(1e-6));
    }

    [Then("the factorial result should be {long}")]
    public void ThenTheFactorialResultShouldBe(long expected)
    {
        Assert.That(_context.IntegerResult, Is.EqualTo(expected));
    }

    [Then("division should be rejected")]
    public void ThenDivisionShouldBeRejected()
    {
        Assert.That(_context.Error, Is.TypeOf<ArgumentException>());
    }

    // KEYWORDS: shared rejection assertion — reused by Factorial, MTBF,
    // Availability and Basic Musa, which all reject invalid input with
    // ArgumentOutOfRangeException rather than the plain ArgumentException
    // used for division by zero.
    [Then("the calculation should be rejected")]
    public void ThenTheCalculationShouldBeRejected()
    {
        Assert.That(_context.Error, Is.TypeOf<ArgumentOutOfRangeException>());
    }
}
