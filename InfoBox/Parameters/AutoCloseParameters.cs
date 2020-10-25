// <copyright file="AutoCloseParameters.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains the parameters used for the auto-close feature</summary>

namespace InfoBox
{
    /// <summary>
    /// Contains the parameters used for the auto-close feature.
    /// </summary>
    public class AutoCloseParameters
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        public AutoCloseParameters(int time)
        {
            this.Mode = AutoCloseDefinedParameters.TimeOnly;
            this.Seconds = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using buttonToUse as the button that will be used for auto-closing.
        /// </summary>
        /// <param name="buttonToUse">The button to use.</param>
        public AutoCloseParameters(InformationBoxDefaultButton buttonToUse)
        {
            this.Mode = AutoCloseDefinedParameters.Button;
            this.DefaultButton = buttonToUse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing, and buttonToUse as the button that will be used for auto-closing.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        /// <param name="buttonToUse">The button to use.</param>
        public AutoCloseParameters(int time, InformationBoxDefaultButton buttonToUse)
        {
            this.Mode = AutoCloseDefinedParameters.Button;
            this.Seconds = time;
            this.DefaultButton = buttonToUse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using result as the InformationBoxResult that will be used as the return.
        /// </summary>
        /// <param name="result">The result.</param>
        public AutoCloseParameters(InformationBoxResult result)
        {
            this.Mode = AutoCloseDefinedParameters.Result;
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCloseParameters"/> class using time as the number of seconds before autoclosing, and result as the InformationBoxResult that will be used as the return.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        /// <param name="result">The result to use.</param>
        public AutoCloseParameters(int time, InformationBoxResult result)
        {
            this.Mode = AutoCloseDefinedParameters.Result;
            this.Seconds = time;
            this.Result = result;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the default AutoCloseParameters.
        /// </summary>
        /// <value>The default AutoCloseParameters.</value>
        public static AutoCloseParameters Default { get; } = new AutoCloseParameters(30);

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        /// <value>The seconds.</value>
        public int Seconds { get; private set; } = 30;

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <value>The default button.</value>
        public InformationBoxDefaultButton DefaultButton { get; private set; } = InformationBoxDefaultButton.Button1;

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public InformationBoxResult Result { get; private set; } = InformationBoxResult.None;

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>The autoclose mode.</value>
        public AutoCloseDefinedParameters Mode { get; private set; }

        #endregion Properties

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

            AutoCloseParameters compared = (AutoCloseParameters)obj;

            return this.DefaultButton == compared.DefaultButton &&
                   this.Mode == compared.Mode &&
                   this.Result == compared.Result &&
                   this.Seconds == compared.Seconds;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.DefaultButton.GetHashCode() ^
                   this.Mode.GetHashCode() ^
                   this.Result.GetHashCode() ^
                   this.Seconds.GetHashCode();
        }

        #endregion Overrides
    }
}
