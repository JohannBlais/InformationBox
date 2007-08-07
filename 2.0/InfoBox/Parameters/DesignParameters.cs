namespace InfoBox
{
    using System.Drawing;

    /// <summary>
    /// Contains the values of the design parameters.
    /// </summary>
    public class DesignParameters
    {
        #region Internals

        private readonly Color _formBackColor = SystemColors.Control;
        private readonly Color _barsBackColor = SystemColors.Control;

        #endregion Internals

        #region Properties

        /// <summary>
        /// Gets the back color of the form.
        /// </summary>
        /// <value>The back color of the form.</value>
        public Color FormBackColor
        {
            get { return _formBackColor; }
        }

        /// <summary>
        /// Gets the color of the bars back.
        /// </summary>
        /// <value>The color of the bars back.</value>
        public Color BarsBackColor
        {
            get { return _barsBackColor; }
        }
        
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignParameters"/> class.
        /// </summary>
        /// <param name="formBackColor">BackColor of the form.</param>
        /// <param name="barsBackColor">BackColor of the bars.</param>
        public DesignParameters(Color formBackColor, Color barsBackColor)
        {
            _formBackColor = formBackColor;
            _barsBackColor = barsBackColor;
        }

        #endregion Constructors
    }
}
