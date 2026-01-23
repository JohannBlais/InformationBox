// <copyright file="ITextMeasurement.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Interface for text measurement operations</summary>

namespace InfoBox.Abstractions
{
    using System.Drawing;

    /// <summary>
    /// Interface for abstracting text measurement operations to enable testability.
    /// </summary>
    /// <remarks>
    /// This interface abstracts graphics-dependent text measurement operations,
    /// allowing tests to run without requiring a graphics context.
    /// See TESTABILITY_ROADMAP.md - P0.2
    /// </remarks>
    public interface ITextMeasurement
    {
        /// <summary>
        /// Measures the specified string when drawn with the specified Font.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <returns>Size structure that represents the size of the string</returns>
        SizeF MeasureString(string text, Font font);

        /// <summary>
        /// Measures the specified string when drawn with the specified Font and maximum width.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <param name="width">Maximum width of the string</param>
        /// <returns>Size structure that represents the size of the string</returns>
        SizeF MeasureString(string text, Font font, int width);

        /// <summary>
        /// Measures the specified string when drawn with the specified Font and format.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <param name="width">Maximum width of the string</param>
        /// <param name="format">String format that specifies formatting information</param>
        /// <returns>Size structure that represents the size of the string</returns>
        SizeF MeasureString(string text, Font font, int width, StringFormat format);

        /// <summary>
        /// Gets the line height for the specified font.
        /// </summary>
        /// <param name="font">Font to measure</param>
        /// <returns>Height of a single line in pixels</returns>
        int GetLineHeight(Font font);
    }
}
