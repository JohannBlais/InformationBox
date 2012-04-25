// <copyright file="InformationBoxTitleIconStyle.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining which icon is displayed on the title bar</summary>

namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining which icon is displayed on the title bar.
    /// </summary>
    public enum InformationBoxTitleIconStyle
    {
        /// <summary>
        /// No title icon.
        /// </summary>
        None,

        /// <summary>
        /// Use the icon displayed in the box.
        /// </summary>
        SameAsBox,

        /// <summary>
        /// Use a custom icon.
        /// </summary>
        Custom,
    }
}
