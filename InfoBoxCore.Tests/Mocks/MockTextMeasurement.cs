// <copyright file="MockTextMeasurement.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Mock implementation of ITextMeasurement for testing</summary>

namespace InfoBoxCore.Tests.Mocks
{
    using InfoBox.Abstractions;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Mock implementation of ITextMeasurement for testing purposes.
    /// </summary>
    /// <remarks>
    /// This mock allows tests to specify predetermined text measurements
    /// without requiring a graphics context, enabling headless testing.
    /// See TESTABILITY_ROADMAP.md - P0.2
    /// </remarks>
    public class MockTextMeasurement : ITextMeasurement
    {
        private readonly Dictionary<string, SizeF> measurements = new Dictionary<string, SizeF>();
        private SizeF defaultSize = new SizeF(100, 20);

        /// <summary>
        /// Gets or sets the default size returned when no specific measurement is set.
        /// </summary>
        public SizeF DefaultSize
        {
            get { return this.defaultSize; }
            set { this.defaultSize = value; }
        }

        /// <summary>
        /// Sets the measured size for a specific text string.
        /// </summary>
        /// <param name="text">Text to set measurement for</param>
        /// <param name="size">Size to return for this text</param>
        public void SetMeasuredSize(string text, SizeF size)
        {
            this.measurements[text] = size;
        }

        /// <summary>
        /// Sets the measured size for a specific text string.
        /// </summary>
        /// <param name="text">Text to set measurement for</param>
        /// <param name="width">Width to return</param>
        /// <param name="height">Height to return</param>
        public void SetMeasuredSize(string text, float width, float height)
        {
            this.measurements[text] = new SizeF(width, height);
        }

        /// <summary>
        /// Clears all custom measurements.
        /// </summary>
        public void ClearMeasurements()
        {
            this.measurements.Clear();
        }

        /// <summary>
        /// Measures the specified string when drawn with the specified Font.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font that defines the text format</param>
        /// <returns>Size structure that represents the size of the string</returns>
        public SizeF MeasureString(string text, Font font)
        {
            SizeF result;
            if (this.measurements.TryGetValue(text, out result))
            {
                return result;
            }

            return this.defaultSize;
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
            SizeF result;
            if (this.measurements.TryGetValue(text, out result))
            {
                return result;
            }

            return this.defaultSize;
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
            SizeF result;
            if (this.measurements.TryGetValue(text, out result))
            {
                return result;
            }

            return this.defaultSize;
        }

        /// <summary>
        /// Gets the line height for the specified font.
        /// </summary>
        /// <param name="font">Font to measure</param>
        /// <returns>Height of a single line in pixels</returns>
        public int GetLineHeight(Font font)
        {
            return (int)this.defaultSize.Height;
        }
    }
}
