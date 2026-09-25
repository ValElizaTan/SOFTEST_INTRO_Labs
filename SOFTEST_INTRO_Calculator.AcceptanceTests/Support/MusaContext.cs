namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

// KEYWORDS: context injection — holds the named lambda0/nu0/tau values read
// from the "the reliability growth parameters are" step data table, for
// the Basic Musa scenarios in section 14 of the lab.
public sealed class MusaContext
{
    public double Lambda0 { get; set; }
    public double Nu0 { get; set; }
    public double Tau { get; set; }
}
