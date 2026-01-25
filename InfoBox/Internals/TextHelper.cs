using System;
using System.Text;
using System.Text.RegularExpressions;

namespace InfoBox.Internals
{
    /// <summary>
    /// Contains methods to process the content of the InformationBox
    /// </summary>
    internal static class TextHelper
    {
        private static readonly Regex _regex;

        /// <summary>
        /// Initialises the static members of the <see cref="TextHelper"/> class.
        /// </summary>
        static TextHelper()
        {
            _regex = new Regex(@"(?<sentence>.+?(\. |$))", RegexOptions.Compiled);
        }

        /// <summary>
        /// Returns a string containing the same content with all line breaks normalized as <see cref="System.Environment.NewLine"/>
        /// </summary>
        /// <param name="text">The text to normalize</param>
        /// <returns>a string containing the same content with all line breaks normalized as <see cref="System.Environment.NewLine"/></returns>
        public static string NormalizeLineBreaks(string text)
        {
            var builder = new StringBuilder(text);
            builder.Replace("\r\n", "\n");
            builder.Replace("\n", Environment.NewLine);
            return builder.ToString();
        }

        /// <summary>
        /// Returns a string containing the same content with all line breaks replaced with a single space per line break.
        /// </summary>
        /// <param name="text">The text to normalize</param>
        /// <returns>a string containing the same content with all line breaks replaced with spaces.</returns>
        public static string ReplaceLineBreaksWithSpaces(string text)
        {
            var builder = new StringBuilder(text);
            builder.Replace(Environment.NewLine, " ");
            return builder.ToString();
        }

        /// <summary>
        /// Transform a text into a list of sentences.
        /// </summary>
        /// <param name="text">The text to split into sentences</param>
        /// <returns>a <see cref="System.Text.RegularExpressions.MatchCollection"/> containing the list of sentences</returns>
        public static MatchCollection SplitTextIntoSentences(string text)
        {            
            return _regex.Matches(text);
        }

        /// <summary>
        /// Adds a line break after most punctuation symbols
        /// </summary>
        /// <param name="text">the text to update</param>
        /// <returns>a string containing the content with the additional line breaks.</returns>
        public static string AddLineBreaksAfterPunctuation(string text)
        {
            var builder = new StringBuilder(text);

            builder.Replace(". ", "." + Environment.NewLine);
            builder.Replace("? ", "?" + Environment.NewLine);
            builder.Replace("! ", "!" + Environment.NewLine);
            builder.Replace(": ", ":" + Environment.NewLine);
            builder.Replace(") ", ")" + Environment.NewLine);
            builder.Replace(", ", "," + Environment.NewLine);

            return builder.ToString();
        }
    }
}
