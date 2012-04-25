// <copyright file="InformationBoxScopeBehavior.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Specifies constants defining how the new scope treats the parameters of the parent scopes</summary>

namespace InfoBox
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Specifies constants defining how the new scope treats the parameters of the parent scopes.
    /// </summary>
    public enum InformationBoxScopeBehavior
    {
        /// <summary>
        /// Parent parameters are ignored.
        /// </summary>
        None,

        /// <summary>
        /// The parameters of the direct parent are taken into account.
        /// </summary>
        InheritParent,

        /// <summary>
        /// The parameters of all active scopes are taken into account.
        /// </summary>
        InheritAll,
    }
}
