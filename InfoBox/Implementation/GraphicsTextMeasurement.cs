// <copyright file="GraphicsTextMeasurement.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Production implementation of ITextMeasurement using Graphics</summary>

namespace InfoBox.Implementation
{
    using InfoBox.Abstractions;
    using System;
    using System.Drawing;

    /// <summary>
    /// Production implementation of ITextMeasurement that uses Graphics for text measurement.
    /// </summary>
    /// <remarks>
    /// This implementation wraps the Graphics.MeasureString methods to provide
    /// text measurement functionality in production environments.
    /// See TESTABILITY_ROADMAP.md - P0.2
    /// </remarks>
    internal class GraphicsTextMeasurement : ITextMeasurement
    {
        private readonly Graphics graphics;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsTextMeasurement"/> class.
        /// </summary>
        /// <param name="graphics">Graphics context to use for measurements</param>
        public GraphicsTextMeasurement(Graphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            this.graphics = graphics;
        }

        /// <summary>
        /// Measures the specified string when drawn with the specified Font.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <returns>Size structure that represents the size of the string</returns>
        public SizeF MeasureString(string text, Font font)
        {
            return this.graphics.MeasureString(text, font);
        }

        /// <summary>
        /// Measures the specified string when drawn with the specified Font and maximum width.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <param name="width">Maximum width of the string</param>
        /// <returns>Size structure that represents the size of the string</returns>
        public SizeF MeasureString(string text, Font font, int width)
        {
            return this.graphics.MeasureString(text, font, width);
        }

        /// <summary>
        /// Measures the specified string when drawn with the specified Font and format.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <param name="width">Maximum width of the string</param>
        /// <param name="format">String format that specifies formatting information</param>
        /// <returns>Size structure that represents the size of the string</returns>
        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            return this.graphics.MeasureString(text, font, width, format);
        }

        /// <summary>
        /// Gets the line height for the specified font.
        /// </summary>
        /// <param name="font">Font to measure</param>
        /// <returns>Height of a single line in pixels</returns>
        public int GetLineHeight(Font font)
        {
            return (int)Math.Ceiling(this.graphics.MeasureString("X", font).Height);
        }
    }
}
