// <copyright file="AutoCloseParameters.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains the parameters used for the auto-close feature</summary>

namespace InfoBox
{
    using InfoBox.Internals;

    /// <summary>
    /// Contains the parameters used for the auto-close feature.
    /// </summary>
    public class AutoCloseParameters
    {
        #region Attributes

        /// <summary>
        /// Contains the default parameters
        /// </summary>
        private static readonly AutoCloseParameters defaultParameters = new AutoCloseParameters(30);

        /// <summary>
        /// Contains the time to wait before return
        /// </summary>
        private readonly int timeToWait = 30;

        /// <summary>
        /// Contains the default button
        /// </summary>
        private readonly InformationBoxDefaultButton button = InformationBoxDefaultButton.Button1;
        
        /// <summary>
        /// Contains the result to use
        /// </summary>
        private readonly InformationBoxResult result = InformationBoxResult.None;

        /// <summary>
        /// Contains the autoclose defined parameters
        /// </summary>
        private readonly AutoCloseDefinedParameters mode = AutoCloseDefinedParameters.TimeOnly;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        public AutoCloseParameters(int time)
        {
            this.mode = AutoCloseDefinedParameters.TimeOnly;
            this.timeToWait = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using buttonToUse as the button that will be used for auto-closing.
        /// </summary>
        /// <param name="buttonToUse">The button to use.</param>
        public AutoCloseParameters(InformationBoxDefaultButton buttonToUse)
        {
            this.mode = AutoCloseDefinedParameters.Button;
            this.button = buttonToUse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing, and buttonToUse as the button that will be used for auto-closing.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        /// <param name="buttonToUse">The button to use.</param>
        public AutoCloseParameters(int time, InformationBoxDefaultButton buttonToUse)
        {
            this.mode = AutoCloseDefinedParameters.Button;
            this.timeToWait = time;
            this.button = buttonToUse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using result as the InformationBoxResult that will be used as the return.
        /// </summary>
        /// <param name="result">The result.</param>
        public AutoCloseParameters(InformationBoxResult result)
        {
            this.mode = AutoCloseDefinedParameters.Result;
            this.result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing, and result as the InformationBoxResult that will be used as the return.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        /// <param name="result">The result to use.</param>
        public AutoCloseParameters(int time, InformationBoxResult result)
        {
            this.mode = AutoCloseDefinedParameters.Result;
            this.timeToWait = time;
            this.result = result;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the default AutoCloseParameters.
        /// </summary>
        /// <value>The default AutoCloseParameters.</value>
        public static AutoCloseParameters Default
        {
            get { return defaultParameters; }
        }

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        /// <value>The seconds.</value>
        public int Seconds
        {
            get { return this.timeToWait; }
        }

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <value>The default button.</value>
        public InformationBoxDefaultButton DefaultButton
        {
            get { return this.button; }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public InformationBoxResult Result
        {
            get { return this.result; }
        }

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>The autoclose mode.</value>
        internal AutoCloseDefinedParameters Mode
        {
            get { return this.mode; }
        }

        #endregion Properties
    }
}
