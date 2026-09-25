using NUnit.Framework;
using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorBasicReliabilitySteps
{
    private readonly CalculatorContext _context;
    private readonly MusaContext _musa;

    public UsingCalculatorBasicReliabilitySteps(CalculatorContext context, MusaContext musa)
    {
        _context = context;
        _musa = musa;
    }

    // KEYWORDS: step data table — reads lambda0, nu0 and tau as named
    // values so all three Basic Musa inputs are represented explicitly in
    // scenario state, rather than being positional numbers.
    [Given("the reliability growth parameters are")]
    public void GivenTheReliabilityGrowthParametersAre(DataTable table)
    {
        var values = table.Rows[0];

        _musa.Lambda0 = double.Parse(values["lambda0"]);
        _musa.Nu0 = double.Parse(values["nu0"]);
        _musa.Tau = double.Parse(values["tau"]);
    }

    [When("I calculate the current failure intensity")]
    public void WhenICalculateTheCurrentFailureIntensity()
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.CurrentFailureIntensity(_musa.Lambda0, _musa.Nu0, _musa.Tau);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [When("I calculate the expected cumulative failures")]
    public void WhenICalculateTheExpectedCumulativeFailures()
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.ExpectedCumulativeFailures(_musa.Lambda0, _musa.Nu0, _musa.Tau);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [Then("the failure intensity should be {double}")]
    public void ThenTheFailureIntensityShouldBe(double expected)
    {
        Assert.That(_context.Result, Is.Not.Null);
        Assert.That(_context.Result!.Value, Is.EqualTo(expected).Within(1e-6));
    }

    [Then("the cumulative failures should be {double}")]
    public void ThenTheCumulativeFailuresShouldBe(double expected)
    {
        Assert.That(_context.Result, Is.Not.Null);
        Assert.That(_context.Result!.Value, Is.EqualTo(expected).Within(1e-6));
    }
}
