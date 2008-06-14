// <copyright file="InformationBoxPosition.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining the position of the InformationBox</summary>

namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining the position of the <see cref="InformationBox"/>.
    /// </summary>
    public enum InformationBoxPosition
    {
        /// <summary>
        /// the <see cref="InformationBox"/> will be centered on the parent window. This is the default value. Only for modal behavior.
        /// </summary>
        CenterOnParent,

        /// <summary>
        /// the <see cref="InformationBox"/> will be centered on the screen.
        /// </summary>
        CenterOnScreen,
    }
}
