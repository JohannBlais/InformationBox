namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining which buttons to display on a <see cref="InformationBoxForm" />.
    /// </summary>
    public enum InformationBoxButtons
    {
        /// <summary>
        /// The message box contains Abort, Retry, and Ignore buttons.
        /// </summary>
        AbortRetryIgnore,
        /// <summary>
        /// The message box contains an OK button.
        /// </summary>
        OK,
        /// <summary>
        /// The message box contains OK and Cancel buttons.
        /// </summary>
        OKCancel,
        /// <summary>
        /// The message box contains Retry and Cancel buttons.
        /// </summary>
        RetryCancel,
        /// <summary>
        /// The message box contains Yes and No buttons.
        /// </summary>
        YesNo,
        /// <summary>
        /// The message box contains Yes, No, and Cancel buttons.
        /// </summary>
        YesNoCancel,
        /// <summary>
        /// The message box contains Yes, No, and a user defined buttons.
        /// </summary>
        YesNoUser1,
        /// <summary>
        /// The message box contains OK, Cancel, and a user defined buttons.
        /// </summary>
        OKCancelUser1,
        /// <summary>
        /// The message box contains two user defined buttons.
        /// </summary>
        User1User2,
		/// <summary>
		/// The message box contains one user defined button.
		/// </summary>
		User1,
    }
}
