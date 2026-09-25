using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorFactorialSteps
{
    private readonly CalculatorContext _context;

    public UsingCalculatorFactorialSteps(CalculatorContext context)
        => _context = context;

    [When("I have entered {int} into the calculator and press factorial")]
    public void WhenIHaveEnteredIntoTheCalculatorAndPressFactorial(int value)
    {
        _context.IntegerResult = null;
        _context.Error = null;

        try
        {
            _context.IntegerResult = _context.Calculator.Factorial(value);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }
}
