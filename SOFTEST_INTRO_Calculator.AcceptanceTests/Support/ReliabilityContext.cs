namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

// KEYWORDS: context injection — holds the named MTBF/MTTR values read from
// the "the reliability values are" step data table, for the Availability
// scenario in section 13 of the lab.
public sealed class ReliabilityContext
{
    public double Mtbf { get; set; }
    public double Mttr { get; set; }
}
