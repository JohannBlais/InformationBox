using System;
using System.Text.RegularExpressions;
using InfoBox.Internals;
using NUnit.Framework;

namespace InfoBoxCore.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="TextHelper"/> class.
    /// </summary>
    [TestFixture]
    public class TextHelperTests
    {
        #region NormalizeLineBreaks Tests

        [Test]
        public void NormalizeLineBreaks_WithCrLf_ReturnsNormalizedText()
        {
            // Arrange
            string input = "Line1\r\nLine2\r\nLine3";
            string expected = $"Line1{Environment.NewLine}Line2{Environment.NewLine}Line3";

            // Act
            string result = TextHelper.NormalizeLineBreaks(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void NormalizeLineBreaks_WithLfOnly_ReturnsNormalizedText()
        {
            // Arrange
            string input = "Line1\nLine2\nLine3";
            string expected = $"Line1{Environment.NewLine}Line2{Environment.NewLine}Line3";

            // Act
            string result = TextHelper.NormalizeLineBreaks(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void NormalizeLineBreaks_WithMixedLineEndings_ReturnsNormalizedText()
        {
            // Arrange
            string input = "Line1\r\nLine2\nLine3\r\nLine4";
            string expected = $"Line1{Environment.NewLine}Line2{Environment.NewLine}Line3{Environment.NewLine}Line4";

            // Act
            string result = TextHelper.NormalizeLineBreaks(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void NormalizeLineBreaks_WithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            string input = string.Empty;

            // Act
            string result = TextHelper.NormalizeLineBreaks(input);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void NormalizeLineBreaks_WithNoLineBreaks_ReturnsSameText()
        {
            // Arrange
            string input = "Single line text without breaks";

            // Act
            string result = TextHelper.NormalizeLineBreaks(input);

            // Assert
            Assert.That(result, Is.EqualTo(input));
        }

        [Test]
        public void NormalizeLineBreaks_WithMultipleConsecutiveLineBreaks_ReturnsNormalizedText()
        {
            // Arrange
            string input = "Line1\r\n\r\nLine2\n\nLine3";
            string expected = $"Line1{Environment.NewLine}{Environment.NewLine}Line2{Environment.NewLine}{Environment.NewLine}Line3";

            // Act
            string result = TextHelper.NormalizeLineBreaks(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region ReplaceLineBreaksWithSpaces Tests

        [Test]
        public void ReplaceLineBreaksWithSpaces_WithSingleLineBreak_ReturnsTextWithSpace()
        {
            // Arrange
            string input = $"Line1{Environment.NewLine}Line2";
            string expected = "Line1 Line2";

            // Act
            string result = TextHelper.ReplaceLineBreaksWithSpaces(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReplaceLineBreaksWithSpaces_WithMultipleLineBreaks_ReturnsTextWithSpaces()
        {
            // Arrange
            string input = $"Line1{Environment.NewLine}Line2{Environment.NewLine}Line3{Environment.NewLine}Line4";
            string expected = "Line1 Line2 Line3 Line4";

            // Act
            string result = TextHelper.ReplaceLineBreaksWithSpaces(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReplaceLineBreaksWithSpaces_WithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            string input = string.Empty;

            // Act
            string result = TextHelper.ReplaceLineBreaksWithSpaces(input);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ReplaceLineBreaksWithSpaces_WithNoLineBreaks_ReturnsSameText()
        {
            // Arrange
            string input = "Single line text without breaks";

            // Act
            string result = TextHelper.ReplaceLineBreaksWithSpaces(input);

            // Assert
            Assert.That(result, Is.EqualTo(input));
        }

        [Test]
        public void ReplaceLineBreaksWithSpaces_WithConsecutiveLineBreaks_ReturnsTextWithMultipleSpaces()
        {
            // Arrange
            string input = $"Line1{Environment.NewLine}{Environment.NewLine}Line2";
            string expected = "Line1  Line2";

            // Act
            string result = TextHelper.ReplaceLineBreaksWithSpaces(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region SplitTextIntoSentences Tests

        [Test]
        public void SplitTextIntoSentences_WithSingleSentence_ReturnsSingleMatch()
        {
            // Arrange
            string input = "This is a sentence.";

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Value, Is.EqualTo("This is a sentence."));
        }

        [Test]
        public void SplitTextIntoSentences_WithMultipleSentences_ReturnsMultipleMatches()
        {
            // Arrange
            string input = "First sentence. Second sentence. Third sentence.";

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Value, Is.EqualTo("First sentence. "));
            Assert.That(result[1].Value, Is.EqualTo("Second sentence. "));
            Assert.That(result[2].Value, Is.EqualTo("Third sentence."));
        }

        [Test]
        public void SplitTextIntoSentences_WithSentenceEndingWithoutSpace_IncludesInMatch()
        {
            // Arrange
            string input = "First sentence.Second sentence.";

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Value, Is.EqualTo("First sentence.Second sentence."));
        }

        [Test]
        public void SplitTextIntoSentences_WithEmptyString_ReturnsEmptyCollection()
        {
            // Arrange
            string input = string.Empty;

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SplitTextIntoSentences_WithTextWithoutPeriod_ReturnsEntireText()
        {
            // Arrange
            string input = "This is text without ending period";

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Value, Is.EqualTo(input));
        }

        [Test]
        public void SplitTextIntoSentences_WithMixedContent_SplitsCorrectly()
        {
            // Arrange
            string input = "First. Second. Third";

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Value, Is.EqualTo("First. "));
            Assert.That(result[1].Value, Is.EqualTo("Second. "));
            Assert.That(result[2].Value, Is.EqualTo("Third"));
        }

        [Test]
        public void SplitTextIntoSentences_WithNumbersAndPeriods_SplitsOnPeriodWithSpace()
        {
            // Arrange
            string input = "Version 1.0 is ready. Version 2.0 will follow.";

            // Act
            MatchCollection result = TextHelper.SplitTextIntoSentences(input);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Value, Is.EqualTo("Version 1.0 is ready. "));
            Assert.That(result[1].Value, Is.EqualTo("Version 2.0 will follow."));
        }

        #endregion

        #region AddLineBreaksAfterPunctuation Tests

        [Test]
        public void AddLineBreaksAfterPunctuation_WithPeriod_AddsLineBreak()
        {
            // Arrange
            string input = "First sentence. Second sentence.";
            string expected = $"First sentence.{Environment.NewLine}Second sentence.";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithQuestionMark_AddsLineBreak()
        {
            // Arrange
            string input = "First question? Second question?";
            string expected = $"First question?{Environment.NewLine}Second question?";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithExclamationMark_AddsLineBreak()
        {
            // Arrange
            string input = "First exclamation! Second exclamation!";
            string expected = $"First exclamation!{Environment.NewLine}Second exclamation!";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithColon_AddsLineBreak()
        {
            // Arrange
            string input = "First part: Second part: Third part";
            string expected = $"First part:{Environment.NewLine}Second part:{Environment.NewLine}Third part";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithClosingParenthesis_AddsLineBreak()
        {
            // Arrange
            string input = "First (note) Second (another)";
            string expected = $"First (note){Environment.NewLine}Second (another)";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithComma_AddsLineBreak()
        {
            // Arrange
            string input = "First, second, third";
            string expected = $"First,{Environment.NewLine}second,{Environment.NewLine}third";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithMixedPunctuation_AddsLineBreaksAfterAll()
        {
            // Arrange
            string input = "Hello! How are you? I am fine, thank you. Great: yes indeed (really) cool!";
            string expected = $"Hello!{Environment.NewLine}How are you?{Environment.NewLine}I am fine,{Environment.NewLine}thank you.{Environment.NewLine}Great:{Environment.NewLine}yes indeed (really){Environment.NewLine}cool!";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            string input = string.Empty;

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithNoPunctuation_ReturnsSameText()
        {
            // Arrange
            string input = "Text without punctuation";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(input));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithPunctuationAtEnd_DoesNotAddSpaceAfter()
        {
            // Arrange
            string input = "End with period.";
            string expected = "End with period.";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddLineBreaksAfterPunctuation_WithDecimalNumbers_DoesNotAddLineBreak()
        {
            // Arrange
            string input = "The price is 9.99 dollars.";
            string expected = $"The price is 9.99 dollars.";

            // Act
            string result = TextHelper.AddLineBreaksAfterPunctuation(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region Integration Tests

        [Test]
        public void Integration_NormalizeAndReplace_WorksCorrectly()
        {
            // Arrange
            string input = "Line1\r\nLine2\nLine3";

            // Act
            string normalized = TextHelper.NormalizeLineBreaks(input);
            string result = TextHelper.ReplaceLineBreaksWithSpaces(normalized);

            // Assert
            Assert.That(result, Is.EqualTo("Line1 Line2 Line3"));
        }

        [Test]
        public void Integration_ReplaceAndSplit_WorksCorrectly()
        {
            // Arrange
            string input = $"First sentence.{Environment.NewLine}Second sentence.";

            // Act
            string replaced = TextHelper.ReplaceLineBreaksWithSpaces(input);
            MatchCollection result = TextHelper.SplitTextIntoSentences(replaced);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Value, Is.EqualTo("First sentence. "));
            Assert.That(result[1].Value, Is.EqualTo("Second sentence."));
        }

        [Test]
        public void Integration_AddLineBreaksAndNormalize_WorksCorrectly()
        {
            // Arrange
            string input = "First. Second. Third.";

            // Act
            string withBreaks = TextHelper.AddLineBreaksAfterPunctuation(input);
            string result = TextHelper.NormalizeLineBreaks(withBreaks);

            // Assert
            string expected = $"First.{Environment.NewLine}Second.{Environment.NewLine}Third.";
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion
    }
}
