using System.Text;

namespace CSharpRegexBenchmark
{
    /// <summary>
    /// Generates a text
    /// </summary>
    public static class TextGenerator
    {
        private static readonly string[] PossibleWords = { "Lorem", "Ipsum", "dolor", "amet" };
        private const string DoubleWord = "gubergren";

        /// <summary>
        /// Generates a text with given length and repeats a word every x words
        /// </summary>
        /// <param name="numberOfWords">The number of word in the text</param>
        /// <param name="repeatDoubleWordEvery">Repeat a word every x words</param>
        /// <returns></returns>
        public static string GenerateText(int numberOfWords, int repeatDoubleWordEvery)
        {
            var textBuilder = new StringBuilder();
            var indexGenerator = new Random();

            for(int i = 0; i < numberOfWords; i++)
            {
                if(i % repeatDoubleWordEvery == 0)
                {
                    textBuilder.Append(DoubleWord + " ");
                } 
                else
                {
                    var wordIndex = indexGenerator.Next(0, 3);
                    textBuilder.Append(PossibleWords[wordIndex] + " ");
                }           
            }

            return textBuilder.ToString().TrimEnd();
        }
    }
}
