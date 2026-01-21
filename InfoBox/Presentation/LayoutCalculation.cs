// <copyright file="LayoutCalculation.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Result of layout calculations for InformationBox</summary>

namespace InfoBox.Presentation
{
    /// <summary>
    /// Contains the results of layout calculations for an InformationBox.
    /// </summary>
    /// <remarks>
    /// This class holds pure calculation results without any WinForms dependencies,
    /// making layout logic fully testable.
    /// See TESTABILITY_ROADMAP.md - P0.1
    /// </remarks>
    public class LayoutCalculation
    {
        /// <summary>
        /// Gets or sets the total required width.
        /// </summary>
        public int RequiredWidth { get; set; }

        /// <summary>
        /// Gets or sets the total required height.
        /// </summary>
        public int RequiredHeight { get; set; }

        /// <summary>
        /// Gets or sets the text area width.
        /// </summary>
        public int TextWidth { get; set; }

        /// <summary>
        /// Gets or sets the text area height.
        /// </summary>
        public int TextHeight { get; set; }

        /// <summary>
        /// Gets or sets the icon height.
        /// </summary>
        public int IconHeight { get; set; }

        /// <summary>
        /// Gets or sets the minimum buttons width.
        /// </summary>
        public int ButtonsMinWidth { get; set; }

        /// <summary>
        /// Gets or sets the checkbox width.
        /// </summary>
        public int CheckBoxWidth { get; set; }

        /// <summary>
        /// Gets or sets the caption width.
        /// </summary>
        public int CaptionWidth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether vertical scrolling is needed.
        /// </summary>
        public bool RequiresVerticalScroll { get; set; }

        /// <summary>
        /// Gets or sets the main panel width.
        /// </summary>
        public int MainPanelWidth { get; set; }

        /// <summary>
        /// Gets or sets the main panel height.
        /// </summary>
        public int MainPanelHeight { get; set; }
    }
}
