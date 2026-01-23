// <copyright file="FontParameters.cs" company="Johann Blais">
// Copyright (c) 2026 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains the font parameters for customizing text display</summary>

namespace InfoBox
{
    using System.Drawing;

    /// <summary>
    /// Contains the font parameters for customizing message text display.
    /// </summary>
    public class FontParameters
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FontParameters"/> class.
        /// </summary>
        /// <param name="messageFont">The font to use for message text.</param>
        public FontParameters(Font messageFont)
        {
            this.MessageFont = messageFont;
            this.MessageColor = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontParameters"/> class.
        /// </summary>
        /// <param name="messageFont">The font to use for message text.</param>
        /// <param name="messageColor">The color to use for message text.</param>
        public FontParameters(Font messageFont, Color messageColor)
        {
            this.MessageFont = messageFont;
            this.MessageColor = messageColor;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the font for the message text.
        /// </summary>
        /// <value>The font for the message text.</value>
        public Font MessageFont { get; private set; }

        /// <summary>
        /// Gets the color for the message text.
        /// </summary>
        /// <value>The color for the message text, or null to use the system default.</value>
        public Color? MessageColor { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Determines whether the current instance has either a font or a color defined.
        /// </summary>
        /// <returns>true if the instance has either a font or a color defined; otherwise, false.</returns>
        public bool IsSet()
        {
            return this.HasFont() || this.HasColor();
        }

        /// <summary>
        /// Determines whether the current instance has a font defined.
        /// </summary>
        /// <returns>true if the instance has a font defined; otherwise, false.</returns>
        public bool HasFont()
        {
            return this.MessageFont != null;
        }

        /// <summary>
        /// Determines whether the current instance has a color defined.
        /// </summary>
        /// <returns>true if the instance has a color defined; otherwise, false.</returns>
        public bool HasColor()
        {
            return this.MessageColor.HasValue && this.MessageColor != Color.Empty;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            FontParameters compared = (FontParameters)obj;

            return object.Equals(this.MessageFont, compared.MessageFont) &&
                   this.MessageColor == compared.MessageColor;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return (this.MessageFont?.GetHashCode() ?? 0) ^
                   (this.MessageColor?.GetHashCode() ?? 0);
        }

        #endregion Overrides
    }
}
