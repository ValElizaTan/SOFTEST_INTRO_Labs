using NUnit.Framework;
using SOFTEST_INTRO_Calculator;
namespace SOFTEST_INTRO_Calculator.IntegrationTests;

// KEYWORDS: integration test fixture, real dependency, component boundary —
// exercises Calculator, the real FileReader and the operating-system file
// system together. Establishes that these components work with each other
// in the test environment, which the isolated unit tests cannot show.
[TestFixture]
public sealed class MagicNumberFileIntegrationTests
{
    private Calculator _calculator = null!;
    private string _path = null!;
    private IFileReader _fileReader = null!;

    // KEYWORDS: setup, dependency injection, temporary file — runs before
    // every test. Creates a Calculator and the real FileReader (passed in
    // through the IFileReader parameter), then asks the OS for a unique
    // temp file via Path.GetTempFileName and writes the lines "42" and "-7".
    // No machine-specific absolute path, so it runs on Windows, macOS and
    // the Ubuntu CI runner.
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
        _fileReader = new FileReader();
        _path = Path.GetTempFileName();
        File.WriteAllLines(_path, new[] { "42", "-7" });
    }


    // KEYWORDS: teardown, cleanup, test independence — runs after every test
    // and deletes the temporary file if it still exists, so no files are
    // left behind and each test starts from a clean state.
    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }



    // KEYWORDS: result test, real file read, data-driven — reads the actual
    // temp file through FileReader. Index 0 selects "42" -> 84 and index 1
    // selects "-7" -> 14, confirming twice the magnitude end to end.
    [TestCase(0, 84)]
    [TestCase(1, 14)]
    public void GenMagicNum_FileContainsNumber_ReturnsTwiceMagnitude(
    int choice, double expected)
    {
        double result = _calculator.GenMagicNum(choice, _path, _fileReader);
        Assert.That(result, Is.EqualTo(expected));
    }

    // KEYWORDS: rejection test, boundary values, range check — -1 is below
    // the first line and 2 is past the last line of the two-line file.
    // Both must throw ArgumentOutOfRangeException.
    [TestCase(-1)]
    [TestCase(2)]
    public void GenMagicNum_IndexOutsideFile_ThrowsArgumentOutOfRangeException(
    int choice)
    {
        Assert.That(
        () => _calculator.GenMagicNum(choice, _path, _fileReader),
        Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}