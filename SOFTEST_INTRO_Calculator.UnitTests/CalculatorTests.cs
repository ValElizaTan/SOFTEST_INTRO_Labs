using SOFTEST_INTRO_Calculator;
using NUnit.Framework;
namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
    private Calculator _calculator = null!;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // KEYWORDS: basic addition — checks a single representative addition.
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange: the calculator is created in SetUp.
        // Act
        double result = _calculator.Add(10, 20);
        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    // KEYWORDS: Add special case — the three step 7 examples.
    [TestCase(1, 11, 7)]
    [TestCase(10, 11, 11)]
    [TestCase(11, 11, 15)]
    public void Add_BinaryLookingOperands_JoinsDigitsAsBinary(double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected));
    }

    // KEYWORDS: Add regression guard — ordinary addition must be unchanged
    // whenever an operand contains another digit, is zero, negative or fractional.
    [TestCase(50, 70, 120)]
    [TestCase(12, 1, 13)]
    [TestCase(10, 0, 10)]
    [TestCase(0, 1, 1)]
    [TestCase(-1, 11, 10)]
    [TestCase(1.5, 1, 2.5)]
    public void Add_OtherInputs_StillReturnsOrdinarySum(double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: parameterised addition — zero, positive, negative and
    // fractional inputs, with a tolerance for floating-point rounding.
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]
    public void Add_RepresentativeInputs_ReturnsSum(double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: division, valid inputs — the three valid rows from the
    // section 5 table: normal division, zero numerator, and a negative result.
    [TestCase(1, 2, 0.5)]
    [TestCase(0, 15, 0)]
    [TestCase(15, -3, -5)]
    public void Divide_ValidInputs_ReturnsExpected(double a, double b, double expected)
    {
        double result = _calculator.Divide(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: division, zero-divisor exception — both rows from the table
    // that must throw, including the 0 / 0 edge case.
    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b),
            Throws.TypeOf<ArgumentException>());
    }

    // KEYWORDS: factorial base case — 0! = 1.
    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        long result = _calculator.Factorial(0);
        Assert.That(result, Is.EqualTo(1L));
    }

    // KEYWORDS: factorial, valid inputs — small and boundary (20!) values.
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInputs_ReturnsExpected(int n, long expected)
    {
        long result = _calculator.Factorial(n);
        Assert.That(result, Is.EqualTo(expected));
    }

    // KEYWORDS: factorial, out-of-range — values just outside the valid
    // 0–20 boundary must throw ArgumentOutOfRangeException.
    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_OutOfRange_ThrowsArgumentOutOfRangeException(int n)
    {
        Assert.That(() => _calculator.Factorial(n),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: triangle area, valid inputs — a normal case plus zero-height
    // and zero-width edge cases. (Stray [Test] attribute removed — this
    // method only runs via the [TestCase] entries below.)
    [TestCase(3, 4, 6)]
    [TestCase(0, 5, 0)]
    [TestCase(5, 0, 0)]
    public void TriangleArea_ValidInputs_ReturnsExpected(double height, double width, double expected)
    {
        double result = _calculator.TriangleArea(height, width);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: triangle area, negative dimensions — any negative height
    // and/or width must throw ArgumentOutOfRangeException.
    [TestCase(-1, 4)]
    [TestCase(3, -1)]
    [TestCase(-1, -1)]
    public void TriangleArea_NegativeDimension_ThrowsArgumentOutOfRangeException(double height, double width)
    {
        Assert.That(() => _calculator.TriangleArea(height, width),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: circle area, valid inputs — unit radius gives pi; zero
    // radius gives zero area.
    [TestCase(1, Math.PI)]
    [TestCase(0, 0)]
    public void CircleArea_ValidInputs_ReturnsExpected(double radius, double expected)
    {
        double result = _calculator.CircleArea(radius);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: circle area, negative radius — must throw
    // ArgumentOutOfRangeException.
    [TestCase(-1)]
    public void CircleArea_NegativeRadius_ThrowsArgumentOutOfRangeException(double radius)
    {
        Assert.That(() => _calculator.CircleArea(radius),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: UnknownFunctionA, permutations P(n, r) — valid inputs from
    // the section 7 table, plus a discriminating case (6, 2) where A and B
    // give different results (30 vs 15), proving they are different functions.
    [TestCase(5, 5, 120L)]
    [TestCase(5, 4, 120L)]
    [TestCase(5, 3, 60L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 30L)]   // discriminating example: A and B disagree here
    public void UnknownFunctionA_ValidInputs_ReturnsExpected(int n, int r, long expected)
    {
        long result = _calculator.UnknownFunctionA(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    // KEYWORDS: UnknownFunctionA, invalid inputs — n negative, or r > n,
    // must throw ArgumentOutOfRangeException.
    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    public void UnknownFunctionA_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionA(n, r),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: UnknownFunctionB, combinations C(n, r) — same table as A,
    // plus the same discriminating (6, 2) case, which returns 15 for B.
    [TestCase(5, 5, 1L)]
    [TestCase(5, 4, 5L)]
    [TestCase(5, 3, 10L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 15L)]   // discriminating example
    public void UnknownFunctionB_ValidInputs_ReturnsExpected(int n, int r, long expected)
    {
        long result = _calculator.UnknownFunctionB(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    // KEYWORDS: UnknownFunctionB, invalid inputs — n negative, or r > n,
    // must throw ArgumentOutOfRangeException.
    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    public void UnknownFunctionB_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionB(n, r),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /*  Lab 2 Part II Starts Here   */

    // KEYWORDS: MTBF, valid input — 1000 operating hours over 10 failures
    // gives an MTBF of 100 hours (matches the Availability feature example).
    [TestCase(1000, 10, 100)]
    [TestCase(1, 1, 1)]
    public void Mtbf_ValidInputs_ReturnsExpected(double operatingTime, double failureCount, double expected)
    {
        double result = _calculator.Mtbf(operatingTime, failureCount);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: MTBF, invalid input — operating time or failure count that
    // is zero or negative must throw ArgumentOutOfRangeException.
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    [TestCase(1000, 0)]
    [TestCase(1000, -1)]
    public void Mtbf_NonPositiveInputs_ThrowsArgumentOutOfRangeException(double operatingTime, double failureCount)
    {
        Assert.That(() => _calculator.Mtbf(operatingTime, failureCount),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: Availability, valid input — MTBF 90 / MTTR 10 gives 0.9,
    // plus a zero-MTTR edge case (should be 1.0, i.e. always available).
    [TestCase(90, 10, 0.9)]
    [TestCase(100, 0, 1.0)]
    public void Availability_ValidInputs_ReturnsExpected(double mtbf, double mttr, double expected)
    {
        double result = _calculator.Availability(mtbf, mttr);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: Availability, invalid input — a negative MTBF or MTTR, or
    // both zero (undefined, denominator would be zero), must throw
    // ArgumentOutOfRangeException.
    [TestCase(-1, 10)]
    [TestCase(90, -1)]
    [TestCase(0, 0)]
    public void Availability_InvalidInputs_ThrowsArgumentOutOfRangeException(double mtbf, double mttr)
    {
        Assert.That(() => _calculator.Availability(mtbf, mttr),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: Basic Musa, current failure intensity — tau = 0 must return
    // lambda0 unchanged; a positive tau matches the worked example from the
    // BasicReliability feature (lambda0 = 0.5, nu0 = 100, tau = 10).
    [TestCase(0.5, 100, 0, 0.5)]
    [TestCase(0.5, 100, 10, 0.475614712250357)]
    public void CurrentFailureIntensity_ValidInputs_ReturnsExpected(double lambda0, double nu0, double tau, double expected)
    {
        double result = _calculator.CurrentFailureIntensity(lambda0, nu0, tau);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: Basic Musa, expected cumulative failures — tau = 0 must
    // return 0 (no execution time, no observed failures yet); a positive
    // tau matches the same worked example as above.
    [TestCase(0.5, 100, 0, 0)]
    [TestCase(0.5, 100, 10, 4.877057549928598)]
    public void ExpectedCumulativeFailures_ValidInputs_ReturnsExpected(double lambda0, double nu0, double tau, double expected)
    {
        double result = _calculator.ExpectedCumulativeFailures(lambda0, nu0, tau);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // KEYWORDS: Basic Musa, invalid input boundaries — lambda0 <= 0,
    // nu0 <= 0, or tau < 0 must each throw ArgumentOutOfRangeException.
    // Checked against both production functions since they share
    // ValidateMusaParameters.
    [TestCase(0, 100, 10)]
    [TestCase(-0.5, 100, 10)]
    [TestCase(0.5, 0, 10)]
    [TestCase(0.5, -100, 10)]
    [TestCase(0.5, 100, -1)]
    public void CurrentFailureIntensity_InvalidInputs_ThrowsArgumentOutOfRangeException(double lambda0, double nu0, double tau)
    {
        Assert.That(() => _calculator.CurrentFailureIntensity(lambda0, nu0, tau),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(0, 100, 10)]
    [TestCase(-0.5, 100, 10)]
    [TestCase(0.5, 0, 10)]
    [TestCase(0.5, -100, 10)]
    [TestCase(0.5, 100, -1)]
    public void ExpectedCumulativeFailures_InvalidInputs_ThrowsArgumentOutOfRangeException(double lambda0, double nu0, double tau)
    {
        Assert.That(() => _calculator.ExpectedCumulativeFailures(lambda0, nu0, tau),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}