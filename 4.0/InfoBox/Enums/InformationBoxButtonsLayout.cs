// <copyright file="InformationBoxButtonsLayout.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining how to place buttons on the InformationBox</summary>

namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining how to place buttons on the <see cref="InformationBox"/>.
    /// </summary>
    public enum InformationBoxButtonsLayout
    {
        /// <summary>
        /// Buttons are grouped on the left side.
        /// </summary>
        GroupLeft,

        /// <summary>
        /// Buttons are grouped in the middle.
        /// </summary>
        GroupMiddle,

        /// <summary>
        /// Buttons are grouped on the right side.
        /// </summary>
        GroupRight,

        /// <summary>
        /// Spacing is constant between the buttons and the edges of the <see cref="InformationBox"/>.
        /// </summary>
        Separate,
    }
}
