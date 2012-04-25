// <copyright file="InformationBoxCheckBox.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining whether the "Do not show this dialog again" checkbox is displayed or not</summary>

namespace InfoBox
{
    using System;

    /// <summary>
    /// Specifies constants defining whether the "Do not show this dialog again" checkbox is displayed or not.
    /// </summary>
    [Flags]
    public enum InformationBoxCheckBox
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
