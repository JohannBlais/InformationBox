using InfoBox.Internals;

namespace InfoBox
{
    /// <summary>
    /// Contains the parameters used for the auto-close feature.
    /// </summary>
    public class AutoCloseParameters
    {
        #region Internals

        private static readonly AutoCloseParameters _default = new AutoCloseParameters(30);

        private readonly int _timeToWait = 30;
        private readonly InformationBoxDefaultButton _button = InformationBoxDefaultButton.Button1;
        private readonly InformationBoxResult _result = InformationBoxResult.None;

        private readonly AutoCloseDefinedParameters _mode = AutoCloseDefinedParameters.TimeOnly;

        #endregion Internals

        #region Properties

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        /// <value>The seconds.</value>
        public int Seconds
        {
            get { return _timeToWait; }
        }

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <value>The default button.</value>
        public InformationBoxDefaultButton DefaultButton
        {
            get { return _button; }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public InformationBoxResult Result
        {
            get { return _result; }
        }

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>The mode.</value>
        internal AutoCloseDefinedParameters Mode
        {
            get { return _mode; }
        }

        /// <summary>
        /// Gets the default AutoCloseParameters.
        /// </summary>
        /// <value>The default AutoCloseParameters.</value>
        public static AutoCloseParameters Default
        {
            get { return _default; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing.
        /// </summary>
        /// <param name="time">The time.</param>
        public AutoCloseParameters(int time)
        {
            _mode = AutoCloseDefinedParameters.TimeOnly;
            _timeToWait = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using buttonToUse as the button that will be used for auto-closing.
        /// </summary>
        /// <param name="buttonToUse">The button to use.</param>
        public AutoCloseParameters(InformationBoxDefaultButton buttonToUse)
        {
            _mode = AutoCloseDefinedParameters.Button;
            _button = buttonToUse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing, and buttonToUse as the button that will be used for auto-closing.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="buttonToUse">The button to use.</param>
        public AutoCloseParameters(int time, InformationBoxDefaultButton buttonToUse)
        {
            _mode = AutoCloseDefinedParameters.Button;
            _timeToWait = time;
            _button = buttonToUse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using result as the InformationBoxResult that will be used as the return.
        /// </summary>
        /// <param name="result">The result.</param>
        public AutoCloseParameters(InformationBoxResult result)
        {
            _mode = AutoCloseDefinedParameters.Result;
            _result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing, and result as the InformationBoxResult that will be used as the return.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="result">The result.</param>
        public AutoCloseParameters(int time, InformationBoxResult result)
        {
            _mode = AutoCloseDefinedParameters.Result;
            _timeToWait = time;
            _result = result;
        }

        #endregion Constructors
    }
}
