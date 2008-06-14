// <copyright file="InformationBoxInitialization.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specify constants defining how to initialize the InformationBox</summary>

namespace InfoBox
{
    /// <summary>
    /// Specify constants defining how to initialize the <see cref="InformationBox"/>.
    /// </summary>
    public enum InformationBoxInitialization
    {
        /// <summary>
        /// The <see cref="InformationBox"/> is initialized from the parameters only. All scopes are ignored.
        /// </summary>
        FromParametersOnly,

        /// <summary>
        /// The <see cref="InformationBox"/> is first initialized from the current scope (if available) and then from the supplied parameters.
        /// </summary>
        FromScopeAndParameters,
    }
}
