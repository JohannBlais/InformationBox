// <copyright file="AutoCloseState.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>State information for auto-close functionality</summary>

namespace InfoBox.Presentation
{
    /// <summary>
    /// Contains the state information for auto-close functionality.
    /// </summary>
    /// <remarks>
    /// This class represents auto-close state without Timer dependencies,
    /// making auto-close logic fully testable.
    /// See TESTABILITY_ROADMAP.md - P0.1
    /// </remarks>
    public class AutoCloseState
    {
        /// <summary>
        /// Gets or sets the remaining seconds until auto-close.
        /// </summary>
        public int RemainingSeconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog should close now.
        /// </summary>
        public bool ShouldClose { get; set; }

        /// <summary>
        /// Gets or sets the updated button text (with countdown).
        /// </summary>
        public string UpdatedButtonText { get; set; }

        /// <summary>
        /// Gets or sets the name of the button to update.
        /// </summary>
        public string ButtonToUpdate { get; set; }

        /// <summary>
        /// Gets or sets the result to return when auto-closing.
        /// </summary>
        public InformationBoxResult ResultOnClose { get; set; }
    }
}
