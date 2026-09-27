using Moq;
using NUnit.Framework;
using SOFTEST_INTRO_Calculator;
namespace SOFTEST_INTRO_Calculator.UnitTests;

// KEYWORDS: unit test fixture, isolated test, test double — exercises
// Calculator.GenMagicNum without touching the real file system. A Moq
// Mock<IFileReader> replaces FileReader, so only the calculator logic is
// under test and no real file is created or read.
[TestFixture]
public sealed class MagicNumberUnitTests
{
    private Calculator _calculator = null!;
    private Mock<IFileReader> _fileReader = null!;

    // KEYWORDS: setup, fresh fixture, stub configuration — runs before every
    // test. Creates a new Calculator and a new Mock<IFileReader>, then
    // configures Read("MagicNumbers.txt") to return the controlled array
    // { "42", "-7" }. Only the dependency's response is faked; the
    // calculation itself still runs inside Calculator.
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
        _fileReader = new Mock<IFileReader>();
        _fileReader
        .Setup(reader => reader.Read("MagicNumbers.txt"))
        .Returns(new[] { "42", "-7" });
    }

    // KEYWORDS: result test, stub role, data-driven — the mock acts as a stub
    // because it only supplies indirect input. Index 0 selects "42" -> 84 and
    // index 1 selects "-7" -> 14, confirming the result is twice the
    // magnitude (absolute value) of the chosen number.
    [TestCase(0, 84)]
    [TestCase(1, 14)]
    public void GenMagicNum_ConfiguredValues_ReturnsTwiceMagnitude(
 int choice, double expected)
    {
        double result = _calculator.GenMagicNum(
        choice, "MagicNumbers.txt", _fileReader.Object);
        Assert.That(result, Is.EqualTo(expected));
    }

    // KEYWORDS: rejection test, boundary values, range check — -1 is just
    // below the lowest valid index and 2 is just past the last line of the
    // two-line stub. Both must throw ArgumentOutOfRangeException.
    [TestCase(-1)]
    [TestCase(2)]
    public void GenMagicNum_UnsupportedIndex_ThrowsArgumentOutOfRangeException(
     int choice)
    {
        Assert.That(
        () => _calculator.GenMagicNum(
        choice, "MagicNumbers.txt", _fileReader.Object),
        Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // KEYWORDS: interaction test, mock role, Verify — checks how the
    // dependency was used rather than what was returned. Verify is the
    // test's assertion: Read must be called with the supplied path
    // "MagicNumbers.txt" exactly once (Times.Once).
    [Test]
    public void GenMagicNum_ValidChoice_ReadsSuppliedPathOnce()
    {
        _calculator.GenMagicNum(
        0, "MagicNumbers.txt", _fileReader.Object);
        _fileReader.Verify(
        reader => reader.Read("MagicNumbers.txt"),
        Times.Once());
    }
}