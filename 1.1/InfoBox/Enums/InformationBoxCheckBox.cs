namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining whether the "Do not show this dialog again" checkbox is displayed or not.
    /// </summary>
    public enum InformationBoxCheckBox : byte
    {
        /// <summary>
        /// The checkbox will be displayed.
        /// </summary>
        Show = 1,
        /// <summary>
        /// Initial unchecked state (default value).
        /// </summary>
        Checked = 2,
        /// <summary>
        /// The checkbox is right aligned.
        /// </summary>
        RightAligned = 4,
    }
}
