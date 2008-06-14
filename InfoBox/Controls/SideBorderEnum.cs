// <copyright file="SideBorderEnum.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Represents which side borders are displayed</summary>

namespace InfoBox.Controls
{
    /// <summary>
    /// Represents which side borders are displayed.
    /// </summary>
    public enum SideBorder
    {
        /// <summary>
        /// No border for the panel
        /// </summary>
        None,

        /// <summary>
        /// Right side only
        /// </summary>
        Right,

        /// <summary>
        /// Left side only
        /// </summary>
        Left,

        /// <summary>
        /// Both sides (left and right)
        /// </summary>
        Both,
    }
}
