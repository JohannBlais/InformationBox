// <copyright file="IconType.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining which source to use for the icon</summary>

namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining which source to use for the icon.
    /// </summary>
    internal enum IconType
    {
        /// <summary>
        /// Uses internal icons
        /// </summary>
        Internal,

        /// <summary>
        /// Uses an icon specified by the client.
        /// </summary>
        UserDefined,
    }
}
