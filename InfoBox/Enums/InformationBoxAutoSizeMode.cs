// <copyright file="InformationBoxAutoSizeMode.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining which mode is used for autosizing the InformationBox</summary>

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
        /// The <see cref="InformationBox"/> will be set with consideration for existing line breaks, but will wrapping lines that are too wide.
        /// </summary>
        None,

        /// <summary>
        /// The <see cref="InformationBox"/> will try to adjust the size so that the text is displayed as provided. It enbale pre-formatted text to remain untouched. In case the text is wider than the screen, horizontal scrollbars will appear.
        /// </summary>
        FitToText
    }
}
