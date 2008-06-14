// <copyright file="DesignParameters.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains the values of the design parameters</summary>

namespace InfoBox
{
    using System.Drawing;

    /// <summary>
    /// Contains the values of the design parameters.
    /// </summary>
    public class DesignParameters
    {
        #region Internals

        /// <summary>
        /// Contains the form back color
        /// </summary>
        private readonly Color formBackColor = SystemColors.Control;

        /// <summary>
        /// Contains the bars back color
        /// </summary>
        private readonly Color barsBackColor = SystemColors.Control;

        #endregion Internals

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignParameters"/> class.
        /// </summary>
        /// <param name="formBackColor">BackColor of the form.</param>
        /// <param name="barsBackColor">BackColor of the bars.</param>
        public DesignParameters(Color formBackColor, Color barsBackColor)
        {
            this.formBackColor = formBackColor;
            this.barsBackColor = barsBackColor;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the back color of the form.
        /// </summary>
        /// <value>The back color of the form.</value>
        public Color FormBackColor
        {
            get { return this.formBackColor; }
        }

        /// <summary>
        /// Gets the back color of the bars.
        /// </summary>
        /// <value>The back color of the bars.</value>
        public Color BarsBackColor
        {
            get { return this.barsBackColor; }
        }
        
        #endregion Properties
    }
}
