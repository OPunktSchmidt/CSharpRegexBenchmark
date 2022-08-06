
namespace CSharpRegexBenchmark.Tests
{
    /// <summary>
    /// Test class to test the TextGenerator
    /// </summary>
    public class TextGeneratorTests
    {
        [Test]
        public void GenerateText_Should_ReturnTextWithCertainLength()
        {
            var numberOfWords = 1000;

            var generatedText = TextGenerator.GenerateText(numberOfWords, 1);

            Assert.IsNotNull(generatedText);

            var splittedText = generatedText.Split(" ");

            Assert.That(splittedText.Length, Is.EqualTo(numberOfWords));
        }

        [Test]
        public void GenerateText_Should_RepeatWord()
        {
            var numberOfWords = 1000;
            var repeatDoubleWordEvery = 5;
            var expectedRepeatCount = numberOfWords / repeatDoubleWordEvery;

            var generatedText = TextGenerator.GenerateText(numberOfWords, repeatDoubleWordEvery);

            Assert.IsNotNull(generatedText);

            var splittedText = generatedText.Split(" ");
            var repeatedWordCount = splittedText.Count(x => x == "gubergren");

            Assert.That(splittedText.Length, Is.EqualTo(numberOfWords));
            Assert.That(repeatedWordCount, Is.EqualTo(expectedRepeatCount));
        }
    }
}