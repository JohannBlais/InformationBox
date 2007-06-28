namespace InfoBox
{
    /// <summary>
    /// Contains all possible values for the Show return value. Identifies which button was clicked.
    /// </summary>
    public enum InformationBoxResult
    {
        /// <summary>
        /// The dialog box return value is Abort (usually sent from a button labeled Abort).
        /// </summary>
        Abort,
        /// <summary>
        /// The dialog box return value is Cancel (usually sent from a button labeled Cancel).
        /// </summary>
        Cancel,
        /// <summary>
        /// The dialog box return value is Ignore (usually sent from a button labeled Ignore).
        /// </summary>
        Ignore,
        /// <summary>
        /// The dialog box return value is No (usually sent from a button labeled No).
        /// </summary>
        No,
        /// <summary>
        /// Nothing is returned from the dialog box. This means that the modal dialog continues running.
        /// </summary>
        None,
        /// <summary>
        /// The dialog box return value is OK (usually sent from a button labeled OK).
        /// </summary>
        OK,
        /// <summary>
        /// The dialog box return value is Retry (usually sent from a button labeled Retry).
        /// </summary>
        Retry,
        /// <summary>
        /// The dialog box return value is Yes (usually sent from a button labeled Yes).
        /// </summary>
        Yes,
        /// <summary>
        /// The dialog box return value is User1 (usually sent from the first user defined button).
        /// </summary>
        User1,
        /// <summary>
        /// The dialog box return value is Yes (usually sent from the second user defined button).
        /// </summary>
        User2,
    }
}
