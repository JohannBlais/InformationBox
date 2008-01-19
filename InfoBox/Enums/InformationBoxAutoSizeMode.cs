namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining which mode is used for autosizing the <see cref="InformationBox"/>.
    /// </summary>
    public enum InformationBoxAutoSizeMode
    {
        /// <summary>
        /// Adjust the height and text to have the highest <see cref="InformationBox"/> possible. Existing line breaks are ignored.
        /// </summary>
        MinimumWidth,
        /// <summary>
        /// Adjust the width and text to have the widest <see cref="InformationBox"/> possible. Existing line breaks are ignored.
        /// </summary>
        MinimumHeight,
        /// <summary>
        /// The <see cref="InformationBox"/> will be set according to existing line breaks.
        /// </summary>
        None,
    }
}
