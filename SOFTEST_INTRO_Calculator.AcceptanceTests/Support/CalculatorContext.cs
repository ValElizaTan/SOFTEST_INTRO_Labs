using SOFTEST_INTRO_Calculator;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

// KEYWORDS: context injection — holds the state for exactly one scenario.
// Reqnroll supplies the same instance to every binding class constructed
// during that scenario, and a fresh instance for the next one, so nothing
// here is static or shared across scenarios.
public sealed class CalculatorContext
{
    public Calculator Calculator { get; set; } = null!;
    public double? Result { get; set; }
    public long? IntegerResult { get; set; }
    public Exception? Error { get; set; }
}
