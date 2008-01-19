using System.Drawing;

namespace InfoBox
{
    /// <summary>
    /// Represents the icon for the title bar
    /// </summary>
    public class InformationBoxTitleIcon
    {
        #region Internals

        private readonly Icon _icon;

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        internal Icon Icon
        {
            get { return _icon; }
        }

        #endregion Internals

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxTitleIcon"/> class.
        /// </summary>
        /// <param name="icon">The icon.</param>
        public InformationBoxTitleIcon(Icon icon)
        {
            _icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxTitleIcon"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public InformationBoxTitleIcon(string filename) : this(new Icon(filename))
        {
        }

        #endregion Constructors
    }
}
