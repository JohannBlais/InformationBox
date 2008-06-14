// <copyright file="AutoCloseDefinedParameters.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Defines constant representing the parameters specified for the auto-close feature</summary>

namespace InfoBox.Internals
{
    /// <summary>
    /// Defines constant representing the parameters specified for the auto-close feature.
    /// </summary>
    internal enum AutoCloseDefinedParameters
    {
        /// <summary>
        /// The button to use is defined.
        /// </summary>
        Button,

        /// <summary>
        /// Only the time to wait is defined.
        /// </summary>
        TimeOnly,

        /// <summary>
        /// The InformationBoxResult is defined.
        /// </summary>
        Result,
    }
}