namespace SOFTEST_INTRO_Calculator;

using System.Globalization;

public class Calculator
{
    // KEYWORDS: addition, special-case rule — normally returns a + b. When both
    // operands are whole numbers >= 1 written only with the digits 0 and 1,
    // their digit strings are joined and read as a binary number
    // (e.g. 1 and 11 -> "111" -> 7). Inferred from the Lab 2 step 7 examples.
    public double Add(double a, double b)
    {
        if (TryGetBinaryDigits(a, out string left) &&
            TryGetBinaryDigits(b, out string right))
        {
            return Convert.ToInt64(left + right, 2);
        }

        return a + b;
    }

    // KEYWORDS: special-case helper — true only for whole numbers in [1, 1e15)
    // whose decimal digits are all 0 or 1. The upper bound keeps the joined
    // binary string short enough to fit in a long.
    private static bool TryGetBinaryDigits(double value, out string digits)
    {
        digits = string.Empty;

        if (!(value >= 1 && value < 1e15) || value != Math.Floor(value))
            return false;

        string text = ((long)value).ToString(CultureInfo.InvariantCulture);
        if (text.Any(c => c != '0' && c != '1'))
            return false;

        digits = text;
        return true;
    }

    // KEYWORDS: subtraction — returns a minus b.
    public double Subtract(double a, double b) => a - b;

    // KEYWORDS: multiplication — returns the product of two numbers.
    public double Multiply(double a, double b) => a * b;

    // KEYWORDS: division, zero-divisor rule — returns a / b, but throws
    // ArgumentException when the divisor (b) is zero, including 0 / 0.
    // A zero numerator with a nonzero divisor is valid and returns 0.
    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("Cannot divide by zero.", nameof(b));

        return a / b;
    }

    // KEYWORDS: operation dispatch — routes a single-character operation code
    // ("a","s","m","d") to the matching arithmetic method; throws
    // ArgumentException for any unrecognised code.
    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            _ => throw new ArgumentException("Unknown operation.")
        };
    }

    // KEYWORDS: factorial, range check — returns n! for 0 <= n <= 20 using an
    // iterative loop; throws ArgumentOutOfRangeException outside that range
    // (the upper bound of 20 avoids overflowing a long).
    public long Factorial(int n)
    {
        if (n < 0 || n > 20)
            throw new ArgumentOutOfRangeException(nameof(n), "n must be between 0 and 20.");

        long result = 1L;
        for (int i = 2; i <= n; i++)
            result *= i;

        return result;
    }

    // KEYWORDS: triangle area, geometry — returns 0.5 * height * width;
    // throws ArgumentOutOfRangeException if either dimension is negative.
    public double TriangleArea(double height, double width)
    {
        if (height < 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");
        if (width < 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");

        return 0.5 * height * width;
    }

    // KEYWORDS: circle area, geometry — returns pi * radius^2; throws
    // ArgumentOutOfRangeException if the radius is negative.
    public double CircleArea(double radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius cannot be negative.");

        return Math.PI * radius * radius;
    }

    // KEYWORDS: permutations, P(n, r) — returns n! / (n - r)! using Factorial;
    // requires 0 <= r <= n <= 20, otherwise throws ArgumentOutOfRangeException.
    public long UnknownFunctionA(int n, int r)
    {
        if (n < 0 || n > 20 || r < 0 || r > n)
            throw new ArgumentOutOfRangeException(nameof(r), "Require 0 <= r <= n <= 20.");

        return Factorial(n) / Factorial(n - r);
    }

    // KEYWORDS: combinations, C(n, r) — returns n! / (r! * (n - r)!) using
    // Factorial; requires 0 <= r <= n <= 20, otherwise throws
    // ArgumentOutOfRangeException.
    public long UnknownFunctionB(int n, int r)
    {
        if (n < 0 || n > 20 || r < 0 || r > n)
            throw new ArgumentOutOfRangeException(nameof(r), "Require 0 <= r <= n <= 20.");

        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    /*  Lab 2 Part II Starts Here   */

    // KEYWORDS: MTBF, mean time between failures — operatingTime / failureCount.
    // Both inputs must be strictly positive; the result is an observed
    // average, not a prediction of the exact time of the next failure.
    public double Mtbf(double operatingTime, double failureCount)
    {
        if (operatingTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(operatingTime), "Operating time must be positive.");
        if (failureCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(failureCount), "Failure count must be positive.");

        return operatingTime / failureCount;
    }

    // KEYWORDS: steady-state availability — mtbf / (mtbf + mttr), using the
    // repairable-system approximation MTTF ≈ MTBF. Neither input may be
    // negative and their sum must be positive; the result is a ratio in
    // [0, 1].
    public double Availability(double mtbf, double mttr)
    {
        if (mtbf < 0)
            throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF cannot be negative.");
        if (mttr < 0)
            throw new ArgumentOutOfRangeException(nameof(mttr), "MTTR cannot be negative.");
        if (mtbf + mttr <= 0)
            throw new ArgumentOutOfRangeException(nameof(mttr), "MTBF + MTTR must be positive.");

        return mtbf / (mtbf + mttr);
    }

    // KEYWORDS: Basic Musa model, current failure intensity —
    // lambda0 * exp(-lambda0 * tau / nu0). lambda0 and nu0 must be
    // strictly positive and tau (accumulated execution time) must be
    // non-negative.
    public double CurrentFailureIntensity(double lambda0, double nu0, double tau)
    {
        ValidateMusaParameters(lambda0, nu0, tau);
        return lambda0 * Math.Exp(-lambda0 * tau / nu0);
    }

    // KEYWORDS: Basic Musa model, expected cumulative failures —
    // nu0 * (1 - exp(-lambda0 * tau / nu0)). Same input domain as
    // CurrentFailureIntensity.
    public double ExpectedCumulativeFailures(double lambda0, double nu0, double tau)
    {
        ValidateMusaParameters(lambda0, nu0, tau);
        return nu0 * (1 - Math.Exp(-lambda0 * tau / nu0));
    }

    // KEYWORDS: Basic Musa model, shared input validation — lambda0 > 0,
    // nu0 > 0, tau >= 0; throws ArgumentOutOfRangeException otherwise.
    private static void ValidateMusaParameters(double lambda0, double nu0, double tau)
    {
        if (lambda0 <= 0)
            throw new ArgumentOutOfRangeException(nameof(lambda0), "lambda0 must be positive.");
        if (nu0 <= 0)
            throw new ArgumentOutOfRangeException(nameof(nu0), "nu0 must be positive.");
        if (tau < 0)
            throw new ArgumentOutOfRangeException(nameof(tau), "tau cannot be negative.");
    }

    /*  Lab 3 Part II Starts Here   */
    public double GenMagicNum(
 int choice, string path, IFileReader fileReader)
    {
        ArgumentNullException.ThrowIfNull(fileReader);
        if (choice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(choice));
        }
        string[] magicStrings = fileReader.Read(path);
        if (choice >= magicStrings.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(choice));
        }
        double magicNumber = double.Parse(magicStrings[choice]);
        return 2 * Math.Abs(magicNumber);
    }
}
